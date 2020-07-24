using PortalSend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Hangfire;
using Hangfire.SqlServer;

namespace PortalSend.Controllers
{
    public class SMSController : Controller
    {
        // GET: SMS
        public ActionResult Index()
        {
            try
            {

           

            
            ViewData["Resultado1"]=SynWayAPI_models.Consultas(SynWayAPI_models.Consultas_Types.GetPortInfo);
            ViewData["Resultado2"] = SynWayAPI_models.Consultas(SynWayAPI_models.Consultas_Types.GetPortConnectState);
            ViewData["Resultado3"] = SynWayAPI_models.Consultas(SynWayAPI_models.Consultas_Types.GetWirelessinfo);
            
            
            ViewData["Resultado4"] = SynWayAPI_models.Consultas(SynWayAPI_models.Consultas_Types.GetDetailPortInfo);
            ViewData["Resultado5"] = SynWayAPI_models.EnvioSMS(SynWayAPI_models.Envios_Types.SendSMS,"2616142278","1","prueba:"+ DateTime.Now.ToShortTimeString());
            ViewData["Resultado6"] = SynWayAPI_models.EnvioSMS(SynWayAPI_models.Envios_Types.SendSMS, "2616142278", "1", "prueba:" + DateTime.Now.ToShortTimeString());
            
            /*
            BackgroundJob.Schedule(() => SynWayAPI_models.EnvioSMS(SynWayAPI_models.Envios_Types.SendSMS, "2616142278", "1", "prueba:" + DateTime.Now.ToShortTimeString()),
                TimeSpan.FromMinutes(1));

                BackgroundJob.Schedule(() => SynWayAPI_models.EnvioSMS(SynWayAPI_models.Envios_Types.SendSMS, "2616142278", "1", "prueba:" + DateTime.Now.ToShortTimeString()),
                TimeSpan.FromMinutes(2));


                BackgroundJob.Schedule(() => SynWayAPI_models.EnvioSMS(SynWayAPI_models.Envios_Types.SendSMS, "2616142278", "1", "prueba:" + DateTime.Now.ToShortTimeString()),
                TimeSpan.FromMinutes(3));
          */

                //  BackgroundJob.Enqueue(() => SynWayAPI_models.EnvioSMS(SynWayAPI_models.Envios_Types.SendSMS, "2616142278", "1", "prueba:" + DateTime.Now.ToShortTimeString()) );
                /*
                using (new BackgroundJobServer())
                {
                    Console.ReadLine();
                }
                */

            }
            catch (Exception ex)
            {

                throw;
            }

            return View();
        }
        public ActionResult EnviarSMS(string Lote)
        {
            ViewData["Lote"] = Lote;
            return View();
        }



        public JsonResult ListarContactos(string Lote)
        {
            List<Contactos_Models> ListarC = new List<Contactos_Models>();
            Contactos_Models C = new Contactos_Models();
         
            try
            {
                ListarC = new Contactos_Models().SelectContactos(Lote);

            }
            catch (Exception ex)
            {
                C = new Contactos_Models();
                C.con_titular = "ERROR:" + ex.Message;
                C.con_phone = "-1";
                ListarC.Add(C);

            }

            return new JsonResult
            {
                ContentType = "application/json",
                Data = ListarC,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public JsonResult EnviarSMSMasivo(string Mensaje, string Lote )
        {

            List<Resultados_Models> ListarR = new List<Resultados_Models>();
            Resultados_Models R = new Resultados_Models();
            Mensajes_Models M = new Mensajes_Models();

            try
            {
                List<Contactos_Models> ListarM = new List<Contactos_Models>();

                ListarM = new Contactos_Models().SelectContactos(Lote);
                string _loteenvio = DateTime.Now.ToString("yyMMdd-HHmmss");
                int i=0;
                foreach (Contactos_Models item in ListarM)
                {
                    M = new Mensajes_Models();
                    
                    M.mencon_id = item.con_id;
                    M.men_phone = item.con_phone;
                    M.mencon_id = item.con_id;
                    M.men_cant = 1;
                    M.men_cuerpo = Mensaje;
                    M.men_enviolote = _loteenvio;
                    M.men_estado = "ERROR";
                    M.men_fecha = DateTime.Now;
                    M.men_fechamodif = DateTime.Now;
                    M.men_lote = item.con_lote;
                    M.men_phone = item.con_phone;
                    M.men_taskid = 0;


                    try
                    {
                        /*
                        R = new Resultados_Models();
                        R = SynWayAPI_models.EnvioSMS(SynWayAPI_models.Envios_Types.SendSMS, item.con_phone, "4", Mensaje);
                        if (R.res_mensaje == "OK")
                        {
                            R.res_mensaje = R.res_contenido.result;
                            M.men_estado = R.res_contenido.result;
                            if (R.res_contenido.result == "ok") M.men_taskid = int.Parse(R.res_contenido.content.ToLower().Replace("taskid:",""));
                        }

                        */

                        R = new Resultados_Models();
                        R.res_id = int.Parse(BackgroundJob.Schedule(() => SynWayAPI_models.EnvioSMS(SynWayAPI_models.Envios_Types.SendSMS, item.con_phone, "4", Mensaje)
                         , TimeSpan.FromMinutes(i)));
                        i++;
                        R.res_mensaje = "Se ha creado la tarea:" + R.res_id.ToString();
                        R.res_contenido= new ReturnValue() { content = "Envio:" + _loteenvio, result = "OK" };
                        M.men_estado = "Creado";
                        M.men_taskid = R.res_id;
                        

                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    M.InsertUpdateMensajes(M);
                    ListarR.Add(R);
                }

            }
            catch (Exception ex)
            {
                R = new Resultados_Models();
                R.res_mensaje = "ERROR:" + ex.Message;
                R.res_excepcion= "-1";
                ListarR.Add(R);

            }

            return new JsonResult
            {
                ContentType = "application/json",
                Data = ListarR,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };

           

        }



    }
}

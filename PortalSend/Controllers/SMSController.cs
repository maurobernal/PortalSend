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
            
            /*ViewData["Resultado5"] = SynWayAPI_models.EnvioSMS(SynWayAPI_models.Envios_Types.SendSMS,"2616142278","1","prueba:"+ DateTime.Now.ToShortTimeString());
            ViewData["Resultado6"] = SynWayAPI_models.EnvioSMS(SynWayAPI_models.Envios_Types.SendSMS, "2616142278", "1", "prueba:" + DateTime.Now.ToShortTimeString());
            */
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
         //   ViewData["Configuración"] = new Chiperas_Configuration_Models();

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

        public JsonResult EnviarSMSMasivo(string Mensaje, string Lote, string reporte, string Port, string Times )
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
                int _Times = 10;
                 int.TryParse(Times, out _Times);

                //Acuse de inicio
                //SynWayAPI_models.EnvioSMS(SynWayAPI_models.Envios_Types.SendSMS,reporte , Port, DateTime.Now.ToString("dd/MM/yy HH:mm:ss")+" Iniciado:"+ListarM.Count+" items."+ "Envio: " + _loteenvio + " Segs:" + _Times + " Puerto:" + Port.ToString());
                Mensajes_Models.EnviarSMS(reporte, Port, DateTime.Now.ToString("dd/MM/yy HH:mm:ss") + " Iniciado:" + ListarM.Count + " items." + "Envio: " + _loteenvio + " Segs:" + _Times + " Puerto:" + Port.ToString());

                //Envios
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
                    ResultadoCRUD_Models rm = M.InsertUpdateMensajes(M);
                    
                    //No pudo crear el mensaje en la base. Se sale del proceso
                    if(rm.res_cantidad==-1)
                    {
                        R = new Resultados_Models();
                        R.res_mensaje = "ERROR al crear el mensaje el la BD:" + rm.res_excepcion;
                        R.res_excepcion = "-1";
                        ListarR.Add(R);
                    }
                    else
                    {
                        R = new Resultados_Models();
                        M.men_id = int.Parse(rm.res_id);
                        //ID de Tarea
                        R.res_id = int.Parse(BackgroundJob.Schedule(() => Mensajes_Models.EnviarSMS(item.con_phone, Port, Mensaje, M.men_id), TimeSpan.FromSeconds(_Times)));
                        i++;


                        R.res_mensaje = "Se ha creado la tarea:" + R.res_id.ToString();
                        R.res_contenido = new ReturnValue() { content = "Envio: " + _loteenvio + " Segs:" + _Times + " Puerto:" + Port.ToString(), result = "OK" };
                        M.men_estado = "Creado";
                        M.men_taskid = R.res_id;
                        ListarR.Add(R);
                    }    
                    
                }

                //Acuse de envio realizado
                BackgroundJob.Schedule(() => SynWayAPI_models.EnvioSMS(SynWayAPI_models.Envios_Types.SendSMS, reporte, Port, DateTime.Now.ToString("dd/MM/yy HH:mm:ss") + " Envio programado:" + ListarM.Count + " items" + "Envio: " + _loteenvio + " Segs:" + _Times + " Puerto:" + Port.ToString())
                         , TimeSpan.FromSeconds(i));

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

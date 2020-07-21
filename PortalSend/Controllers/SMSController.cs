using PortalSend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalSend.Controllers
{
    public class SMSController : Controller
    {
        // GET: SMS
        public ActionResult Index()
        {
            /*
            ViewData["Resultado1"]=SynWayAPI_models.Consultas(SynWayAPI_models.Consultas_Types.GetPortInfo);
            ViewData["Resultado2"] = SynWayAPI_models.Consultas(SynWayAPI_models.Consultas_Types.GetPortConnectState);
            ViewData["Resultado3"] = SynWayAPI_models.Consultas(SynWayAPI_models.Consultas_Types.GetWirelessinfo);
            ViewData["Resultado4"] = SynWayAPI_models.Consultas(SynWayAPI_models.Consultas_Types.GetDetailPortInfo);
            ViewData["Resultado5"] = SynWayAPI_models.EnvioSMS(SynWayAPI_models.Envios_Types.SendSMS,"2616142278","1","prueba:"+ DateTime.Now.ToShortTimeString());
            ViewData["Resultado6"] = SynWayAPI_models.EnvioSMS(SynWayAPI_models.Envios_Types.SendSMS, "2616142278", "1", "prueba:" + DateTime.Now.ToShortTimeString());
            */
            return View();
        }
        public ActionResult EnviarSMS(string Lote)
        {
            ViewData["Lote"] = Lote;
            return View();
        }



        public JsonResult ListarMensajes(string Lote)
        {
            List<Mensajes_Models> ListarM = new List<Mensajes_Models>();
            Mensajes_Models M = new Mensajes_Models();
         
            try
            {
                ListarM = new Mensajes_Models().SelectMensajes(Lote);

            }
            catch (Exception ex)
            {
                M = new Mensajes_Models();
                M.men_titular = "ERROR:" + ex.Message;
                M.men_phone = "-1";
                ListarM.Add(M);

            }

            return new JsonResult
            {
                ContentType = "application/json",
                Data = ListarM,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }

        public JsonResult EnviarSMSMasivo(string Mensaje, string Lote )
        {

            List<Resultados_Models> ListarR = new List<Resultados_Models>();
            Resultados_Models R = new Resultados_Models();

            try
            {
                List<Mensajes_Models> ListarM = new List<Mensajes_Models>();

                ListarM = new Mensajes_Models().SelectMensajes(Lote);
                foreach (Mensajes_Models item in ListarM)
                {
                    R = new Resultados_Models();
                   R= SynWayAPI_models.EnvioSMS(SynWayAPI_models.Envios_Types.SendSMS, item.men_phone,"1",Mensaje);
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

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

        
    }
}

using PortalSend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PortalSend.Controllers
{
    public class ReporteController : Controller
    {
        // GET: Reporte
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarReporte(string Fecha_Desde, string Fecha_Hasta)
        {
            List<Reporte_Envios_Models> ListarL = new List<Reporte_Envios_Models>();
            Reporte_Envios_Models L = new Reporte_Envios_Models();
            DateTime fechadesde = new DateTime(1900, 01, 01);
            DateTime fechahasta = new DateTime(2050, 12, 31);

            DateTime.TryParse(Fecha_Desde, out fechadesde);
            DateTime.TryParse(Fecha_Hasta, out fechahasta);
            try
            {
                ListarL = L.SelectMensajesEnviados(fechadesde, fechahasta);

            }
            catch (Exception ex)
            {
                L = new Reporte_Envios_Models();
                L.men_cuerpo = "ERROR:" + ex.Message;
                L.men_phone ="-1";
                ListarL.Add(L);

            }

            return new JsonResult
            {
                ContentType = "application/json",
                Data = ListarL,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }



    }
}
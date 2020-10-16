using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PortalSend.Models;
namespace PortalSend.Controllers
{
    public class LotesController : Controller
    {
        // GET: Lotes
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult ListarLotes(string Fecha_Desde, string Fecha_Hasta)
        {
            List<Lotes_Models> ListarL = new List<Lotes_Models>();
            Lotes_Models L = new Lotes_Models();
            DateTime fechadesde = new DateTime(1900, 01, 01);
            DateTime fechahasta = new DateTime(2050, 12, 31);

            DateTime.TryParse(Fecha_Desde, out fechadesde);
            DateTime.TryParse(Fecha_Hasta, out fechahasta);
            try
            {
                ListarL = L.SelectLotes(fechadesde, fechahasta);

            }
            catch (Exception ex)
            {
                L = new Lotes_Models();
                L.Lote = "ERROR:" + ex.Message;
                L.Cant = -1;
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

        public JsonResult EliminarLotes(string Lote)
        {
            List<Contactos_Models> ListarL = new List<Contactos_Models>();
            ResultadoCRUD_Models R = new ResultadoCRUD_Models();
            List<ResultadoCRUD_Models> ListR = new List<ResultadoCRUD_Models>();
            try
            {


                
                    R = new ResultadoCRUD_Models();
                    R=new Contactos_Models().DeleteContactosByLote(Lote);
                    ListR.Add(R);
                

            }
            catch (Exception ex)
            {
                R = new ResultadoCRUD_Models();
                R.res_observacion = "ERROR:" + ex.Message;
                R.res_cantidad = -1;
                R.res_metodo = "Lotes.EliminarLotes";
                ListR.Add(R);
            }

            return new JsonResult
            {
                ContentType = "application/json",
                Data = ListR,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };
        }
    }
}

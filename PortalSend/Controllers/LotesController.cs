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

        public JsonResult ListarLotes()
        {
            List<Lotes_Models> ListarL = new List<Lotes_Models>();
            Lotes_Models L = new Lotes_Models();
            try
            {
                ListarL = L.SelectLotes(new DateTime(2020, 01, 01),new DateTime(2020,12,31));

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
    }
}
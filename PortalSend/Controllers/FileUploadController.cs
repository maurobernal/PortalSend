using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using FileHelpers;
using PortalSend.Formats;
using Newtonsoft.Json;
using PortalSend.Models;
using PortalSend.Common;
namespace PortalSend.Controllers
{
    public class FileUploadController : Controller
    {
        /*
        FilesHelper filesHelper;
        String tempPath = "~/somefiles/";
        String serverMapPath = "~/Files/somefiles/";
        private string StorageRoot
        {
            get { return Path.Combine(HostingEnvironment.MapPath(serverMapPath)); }
        }
        private string UrlBase = "/Files/somefiles/";
        String DeleteURL = "/FileUpload/DeleteFile/?file=";
        String DeleteType = "GET";
        public FileUploadController()
        {
            filesHelper = new FilesHelper(DeleteURL, DeleteType, StorageRoot, UrlBase, tempPath, serverMapPath);
        }

      
        public ActionResult Show()
        {
            JsonFiles ListOfFiles = filesHelper.GetFileList();
            var model = new FilesView_Models()
            {
                Files = ListOfFiles.files
            };

            return View(model);
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Upload()
        {
            var resultList = new List<ViewDataUploadFilesResult>();

            var CurrentContext = HttpContext;

            filesHelper.UploadAndShowResults(CurrentContext, resultList);
            JsonFiles files = new JsonFiles(resultList);

            bool isEmpty = !resultList.Any();
            if (isEmpty)
            {
                return Json("Error ");
            }
            else
            {
                return Json(files);
            }
        }
        public JsonResult GetFileList()
        {
            var list = filesHelper.GetFileList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult DeleteFile(string file)
        {
            filesHelper.DeleteFile(file);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        */


        public ActionResult Index()
        {
            return View();
        }
        public JsonResult MaxSubirArchivo()
        {
            ResultadoCRUD_Models R = new ResultadoCRUD_Models();
            List<ResultadoCRUD_Models> ListR = new List<ResultadoCRUD_Models>();
            R.res_cantidad = -1;
            R.res_excepcion = "ERROR";
            R.res_observacion = "El archivo es mas grande de lo permitido";
            R.res_metodo = "SubirArchivo";
            R.res_id = "-1";
            ListR.Add(R);
            return new JsonResult
            {
                ContentType = "application/json",
                Data = ListR,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                MaxJsonLength = Int32.MaxValue
            };

        }

            public JsonResult SubirArchivo(HttpPostedFileBase file)
        {
            ResultadoCRUD_Models R = new ResultadoCRUD_Models();
            List<ResultadoCRUD_Models> ListR = new List<ResultadoCRUD_Models>();
            try
            {

            if (file == null) R.res_excepcion = "no se recibió ningún archivo";
            if (!(file.FileName.EndsWith(".csv") || (file.FileName.EndsWith(".txt")))) R.res_excepcion = "Debe ser un .csv o un .txt";


            var engine = new FileHelperEngine<Importar_Models>();
            Importar_Models[] List_Importar;
            List_Importar = engine.ReadStream(new StreamReader(file.InputStream));

            string _lote = DateTime.Now.ToString("yyyyMMdd-hhmm");
            foreach (Importar_Models item in List_Importar)
            {
                Mensajes_Models M = new Mensajes_Models();
                R = new ResultadoCRUD_Models();
                M.men_fecha = DateTime.Now;
                M.men_cant = 0;
                M.men_cuerpo = "";
                M.men_estado = "I";
                M.men_fechamodif = DateTime.Now;
                M.men_lote = _lote;
                M.men_phone = item.tel1;
                M.men_titular = item.titular;
                R=M.InsertUpdateMensajes(M);
                ListR.Add(R);

            }

            

            }
            catch (Exception ex)
            {
                R = new ResultadoCRUD_Models();
                R.res_cantidad = -1;
                R.res_metodo = "FileUploadController.SubirArchivo";
                R.res_observacion="ERROR:"+ex.Message;
                R.res_excepcion=(ex.InnerException==null)?"":ex.InnerException.ToString();
                ListR.Add(R);
                
            }
           // ViewData["Resultados"] = ListR;
            //return JsonConvert.SerializeObject(ListR);
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



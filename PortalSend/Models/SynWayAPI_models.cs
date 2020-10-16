using Newtonsoft.Json;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace PortalSend.Models
{
    public static class SynWayAPI_models
    {
        private static string UserName = ConfigurationManager.AppSettings["userAdmin"].ToString();
        private static string PassName = ConfigurationManager.AppSettings["userPass"].ToString();
        private static string UrlBase = ConfigurationManager.AppSettings["baseUrl"].ToString();


        public static Resultados_Models Consultas(Consultas_Types type)
        {
            string parseContent = "";
            string action = "API/QueryInfo";


            switch (type)
            {
                case Consultas_Types.GetPortInfo:
                    //Obtaining Port Status
                    parseContent = "{\"event\":\"getportinfo\"}";

                    break;
                case Consultas_Types.GetPortConnectState:
                    //QueryConnectionStatus estado de la conexion 
                    parseContent = "{\"event\":\"getportconnectstate\"}";
                    break;

                case Consultas_Types.GetWirelessinfo:
                    //Obtaining Wireless Parameters Information
                    parseContent = "{\"event\":\"getwirelessinfo\",\"type\":\"IMEI\"}";
                    break;

                case Consultas_Types.GetDetailPortInfo:
                    // Obtaining Detailed Port Information
                     parseContent = "{\"event\":\"getDetailPortInfo\"}";
                    break;

                case Consultas_Types.GetInbox:
                    // Obtaining Detailed Port Information
                    parseContent = "{\"event\":\"queryrxsms\"}";
                    break;

                    /*
                case Consultas_Types.GetTaskState:
                    //Obtaining estado del la tarea
                    parseContent = "{\"event\":\"querytxsms\",\"taskid\":" + taskid + "}";
                    break;
                case Consultas_Types.GetPortInfoMore:
                    //“getportinfoex”
                    parseContent = "{\"event\":\"getportinfoex\"}";
                    break;
                default:
                    break;
                    */
            }
            return WebService(action, parseContent);
        }


        public static Resultados_Models EnvioSMS(Envios_Types type, string numero, string port, string mensaje)
        {
            string parseContent = "";
            string action = "API/TaskHandle";
            switch (type)
            {
                case Envios_Types.SendSMS:
                    //Obtaining Port Status
                    parseContent = "{\"event\":\"txsms\",\"userid\":\"0\",\"num\":\"" + numero + "\",\"port\":\"" + port + "\",\"encoding\":\"0\",\"smsinfo\":\"" +mensaje+ "\" }";
                    break;
            }
            return WebService(action, parseContent);
        }



        #region "auxiliares"
        public enum Consultas_Types : ushort {
            GetPortInfo = 0,
            GetPortConnectState = 1,
            GetWirelessinfo=2,
            GetDetailPortInfo=3,
            GetTaskState = 4,
            GetPortInfoMore = 5,
            GetInbox = 6
        }

        public enum Envios_Types : ushort
        {
            SendSMS = 0,
        }

        public static string Base64Encode(string plainText)
    {
        var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
        return System.Convert.ToBase64String(plainTextBytes);
    }
    public static string Base64Decode(string base64EncodedData)
    {
        var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }

    #endregion


    public static Resultados_Models WebService(string action, string parseContent)
        {
            Resultados_Models R = new Resultados_Models();
            R.res_metodo = "WebService:" + action;
            try
            {

            
            string url = UrlBase + "/" + action;
            //creo un objeto peticion del web service
              HttpWebRequest _request_client = (HttpWebRequest)WebRequest.Create("http://" + url + "/" + action);

            // Get Token
            var client = new RestClient(url);
            //client.Authenticator = new HttpBasicAuthenticator(UserName, PassName);
            var request = new RestRequest(Method.POST);
            request.AddHeader("Accept", "applicaction/json");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", "Basic " + Base64Encode(UserName+":"+PassName));
            request.AddJsonBody(parseContent);
            IRestResponse response = client.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    R.res_mensaje = "OK";
                    R.res_contenido = JsonConvert.DeserializeObject<ReturnValue>(response.Content);
                    R.res_cantidad = 1;

                }
                else
                {
                    R.res_mensaje = "NOOK";
                    R.res_contenido = JsonConvert.DeserializeObject<ReturnValue>(response.Content);
                    R.res_cantidad = -1;

                }
                return R;

            }
            catch (Exception ex )
            {

                R.res_cantidad = -1;
                R.res_excepcion = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                R.res_mensaje = "ERROR:" + ex.Message;
            }
            return R;

        }

    }
}





#region "Clases auxiliares para JSON results"
public class ReturnValue
{
    public string result { get; set; }
    public string content { get; set; }

}

#endregion
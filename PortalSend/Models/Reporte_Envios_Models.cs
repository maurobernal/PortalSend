using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using PortalSend.App_Data.PORTALSEND;
using PortalSend.ExtensionMethods;

namespace PortalSend.Models
{
    public class Reporte_Envios_Models
    {
        private PortalSend_Entities _conexion = new PortalSend_Entities();
        public long id { get; set; }
        public string state { get; set; }
        public DateTime created { get; set; }
        public string men_lote { get; set; }
        public DateTime men_fechamodif { get; set; }
        public string men_cuerpo { get; set; }
        public string men_phone { get; set; }

        public List<Reporte_Envios_Models> SelectMensajesEnviados(DateTime _fechadesde, DateTime _fechahasta)
        {
            List<Reporte_Envios_Models> query = new List<Reporte_Envios_Models>();
            _fechadesde = _fechadesde.ChangeTime(0, 0, 0, 0);
            _fechahasta = _fechahasta.ChangeTime(23, 59, 0, 0);
            try
            {
                query = (from q in _conexion.Mensajes where q.men_fechamodif >= _fechadesde && q.men_fechamodif <= _fechahasta
                         join j in _conexion.Job on q.men_taskid equals j.Id into qj from menjos in qj.DefaultIfEmpty()
                         orderby q.men_id descending
                         
                         select new Reporte_Envios_Models {
                         id=menjos.Id,
                         state=menjos.StateName,
                         created=menjos.CreatedAt,
                         men_lote=q.men_lote,
                         men_fechamodif=q.men_fechamodif,
                         men_cuerpo=q.men_cuerpo,
                         men_phone=q.men_phone,
                         
                         }).ToList();
                      
                      
                return query;
            }
            catch (Exception ex)
            {

                query.Add(new Reporte_Envios_Models { men_cuerpo = "ERROR:" + ex.Message });
                return query;
            }

        }


    }
}
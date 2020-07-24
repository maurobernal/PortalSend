 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalSend.App_Data.PORTALSEND;

namespace PortalSend.Models
{
    public class Templates_Models
    {
        private PortalSend_Entities _conexion = new PortalSend_Entities();
        public int tem_id { get; set; }
        public string tem_titulo { get; set; }
        public string tem_cuerpo { get; set; }


        public List<Templates_Models> SelectTemplates()
        {
            List<Templates_Models> query = new List<Templates_Models>();
            try
            {
                query = (from q in _conexion.Templates
                             //join
                             //order
                             //where
                         select new Templates_Models
                         {
                             tem_id = q.tem_id,
                             tem_titulo = q.tem_titulo,
                             tem_cuerpo = q.tem_cuerpo,

                         }

                    ).ToList();

                return query;

            }
            catch (Exception ex)
            {
                query.Add(new Templates_Models { tem_cuerpo = "ERROR:" + ex.Message });
                return query;
            }



        }




        //INSERT & //UPDATE 
        public ResultadoCRUD_Models InsertUpdateTemplates(Templates_Models T)
        {
            ResultadoCRUD_Models R = new ResultadoCRUD_Models();
            R.res_metodo = "Templates.InsertUpdateTemplates";
            try
            {

                Templates t = new Templates();
                t = (from q in _conexion.Templates
                     where q.tem_id == T.tem_id
                     select q
                     ).FirstOrDefault();
                if (t == null)
                {
                    //INSERT
                    t = new Templates();
                    t.tem_cuerpo = T.tem_cuerpo;
                    t.tem_titulo = T.tem_titulo;
                    t.tem_id = T.tem_id;

                    _conexion.Templates.Add(t);
                    R.res_cantidad = _conexion.SaveChanges();
                    R.res_id = t.tem_id.ToString();
                    R.res_observacion = "INSERT";


                }
                else
                {
                    //UPDATE
                    t.tem_cuerpo = T.tem_cuerpo;
                    t.tem_titulo = T.tem_titulo;
                    t.tem_id = T.tem_id;
                    R.res_cantidad = _conexion.SaveChanges();
                    R.res_id = t.tem_id.ToString();
                    R.res_observacion = "UPDATE";
                }




            }
            catch (Exception ex)
            {
                R.res_cantidad = -1;
                R.res_observacion = "ERROR:" + ex.Message;
                R.res_excepcion = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
            }



            return R;



        }



        //DELETE
        public ResultadoCRUD_Models DeleteTemplates(Templates_Models T)
        {
            ResultadoCRUD_Models R = new ResultadoCRUD_Models();
            R.res_metodo = "Templates.InsertUpdateTemplates";
            try
            {
                Templates t = new Templates();
                t = (from q in _conexion.Templates
                     where q.tem_id == T.tem_id
                     select q).FirstOrDefault();


                if (t == null)
                {
                    //VACIO
                    R.res_cantidad = -1;
                    R.res_id = T.tem_id.ToString();
                    R.res_observacion = "ERROR:No hay nada para eliminar";
                }
                else
                {
                    //DELETE
                    _conexion.Templates.Remove(t);
                    _conexion.SaveChanges(); // -> No aplica en la BD
                    R.res_id = t.tem_id.ToString();
                    R.res_observacion = "DELETE";
                }

                return R;
            }
            catch (Exception ex)
            {
                R.res_cantidad = -1;
                R.res_observacion = "ERROR:" + ex.Message;
                R.res_excepcion = (ex.InnerException == null) ? "" : ex.InnerException.ToString();
                return R;
            }


        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PortalSend.App_Data.PORTALSEND;

namespace PortalSend.Models
{
    public class Template_Models
    {
        private PortalSend_Entities _conexion = new PortalSend_Entities();
        public int tem_id { get; set; }
        public string tem_titulo { get; set; }
        public string tem_cuerpo { get; set; }


        public List<Template_Models> SelectTemplate()
        {
            List<Template_Models> query = new List<Template_Models>();
            try
            {
                query = (from q in _conexion.Template
                             //join
                             //order
                             //where
                         select new Template_Models
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
                query.Add(new Template_Models { tem_cuerpo = "ERROR:" + ex.Message });
                return query;
            }



        }




        //INSERT & //UPDATE 
        public ResultadoCRUD_Models InsertUpdateTemplate(Template_Models T)
        {
            ResultadoCRUD_Models R = new ResultadoCRUD_Models();
            R.res_metodo = "Template.InsertUpdateTemplate";
            try
            {

                Template t = new Template();
                t = (from q in _conexion.Template
                     where q.tem_id == T.tem_id
                     select q
                     ).FirstOrDefault();
                if (t == null)
                {
                    //INSERT
                    t = new Template();
                    t.tem_cuerpo = T.tem_cuerpo;
                    t.tem_titulo = T.tem_titulo;
                    t.tem_id = T.tem_id;

                    _conexion.Template.Add(t);
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
        public ResultadoCRUD_Models DeleteTemplate(Template_Models T)
        {
            ResultadoCRUD_Models R = new ResultadoCRUD_Models();
            R.res_metodo = "Template.InsertUpdateTemplate";
            try
            {
                Template t = new Template();
                t = (from q in _conexion.Template
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
                    _conexion.Template.Remove(t);
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
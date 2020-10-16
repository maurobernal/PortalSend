using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using PortalSend.App_Data.PORTALSEND;

namespace PortalSend.Models
{
    public class Contactos_Models
    {
        private PortalSend_Entities _conexion = new PortalSend_Entities();
        public int con_id { get; set; }
        public string con_titular { get; set; }
        public string con_phone { get; set; }
        public DateTime con_fecha { get; set; }
        public int con_cant { get; set; }
        public string con_lote { get; set; }
        


        //SELECT Contactos
        public List<Contactos_Models> SelectContactos()
        {
            List<Contactos_Models> query = new List<Contactos_Models>();
            try
            {
                query = (from q in _conexion.Contactos
                         orderby q.con_fecha descending
                         //join
                         //order
                         //where
                         select new Contactos_Models
                         { 
                         con_id=q.con_id,
                         con_titular=q.con_titular,
                         con_phone=q.con_phone,
                         con_fecha=q.con_fecha,
                         con_cant=q.con_cant,
                         con_lote=q.con_lote
                           
                         }

                    ).ToList();

                return query;

            }
            catch (Exception ex)
            {
                query.Add(new Contactos_Models { con_titular = "ERROR:" + ex.Message });
                return query;
            }



        }

        public List<Contactos_Models> SelectContactos(string lote)
        {
            List<Contactos_Models> query = new List<Contactos_Models>();
            try
            {
                query = (from q in _conexion.Contactos 
                         orderby q.con_fecha descending
                         //join
                         //order
                         where q.con_lote==lote
                         select new Contactos_Models
                         {
                             con_id = q.con_id,
                             con_titular = q.con_titular,
                             con_phone = q.con_phone,
                             con_fecha = q.con_fecha,
                             con_cant = q.con_cant,
                             con_lote = q.con_lote,
                            
                         }

                    ).ToList();

                return query;

            }
            catch (Exception ex)
            {
                query.Add(new Contactos_Models { con_titular = "ERROR:" + ex.Message });
                return query;
            }



        }

        //INSERT & //UPDATE 
        public ResultadoCRUD_Models InsertUpdateContactos(Contactos_Models C)
        {
            ResultadoCRUD_Models R = new ResultadoCRUD_Models();
            R.res_metodo = "Contactos.UpdateContactos";
            try
            {
              
                Contactos c = new Contactos();
                bool insert = false;
                c = (from q in _conexion.Contactos
                     where q.con_id == C.con_id
                     select q
                     ).FirstOrDefault();

                if (c == null) insert = true; //INSERT
                if(insert) c = new Contactos();  //Solo si es Insert
                
                c.con_fecha = DateTime.Now;
                c.con_titular = C.con_titular;
                c.con_phone = C.con_phone;
                c.con_cant = C.con_cant;
                c.con_lote = C.con_lote;
                c.con_id = C.con_id;

                
                if (insert) _conexion.Contactos.Add(c); //Solo si es insert
                if (insert) R.res_metodo = "Contactos.UpdateContactos"; //Solo si es insert

                R.res_cantidad = _conexion.SaveChanges();
                R.res_id = c.con_id.ToString();
                R.res_observacion = c.con_id + ":" + c.con_titular;


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
        public ResultadoCRUD_Models DeleteContactos(Contactos_Models C)
        {
            ResultadoCRUD_Models R = new ResultadoCRUD_Models();
            R.res_metodo = "Contactos.InsertUpdateContactos";
            try
            {
                Contactos c = new Contactos();
                c = (from q in _conexion.Contactos
                     where q.con_id == C.con_id
                     select q).FirstOrDefault();


                if(c==null)
                {
                    //VACIO
                    R.res_cantidad = -1;
                    R.res_id = C.con_id.ToString();
                    R.res_observacion = "ERROR:No hay nada para eliminar";
                }
                else
                {
                    //DELETE
                    _conexion.Contactos.Remove(c);
                    _conexion.SaveChanges();
                    R.res_id = c.con_id.ToString();
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

        public ResultadoCRUD_Models DeleteContactosByLote(string Lote)
        {
            ResultadoCRUD_Models R = new ResultadoCRUD_Models();
            R.res_metodo = "Contactos.DeleteContactos";
            try
            {
                //DELETE
                _conexion.Contactos.RemoveRange(_conexion.Contactos.Where(w => w.con_lote == Lote));
                 _conexion.SaveChanges();
                R.res_id = "1";
                    R.res_observacion = "DELETE";
                

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
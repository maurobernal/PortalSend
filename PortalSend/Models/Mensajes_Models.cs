﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using PortalSend.App_Data.PORTALSEND;

namespace PortalSend.Models
{
    public class Mensajes_Models
    {
        private PortalSend_Entities _conexion = new PortalSend_Entities();
        public int men_id { get; set; }
        public string men_phone { get; set; }
        public DateTime men_fecha { get; set; }
        public string men_estado { get; set; }
        public string men_cuerpo { get; set; }
        public string men_resumido { get; set; }


        //SELECT Mensajes
        public List<Mensajes_Models> SelectMensajes()
        {
            List<Mensajes_Models> query = new List<Mensajes_Models>();
            try
            {
                query = (from q in _conexion.Mensajes
                         orderby q.men_fecha descending
                         //join
                         //order
                         //where
                         select new Mensajes_Models
                         { 
                         men_id=q.men_id,
                         men_phone=q.men_phone,
                         men_fecha=q.men_fecha,
                         men_estado=q.men_estado,
                         men_cuerpo=q.men_cuerpo,
                         men_resumido=q.men_id + "-"+ men_phone
                         }

                    ).ToList();

                return query;

            }
            catch (Exception ex)
            {
                query.Add(new Mensajes_Models { men_cuerpo = "ERROR:" + ex.Message });
                return query;
            }



        }
        
        
        //INSERT & //UPDATE 
        public ResultadoCRUD_Models InsertUpdateMensajes(Mensajes_Models M)
        {
            ResultadoCRUD_Models R = new ResultadoCRUD_Models();
            R.res_metodo = "Mensajes.InsertUpdateMensajes";
            try
            {
              
                Mensajes m = new Mensajes();
                m = (from q in _conexion.Mensajes
                     where q.men_id == M.men_id
                     select q
                     ).FirstOrDefault();
                if(m==null)
                {
                    m = new Mensajes();
                    //INSERT
                    m.men_cuerpo = M.men_cuerpo;
                    m.men_estado = M.men_estado;
                    m.men_fecha = M.men_fecha;
                    m.men_phone = M.men_phone;
                    m.men_id = M.men_id;

                    _conexion.Mensajes.Add(m);
                    R.res_cantidad= _conexion.SaveChanges();
                    R.res_id = m.men_id.ToString();
                    R.res_observacion = "INSERT";


                } else
                {
                    //UPDATE
                    m.men_cuerpo = M.men_cuerpo;
                    m.men_estado = M.men_estado;
                    m.men_fecha = M.men_fecha;
                    m.men_phone = M.men_phone;
                    m.men_id = M.men_id;
                    R.res_cantidad = _conexion.SaveChanges();
                    R.res_id = m.men_id.ToString();
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
        public ResultadoCRUD_Models DeleteMensajes(Mensajes_Models M)
        {
            ResultadoCRUD_Models R = new ResultadoCRUD_Models();
            R.res_metodo = "Mensajes.InsertUpdateMensajes";
            try
            {
                Mensajes m = new Mensajes();
                m = (from q in _conexion.Mensajes
                     where q.men_id == M.men_id
                     select q).FirstOrDefault();


                if(m==null)
                {
                    //VACIO
                    R.res_cantidad = -1;
                    R.res_id = M.men_id.ToString();
                    R.res_observacion = "ERROR:No hay nada para eliminar";
                }
                else
                {
                    //DELETE
                    _conexion.Mensajes.Remove(m);
                    _conexion.SaveChanges();
                    R.res_id = m.men_id.ToString();
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
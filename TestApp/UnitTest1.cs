using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PortalSend.Models;

namespace TestApp
{
    [TestClass]
    public class UnitTest1
    {
        ResultadoCRUD_Models R;
        Mensajes_Models M;
        Templates_Models T;
        
        
        [TestMethod]
        public void Test_MensajesModels()
        {
            
            //Insert
            R = new ResultadoCRUD_Models();
            M = new Mensajes_Models();
            M.men_cuerpo = "Este es el cuerpo";
            M.men_estado = "N";
            M.men_fecha = DateTime.Now;
            M.men_phone = "332432423423";
            R = M.InsertUpdateMensajes(M);

            //Update
            R = new ResultadoCRUD_Models();
            M = new Mensajes_Models();
            M.men_id = 11;
            M.men_cuerpo = "Este es un registro actualizado";
            M.men_estado = "U";
            M.men_fecha = DateTime.Now;
            M.men_phone = "332432423423";
            R = M.InsertUpdateMensajes(M);

            //Delete
            /*
            List<ResultadoCRUD_Models> ListR = new List<ResultadoCRUD_Models>();
            M = new Mensajes_Models();
            for (int i = 0; i < 15; i++)
            {
                R = new ResultadoCRUD_Models();
                R = M.DeleteMensajes(new Mensajes_Models() { men_id = i });
                ListR.Add(R);
            }
            */
            //Select
            List<Mensajes_Models> ListM = new List<Mensajes_Models>();
            ListM = M.SelectMensajes();


        }




        [TestMethod]
        public void Test_TemplatesModels()
        {
            //Insert
            R = new ResultadoCRUD_Models();
            T = new Templates_Models();
            T.tem_cuerpo = "Este es el cuerpo";
            T.tem_titulo = "332432423423";
            R = T.InsertUpdateTemplates(T);

            //Update
            R = new ResultadoCRUD_Models();
            T = new Templates_Models();
            T.tem_id = 11;
            T.tem_cuerpo = "Este es un registro actualizado";
            T.tem_titulo = "332432423423";
            R = T.InsertUpdateTemplates(T);

            //Delete
            
            List<ResultadoCRUD_Models> ListR = new List<ResultadoCRUD_Models>();
            T = new Templates_Models();
            for (int i = 0; i < 15; i++)
            {
                R = new ResultadoCRUD_Models();
                R = T.DeleteTemplates(new Templates_Models() { tem_id = i });
                ListR.Add(R);
            }
            
            //Select
            List<Templates_Models> ListM = new List<Templates_Models>();
            ListM = T.SelectTemplates();



        }
    }
}

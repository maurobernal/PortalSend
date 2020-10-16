using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Web;

namespace PortalSend.Models
{
    public class Inbox_Models
    {
        public int id { get; set; }
        public string content { get; set; }


        public List<Inbox_Models> GetInbox()
        {
            List<Inbox_Models> ListI = new List<Inbox_Models>();

            try
            {
                var query = SynWayAPI_models.Consultas(SynWayAPI_models.Consultas_Types.GetInbox).res_contenido;
                if (query.result=="ok")
                {
                    int i = 1;
                    foreach (var item in query.content.Split(';'))
                    {
                        ListI.Add(new Inbox_Models { id = i, content = item });
                        i++;
                    }
                }
                else
                {
                    ListI.Add(new Inbox_Models { id = -1, content = query.content});

                }
                

                return ListI;
            }
            catch (Exception ex)
            {
                ListI.Add(new Inbox_Models() { id = -1, content = ex.Message });

                return ListI;
            }

        }
    }
}
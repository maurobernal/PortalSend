using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Hangfire;
using Hangfire.Common;
using Hangfire.Logging;
using Hangfire.SqlServer;
using Hangfire.States;
using Hangfire.Storage;

namespace PortalSend.Models
{
    public class LogFailureAttribute : JobFilterAttribute, IApplyStateFilter
    {
        private static readonly ILog Logger = LogProvider.GetCurrentClassLogger();

        public void OnStateApplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
            try
            {

           
            Mensajes_Models M = new Mensajes_Models().SelectMensaje(int.Parse(context.BackgroundJob.Id));
                if (M!=null)
                { 
                        M.men_fechamodif = DateTime.Now;
                        M.men_estado = context.NewState.Name;
                        M.InsertUpdateMensajes(M);

                }
                var failedState = context.NewState as FailedState;
            if (failedState != null)
            {
                Logger.ErrorException(
                    String.Format("Background job #{0} was failed with an exception.", context.BackgroundJob.Id),
                    failedState.Exception);
            }
            else
            {

            }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void OnStateUnapplied(ApplyStateContext context, IWriteOnlyTransaction transaction)
        {
        }
    }
}
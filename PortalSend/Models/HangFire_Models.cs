using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using Hangfire;
using Hangfire.Logging;
using Hangfire.SqlServer;
using Hangfire.States;
using Hangfire.Storage;

namespace PortalSend.Models
{
    public class HangFire_Models
    {

        public HangFire_Models()
        {
            try
            {


                GlobalConfiguration.Configuration
                   .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                   .UseColouredConsoleLogProvider()
                   .UseSimpleAssemblyNameTypeSerializer()
                   .UseRecommendedSerializerSettings()
              //.UseSqlServerStorage("PortalSend_Entities");
              //  .UseSqlServerStorage("datasource=localhost;initial catalog=PortalSend;persist security info=True;user id=user_portalsend;password=Codeme123;MultipleActiveResultSets=True;", new SqlServerStorageOptions
              .UseSqlServerStorage(WebConfigurationManager.ConnectionStrings["HangFire_Connection"].ConnectionString, new SqlServerStorageOptions

              {
                   CommandBatchMaxTimeout = TimeSpan.FromMinutes(2),
                   SlidingInvisibilityTimeout = TimeSpan.FromMinutes(2),
                   QueuePollInterval = TimeSpan.Zero,
                   UseRecommendedIsolationLevel = true,
                   UsePageLocksOnDequeue = true,
                   DisableGlobalLocks = true,
               }).WithJobExpirationTimeout(TimeSpan.FromHours(6))
                .UseFilter(new LogFailureAttribute())

              
                

                ;
            }
            catch (Exception ex)
            {

                throw;
            }


        }


    }
}






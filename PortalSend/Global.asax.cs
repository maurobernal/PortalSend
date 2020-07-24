using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Hangfire;
using PortalSend.Models;

namespace PortalSend
{
    public class MvcApplication : System.Web.HttpApplication
    {
        private BackgroundJobServer _backgroundJobServer;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            HangFire_Models H = new HangFire_Models();
            _backgroundJobServer = new BackgroundJobServer();

        }

        protected void Application_End(object sender, EventArgs e)
        {
            _backgroundJobServer.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Http;
using System.Web.Routing;
using Serilog;
using System.Configuration;

namespace Hostsol.Demo.BlueGreenTestWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Log.Logger = new LoggerConfiguration()
                .WriteTo.ColoredConsole()
                .WriteTo.Seq(ConfigurationManager.AppSettings["SeqServerUrl"])
                .CreateLogger();

            //emulate long warmup process
            System.Threading.Thread.Sleep(10000);
            StartUpComplete = true;
        }

        public static bool StartUpComplete = false;
    }
}

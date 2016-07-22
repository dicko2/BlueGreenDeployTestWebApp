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
using System.Reflection;
using System.Web.Hosting;
using System.Web.Compilation;

namespace Hostsol.Demo.BlueGreenTestWeb
{
    public class MvcApplication : System.Web.HttpApplication
    {

        private readonly Assembly _assembly = HostingEnvironment.IsHosted
          ? BuildManager.GetGlobalAsaxType().BaseType.Assembly
          : Assembly.GetEntryAssembly()
            ?? Assembly.GetExecutingAssembly();
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("MachineName",Environment.MachineName)
                .Enrich.WithProperty("AppName", _assembly.GetName().Name)
                .Enrich.WithProperty("AppVersion", _assembly.GetName().Version)
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

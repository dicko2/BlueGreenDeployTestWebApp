using Serilog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace Hostsol.Demo.BlueGreenTestWeb.Controllers
{
    public class HealthCheckController : ApiController
    {
        // GET: api/HealthCheck
        public string Get()
        {
            if(MvcApplication.StartUpComplete)
            {
                Log.Debug("OK health check");
                return "STATUSOK";
            }
            else
            {
                Log.Debug("Application not ready health check");
                throw new Exception("Not Ready");
            }
        }
    }
}

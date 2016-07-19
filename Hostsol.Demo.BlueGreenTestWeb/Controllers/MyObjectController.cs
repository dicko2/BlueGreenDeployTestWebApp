using Hostsol.Demo.BlueGreenTestWeb.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Hostsol.Demo.BlueGreenTestWeb.Controllers
{
    public class MyObjectController : Controller
    {
        // GET: MyObject/5
        public ActionResult Index(int id)
        {

            if (bool.Parse(ConfigurationManager.AppSettings["FailRandom"].ToString())
                && (new Random()).Next(1,3) == 2)
            {
                throw new Exception("Request is dead, ahhhhhhhh!");
            }

            if (bool.Parse(ConfigurationManager.AppSettings["FailNumber10"].ToString())
                && id == 10)
            {
                throw new Exception("Request is dead, ahhhhhhhh!");
            }

            var sqlconn = new SqlConnection(ConfigurationManager.ConnectionStrings["MyObjectDatabase"].ConnectionString);
            var sqlcmd = new SqlCommand("", sqlconn);
            sqlcmd.CommandText = "SELECT Id ,Description ,MetaData FROM dbo.MyData WHERE Id=@Id";
            sqlcmd.Parameters.AddWithValue("@Id", id);

            var md = new MyData();
            sqlconn.Open();
            using (var dr = sqlcmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection))
            {
                while (dr.Read())
                {
                    md.Id = (int)dr["Id"];
                    md.MetaData = dr["MetaData"].ToString();
                    md.Description = dr["Description"].ToString();
                }
            }

            ViewBag.MyData = md;

            return View();
        }
    }
}

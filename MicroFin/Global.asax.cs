using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace MicroFin
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        static MySqlConnection con;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            string conStr = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
            con = new MySqlConnection(conStr);
            con.Open();
        }
        public static MySqlConnection getConnection()
        {
            if (con.State == System.Data.ConnectionState.Closed)
            {
                con.Open();
            }
            return con;
        }
    }
}

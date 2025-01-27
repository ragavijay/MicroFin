﻿using System;
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
        public static string conStr;
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            conStr = ConfigurationManager.ConnectionStrings["MySqlConnectionString"].ConnectionString;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using CasasRed_Nuevo3_.Controllers;
using Hangfire;

namespace CasasRed_Nuevo3_
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //private BackgroundJobServer _backgroundJobServer;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //GlobalConfiguration.Configuration
            //    .UseSqlServerStorage("Data Source=mi3-wsq4.a2hosting.com;Initial Catalog=CasasRed;Persist Security Info=True;User ID=adminDani;Password=sql_Password123;MultipleActiveResultSets=True;Application Name=EntityFramework");

            //_backgroundJobServer = new BackgroundJobServer();

            //GestionsController gc = new GestionsController();
            //RecurringJob.AddOrUpdate(() => gc.ChecarRecordatorios(), Cron.Minutely);
        }

        //protected void Application_End(object sender, EventArgs e)
        //{
        //    _backgroundJobServer.Dispose();
        //}
    }
}

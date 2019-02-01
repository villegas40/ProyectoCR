using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Hangfire;
using CasasRed_Nuevo3_.Controllers;

[assembly: OwinStartup(typeof(CasasRed_Nuevo3_.StartUpClass))]

namespace CasasRed_Nuevo3_
{
    public class StartUpClass
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            GlobalConfiguration.Configuration
                .UseSqlServerStorage("Data Source=mi3-wsq4.a2hosting.com;Initial Catalog=CasasRed;Persist Security Info=True;User ID=adminDani;Password=sql_Password123;MultipleActiveResultSets=True;Application Name=EntityFramework");
            
            app.UseHangfireDashboard();

            BackgroundJob.Enqueue(() => Console.WriteLine("Fire-and-forget!"));
            //Correr el metodo de AddNotify para que cheque cada minuto los registros nuevos y sean agregados al schedule
            GestionsController gc = new GestionsController();
            RecurringJob.AddOrUpdate(() => gc.ChecarRecordatorios(), Cron.Minutely);

            app.UseHangfireServer();
        }
    }
}

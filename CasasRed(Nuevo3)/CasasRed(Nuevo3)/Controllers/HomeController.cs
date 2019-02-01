using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
//De aqui empieza la basura, Abiel
using Hangfire;
using System.Net.Mail;
using System.Net;

namespace CasasRed_Nuevo3_.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //Borrar solo para propositos de test
            //BackgroundJob.Schedule(
            //    () => SendMail(),
            //        new DateTime(2018, 11, 10, 21, 25, 00));

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /*Esto tambien es para fines de prueba borrrar después*/
        public static void SendMail()
        {
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("casasredposventa@gmail.com", "casasred123");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;

            MailMessage mail = new MailMessage("casasredposventa@gmail.com", "drokemx@gmail.com");
            mail.Subject = "Test";
            mail.Body = "Este es un mensaje programado al entrar al index de la página web, me fui a cenar, deje coriendo el servidor, puse que se enviara a las 7:40, me avisas si llego.";

            try
            {
                client.Send(mail);
                //Console.Write("Mensaje enviado con éxito..");
            }
            catch (SmtpException ex)
            {
                string msg = "Error al enviar el mail..";
                msg += ex.Message;
            }
        }
    }
}
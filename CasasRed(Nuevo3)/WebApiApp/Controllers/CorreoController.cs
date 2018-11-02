using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;

namespace CasasRed_Nuevo3_.Controllers
{
    public class CorreoController : Controller
    {
        // GET: Correo
        public ActionResult Index()
        {
            return View();
        }

        public void sendmail(string correo)
        {
            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("casasredposventa@gmail.com", "casasred123");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("drokemx@gmail.com");
            mail.To.Add(correo); //cambiar a variable para enviar correo
            mail.Subject = "Bienvenido a casas Red";
            mail.Body = "Correo de prueba..";


            try
            {
                client.Send(mail);
            }
            catch (SmtpException x)
            {
                Console.Write(x.InnerException.Message);
                Console.ReadLine();
            }
        }

        public void sendMailGestion(string correo)
        {
            SmtpClient client = new SmtpClient
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("casasredposventa@gmail.com", "casasred123"),
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true
            };

            MailMessage mail = new MailMessage
            {
                From = new MailAddress("casasredposventa@gmail.com")
            };

            mail.To.Add(correo);
            mail.Subject = "CasasRed";
            mail.IsBodyHtml = true;
            mail.Body = "<center><img src=\"http://oi64.tinypic.com/2rh5ukp.jpg\"><p>Buen día.</p><h3>Un nuevo cliente se ha dado de alta.</h3></center><center><table><tr><th><th></tr><tr><td><a href=\"http://www.casasredposventa.com/\"><img src=\"http://oi65.tinypic.com/2uyq245.jpg\"></a></td><td><a href=\"http://www.casasredposventa.com/\"><img src=\"http://oi68.tinypic.com/30nad8p.jpg\"></a></td></tr></table><br></br><br></br><table><tr><th><th></tr><tr><td><a href=\"http://casasred.com/servicios/\"><img src=\"http://i63.tinypic.com/11l53cx_th.jpg\"></a></td><td><a href=\"http://casasred.com/servicios/\"><img src=\"http://i63.tinypic.com/1zp2yqb_th.png\"></a></td></tr></table><p>Dar clic a la opción deseada para mayor información</p></center><br></br><br></br><footer><center>CasasRed 2018 © </center></footer>";

            try
            {
                client.Send(mail);
            }
            catch (SmtpException x)
            {
                Console.Write(x.InnerException.Message);
                Console.ReadLine();
            }
        }

        public void sendMailCorretaje(string correo)
        {
            SmtpClient client = new SmtpClient
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("casasredposventa@gmail.com", "casasred123"),
                Port = 587,
                Host = "smtp.gmail.com",
                EnableSsl = true
            };

            MailMessage mail = new MailMessage
            {
                From = new MailAddress("casasredposventa@gmail.com")
            };

            mail.To.Add(correo);
            mail.Subject = "CasasRed";
            mail.IsBodyHtml = true;
            mail.Body = "<center><img src=\"http://oi64.tinypic.com/2rh5ukp.jpg\"><p>Buen día.</p><h3>Una nueva casa se ha dado de alta.</h3></center><center><table><tr><th><th></tr><tr><td><a href=\"http://www.casasredposventa.com/\"><img src=\"http://oi65.tinypic.com/2uyq245.jpg\"></a></td><td><a href=\"http://www.casasredposventa.com/\"><img src=\"http://oi68.tinypic.com/30nad8p.jpg\"></a></td></tr></table><br></br><br></br><table><tr><th><th></tr><tr><td><a href=\"http://casasred.com/servicios/\"><img src=\"http://i63.tinypic.com/11l53cx_th.jpg\"></a></td><td><a href=\"http://casasred.com/servicios/\"><img src=\"http://i63.tinypic.com/1zp2yqb_th.png\"></a></td></tr></table><p>Dar clic a la opción deseada para mayor información</p></center><br></br><br></br><footer><center>CasasRed 2018 © </center></footer>";

            try
            {
                client.Send(mail);
            }
            catch (SmtpException x)
            {
                Console.Write(x.InnerException.Message);
                Console.ReadLine();
            }
        }
    }
}
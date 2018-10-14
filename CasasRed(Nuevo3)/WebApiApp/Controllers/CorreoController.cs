﻿using System;
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
    }
}
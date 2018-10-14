using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nexmo.Api;

namespace CasasRed_Nuevo3_.Controllers
{
    public class SmsController : Controller
    {
        // GET: Sms
        public ActionResult Index()
        {
            return View();
        }

        public void SendSms(string telefono)
        {
            var client = new Client(creds: new Nexmo.Api.Request.Credentials
            {
                //mias:9565a1b8,c9ZGXHvI2ghr7dq7
                //casasred: 12dc61d8,cHhWZ0gmeqUicCyg
                ApiKey = "12dc61d8",
                ApiSecret = "cHhWZ0gmeqUicCyg"
            });
            /* https://www.nexmo.com/ 
                Credenciales de acceso:
                        email: drokemx@gmail.com
                        clave: Hola123$$

                        email: casasredposventa@gmail.com
                        clave: Hola123$$

            */
            var results = client.SMS.Send(request: new SMS.SMSRequest
            {
                from = "CasasRed",
                to = telefono,
                text = "Bienvenidos a casas red , gracias por confiar en nosotros."
            });
        }
    }
}
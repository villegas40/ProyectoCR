using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Nexmo.Api;
using System.Web.Mvc;

namespace CasasRed_Nuevo3_.Controllers
{
    public class SmsController : Controller
    {
        // GET: Sms
        public ActionResult Index()
        {
            return View();
        }

        public void SendSms()
        {
            var client = new Client(creds: new Nexmo.Api.Request.Credentials
            {
                ApiKey = "9565a1b8",
                ApiSecret = "c9ZGXHvI2ghr7dq7"
            });
            /* https://www.nexmo.com/ 
                Credenciales de acceso:
                        email: drokemx@gmail.com
                        clave: Hola123$$

            */
            var results = client.SMS.Send(request: new SMS.SMSRequest
            {
                from = "CasasRed",
                to = "526643039336", 
                text = "Bienvenidos a casas red , gracias por confiar en nosotros."
            });
        }

    }
}
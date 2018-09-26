using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CasasRed_Nuevo3_.Controllers
{
    public class ReportesController : Controller
    {
        // GET: Reportes
        public ActionResult Index(string titulo = "")
        {
            @ViewBag.Title = titulo.Replace('-', ' ');
            return View();
        }

        public ActionResult MultiReport(string titulo = "")
        {
            ViewBag.Title = titulo.Replace('-', ' ');
            return View();
        }
    }
}
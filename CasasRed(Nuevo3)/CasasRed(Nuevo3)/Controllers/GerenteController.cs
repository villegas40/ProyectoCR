using System;

using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CasasRed_Nuevo3_.Models;

namespace CasasRed_Nuevo3_.Controllers
{
    public class GerenteController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();
        // GET: Gerente
        public ActionResult Index()
        {
            var gen = new FooViewModel();
            gen.gestions = db.Gestion.ToList();
            gen.corretajes = db.Corretaje.ToList();

            var ge = ((from g in db.Gestion select new { g.Id_Corretaje, g.Cliente.Gral_Nombre }).ToList());

            List<string> nombres = new List<string>();
            List<int?> ids = new List<int?>();
            foreach (var XD in ge)
            {
                nombres.Add(XD.Gral_Nombre);
                ids.Add(XD.Id_Corretaje);
            }

            //ViewBag.test = (from g in db.Gestion select new {g.Id_Corretaje, g.Cliente.Gral_Nombre}).ToList();
            ViewBag.listanombres = nombres;
            ViewBag.listaids = ids;

            return View(gen);

        }

        public class FooViewModel
        {
            public IEnumerable<Gestion> gestions { get; set; }
            public IEnumerable<Corretaje> corretajes { get; set; }
        }

        public class DetailsViewModel
        {
            public IEnumerable<Cliente> clientes { get; set; }
            public IEnumerable<Corretaje> corretajes { get; set; }
            public IEnumerable<Contaduria> contadurias { get; set; }
            public IEnumerable<Gestion> gestions { get; set; }
            public IEnumerable<Habilitacion> habilitacions { get; set; }
            public IEnumerable<Verificacion> verificacions { get; set; }
        }

        //public ActionResult Details(int? id, int? idc)
        //{
        //   if (id == null)
        //   {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    else
        //    {
        //        var result = new DetailsViewModel();
        //        result.corretajes = db.Corretaje.Find(id);
               

        //        db.Cliente.Find(id);
        //        db.Corretaje.Find(id);
        //        return View(result);
        //   }
        //}
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CasasRed_Nuevo3_.Models;
using System.Web.Mvc;

namespace CasasRed_Nuevo3_.Controllers
{
    public class GerenteController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();
        // GET: Gerente
        public ActionResult Index()
        {
            var gen = new FooViewModel();
            gen.clientes = db.Cliente.ToList();
            gen.corretajes = db.Corretaje.ToList();
            return View(gen);
        }

        public class FooViewModel
        {
            public IEnumerable<Cliente> clientes { get; set; }
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
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    else
        //    {
        //        var result = new DetailsViewModel();
        //        result.clientes = db.Cliente.Find(id);
        //        result.gestions = db.Gestion.Find();
        //        db.Cliente.Find(id);
        //        db.Corretaje.Find();
        //        return View(result);
        //   }
        //}
    }
}
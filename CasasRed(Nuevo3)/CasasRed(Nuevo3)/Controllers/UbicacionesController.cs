using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CasasRed_Nuevo3_.Models;
//El bueno que se subira a bitbuket y github
namespace CasasRed_Nuevo3_.Controllers
{
    public class UbicacionesController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: Ubicaciones
        public ActionResult Index()
        {
            return View(db.Ubicaciones.ToList());
        }

        // GET: Ubicaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubicaciones ubicaciones = db.Ubicaciones.Find(id);
            if (ubicaciones == null)
            {
                return HttpNotFound();
            }
            return View(ubicaciones);
        }

        // GET: Ubicaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Ubicaciones/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ubi_id,ubi_codigo,ubi_descripcion")] Ubicaciones ubicaciones)
        {
            if (ModelState.IsValid)
            {
                db.Ubicaciones.Add(ubicaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(ubicaciones);
        }

        // GET: Ubicaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubicaciones ubicaciones = db.Ubicaciones.Find(id);
            if (ubicaciones == null)
            {
                return HttpNotFound();
            }
            return View(ubicaciones);
        }

        // POST: Ubicaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ubi_id,ubi_codigo,ubi_descripcion")] Ubicaciones ubicaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ubicaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(ubicaciones);
        }

        // GET: Ubicaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ubicaciones ubicaciones = db.Ubicaciones.Find(id);
            if (ubicaciones == null)
            {
                return HttpNotFound();
            }
            return View(ubicaciones);
        }

        // POST: Ubicaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ubicaciones ubicaciones = db.Ubicaciones.Find(id);
            db.Ubicaciones.Remove(ubicaciones);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult BuscarUbicaciones(string filtro = "", int pagina = 1)
        {
            int totalPagina = (int)Math.Ceiling((decimal)(db.Ubicaciones.Count() / 15));
            var busqueda = (from u in db.Ubicaciones where u.ubi_codigo.Contains(filtro) || u.ubi_descripcion.Contains(filtro) select new { id = u.ubi_id, descripcion = u.ubi_descripcion, codigo = u.ubi_codigo, total = totalPagina }).Skip((pagina - 1) * 15).Take(15);
            if (filtro == "")
	        {
                totalPagina = (int)Math.Ceiling((decimal)(db.Ubicaciones.Count() / 15));
                busqueda = (from u in db.Ubicaciones select new { id = u.ubi_id, descripcion = u.ubi_descripcion, codigo = u.ubi_codigo, total = totalPagina }).Skip((pagina - 1) * 15).Take(15);
            }
            return Json(busqueda, JsonRequestBehavior.AllowGet);
        }

    }
}

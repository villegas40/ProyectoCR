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
    public class ExistenciasController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: Existencias
        public ActionResult Index()
        {
            var existencias = db.Existencias.Include(e => e.Articulos).Include(e => e.Usuario).Include(e => e.Ubicaciones);
            return View(existencias.ToList());
        }

        // GET: Existencias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Existencias existencias = db.Existencias.Find(id);
            if (existencias == null)
            {
                return HttpNotFound();
            }
            return View(existencias);
        }

        // GET: Existencias/Create
        public ActionResult Create()
        {
            //ViewBag.ext_art_id = new SelectList(db.Articulos, "art_id", "art_nombre");
            //ViewBag.ext_usuarioAgrego = new SelectList(db.Usuario, "Id", "usu_nombre");
            //ViewBag.ext_ubicacion = new SelectList(db.Ubicaciones, "ubi_id", "ubi_codigo");
            ViewBag.ext_art_id = new SelectList(db.Articulos, "art_id", "art_nombre");
            ViewBag.ext_usuarioAgrego = new SelectList(db.Usuario, "Id", "usu_username");
            ViewBag.ext_ubicacion = new SelectList(db.Ubicaciones, "ubi_id", "ubi_codigo");
            return View();
        }

        // POST: Existencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ext_art_id,ext_cantidad,ext_cantidadActual,ext_precioUnitario,ext_fechaAgregado,ext_usuarioAgrego,ext_ubicacion")] Existencias existencias)
        {
            if (ModelState.IsValid)
            {
                db.Existencias.Add(existencias);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.ext_art_id = new SelectList(db.Articulos, "art_id", "art_nombre", existencias.ext_art_id);
            //ViewBag.ext_usuarioAgrego = new SelectList(db.Usuario, "Id", "usu_nombre", existencias.ext_usuarioAgrego);
            //ViewBag.ext_ubicacion = new SelectList(db.Ubicaciones, "ubi_id", "ubi_codigo", existencias.ext_ubicacion);
            ViewBag.ext_art_id = new SelectList(db.Articulos, "art_id", "art_nombre", existencias.ext_art_id);
            ViewBag.ext_usuarioAgrego = new SelectList(db.Usuario, "Id", "usu_username", existencias.ext_usuarioAgrego);
            ViewBag.ext_ubicacion = new SelectList(db.Ubicaciones, "ubi_id", "ubi_codigo", existencias.ext_ubicacion);
            return View(existencias);
        }

        // GET: Existencias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Existencias existencias = db.Existencias.Find(id);
            if (existencias == null)
            {
                return HttpNotFound();
            }
            //ViewBag.ext_art_id = new SelectList(db.Articulos, "art_id", "art_nombre", existencias.ext_art_id);
            //ViewBag.ext_usuarioAgrego = new SelectList(db.Usuario, "Id", "usu_nombre", existencias.ext_usuarioAgrego);
            //ViewBag.ext_ubicacion = new SelectList(db.Ubicaciones, "ubi_id", "ubi_codigo", existencias.ext_ubicacion);
            ViewBag.ext_art_id = new SelectList(db.Articulos, "art_id", "art_nombre", existencias.ext_art_id);
            ViewBag.ext_usuarioAgrego = new SelectList(db.Usuario, "Id", "usu_username", existencias.ext_usuarioAgrego);
            ViewBag.ext_ubicacion = new SelectList(db.Ubicaciones, "ubi_id", "ubi_codigo", existencias.ext_ubicacion);
            return View(existencias);
        }

        // POST: Existencias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,ext_art_id,ext_cantidad,ext_cantidadActual,ext_precioUnitario,ext_fechaAgregado,ext_usuarioAgrego,ext_ubicacion")] Existencias existencias)
        {
            if (ModelState.IsValid)
            {
                db.Entry(existencias).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.ext_art_id = new SelectList(db.Articulos, "art_id", "art_nombre", existencias.ext_art_id);
            //ViewBag.ext_usuarioAgrego = new SelectList(db.Usuario, "Id", "usu_nombre", existencias.ext_usuarioAgrego);
            //ViewBag.ext_ubicacion = new SelectList(db.Ubicaciones, "ubi_id", "ubi_codigo", existencias.ext_ubicacion);
            ViewBag.ext_art_id = new SelectList(db.Articulos, "art_id", "art_nombre", existencias.ext_art_id);
            ViewBag.ext_usuarioAgrego = new SelectList(db.Usuario, "Id", "usu_username", existencias.ext_usuarioAgrego);
            ViewBag.ext_ubicacion = new SelectList(db.Ubicaciones, "ubi_id", "ubi_codigo", existencias.ext_ubicacion);
            return View(existencias);
        }

        // GET: Existencias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Existencias existencias = db.Existencias.Find(id);
            if (existencias == null)
            {
                return HttpNotFound();
            }
            return View(existencias);
        }

        // POST: Existencias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Existencias existencias = db.Existencias.Find(id);
            db.Existencias.Remove(existencias);
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
    }
}

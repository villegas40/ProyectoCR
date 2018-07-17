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
    public class VerificacionsController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: Verificacions
        public ActionResult Index()
        {
            var verificacion = db.Verificacion.Include(v => v.Cliente);
            return View(verificacion.ToList());
        }

        // GET: Verificacions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verificacion verificacion = db.Verificacion.Find(id);
            if (verificacion == null)
            {
                return HttpNotFound();
            }
            return View(verificacion);
        }

        // GET: Verificacions/Create
        public ActionResult Create()
        {
            ViewBag.Id = new SelectList(db.Cliente, "Id", "Gral_Nombre");
            return View();
        }

        // POST: Verificacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Vfn_Persona_fisica,Vfn_Visto_persona,Vfn_Tiempo_estimado,Vfn_Tiempo,Vfn_Tiene_costo,Vfn_Costo,Vfn_Trato_asesor,Vfn_Observaciones")] Verificacion verificacion)
        {
            if (ModelState.IsValid)
            {
                db.Verificacion.Add(verificacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id = new SelectList(db.Cliente, "Id", "Gral_Nombre", verificacion.Id);
            return View(verificacion);
        }

        // GET: Verificacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verificacion verificacion = db.Verificacion.Find(id);
            if (verificacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id = new SelectList(db.Cliente, "Id", "Gral_Nombre", verificacion.Id);
            return View(verificacion);
        }

        // POST: Verificacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Vfn_Persona_fisica,Vfn_Visto_persona,Vfn_Tiempo_estimado,Vfn_Tiempo,Vfn_Tiene_costo,Vfn_Costo,Vfn_Trato_asesor,Vfn_Observaciones")] Verificacion verificacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(verificacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id = new SelectList(db.Cliente, "Id", "Gral_Nombre", verificacion.Id);
            return View(verificacion);
        }

        // GET: Verificacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verificacion verificacion = db.Verificacion.Find(id);
            if (verificacion == null)
            {
                return HttpNotFound();
            }
            return View(verificacion);
        }

        // POST: Verificacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Verificacion verificacion = db.Verificacion.Find(id);
            db.Verificacion.Remove(verificacion);
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

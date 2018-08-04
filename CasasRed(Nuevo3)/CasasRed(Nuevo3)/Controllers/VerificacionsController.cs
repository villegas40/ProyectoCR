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
            //var verificacion = db.Verificacion.Include(v => v.Gestion);
            //return View(verificacion.ToList());
            var verificacion = db.Verificacion.Include(v => v.Gestion);
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
            //ViewBag.Id = new SelectList(db.Cliente, "Id", "Gral_Nombre");
            ViewBag.Id_Gestion = new SelectList(db.Gestion, "Id", "Id");
            return View();
        }

        // POST: Verificacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Vfn_Persona_fisica,Vfn_Visto_persona,Vfn_Tiempo_estimado,Vfn_Tiempo,Vfn_Tiene_costo,Vfn_Costo,Vfn_Trato_asesor,Vfn_Observaciones,Id_Gestion")] Verificacion verificacion)
        {
            if (ModelState.IsValid)
            {
                verificacion.Vfn_Costo = ((verificacion.Vfn_Costo == null) ? 0 : verificacion.Vfn_Costo);
                verificacion.Vfn_Observaciones = ((verificacion.Vfn_Observaciones == null) ? "" : verificacion.Vfn_Observaciones);
                verificacion.Vfn_Persona_fisica = ((verificacion.Vfn_Persona_fisica == null) ? false : verificacion.Vfn_Persona_fisica);
                verificacion.Vfn_Tiempo = ((verificacion.Vfn_Tiempo == null) ? "" : verificacion.Vfn_Tiempo);
                verificacion.Vfn_Tiempo_estimado = ((verificacion.Vfn_Tiempo_estimado == null) ? false : verificacion.Vfn_Tiempo_estimado);
                verificacion.Vfn_Tiene_costo = ((verificacion.Vfn_Tiene_costo == null) ? false : verificacion.Vfn_Tiene_costo);
                verificacion.Vfn_Trato_asesor = ((verificacion.Vfn_Trato_asesor == null) ? "" : verificacion.Vfn_Trato_asesor);
                verificacion.Vfn_Visto_persona = ((verificacion.Vfn_Visto_persona == null) ? false : verificacion.Vfn_Visto_persona);

                db.Verificacion.Add(verificacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.Id = new SelectList(db.Cliente, "Id", "Gral_Nombre", verificacion.Id);
            ViewBag.Id_Gestion = new SelectList(db.Gestion, "Id", "Id", verificacion.Id_Gestion);
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
            //ViewBag.Id = new SelectList(db.Cliente, "Id", "Gral_Nombre", verificacion.Id);
            ViewBag.Id_Gestion = new SelectList(db.Gestion, "Id", "Id", verificacion.Id_Gestion);
            return View(verificacion);
        }

        // POST: Verificacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Vfn_Persona_fisica,Vfn_Visto_persona,Vfn_Tiempo_estimado,Vfn_Tiempo,Vfn_Tiene_costo,Vfn_Costo,Vfn_Trato_asesor,Vfn_Observaciones,Id_Gestion")] Verificacion verificacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(verificacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.Id = new SelectList(db.Cliente, "Id", "Gral_Nombre", verificacion.Id);
            ViewBag.Id_Gestion = new SelectList(db.Gestion, "Id", "Id", verificacion.Id_Gestion);
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

        [HttpPost]
        public string VerfificacionCreate(Verificacion verificacion, int gestion_id)
        {
            CasasRedEntities CS = new CasasRedEntities();
            Verificacion verificacion_obj = new Verificacion {
            Vfn_Persona_fisica = false,
            Vfn_Visto_persona = false,
            Vfn_Tiempo_estimado = false,
            Vfn_Tiene_costo = false,
            Id_Gestion = gestion_id
        };

            CS.Verificacion.Add(verificacion_obj);
            CS.SaveChanges();

            return "String si se pudo...";
        }
    }
}

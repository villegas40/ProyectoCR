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
    public class ContaduriasController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: Contadurias
        public ActionResult Index()
        {
            //var contaduria = db.Contaduria.Include(c => c.Corretaje).Include(c => c.Gestion);
            var contaduria = db.Contaduria.Include(c => c.Corretaje).Include(c => c.Gestion).Include(c => c.Habilitacion);
            return View(contaduria.ToList());
        }

        // GET: Contadurias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contaduria contaduria = db.Contaduria.Find(id);
            if (contaduria == null)
            {
                return HttpNotFound();
            }
            return View(contaduria);
        }

        // GET: Contadurias/Create
        public ActionResult Create()
        {
            //ViewBag.Id = new SelectList(db.Corretaje, "Id", "Crt_Status");
            //ViewBag.Id = new SelectList(db.Gestion, "Id", "Id");
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status");
            ViewBag.Id_Gestion = new SelectList(db.Gestion, "Id", "Id");
            ViewBag.Id_Habilitacion = new SelectList(db.Habilitacion, "Id", "Hbt_Calibre_cableado");
            return View();
        }

        // POST: Contadurias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cnt_Presupuesto_gestion,Cnt_Presupuesto_corretaje,Cnt_Presupuesto_habilitacion,Cnt_Mensualidad,Cnt_Vigilancia,Id_Corretaje,Id_Gestion,Id_Habilitacion")] Contaduria contaduria)
        {
            if (ModelState.IsValid)
            {
                db.Contaduria.Add(contaduria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.Id = new SelectList(db.Corretaje, "Id", "Crt_Status", contaduria.Id);
            //ViewBag.Id = new SelectList(db.Gestion, "Id", "Id", contaduria.Id);
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", contaduria.Id_Corretaje);
            ViewBag.Id_Gestion = new SelectList(db.Gestion, "Id", "Id", contaduria.Id_Gestion);
            ViewBag.Id_Habilitacion = new SelectList(db.Habilitacion, "Id", "Hbt_Calibre_cableado", contaduria.Id_Habilitacion);
            return View(contaduria);
        }

        // GET: Contadurias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contaduria contaduria = db.Contaduria.Find(id);
            if (contaduria == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Id = new SelectList(db.Corretaje, "Id", "Crt_Status", contaduria.Id);
            //ViewBag.Id = new SelectList(db.Gestion, "Id", "Id", contaduria.Id);
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", contaduria.Id_Corretaje);
            ViewBag.Id_Gestion = new SelectList(db.Gestion, "Id", "Id", contaduria.Id_Gestion);
            ViewBag.Id_Habilitacion = new SelectList(db.Habilitacion, "Id", "Hbt_Calibre_cableado", contaduria.Id_Habilitacion);
            return View(contaduria);
        }

        // POST: Contadurias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cnt_Presupuesto_gestion,Cnt_Presupuesto_corretaje,Cnt_Presupuesto_habilitacion,Cnt_Mensualidad,Cnt_Vigilancia,Id_Corretaje,Id_Gestion,Id_Habilitacion")] Contaduria contaduria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contaduria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.Id = new SelectList(db.Corretaje, "Id", "Crt_Status", contaduria.Id);
            //ViewBag.Id = new SelectList(db.Gestion, "Id", "Id", contaduria.Id);
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", contaduria.Id_Corretaje);
            ViewBag.Id_Gestion = new SelectList(db.Gestion, "Id", "Id", contaduria.Id_Gestion);
            ViewBag.Id_Habilitacion = new SelectList(db.Habilitacion, "Id", "Hbt_Calibre_cableado", contaduria.Id_Habilitacion);
            return View(contaduria);
        }

        // GET: Contadurias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contaduria contaduria = db.Contaduria.Find(id);
            if (contaduria == null)
            {
                return HttpNotFound();
            }
            return View(contaduria);
        }

        // POST: Contadurias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Contaduria contaduria = db.Contaduria.Find(id);
            db.Contaduria.Remove(contaduria);
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

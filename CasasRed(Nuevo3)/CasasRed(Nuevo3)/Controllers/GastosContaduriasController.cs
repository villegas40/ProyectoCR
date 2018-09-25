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
    public class GastosContaduriasController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: GastosContadurias
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Administrador")
            {
                var gastosContaduria = db.GastosContaduria.Include(g => g.Corretaje);
                return View(gastosContaduria.ToList());
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
            
        }

        // GET: GastosContadurias/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GastosContaduria gastosContaduria = db.GastosContaduria.Find(id);
            if (gastosContaduria == null)
            {
                return HttpNotFound();
            }
            return View(gastosContaduria);
        }

        // GET: GastosContadurias/Create
        public ActionResult Create()
        {
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status");
            return View();
        }

        // POST: GastosContadurias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GstCon_Concepto,GstCon_Monto,GstCon_Fecha,Id_TipoGasto,Id_Contaduria,Id_Corretaje,Id_Usuario")] GastosContaduria gastosContaduria, int id_casa = 0)
        {
            if (ModelState.IsValid)
            {
                gastosContaduria.Id_Corretaje = id_casa;
                db.GastosContaduria.Add(gastosContaduria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", gastosContaduria.Id_Corretaje); <--------------------------------------------Abiel Aqui Mero
            return View(gastosContaduria);
        }

        // GET: GastosContadurias/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GastosContaduria gastosContaduria = db.GastosContaduria.Find(id);
            if (gastosContaduria == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", gastosContaduria.Id_Corretaje);  <--------------------------------------------Abiel Aqui Mero
            return View(gastosContaduria);
        }

        // POST: GastosContadurias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,GstCon_Concepto,GstCon_Monto,GstCon_Fecha,Id_TipoGasto,Id_Contaduria,Id_Corretaje,Id_Usuario")] GastosContaduria gastosContaduria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(gastosContaduria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", gastosContaduria.Id_Corretaje); <--------------------------------------------Abiel Aqui Mero
            return View(gastosContaduria);
        }

        // GET: GastosContadurias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GastosContaduria gastosContaduria = db.GastosContaduria.Find(id);
            if (gastosContaduria == null)
            {
                return HttpNotFound();
            }
            return View(gastosContaduria);
        }

        // POST: GastosContadurias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GastosContaduria gastosContaduria = db.GastosContaduria.Find(id);
            db.GastosContaduria.Remove(gastosContaduria);
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

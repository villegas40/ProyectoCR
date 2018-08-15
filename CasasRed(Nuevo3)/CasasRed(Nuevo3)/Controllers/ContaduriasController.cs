using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CasasRed_Nuevo3_.Models;
//El bueno que se subira a Github y Bitbucket
namespace CasasRed_Nuevo3_.Controllers
{
    public class ContaduriasController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: Contadurias
        public ActionResult Index()
        {
            var contaduria = db.Contaduria.Include(c => c.Gastos).Include(c => c.GastosContaduria);
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
            ViewBag.Id_Gastos = new SelectList(db.Gastos, "Id", "Gst_Concepto");
            ViewBag.Id_GastosContaduria = new SelectList(db.GastosContaduria, "Id", "Id");
            return View();
        }

        // POST: Contadurias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cnt_Presupuesto_gestion,Cnt_Presupuesto_corretaje,Cnt_Presupuesto_habilitacion,Cnt_Presupuesto,Id_Gastos,Id_GastosContaduria,Id_Corretaje")] Contaduria contaduria)
        {
            if (ModelState.IsValid)
            {
                db.Contaduria.Add(contaduria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Gastos = new SelectList(db.Gastos, "Id", "Gst_Concepto", contaduria.Id_Gastos);
            ViewBag.Id_GastosContaduria = new SelectList(db.GastosContaduria, "Id", "Id", contaduria.Id_GastosContaduria);
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
            ViewBag.Id_Gastos = new SelectList(db.Gastos, "Id", "Gst_Concepto", contaduria.Id_Gastos);
            ViewBag.Id_GastosContaduria = new SelectList(db.GastosContaduria, "Id", "Id", contaduria.Id_GastosContaduria);
            return View(contaduria);
        }

        // POST: Contadurias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cnt_Presupuesto_gestion,Cnt_Presupuesto_corretaje,Cnt_Presupuesto_habilitacion,Cnt_Presupuesto,Id_Gastos,Id_GastosContaduria,Id_Corretaje")] Contaduria contaduria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contaduria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Gastos = new SelectList(db.Gastos, "Id", "Gst_Concepto", contaduria.Id_Gastos);
            ViewBag.Id_GastosContaduria = new SelectList(db.GastosContaduria, "Id", "Id", contaduria.Id_GastosContaduria);
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

        //Funcion para crear un registro vacio cuando se de de alta una nueva casa BUSCAR COMO HACER UPDATE PARA QUE CUANDO SE DE DE ALTA EN GESTION PONER EL NUMERO EN LA CASA QUE CORRESPONDE
        [HttpPost]
        public string CrearContaduria(Contaduria contaduria, int corretaje_id)
        {
            CasasRedEntities CS = new CasasRedEntities();
            Contaduria contaduria_obj = new Contaduria {
                Cnt_Presupuesto_corretaje = 0,
                Cnt_Presupuesto_gestion = 0,
                Cnt_Presupuesto_habilitacion = 0,
                Id_Corretaje = corretaje_id //Para saber a que casa esta asociado el gasto
            };

        CS.Contaduria.Add(contaduria_obj);
            CS.SaveChanges();

            return "Si funciona!...";
        }
    }
}

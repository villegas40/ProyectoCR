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
    public class CalificacionVendedorsController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: CalificacionVendedors
        public ActionResult Index()
        {
            var calificacionVendedor = db.CalificacionVendedor.Include(c => c.Corretaje).Include(c => c.Vendedor);
            return View(calificacionVendedor.ToList());
        }

        // GET: CalificacionVendedors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CalificacionVendedor calificacionVendedor = db.CalificacionVendedor.Find(id);
            if (calificacionVendedor == null)
            {
                return HttpNotFound();
            }
            return View(calificacionVendedor);
        }

        // GET: CalificacionVendedors/Create
        public ActionResult Create()
        {
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status");
            ViewBag.Id_Corretaje = new SelectList(db.Vendedor, "Id", "Vndr_Nombre");
            return View();
        }

        // POST: CalificacionVendedors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,DVndr_Puntaje,Id_Corretaje,Id_Vendedor")] CalificacionVendedor calificacionVendedor)
        {
            if (ModelState.IsValid)
            {
                db.CalificacionVendedor.Add(calificacionVendedor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", calificacionVendedor.Id_Corretaje);
            ViewBag.Id_Corretaje = new SelectList(db.Vendedor, "Id", "Vndr_Nombre", calificacionVendedor.Id_Corretaje);
            return View(calificacionVendedor);
        }

        // GET: CalificacionVendedors/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CalificacionVendedor calificacionVendedor = db.CalificacionVendedor.Find(id);
            if (calificacionVendedor == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", calificacionVendedor.Id_Corretaje);
            ViewBag.Id_Corretaje = new SelectList(db.Vendedor, "Id", "Vndr_Nombre", calificacionVendedor.Id_Corretaje);
            return View(calificacionVendedor);
        }

        // POST: CalificacionVendedors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,DVndr_Puntaje,Id_Corretaje,Id_Vendedor")] CalificacionVendedor calificacionVendedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(calificacionVendedor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", calificacionVendedor.Id_Corretaje);
            ViewBag.Id_Corretaje = new SelectList(db.Vendedor, "Id", "Vndr_Nombre", calificacionVendedor.Id_Corretaje);
            return View(calificacionVendedor);
        }

        // GET: CalificacionVendedors/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CalificacionVendedor calificacionVendedor = db.CalificacionVendedor.Find(id);
            if (calificacionVendedor == null)
            {
                return HttpNotFound();
            }
            return View(calificacionVendedor);
        }

        // POST: CalificacionVendedors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CalificacionVendedor calificacionVendedor = db.CalificacionVendedor.Find(id);
            db.CalificacionVendedor.Remove(calificacionVendedor);
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

        public string CalificacionVendedor(int corretaje_id, int puntaje, int vendedor_id)
        {
            CasasRedEntities CS = new CasasRedEntities();
            var vendedores = (from a in db.CalificacionVendedor where corretaje_id == a.Id_Corretaje && vendedor_id == a.Id_Vendedor select a).FirstOrDefault();
            //var vend = (from a in db.CalificacionVendedor where corretaje_id == a.Id_Corretaje select a).ToList();
            //bool continuar = false;

            if (vendedores != null)
            {

                //if (vendedor_id == Id_Vendedor)
                //{
                vendedores.DVndr_Puntaje = puntaje;
                db.SaveChanges();
                //continuar = true;
                //}

                //else if(vendedor_id == item.Id_Vendedor && continuar == false)
                //{
                //    CalificacionVendedor calVen = new CalificacionVendedor
                //    {
                //        DVndr_Puntaje = puntaje,
                //        Id_Corretaje = corretaje_id,
                //        Id_Vendedor = vendedor_id
                //    };

                //    CS.CalificacionVendedor.Add(calVen);
                //    CS.SaveChanges();
                //}




            }
            else
            {
                CalificacionVendedor calVen = new CalificacionVendedor
                {
                    DVndr_Puntaje = puntaje,
                    Id_Corretaje = corretaje_id,
                    Id_Vendedor = vendedor_id
                };

                CS.CalificacionVendedor.Add(calVen);
                CS.SaveChanges();
            }

            return "String...";
        }
    }
}



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
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {   
                return View();
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Ubicaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
             
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Ubicaciones ubicaciones = db.Ubicaciones.Find(id);
                if (ubicaciones == null)
                {
                    return RedirectToAction("Index");
                }
                return View(ubicaciones);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
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
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
                if (ModelState.IsValid)
                {
                    db.Ubicaciones.Add(ubicaciones);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(ubicaciones);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Ubicaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Ubicaciones ubicaciones = db.Ubicaciones.Find(id);
                if (ubicaciones == null)
                {
                    return RedirectToAction("Index");
                }
                return View(ubicaciones);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // POST: Ubicaciones/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ubi_id,ubi_codigo,ubi_descripcion")] Ubicaciones ubicaciones)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(ubicaciones).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(ubicaciones);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Ubicaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
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
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // POST: Ubicaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
                Ubicaciones ubicaciones = db.Ubicaciones.Find(id);
                db.Ubicaciones.Remove(ubicaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult BuscarUbicaciones(string filtro = "", int pagina = 1, int registrosPagina = 15)
        {
            int totalPagina = (int)Math.Ceiling((double)(db.Ubicaciones.Count()) / registrosPagina);
            var busqueda = (from u in db.Ubicaciones where u.ubi_codigo.Contains(filtro) || u.ubi_descripcion.Contains(filtro) select new { id = u.ubi_id, descripcion = u.ubi_descripcion, codigo = u.ubi_codigo, total = totalPagina }).OrderBy(a => a.codigo).Skip((pagina - 1) * registrosPagina).Take(registrosPagina);
            if (filtro == "")
	        {
                totalPagina = (int)Math.Ceiling((double)(db.Ubicaciones.Count()) / registrosPagina);
                busqueda = (from u in db.Ubicaciones select new { id = u.ubi_id, descripcion = u.ubi_descripcion, codigo = u.ubi_codigo, total = totalPagina }).OrderBy(a => a.codigo).Skip((pagina - 1) * registrosPagina).Take(registrosPagina);
            }
            return Json(busqueda, JsonRequestBehavior.AllowGet);
        }

    }
}

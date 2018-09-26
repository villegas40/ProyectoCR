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
    public class ExistenciasController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: Existencias
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index","Home");
            }
            else if (Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
            var existencias = db.Existencias.Include(e => e.Articulos).Include(e => e.Usuario).Include(e => e.Ubicaciones).Where(e => e.ext_cantidadActual > 0);
            return View(existencias.ToList());
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Existencias/Details/5
        public ActionResult Details(string id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
                if (id == null)
                {
                    //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                    return RedirectToAction("Index");
                }
                List<Existencias> existencias = (from e in db.Existencias where e.ext_art_id == id select e).ToList();
                if (existencias == null)
                {
                    return RedirectToAction("Index");
                }
                return View(existencias);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        public ActionResult Detalle(int? id)
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
                Existencias existencias = db.Existencias.Find(id);
                if (existencias == null)
                {
                    return RedirectToAction("Index");
                }
                return View(existencias);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Detalle([Bind(Include = "Id,ext_art_id,ext_cantidad,ext_precioUnitario,ext_ubicacion")] Existencias existencias)
        {
            Existencias ex = db.Existencias.AsNoTracking().Where( x => x.Id == existencias.Id).First();
            existencias.ext_cantidadActual = ex.ext_cantidadActual;
            existencias.ext_fechaAgregado = ex.ext_fechaAgregado;
            existencias.ext_usuarioAgrego = ex.ext_usuarioAgrego;
            
            db.Entry(existencias).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        // GET: Existencias/Create
        public ActionResult Create()
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

        // POST: Existencias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,ext_art_id,ext_cantidad,ext_cantidadActual,ext_precioUnitario,ext_fechaAgregado,ext_usuarioAgrego,ext_ubicacion")] Existencias existencias)
        {
            bool correcto = true;
            try
            {
                if (ModelState.IsValid)
                {
                    string usuario = Session["Usuario"].ToString();
                    existencias.ext_cantidadActual = existencias.ext_cantidad;
                    existencias.ext_fechaAgregado = DateTime.Now;
                    existencias.ext_usuarioAgrego = (from u in db.Usuario where u.usu_username == usuario select u.Id).FirstOrDefault();
                    db.Existencias.Add(existencias);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                }
                else
                {
                    correcto = false;
                }

            }
            catch (Exception ex)
            {
                correcto = false;
                
            }
            ViewBag.ext_ubicacion = new SelectList(db.Ubicaciones, "ubi_id", "ubi_codigo", existencias.ext_ubicacion);
            ViewBag.Correcto = correcto;
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
                return RedirectToAction("Index");
            }
            
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
                return RedirectToAction("Details", new { id = existencias.ext_art_id });
            }

            return View(existencias);
        }

        // GET: Existencias/Delete/5
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
                Existencias existencias = db.Existencias.Find(id);
                if (existencias == null)
                {
                    return HttpNotFound();
                }
                return View(existencias);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
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
        public JsonResult BuscarArticulo(string filtro = "", int pagina = 1, int registrosPagina = 15)
        {
            if (filtro == "")
            {
                int totalPaginas = (int)Math.Ceiling((double)db.Articulos.Count() / registrosPagina);
                var busqueda = (from a in db.Articulos select new { a.art_id, a.art_nombre, a.art_descripcion, total = totalPaginas }).OrderBy(a => a.art_nombre).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
            return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Articulos where a.art_id.Contains(filtro) || a.art_descripcion.Contains(filtro) || a.art_nombre.Contains(filtro) select new { a.art_id, a.art_nombre, a.art_descripcion}).Count() / registrosPagina);
                var busqueda = (from a in db.Articulos where a.art_id.Contains(filtro) || a.art_descripcion.Contains(filtro) || a.art_nombre.Contains(filtro) select new { a.art_id, a.art_nombre, a.art_descripcion, total = totalPaginas }).OrderBy(a => a.art_nombre).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult BuscarUbicaciones(string filtro = "", int pagina = 1)
        {
            if (filtro == "")
            {
                double tot = (from u in db.Ubicaciones select u).Count();
                int totalPagina = (int)Math.Ceiling((double)((tot / 15)));
                var busqueda = (from u in db.Ubicaciones select new { id = u.ubi_id, descripcion = u.ubi_descripcion, codigo = u.ubi_codigo, total = totalPagina }).OrderBy(u => u.codigo).Skip((pagina - 1) * 15).Take(15).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int totalPagina = (int)Math.Ceiling(((double)(from u in db.Ubicaciones where u.ubi_codigo.Contains(filtro) || u.ubi_descripcion.Contains(filtro) select new { id = u.ubi_id, descripcion = u.ubi_descripcion, codigo = u.ubi_codigo }).Count() / 15));
                var busqueda = (from u in db.Ubicaciones where u.ubi_codigo.Contains(filtro) || u.ubi_descripcion.Contains(filtro) select new { id = u.ubi_id, descripcion = u.ubi_descripcion, codigo = u.ubi_codigo, total = totalPagina }).OrderBy(u => u.codigo).Skip((pagina - 1) * 15).Take(15).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);

            }

        }
        public JsonResult BuscarExistencias(string filtro = "", int pagina = 1, int registrosPagina = 15)
        {
            if (filtro == "")
            {
                if (registrosPagina == 0)
                {
                    registrosPagina = (int)Math.Ceiling((double)(from e in db.Existencias group e by e.ext_art_id into eGroup select new { item = eGroup.Key, cantidad = eGroup.Sum(e => e.ext_cantidadActual) }).Count());
                }
                var totalPaginas = (int)Math.Ceiling((double)(from e in db.Existencias group e by e.ext_art_id into eGroup select new { item = eGroup.Key, cantidad = eGroup.Sum(e => e.ext_cantidadActual) }).Count() / registrosPagina);
                var busqueda = (from e in db.Existencias group e by new { e.ext_art_id, e.Articulos.art_descripcion } into eGroup select new { item = eGroup.Key.ext_art_id, descripcion = eGroup.Key.art_descripcion, cantidad = eGroup.Sum(e => e.ext_cantidadActual), precioTotal = eGroup.Sum(e => e.ext_precioUnitario * e.ext_cantidadActual), total = totalPaginas }).OrderBy(e => e.item).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);

            }
            else
            {
                if (registrosPagina == 0)
                {
                    registrosPagina = (int)Math.Ceiling((double)(from e in db.Existencias group e by e.ext_art_id into eGroup where eGroup.Key.Contains(filtro) select new { item = eGroup.Key, cantidad = eGroup.Sum(e => e.ext_cantidadActual) }).Count());
                }
                var totalPaginas = (int)Math.Ceiling((double)(from e in db.Existencias group e by new { e.ext_art_id, e.Articulos.art_descripcion } into eGroup where eGroup.Key.ext_art_id.Contains(filtro) || eGroup.Key.art_descripcion.Contains(filtro) select new { item = eGroup.Key, cantidad = eGroup.Sum(e => e.ext_cantidadActual) }).Count() / registrosPagina);
                var busqueda = (from e in db.Existencias group e by new { e.ext_art_id, e.Articulos.art_descripcion } into eGroup where eGroup.Key.ext_art_id.Contains(filtro) || eGroup.Key.art_descripcion.Contains(filtro) select new { item = eGroup.Key.ext_art_id, descripcion = eGroup.Key.art_descripcion, cantidad = eGroup.Sum(e => e.ext_cantidadActual), precioTotal = eGroup.Sum(e => e.ext_precioUnitario * e.ext_cantidadActual) , total = totalPaginas }).OrderBy(e => e.item).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UsuarioDetalle(int usu)
        {
            var usuario = (from u in db.Usuario where u.Id == usu select u.usu_username).FirstOrDefault();
            return Json(usuario, JsonRequestBehavior.AllowGet);
        }

        public JsonResult UbicacionDetalle(int ubi)
        {
            var usuario = (from u in db.Ubicaciones where u.ubi_id == ubi select u.ubi_codigo).FirstOrDefault();
            return Json(usuario, JsonRequestBehavior.AllowGet);
        }
    }
}

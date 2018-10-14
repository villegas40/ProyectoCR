using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CasasRed_Nuevo3_.Models;
//El bueno que se subira a Github y Bitbucket
namespace CasasRed_Nuevo3_.Controllers
{
    public class ArticulosController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();
        LoginController lc = new LoginController();
        // GET: Articulos
        public ActionResult Index()
        {
            ActualizarVariables();
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
                return View(db.Articulos.ToList());
            }
            else
            {
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Articulos/Details/5
        public ActionResult Details(string id)
        {
            ActualizarVariables();
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
                Articulos articulos = db.Articulos.Find(id);
                if (articulos == null)
                {
                    return HttpNotFound();
                }
                return View(articulos);
            }
            else
            {
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Articulos/Create
        public ActionResult Create()
        {
            ActualizarVariables();
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
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // POST: Articulos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "art_id,art_nombre,art_descripcion,art_cantidadMinima")] Articulos articulos)
        {
            ActualizarVariables();
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
                if (ModelState.IsValid)
                {
                    articulos.art_fechaIngreso = DateTime.Now;
                    db.Articulos.Add(articulos);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(articulos);

            }
            else
            {
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }

        }

        // GET: Articulos/Edit/5
        public ActionResult Edit(string id)
        {
            ActualizarVariables();
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
                Articulos articulos = db.Articulos.Find(id);
                if (articulos == null)
                {
                    return HttpNotFound();
                }
                return View(articulos);
            }
            else
            {
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // POST: Articulos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "art_id,art_nombre,art_descripcion,art_fechaIngreso,art_cantidadMinima")] Articulos articulos)
        {
            ActualizarVariables();
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
                if (ModelState.IsValid)
                {
                    db.Entry(articulos).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View(articulos);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Articulos/Delete/5
        public ActionResult Delete(string id)
        {
            ActualizarVariables();
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
                Articulos articulos = db.Articulos.Find(id);
                if (articulos == null)
                {
                    return HttpNotFound();
                }
                return View(articulos);
            }
            else
            {
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // POST: Articulos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        //Solo para administrador para eliminar articulos de mas o que no se esten usando y  esten dados de alta
        public ActionResult DeleteConfirmed(string id)
        {
            ActualizarVariables();
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
                Articulos articulos = db.Articulos.Find(id);
                db.Articulos.Remove(articulos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        public ActionResult Historial(string id)
        {   //Articulo campo historial, el historial de asignaciones que tiene ese articulo en todas las casas
            ActualizarVariables();
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
                ViewBag.Articulo = (from c in db.Articulos where c.art_id == id select c.art_nombre).FirstOrDefault();
                ViewBag.id = id;
                return View();
            }
            else
            {
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

        public JsonResult BuscarArticulo(string filtro = "", int pagina = 1, int registrosPagina = 15, string fechaInicio = "1990-01-01", string fechaFin = "3500-12-30")
        {
            DateTime FIn = Convert.ToDateTime(fechaInicio); 
            DateTime FFin = Convert.ToDateTime(fechaFin);
            if (filtro == "")
            {
                if (registrosPagina == 0)
                {
                    registrosPagina = db.Articulos.Count();
                }
                int totalPaginas = (int)Math.Ceiling((double)db.Articulos.Count() / registrosPagina);
                var busqueda = (from a in db.Articulos select new { a.art_id, a.art_nombre, a.art_descripcion, fecha = SqlFunctions.DateName("year", a.art_fechaIngreso).Trim() + "/" + SqlFunctions.StringConvert((double)a.art_fechaIngreso.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("day", a.art_fechaIngreso).Trim(), a.art_cantidadMinima, total = totalPaginas }).OrderBy(a => a.art_nombre).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (registrosPagina == 0)
                {
                    registrosPagina = (from a in db.Articulos where a.art_id.Contains(filtro) || a.art_descripcion.Contains(filtro) || a.art_nombre.Contains(filtro) select new { a.art_id, a.art_nombre, a.art_descripcion }).Count();
                }
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Articulos where a.art_id.Contains(filtro) || a.art_descripcion.Contains(filtro) || a.art_nombre.Contains(filtro) select new { a.art_id, a.art_nombre, a.art_descripcion }).Count() / registrosPagina);
                var busqueda = (from a in db.Articulos where a.art_id.Contains(filtro) || a.art_descripcion.Contains(filtro) || a.art_nombre.Contains(filtro) select new { a.art_id, a.art_nombre, a.art_descripcion, fecha = SqlFunctions.DateName("year", a.art_fechaIngreso).Trim() + "/" + SqlFunctions.StringConvert((double)a.art_fechaIngreso.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("day", a.art_fechaIngreso).Trim(), a.art_cantidadMinima, total = totalPaginas }).OrderBy(a => a.art_nombre).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult BuscarInventarioAsignado(string id = "", string filtro = "", int pagina = 1, int registrosPagina = 15, string tipo = "", string fechaInicio = "1990-01-01", string fechaFin = "3500-12-30")
        {
            DateTime FIn = Convert.ToDateTime(fechaInicio);
            DateTime FFin = Convert.ToDateTime(fechaFin);
            if (filtro != "")
            {
                if (registrosPagina == 0)
                {
                    registrosPagina = (from ci in db.CasaInventario where ci.ci_articulo_id == id && ci.ci_fecha >= FIn && ci.ci_fecha <= FFin && (ci.Corretaje.Crt_Direccion.Contains(filtro) || ci.Usuario.usu_username.Contains(filtro) || (ci.Usuario.usu_nombre + " " + ci.Usuario.usu_apellidoPa + " " + ci.Usuario.usu_apellidoMa).Contains(filtro)) select ci).Count();
                }
                int totalPaginas = (int)Math.Ceiling((double)(from ci in db.CasaInventario
                                                              where ci.ci_articulo_id == id && ci.ci_fecha >= FIn && ci.ci_fecha <= FFin && (ci.Corretaje.Crt_Direccion.Contains(filtro) || ci.Usuario.usu_username.Contains(filtro))
                                                              select ci).Count() / registrosPagina);


                var respuesta = (from ci in db.CasaInventario
                                 where ci.ci_articulo_id == id && ci.ci_fecha >= FIn && ci.ci_fecha <= FFin && (ci.Corretaje.Crt_Direccion.Contains(filtro) || ci.Usuario.usu_username.Contains(filtro) || (ci.Usuario.usu_nombre + " " + ci.Usuario.usu_apellidoPa + " " + ci.Usuario.usu_apellidoMa).Contains(filtro))
                                 select new
                                 {
                                     descripcion = ci.Corretaje.Crt_Direccion,
                                     tipo = "Salida",
                                     cantidad = ci.ci_cantidadAsignada,
                                     fecha = SqlFunctions.DateName("year", ci.ci_fecha).Trim() + "/" + SqlFunctions.StringConvert((double)ci.ci_fecha.Month).TrimStart() + "/" + SqlFunctions.DateName("day", ci.ci_fecha).Trim(),
                                     nombre = (ci.Usuario.usu_nombre + " " + ci.Usuario.usu_apellidoPa + " " + ci.Usuario.usu_apellidoMa),
                                     fechaOrden = ci.ci_fecha,
                                     total = totalPaginas
                                 }).OrderBy(c => c.fechaOrden).Skip((pagina - 1) * registrosPagina).Take(registrosPagina);
                return Json(respuesta.ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (registrosPagina == 0)
                {
                    registrosPagina = (from ci in db.CasaInventario where ci.ci_articulo_id == id && ci.ci_fecha >= FIn && ci.ci_fecha <= FFin select ci).Count();
                }
                int totalPaginas = (int)Math.Ceiling((double)(from ci in db.CasaInventario where ci.ci_articulo_id == id && ci.ci_fecha >= FIn && ci.ci_fecha <= FFin select ci).Count() / registrosPagina);

                var respuesta = (from ci in db.CasaInventario
                                 where ci.ci_articulo_id == id && ci.ci_fecha >= FIn && ci.ci_fecha <= FFin
                                 select new
                                 {
                                     descripcion = ci.Corretaje.Crt_Direccion,
                                     tipo = "Salida",
                                     cantidad = ci.ci_cantidadAsignada,
                                     fecha = SqlFunctions.DateName("year", ci.ci_fecha).Trim() + "/" + SqlFunctions.StringConvert((double)ci.ci_fecha.Month).TrimStart() + "/" + SqlFunctions.DateName("day", ci.ci_fecha).Trim(),
                                     nombre = (ci.Usuario.usu_nombre + " " + ci.Usuario.usu_apellidoPa),
                                     fechaOrden = ci.ci_fecha,
                                     total = totalPaginas
                                 }).OrderBy(c => c.fechaOrden).Skip((pagina - 1) * registrosPagina).Take(registrosPagina);

                //.Union(from e in db.Existencias where e.ext_art_id == id select new{ descripcion = ("Inventario"), tipo = "Entrada", cantidad =  }))
                return Json(respuesta.ToList(), JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ValidItem(string articulo = "")
        {
            articulo = articulo.ToLower();
            bool existe = false;
            var art = (from a in db.Articulos where a.art_id.ToLower() == articulo select a).FirstOrDefault();
            if (art != null) existe = true;
            return Json(existe, JsonRequestBehavior.AllowGet);
        }
        public void ActualizarVariables()
        {
            if (Session["UsuarioID"] != null)
            {

                int idUser = Convert.ToInt32(Session["UsuarioID"].ToString());
                var user = (from u in db.Usuario join ut in db.TipoUsuario on u.Id_TipoUsiario equals ut.Id where u.Id == idUser && u.usu_activo == true select new { u, ut }).FirstOrDefault();
                if (user != null)
                {
                    Session["Usuario"] = user.u.usu_username;
                    Session["Nombre"] = user.u.usu_nombre;
                    Session["ApellidoP"] = user.u.usu_apellidoPa;
                    Session["ApellidoM"] = user.u.usu_apellidoMa;
                    Session["NombreCompleto"] = user.u.usu_nombre + " " + user.u.usu_apellidoPa + " " + user.u.usu_apellidoMa;
                    Session["Tipo"] = user.ut.tipusu_descricion;
                    string direccion = lc.Redireccionar(Session["Tipo"].ToString());
                    Session["Vista"] = direccion.Split('-')[1];
                    Session["Controlador"] = direccion.Split('-')[0];
                }
                else
                {
                    Session["UsuarioID"] = null;
                    Session["Usuario"] = null;
                    Session["Nombre"] = null;
                    Session["ApellidoP"] = null;
                    Session["ApellidoM"] = null;
                    Session["NombreCompleto"] = null;
                    Session["Tipo"] = null;
                    Session["Vista"] = null;
                    Session["Controlador"] = null;
                }
            }
        }
    }
}

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

namespace CasasRed_Nuevo3_.Controllers
{
    public class CasaInventariosController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();
        LoginController lc = new LoginController();
        // GET: CasaInventarios
        public ActionResult Index(int id)
        {
            ActualizarVariables();
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Corretaje" || Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
                ViewBag.Casa = (from c in db.Corretaje where c.Id == id select c.Crt_Direccion).FirstOrDefault();
                ViewBag.id = id;
                return View();
            }
            else
            {
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: CasaInventarios/Details/5
        public ActionResult Details(int? id)
        {
            ActualizarVariables();
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Corretaje" || Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CasaInventario casaInventario = db.CasaInventario.Find(id);
                if (casaInventario == null)
                {
                    return HttpNotFound();
                }
                return View(casaInventario);
            }
            else
            {
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
           
        }

        // GET: CasaInventarios/Create
        public ActionResult Create(int id)
        {
            ActualizarVariables();
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Corretaje" || Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
                ViewBag.casa = id;
                return View();
            }
            else
            {
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
            
        }

        // POST: CasaInventarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ci_corretaje_id,ci_articulo_id,ci_cantidadAsignada")] CasaInventario casaInventario)
        {
            ActualizarVariables();
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Corretaje" || Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
               
            if (ModelState.IsValid)
            {
                casaInventario.ci_fecha = DateTime.Now;
                casaInventario.ci_usuario_id = Convert.ToInt32(Session["UsuarioID"].ToString());
                db.CasaInventario.Add(casaInventario);
                db.SaveChanges();
                decimal cantidadRestante = casaInventario.ci_cantidadAsignada;
                List<Existencias> ex = db.Existencias.Select(x => x).Where(x => x.ext_art_id == casaInventario.ci_articulo_id).Where(x=> x.ext_cantidadActual>0).OrderBy(e => e.ext_fechaAgregado).ToList();
                foreach (Existencias item in ex)
                {
                    if (cantidadRestante == 0)
                    {
                        break;
                    }
                    if (item.ext_cantidadActual >= cantidadRestante)
                    {
                        item.ext_cantidadActual -= cantidadRestante;
                        cantidadRestante = 0;
                    }
                    else
                    {
                        decimal diferencia = cantidadRestante - (decimal)item.ext_cantidadActual;
                        item.ext_cantidadActual = 0;
                        cantidadRestante = diferencia;
                    }
                    HistorialAsignacion ha = new HistorialAsignacion();
                    ha.ha_existencia_id = item.Id;
                    ha.ha_casaInventario = casaInventario.ci_Id;
                    db.HistorialAsignacion.Add(ha);
                    db.SaveChanges();
                }
                return RedirectToAction("Index", new { id = casaInventario.ci_corretaje_id});
            }

            return View(casaInventario);
            }
            else
            {
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: CasaInventarios/Edit/5
        public ActionResult Edit(int? id)
        {
            ActualizarVariables();
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Corretaje" || Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CasaInventario casaInventario = db.CasaInventario.Find(id);
                if (casaInventario == null)
                {
                    return HttpNotFound();
                }
                else
                {
                    ViewBag.Max = (from e in db.Existencias where e.ext_art_id == casaInventario.ci_articulo_id select e.ext_cantidadActual).Sum();
                    ViewBag.Max += casaInventario.ci_cantidadAsignada;
                    ViewBag.Max = Convert.ToString(ViewBag.Max);
                }
                return View(casaInventario);
            }
            else
            {
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
           
        }

        // POST: CasaInventarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ci_Id,ci_articulo_id,ci_corretaje_id,ci_cantidadAsignada,ci_fecha,ci_usuario_id")] CasaInventario casaInventario)
        {
            ActualizarVariables();
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Corretaje" || Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
                if (ModelState.IsValid)
                {
                    CasaInventario ci = db.CasaInventario.AsNoTracking().Where(x => x.ci_Id == casaInventario.ci_Id).FirstOrDefault();
                    decimal diferencia = ci.ci_cantidadAsignada - casaInventario.ci_cantidadAsignada;
                    List<Existencias> le = new List<Existencias>();
                    foreach (HistorialAsignacion ha in ci.HistorialAsignacion)
                    {
                        le.Add(ha.Existencias);
                    }
                    le.OrderByDescending(x => x.ext_fechaAgregado);
                    if (diferencia < 0)
                    {
                        foreach (Existencias ex in le)
                        {
                            if (diferencia == 0)
                            {
                                break;
                            }
                            if (ex.ext_cantidad >= diferencia)
                            {
                                Existencias ex1 = db.Existencias.Find(ex.Id);
                                ex1.ext_cantidadActual = ex1.ext_cantidadActual + diferencia;
                                diferencia = 0;
                                db.SaveChanges();
                            }
                            else
                            {
                                Existencias ex1 = db.Existencias.Find(ex.Id);
                                ex1.ext_cantidadActual = ex1.ext_cantidad;
                                diferencia = diferencia - (decimal)ex1.ext_cantidad;
                                db.HistorialAsignacion.Remove(ci.HistorialAsignacion.Where(x => x.Existencias.Id == ex.Id).FirstOrDefault());
                                db.SaveChanges();
                            }

                        }
                    }
                    else if (diferencia > 0)
                    {
                        decimal cantidadRestante = diferencia;
                        List<Existencias> ex = db.Existencias.Select(x => x).Where(x => x.ext_art_id == casaInventario.ci_articulo_id).OrderByDescending(e => e.ext_fechaAgregado).ToList();
                        foreach (Existencias item in ex)
                        {
                            if (cantidadRestante == 0)
                            {
                                break;
                            }
                            if (item.ext_cantidad >= (cantidadRestante + item.ext_cantidadActual))
                            {
                                item.ext_cantidadActual += cantidadRestante;
                                cantidadRestante = 0;
                            }
                            else
                            {
                                diferencia = (decimal)item.ext_cantidad - (decimal)item.ext_cantidadActual;
                                item.ext_cantidadActual = item.ext_cantidad;
                                cantidadRestante -= diferencia;
                                HistorialAsignacion ha = db.HistorialAsignacion.Where(x => x.ha_existencia_id == item.Id && x.ha_casaInventario == casaInventario.ci_Id).FirstOrDefault();
                                db.HistorialAsignacion.Remove(ha);
                            }
                            db.SaveChanges();
                        }
                    }
                    db.Entry(casaInventario).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index", new { id = casaInventario.ci_corretaje_id });
                }
                return View(casaInventario);
            }
            else
            {
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: CasaInventarios/Delete/5
        public ActionResult Delete(int? id)
        {
            ActualizarVariables();
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Corretaje" || Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                CasaInventario casaInventario = db.CasaInventario.Find(id);
                if (casaInventario == null)
                {
                    return HttpNotFound();
                }
                return View(casaInventario);
            }
            else
            {
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
           
        }

        // POST: CasaInventarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ActualizarVariables();
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Corretaje" || Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
                CasaInventario casaInventario = db.CasaInventario.Find(id);
                int corretaje = casaInventario.ci_corretaje_id;
                CasaInventario ci = db.CasaInventario.AsNoTracking().Where(x => x.ci_Id == casaInventario.ci_Id).FirstOrDefault();
                decimal diferencia = ci.ci_cantidadAsignada;
                List<Existencias> le = new List<Existencias>();
                foreach (HistorialAsignacion ha in ci.HistorialAsignacion)
                {
                    le.Add(ha.Existencias);
                }
                le.OrderByDescending(x => x.ext_fechaAgregado);
                if (diferencia > 0)
                {
                    decimal cantidadRestante = diferencia;
                    List<Existencias> ex = db.Existencias.Select(x => x).Where(x => x.ext_art_id == casaInventario.ci_articulo_id).OrderByDescending(e => e.ext_fechaAgregado).ToList();
                    foreach (Existencias item in ex)
                    {
                        if (cantidadRestante == 0)
                        {
                            break;
                        }
                        if (item.ext_cantidad >= (cantidadRestante + item.ext_cantidadActual))
                        {
                            item.ext_cantidadActual += cantidadRestante;
                            cantidadRestante = 0;
                            HistorialAsignacion ha = db.HistorialAsignacion.Where(x => x.ha_existencia_id == item.Id && x.ha_casaInventario == casaInventario.ci_Id).FirstOrDefault();
                            db.HistorialAsignacion.Remove(ha);
                            db.SaveChanges();
                        }
                        else
                        {
                            diferencia = (decimal)item.ext_cantidad - (decimal)item.ext_cantidadActual;
                            item.ext_cantidadActual = item.ext_cantidad;
                            cantidadRestante -= diferencia;
                            HistorialAsignacion ha = db.HistorialAsignacion.Where(x => x.ha_existencia_id == item.Id && x.ha_casaInventario == casaInventario.ci_Id).FirstOrDefault();
                            db.HistorialAsignacion.Remove(ha);
                            db.SaveChanges();
                        }
                    }
                }

                db.CasaInventario.Remove(db.CasaInventario.Find(casaInventario.ci_Id));
                db.SaveChanges();
                return RedirectToAction("Index", new { id = corretaje });
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
        public JsonResult BuscarInventarioAsignado(int id, string filtro = "", int pagina = 1, int registrosPagina = 15)
        {
            if (filtro != "")
            {
                if (registrosPagina == 0)
                {
                    registrosPagina = (from ci in db.CasaInventario
                                       where ci.ci_corretaje_id == id && (ci.Articulos.art_descripcion.Contains(filtro) || ci.ci_articulo_id.Contains(filtro) || ci.Usuario.usu_username.Contains(filtro))
                                       select ci).Count();
                }
                int totalPaginas = (int)Math.Ceiling((double)(from ci in db.CasaInventario
                                                              where ci.ci_corretaje_id == id && (ci.Articulos.art_descripcion.Contains(filtro) || ci.ci_articulo_id.Contains(filtro) || ci.Usuario.usu_username.Contains(filtro))
                                                              select ci).Count() / 15);


                var respuesta = (from ci in db.CasaInventario
                                 where ci.ci_corretaje_id == id && (ci.Articulos.art_descripcion.Contains(filtro) || ci.ci_articulo_id.Contains(filtro) || ci.Usuario.usu_username.Contains(filtro))
                                 select new
                                 {
                                     ci.ci_articulo_id,
                                     ci.Articulos.art_descripcion,
                                     ci.ci_cantidadAsignada,
                                     fecha = SqlFunctions.DateName("year", ci.ci_fecha).Trim() + "/" + SqlFunctions.StringConvert((double)ci.ci_fecha.Month).TrimStart() + "/" + SqlFunctions.DateName("day", ci.ci_fecha).Trim(),
                                     nombre = (ci.Usuario.usu_nombre + " " + ci.Usuario.usu_apellidoPa),
                                     ci.ci_Id,
                                     total = totalPaginas
                                 }).OrderBy(c => c.ci_articulo_id);
                return Json(respuesta.ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (registrosPagina == 0)
                {
                    registrosPagina = (from ci in db.CasaInventario where ci.ci_corretaje_id == id select ci).Count();
                }
                int totalPaginas = (int)Math.Ceiling((double)(from ci in db.CasaInventario where ci.ci_corretaje_id == id select ci).Count() / 15);

                var respuesta = (from ci in db.CasaInventario
                                 where ci.ci_corretaje_id == id
                                 select new
                                 {
                                     ci.ci_articulo_id,
                                     ci.Articulos.art_descripcion,
                                     ci.ci_cantidadAsignada,
                                     fecha = SqlFunctions.DateName("year", ci.ci_fecha).Trim() + "/" + SqlFunctions.StringConvert((double)ci.ci_fecha.Month).TrimStart() + "/" + SqlFunctions.DateName("day", ci.ci_fecha).Trim(),
                                     nombre = (ci.Usuario.usu_nombre + " " + ci.Usuario.usu_apellidoPa),
                                     ci.ci_Id,
                                     total = totalPaginas
                                 }).OrderBy(c => c.ci_articulo_id);
                return Json(respuesta.ToList(), JsonRequestBehavior.AllowGet);
            }
        }
        public void ActualizarVariables()
        {
            if (Session["UsuarioID"] != null)
            {
                int idUser = Convert.ToInt32(Session["UsuarioID"].ToString());
                var user = (from u in db.Usuario join ut in db.TipoUsuario on u.Id_TipoUsiario equals ut.Id where u.Id == idUser &&  u.usu_activo == true select new { u, ut }).FirstOrDefault();
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

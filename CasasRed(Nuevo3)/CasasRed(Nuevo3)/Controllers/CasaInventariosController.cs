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

        // GET: CasaInventarios
        public ActionResult Index(int id)
        {
            ViewBag.Casa = (from c in db.Corretaje where c.Id == id select c.Crt_Direccion).FirstOrDefault();
            ViewBag.id = id;
            //var casaInventario = db.CasaInventario.Include(c => c.Corretaje).Include(c => c.Usuario).Where(x=> x.Corretaje.Id == id);
            //return View(casaInventario.ToList());
            return View();
        }

        // GET: CasaInventarios/Details/5
        public ActionResult Details(int? id)
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

        // GET: CasaInventarios/Create
        public ActionResult Create(int id)
        {
            ViewBag.casa = id;
            return View();
        }

        // POST: CasaInventarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ci_corretaje_id,ci_articulo_id,ci_cantidadAsignada")] CasaInventario casaInventario)
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

        // GET: CasaInventarios/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: CasaInventarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ci_Id,ci_articulo_id,ci_corretaje_id,ci_cantidadAsignada,ci_fecha,ci_usuario_id")] CasaInventario casaInventario)
        {
            if (ModelState.IsValid)
            {
                CasaInventario ci = db.CasaInventario.AsNoTracking().Where(x=> x.ci_Id == casaInventario.ci_Id).FirstOrDefault();
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
                        if (diferencia== 0)
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
                else if(diferencia > 0)
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

        // GET: CasaInventarios/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: CasaInventarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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
    }
}

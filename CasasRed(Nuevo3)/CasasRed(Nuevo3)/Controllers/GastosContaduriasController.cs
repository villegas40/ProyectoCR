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
    public class GastosContaduriasController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: GastosContadurias
        public ActionResult Index(int id)
        {
            //if (Session["Usuario"] == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //else if (Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Administrador")
            //{
                ViewBag.Gasto = (from c in db.Corretaje where c.Id == id select c.Crt_Direccion).FirstOrDefault();
                ViewBag.id = id;
                //var gastosContaduria = db.GastosContaduria.Include(g => g.Corretaje);
                //return View(gastosContaduria.ToList());
                return View();
            //}
            //else
            //{
            //    LoginController lc = new LoginController();
            //    string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
            //    return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            //}

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
        public ActionResult Create(int id)
        {
            ViewBag.casa = id;
            ViewBag.Tipo = Session["Tipo"].ToString();

            //Selectlist conceptos de Corretaje
            var listaCorr = new SelectList(new[] {
                new {value = "1", text = "Seleccione un conepto...."},
                new {value = "2", text = "CESPT"},
                new {value = "3", text = "CFE"},
            }, "value", "text", 0);

            //Selectlist conceptos de Gestion
            var listaGest= new SelectList(new[] {
                new {value = "1", text = "Seleccione un conepto...."},
                new {value = "2", text = "CESPT"},
                new {value = "3", text = "CFE"},
            }, "value", "text", 0);

            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status");
            ViewData["Corretaje"] = listaCorr;
            ViewData["Gestion"] = listaGest;

            return View();
        }

        // POST: GastosContadurias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,GstCon_Concepto,GstCon_Monto,GstCon_Descripcion,GstCon_Fecha,Id_Corretaje,Id_Usuario")] GastosContaduria gastosContaduria)
        {
            //Selectlist conceptos de Corretaje
            var listaCorr = new SelectList(new[] {
                new {value = "1", text = "Seleccione un conepto...."},
                new {value = "2", text = "CESPT"},
                new {value = "3", text = "CFE"},
            }, "value", "text", 0);

            //Selectlist conceptos de Gestion
            var listaGest = new SelectList(new[] {
                new {value = "1", text = "Seleccione un conepto...."},
                new {value = "2", text = "CESPT"},
                new {value = "3", text = "CFE"},
            }, "value", "text", 0);

            if (ModelState.IsValid)
            {
                //Guardar campos automaticps
                gastosContaduria.Id_Usuario = int.Parse(Session["UsuarioID"].ToString());
                gastosContaduria.GstCon_Fecha = DateTime.Now;

                db.GastosContaduria.Add(gastosContaduria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewData["Corretaje"] = listaCorr;
            ViewData["Gestion"] = listaGest;
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
        public ActionResult Edit([Bind(Include = "Id,GstCon_Concepto,GstCon_Monto,GstCon_Descripcion,GstCon_Fecha,Id_Corretaje,Id_Usuario")] GastosContaduria gastosContaduria)
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

        public JsonResult BuscarGastosAsignado(int id, string filtro = "", int pagina = 1, int registrosPagina = 15)
        {
            if (filtro != "")
            {
                if (registrosPagina == 0)
                {
                    registrosPagina = (from ci in db.GastosContaduria
                                       where ci.Id_Corretaje == id && (/*ci.Articulos.art_descripcion.Contains(filtro) || ci.ci_articulo_id.Contains(filtro) || */ci.Usuario.usu_username.Contains(filtro))
                                       select ci).Count();
                }
                int totalPaginas = (int)Math.Ceiling((double)(from ci in db.GastosContaduria
                                                              where ci.Id_Corretaje == id && (/*ci.Articulos.art_descripcion.Contains(filtro) || ci.ci_articulo_id.Contains(filtro) ||*/ ci.Usuario.usu_username.Contains(filtro))
                                                              select ci).Count() / 15);


                var respuesta = (from ci in db.GastosContaduria
                                 where ci.Id_Corretaje == id && (/*ci.Articulos.art_descripcion.Contains(filtro) || ci.ci_articulo_id.Contains(filtro) ||*/ ci.Usuario.usu_username.Contains(filtro))
                                 select new
                                 {
                                     ci.Id_Corretaje,
                                     ci.Corretaje.Crt_Direccion,
                                     ci.GstCon_Monto,
                                     ci.GstCon_Concepto,
                                     //fecha = SqlFunctions.DateName("year", ci.GstCon_Fecha).Trim() + "/" + SqlFunctions.StringConvert((double)ci.GstCon_Fecha.).TrimStart() + "/" + SqlFunctions.DateName("day", ci.GstCon_Fecha).Trim(),
                                     nombre = (ci.Usuario.usu_nombre + " " + ci.Usuario.usu_apellidoPa),
                                     ci.Id,
                                     total = totalPaginas
                                 }).OrderBy(c => c.Id);
                return Json(respuesta.ToList(), JsonRequestBehavior.AllowGet);
            }
            else
            {
                if (registrosPagina == 0)
                {
                    registrosPagina = (from ci in db.GastosContaduria where ci.Id_Corretaje == id select ci).Count();
                }
                int totalPaginas = (int)Math.Ceiling((double)(from ci in db.GastosContaduria where ci.Id_Corretaje == id select ci).Count() / 15);

                var respuesta = (from ci in db.GastosContaduria
                                 where ci.Id_Corretaje == id
                                 select new
                                 {
                                     ci.Id_Corretaje,
                                     ci.Corretaje.Crt_Direccion,
                                     ci.GstCon_Monto,
                                     ci.GstCon_Concepto,
                                     //fecha = SqlFunctions.DateName("year", ci.ci_fecha).Trim() + "/" + SqlFunctions.StringConvert((double)ci.ci_fecha.Month).TrimStart() + "/" + SqlFunctions.DateName("day", ci.ci_fecha).Trim(),
                                     nombre = (ci.Usuario.usu_nombre + " " + ci.Usuario.usu_apellidoPa),
                                     ci.Id,
                                     total = totalPaginas
                                 }).OrderBy(c => c.Id);
                return Json(respuesta.ToList(), JsonRequestBehavior.AllowGet);
            }
        }
    }
}

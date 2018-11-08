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
    public class HabilitacionsController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: Habilitacions
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
                var habilitacion = db.Habilitacion.Include(h => h.Corretaje);
                return View(habilitacion.ToList());
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
            
            
        }

        // GET: Habilitacions/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Habilitacion habilitacion = db.Habilitacion.Find(id);
                if (habilitacion == null)
                {
                    return HttpNotFound();
                }
                return View(habilitacion);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Habilitacions/Create
        public ActionResult Create()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {

                ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status");
                return View();
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }

        }

        // POST: Habilitacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Hbt_Puertas,Hbt_Chapas,Hbt_Marcos_puertas,Hbt_Bisagras,Hbt_Taza,Hbt_Lavamanos,Hbt_Bastago,Hbt_Chapeton,Hbt_Maneral,Hbt_Regadera_completa,Hbt_Kit_lavamanos,Hbt_Kit_taza,Hbt_Rosetas,Hbt_Apagador_sencillo,Hbt_Conector_sencillo,Hbt_Apagador_doble,Hbt_Conector_apagador,Hbt_Domo,Hbt_Ventanas,Hbt_Cableado,Hbt_Calibre_cableado,Hbt_Break_interior,Hbt_Break_medidor,Hbt_Pinturas,Hbt_AvisoSusp,Id_Corretaje, Hbt_ProgresoForm,Id_Usuario,Hbt_StatusCasa,Hbt_FchEntrega")] Habilitacion habilitacion)
        {
            habilitacion.Hbt_Puertas = (habilitacion.Hbt_Puertas == null) ? false : habilitacion.Hbt_Puertas;
            habilitacion.Hbt_Chapas = (habilitacion.Hbt_Chapas == null) ? false : habilitacion.Hbt_Chapas;
            habilitacion.Hbt_Marcos_puertas = (habilitacion.Hbt_Marcos_puertas == null) ? false : habilitacion.Hbt_Marcos_puertas;
            habilitacion.Hbt_Bisagras = (habilitacion.Hbt_Bisagras == null) ? false : habilitacion.Hbt_Bisagras;
            habilitacion.Hbt_Taza = (habilitacion.Hbt_Taza == null) ? false : habilitacion.Hbt_Taza;
            habilitacion.Hbt_Lavamanos = (habilitacion.Hbt_Lavamanos == null) ? false : habilitacion.Hbt_Lavamanos;
            habilitacion.Hbt_Bastago = (habilitacion.Hbt_Bastago == null) ? false : habilitacion.Hbt_Bastago;
            habilitacion.Hbt_Chapeton = (habilitacion.Hbt_Chapeton == null) ? false : habilitacion.Hbt_Chapeton;
            habilitacion.Hbt_Maneral = (habilitacion.Hbt_Maneral == null) ? false : habilitacion.Hbt_Maneral;
            habilitacion.Hbt_Regadera_completa = (habilitacion.Hbt_Regadera_completa == null) ? false : habilitacion.Hbt_Regadera_completa;
            habilitacion.Hbt_Kit_lavamanos = (habilitacion.Hbt_Kit_lavamanos == null) ? false : habilitacion.Hbt_Kit_lavamanos;
            habilitacion.Hbt_Kit_taza = (habilitacion.Hbt_Kit_taza == null) ? false : habilitacion.Hbt_Kit_taza;
            habilitacion.Hbt_Rosetas = (habilitacion.Hbt_Rosetas == null) ? false : habilitacion.Hbt_Rosetas;
            habilitacion.Hbt_Apagador_sencillo = (habilitacion.Hbt_Apagador_sencillo == null) ? false : habilitacion.Hbt_Apagador_sencillo;
            habilitacion.Hbt_Conector_sencillo = (habilitacion.Hbt_Conector_sencillo == null) ? false : habilitacion.Hbt_Conector_sencillo;
            habilitacion.Hbt_Apagador_doble = (habilitacion.Hbt_Apagador_doble == null) ? false : habilitacion.Hbt_Apagador_doble;
            habilitacion.Hbt_Conector_apagador = (habilitacion.Hbt_Conector_apagador == null) ? false : habilitacion.Hbt_Conector_apagador;
            habilitacion.Hbt_Domo = (habilitacion.Hbt_Domo == null) ? false : habilitacion.Hbt_Domo;
            habilitacion.Hbt_Ventanas = (habilitacion.Hbt_Ventanas == null) ? false : habilitacion.Hbt_Ventanas;
            habilitacion.Hbt_Cableado = (habilitacion.Hbt_Cableado == null) ? false : habilitacion.Hbt_Cableado;
            habilitacion.Hbt_Break_interior = (habilitacion.Hbt_Break_interior == null) ? false : habilitacion.Hbt_Break_interior;
            habilitacion.Hbt_Break_medidor = (habilitacion.Hbt_Break_medidor == null) ? false : habilitacion.Hbt_Break_medidor;
            habilitacion.Hbt_Pinturas = (habilitacion.Hbt_Pinturas == null) ? false : habilitacion.Hbt_Pinturas;
            habilitacion.Hbt_AvisoSusp = (habilitacion.Hbt_AvisoSusp == null) ? false : habilitacion.Hbt_AvisoSusp;

            if (ModelState.IsValid)
            {
                habilitacion.Hbt_FechaAlta = DateTime.Now;
                db.Habilitacion.Add(habilitacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", habilitacion.Id_Corretaje);
            return View(habilitacion);
        }

        // GET: Habilitacions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Habilitacion habilitacion = db.Habilitacion.Find(id);
            if (habilitacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", habilitacion.Id_Corretaje);
            return View(habilitacion);
        }

        // POST: Habilitacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Hbt_Puertas,Hbt_Chapas,Hbt_Marcos_puertas,Hbt_Bisagras,Hbt_Taza,Hbt_Lavamanos,Hbt_Bastago,Hbt_Chapeton,Hbt_Maneral,Hbt_Regadera_completa,Hbt_Kit_lavamanos,Hbt_Kit_taza,Hbt_Rosetas,Hbt_Apagador_sencillo,Hbt_Conector_sencillo,Hbt_Apagador_doble,Hbt_Conector_apagador,Hbt_Domo,Hbt_Ventanas,Hbt_Cableado,Hbt_Calibre_cableado,Hbt_Break_interior,Hbt_Break_medidor,Hbt_Pinturas,Hbt_AvisoSusp,Id_Corretaje, Hbt_ProgresoForm,Id_Usuario,Hbt_StatusCasa,Hbt_FchEntrega")] Habilitacion habilitacion)
        {
            habilitacion.Hbt_Puertas = (habilitacion.Hbt_Puertas == null) ? false : habilitacion.Hbt_Puertas;
            habilitacion.Hbt_Chapas = (habilitacion.Hbt_Chapas == null) ? false : habilitacion.Hbt_Chapas;
            habilitacion.Hbt_Marcos_puertas = (habilitacion.Hbt_Marcos_puertas == null) ? false : habilitacion.Hbt_Marcos_puertas;
            habilitacion.Hbt_Bisagras = (habilitacion.Hbt_Bisagras == null) ? false : habilitacion.Hbt_Bisagras;
            habilitacion.Hbt_Taza = (habilitacion.Hbt_Taza == null) ? false : habilitacion.Hbt_Taza;
            habilitacion.Hbt_Lavamanos = (habilitacion.Hbt_Lavamanos == null) ? false : habilitacion.Hbt_Lavamanos;
            habilitacion.Hbt_Bastago = (habilitacion.Hbt_Bastago == null) ? false : habilitacion.Hbt_Bastago;
            habilitacion.Hbt_Chapeton = (habilitacion.Hbt_Chapeton == null) ? false : habilitacion.Hbt_Chapeton;
            habilitacion.Hbt_Maneral = (habilitacion.Hbt_Maneral == null) ? false : habilitacion.Hbt_Maneral;
            habilitacion.Hbt_Regadera_completa = (habilitacion.Hbt_Regadera_completa == null) ? false : habilitacion.Hbt_Regadera_completa;
            habilitacion.Hbt_Kit_lavamanos = (habilitacion.Hbt_Kit_lavamanos == null) ? false : habilitacion.Hbt_Kit_lavamanos;
            habilitacion.Hbt_Kit_taza = (habilitacion.Hbt_Kit_taza == null) ? false : habilitacion.Hbt_Kit_taza;
            habilitacion.Hbt_Rosetas = (habilitacion.Hbt_Rosetas == null) ? false : habilitacion.Hbt_Rosetas;
            habilitacion.Hbt_Apagador_sencillo = (habilitacion.Hbt_Apagador_sencillo == null) ? false : habilitacion.Hbt_Apagador_sencillo;
            habilitacion.Hbt_Conector_sencillo = (habilitacion.Hbt_Conector_sencillo == null) ? false : habilitacion.Hbt_Conector_sencillo;
            habilitacion.Hbt_Apagador_doble = (habilitacion.Hbt_Apagador_doble == null) ? false : habilitacion.Hbt_Apagador_doble;
            habilitacion.Hbt_Conector_apagador = (habilitacion.Hbt_Conector_apagador == null) ? false : habilitacion.Hbt_Conector_apagador;
            habilitacion.Hbt_Domo = (habilitacion.Hbt_Domo == null) ? false : habilitacion.Hbt_Domo;
            habilitacion.Hbt_Ventanas = (habilitacion.Hbt_Ventanas == null) ? false : habilitacion.Hbt_Ventanas;
            habilitacion.Hbt_Cableado = (habilitacion.Hbt_Cableado == null) ? false : habilitacion.Hbt_Cableado;
            habilitacion.Hbt_Break_interior = (habilitacion.Hbt_Break_interior == null) ? false : habilitacion.Hbt_Break_interior;
            habilitacion.Hbt_Break_medidor = (habilitacion.Hbt_Break_medidor == null) ? false : habilitacion.Hbt_Break_medidor;
            habilitacion.Hbt_Pinturas = (habilitacion.Hbt_Pinturas == null) ? false : habilitacion.Hbt_Pinturas;
            habilitacion.Hbt_AvisoSusp = (habilitacion.Hbt_AvisoSusp == null) ? false : habilitacion.Hbt_AvisoSusp;

            if (ModelState.IsValid)
            {
                db.Entry(habilitacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", habilitacion.Id_Corretaje);
            return View(habilitacion);
        }

        // GET: Habilitacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Habilitacion" || Session["Tipo"].ToString() == "Administrador")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Habilitacion habilitacion = db.Habilitacion.Find(id);
                if (habilitacion == null)
                {
                    return HttpNotFound();
                }
                return View(habilitacion);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // POST: Habilitacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Habilitacion habilitacion = db.Habilitacion.Find(id);
            db.Habilitacion.Remove(habilitacion);
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

        //Método para crear automaticamente un registro vacío cuando una casa se da de alta, para que solo se edite
        [HttpPost]
        public string CrearHabilitacion(Habilitacion habilitacion, int corretaje_id)
        {
            CasasRedEntities CS = new CasasRedEntities();
            Habilitacion hab_obj = new Habilitacion
            {

                Hbt_Apagador_doble = false,
                Hbt_Apagador_sencillo = false,
                Hbt_AvisoSusp = false,
                Hbt_Bastago = false,
                Hbt_Bisagras = false,
                Hbt_Break_interior = false,
                Hbt_Break_medidor = false,
                Hbt_Chapas = false,
                Hbt_Chapeton = false,
                Hbt_Conector_apagador = false,
                Hbt_Conector_sencillo = false,
                Hbt_Domo = false,
                Hbt_Kit_lavamanos = false,
                Hbt_Kit_taza = false,
                Hbt_Lavamanos = false,
                Hbt_Maneral = false,
                Hbt_Marcos_puertas = false,
                Hbt_Pinturas = false,
                Hbt_Puertas = false,
                Hbt_Regadera_completa = false,
                Hbt_Rosetas = false,
                Hbt_Taza = false,
                Hbt_Ventanas = false,
                Id_Corretaje = corretaje_id,
                Hbt_ProgresoForm = 0,
                Hbt_Cableado = false,
                Hbt_FechaAlta = DateTime.Now,
            };

            CS.Habilitacion.Add(hab_obj);
            CS.SaveChanges();

            //FotosHabilitacion obj_hab = new FotosHabilitacion
            //{
            //    fh_archivo = null,
            //    fh_nombre=null,
            //    fh_habilitacion=hab_obj.Id
            //};
            
            //CS.FotosHabilitacion.Add(obj_hab);
            //CS.SaveChanges();

            return "Esto es un string, repito, es un string...";
        }

        public JsonResult BuscarHabilitacion(string filtro = "", int pagina =1, int registrosPagina = 15, int mes = 0 , int ano = 0)
        {
            if (filtro == "" && mes == 0 && ano == 0)
            {
                int totalPaginas = (int)Math.Ceiling((double)db.Habilitacion.Count() / registrosPagina);
                var busqueda = (from a in db.Habilitacion select new { a.Id, fecha = (a.Id_Corretaje != null) ? a.Corretaje.Crt_FechaAlta.ToString() : "--", direccion = (a.Id_Corretaje != null) ? a.Corretaje.Crt_Direccion : "No asignada", vendedor = (a.Id_Corretaje != null) ? (a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat) : "No asingado", a.Hbt_ProgresoForm, corretaje = (a.Id_Corretaje != null) ? a.Corretaje.Id : 0, total = totalPaginas }).OrderBy(a => a.direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            //solo filtro
            else if (filtro != "" && mes == 0 && ano == 0)
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Habilitacion where (a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat).Contains(filtro) || a.Corretaje.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Habilitacion where (a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat).Contains(filtro) || a.Corretaje.Crt_Direccion.Contains(filtro) select new { a.Id, fecha = (a.Id_Corretaje != null) ? a.Corretaje.Crt_FechaAlta.ToString() : "--", direccion = (a.Id_Corretaje != null) ? a.Corretaje.Crt_Direccion : "No asignada", vendedor = (a.Id_Corretaje != null) ? (a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat) : "No asingado", a.Hbt_ProgresoForm, corretaje = (a.Id_Corretaje != null) ? a.Corretaje.Id : 0, total = totalPaginas }).OrderBy(a => a.direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            //solo mes sin año y sin filtro
            else if (mes != 0 && filtro == "" && ano == 0)
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Habilitacion where (a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat).Contains(filtro) || a.Corretaje.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Habilitacion where a.Hbt_FechaAlta.Value.Month.Equals(mes) select new { a.Id, fecha = (a.Id_Corretaje != null) ? a.Corretaje.Crt_FechaAlta.ToString() : "--", direccion = (a.Id_Corretaje != null) ? a.Corretaje.Crt_Direccion : "No asignada", vendedor = (a.Id_Corretaje != null) ? (a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat) : "No asingado", a.Hbt_ProgresoForm, corretaje = (a.Id_Corretaje != null) ? a.Corretaje.Id : 0, total = totalPaginas }).OrderBy(a => a.direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            //solo ano sin mes y sin filtro
            else if (ano != 0 && filtro == "" && mes == 0)
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Habilitacion where (a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat).Contains(filtro) || a.Corretaje.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Habilitacion where a.Hbt_FechaAlta.Value.Year.Equals(ano) select new { a.Id, fecha = (a.Id_Corretaje != null) ? a.Corretaje.Crt_FechaAlta.ToString() : "--", direccion = (a.Id_Corretaje != null) ? a.Corretaje.Crt_Direccion : "No asignada", vendedor = (a.Id_Corretaje != null) ? (a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat) : "No asingado", a.Hbt_ProgresoForm, corretaje = (a.Id_Corretaje != null) ? a.Corretaje.Id : 0, total = totalPaginas }).OrderBy(a => a.direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }

            //ano y filtro sin mes 
            else if (ano != 0 && filtro != "" && mes == 0)
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Habilitacion where (a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat).Contains(filtro) || a.Corretaje.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Habilitacion where ((a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat).Contains(filtro) || a.Corretaje.Crt_Direccion.Contains(filtro)) && a.Hbt_FechaAlta.Value.Year.Equals(ano) select new { a.Id, fecha = (a.Id_Corretaje != null) ? a.Corretaje.Crt_FechaAlta.ToString() : "--", direccion = (a.Id_Corretaje != null) ? a.Corretaje.Crt_Direccion : "No asignada", vendedor = (a.Id_Corretaje != null) ? (a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat) : "No asingado", a.Hbt_ProgresoForm, corretaje = (a.Id_Corretaje != null) ? a.Corretaje.Id : 0, total = totalPaginas }).OrderBy(a => a.direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            //mes y filtro sin a;o
            else if (filtro != "" && mes != 0 && ano == 0)
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Habilitacion where (a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat).Contains(filtro) || a.Corretaje.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Habilitacion where ((a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat).Contains(filtro) || a.Corretaje.Crt_Direccion.Contains(filtro)) && a.Hbt_FechaAlta.Value.Month.Equals(mes) select new { a.Id, fecha = (a.Id_Corretaje != null) ? a.Corretaje.Crt_FechaAlta.ToString() : "--", direccion = (a.Id_Corretaje != null) ? a.Corretaje.Crt_Direccion : "No asignada", vendedor = (a.Id_Corretaje != null) ? (a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat) : "No asingado", a.Hbt_ProgresoForm, corretaje = (a.Id_Corretaje != null) ? a.Corretaje.Id : 0, total = totalPaginas }).OrderBy(a => a.direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Habilitacion where (a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat).Contains(filtro) || a.Corretaje.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Habilitacion where ((a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat).Contains(filtro) || a.Corretaje.Crt_Direccion.Contains(filtro)) && a.Hbt_FechaAlta.Value.Month.Equals(mes) && a.Hbt_FechaAlta.Value.Year.Equals(ano) select new { a.Id, fecha = (a.Id_Corretaje != null) ? a.Corretaje.Crt_FechaAlta.ToString() : "--", direccion = (a.Id_Corretaje != null) ? a.Corretaje.Crt_Direccion : "No asignada", vendedor = (a.Id_Corretaje != null) ? (a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat) : "No asingado", a.Hbt_ProgresoForm, corretaje = (a.Id_Corretaje != null) ? a.Corretaje.Id : 0, total = totalPaginas }).OrderBy(a => a.direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

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
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Administrador")
            {
                var contaduria = db.Contaduria.Include(c => c.Corretaje);
                return View(contaduria.ToList());
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
            
        }

        // GET: Contadurias/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Administrador")
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
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Contadurias/Create
        public ActionResult Create()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Administrador")
            {
                //ViewBag.Id_Gastos = new SelectList(db.Gastos, "Id", "Gst_Concepto");
                ViewBag.Id_GastosContaduria = new SelectList(db.GastosContaduria, "Id", "Id");
                return View();
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // POST: Contadurias/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Cnt_Presupuesto_gestion,Cnt_Presupuesto_corretaje,Cnt_Presupuesto_habilitacion,Cnt_Presupuesto,Id_Corretaje,Id_Usuario,Cnt_DevMensualidad")] Contaduria contaduria)
        {
            if (ModelState.IsValid)
            {
                db.Contaduria.Add(contaduria);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.Id_Gastos = new SelectList(db.Gastos, "Id", "Gst_Concepto", contaduria.Id_Gastos);
            //ViewBag.Id_GastosContaduria = new SelectList(db.GastosContaduria, "Id", "Id", contaduria.Id_GastosContaduria);
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
            //ViewBag.Id_Gastos = new SelectList(db.Gastos, "Id", "Gst_Concepto", contaduria.Id_Gastos);
            //ViewBag.Id_GastosContaduria = new SelectList(db.GastosContaduria, "Id", "Id", contaduria.Id_GastosContaduria);
            return View(contaduria);
        }

        // POST: Contadurias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Cnt_Presupuesto_gestion,Cnt_Presupuesto_corretaje,Cnt_Presupuesto_habilitacion,Cnt_Presupuesto,Id_Corretaje,Id_Usuario,Cnt_DevMensualidad")] Contaduria contaduria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contaduria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.Id_Gastos = new SelectList(db.Gastos, "Id", "Gst_Concepto", contaduria.Id_Gastos);
            //ViewBag.Id_GastosContaduria = new SelectList(db.GastosContaduria, "Id", "Id", contaduria.Id_GastosContaduria);
            return View(contaduria);
        }

        // GET: Contadurias/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Contabilidad" || Session["Tipo"].ToString() == "Administrador")
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
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
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
                Cnt_M_Preguntar = 0,
                Cnt_Material = 0,
                Cnt_Tramites = 0,
                Cnt_Vigilancia = 0,
                Cnt_CESPT = 0,
                Cnt_CFE = 0,
                Cnt_DevMensualidad = 0,
                Id_Corretaje = corretaje_id //Para saber a que casa esta asociado el gasto
            };

            CS.Contaduria.Add(contaduria_obj);
            CS.SaveChanges();

            return "Si funciona!...";
        }

        public JsonResult BuscarConduria(string filtro = "", int pagina = 1, int registrosPagina = 15)
        {
            if (filtro == "")
            {
                int totalPaginas = (int)Math.Ceiling((double)db.Contaduria.Count() / registrosPagina);
                var busqueda = (from a in db.Contaduria join v in db.VendedorAsig on a.Id_Corretaje equals v.Id_Corretaje into c from algo in c.DefaultIfEmpty() join ve in db.Vendedor on algo.Id_Vendedor equals ve.Id into ce from algoo in ce.DefaultIfEmpty() select new { idven = ((algo.Id_Vendedor !=null)? algo.Id_Vendedor : 0),vendenombre = ((algoo.Vndr_Nombre!=null)? algoo.Vndr_Nombre: "Sin asignar" ),vendeapp = ((algoo.Vndr_Apellidopa != null) ? algoo.Vndr_Apellidopa : "Sin asignar"),vendeapm = ((algoo.Vndr_Apellidoma != null) ? algoo.Vndr_Apellidoma : " "), a.Id, direccion = ((a.Id_Corretaje != null)? a.Corretaje.Crt_Direccion : "Sin asignar"), vendedor = ((a.Id_Corretaje != null)?(a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat): "Sin asignar"), estatus = ((a.Id_Corretaje != null)? a.Corretaje.Crt_Status : "--"), corretaje = ((a.Id_Corretaje != null)?a.Corretaje.Id : 0), total = totalPaginas }).OrderBy(a => a.direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
               // var busqueda = (from a in db.Contaduria select new { a.Id, direccion = ((a.Id_Corretaje != null) ? a.Corretaje.Crt_Direccion : "Sin asignar"), vendedor = ((a.Id_Corretaje != null) ? (a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat) : "Sin asignar"), estatus = ((a.Id_Corretaje != null) ? a.Corretaje.Crt_Status : "--"), corretaje = ((a.Id_Corretaje != null) ? a.Corretaje.Id : 0), total = totalPaginas }).OrderBy(a => a.direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();

                return Json(busqueda, JsonRequestBehavior.AllowGet);

            }
            else
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Contaduria where (a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat).Contains(filtro) || a.Corretaje.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Contaduria where (a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat).Contains(filtro) || a.Corretaje.Crt_Direccion.Contains(filtro) select new { a.Id, direccion = ((a.Id_Corretaje != null) ? a.Corretaje.Crt_Direccion : "Sin asignar"), vendedor = ((a.Id_Corretaje != null) ? (a.Corretaje.Crt_Cliente_Nombre + " " + a.Corretaje.Crt_Cliente_ApPat + " " + a.Corretaje.Crt_Cliente_ApMat) : "Sin asignar"), estatus = ((a.Id_Corretaje != null) ? a.Corretaje.Crt_Status : "--"), corretaje = ((a.Id_Corretaje != null) ? a.Corretaje.Id : 0), total = totalPaginas }).OrderBy(a => a.direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

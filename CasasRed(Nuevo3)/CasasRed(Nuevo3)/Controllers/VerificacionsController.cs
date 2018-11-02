using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CasasRed_Nuevo3_.Models;
//El bueno que va a bitbuket
namespace CasasRed_Nuevo3_.Controllers
{
    public class VerificacionsController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: Verificacions
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Verificacion" || Session["Tipo"].ToString() == "Administrador")
            {
                var verificacion = db.Verificacion.Include(v => v.Cliente);
                return View(verificacion.ToList());
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Verificacions/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Verificacion" || Session["Tipo"].ToString() == "Administrador")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Verificacion verificacion = db.Verificacion.Find(id);
                if (verificacion == null)
                {
                    return HttpNotFound();
                }
                return View(verificacion);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Verificacions/Create
        public ActionResult Create()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Verificacion" || Session["Tipo"].ToString() == "Administrador")
            {
                //Selectlist calificacion vendedor
                var calificacion = new SelectList(new []
                {
                    new {value = 0, text = "Seleccione una opción...."},
                    new {value = 1, text= "1"},
                    new {value = 2, text= "2"},
                    new {value = 3, text= "3"},
                    new {value = 4, text= "4"},
                    new {value = 5, text= "5"},
                    new {value = 6, text= "6"},
                    new {value = 7, text= "7"},
                    new {value = 8, text= "8"},
                    new {value = 9, text= "9"},
                    new {value = 10, text= "10"}
                }, "value", "text", 0);

                ViewData["Calificacion"] = calificacion;

                ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Gral_Nombre");
                return View();
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // POST: Verificacions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Vfn_Persona_fisica,Vfn_Visto_persona,Vfn_Tiempo_estimado,Vfn_Tiempo,Vfn_Tiene_costo,Vfn_Costo,Vfn_Trato_asesor,Vfn_Observaciones,Id_Cliente,Vfn_ProgresoForm,Id_Usuario")] Verificacion verificacion)
        {
            Cliente cliente = new Cliente();

            //Selectlist calificacion vendedor
            var calificacion = new SelectList(new[]
            {
                    new {value = 0, text = "Seleccione una opción...."},
                    new {value = 1, text= "1"},
                    new {value = 2, text= "2"},
                    new {value = 3, text= "3"},
                    new {value = 4, text= "4"},
                    new {value = 5, text= "5"},
                    new {value = 6, text= "6"},
                    new {value = 7, text= "7"},
                    new {value = 8, text= "8"},
                    new {value = 9, text= "9"},
                    new {value = 10, text= "10"}
                }, "value", "text", 0);

            if (ModelState.IsValid)
            {
                //Usuario que modifico
                verificacion.Id_Usuario = int.Parse(Session["UsuarioID"].ToString());

                verificacion.Vfn_Costo = ((verificacion.Vfn_Costo == null) ? 0 : verificacion.Vfn_Costo);
                verificacion.Vfn_Observaciones = ((verificacion.Vfn_Observaciones == null) ? "" : verificacion.Vfn_Observaciones);
                verificacion.Vfn_Persona_fisica = ((verificacion.Vfn_Persona_fisica == null) ? false : verificacion.Vfn_Persona_fisica);
                verificacion.Vfn_Tiempo = ((verificacion.Vfn_Tiempo == null) ? "" : verificacion.Vfn_Tiempo);
                verificacion.Vfn_Tiempo_estimado = ((verificacion.Vfn_Tiempo_estimado == null) ? false : verificacion.Vfn_Tiempo_estimado);
                verificacion.Vfn_Tiene_costo = ((verificacion.Vfn_Tiene_costo == null) ? false : verificacion.Vfn_Tiene_costo);
                verificacion.Vfn_Visto_persona = ((verificacion.Vfn_Visto_persona == null) ? false : verificacion.Vfn_Visto_persona);
                verificacion.Vfn_Trato_asesor = ((verificacion.Vfn_Trato_asesor == null) ? 0 : verificacion.Vfn_Trato_asesor);

                verificacion.Vfn_FechaAlta = DateTime.Now;

                db.Verificacion.Add(verificacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewData["Calificacion"] = calificacion;
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Gral_Nombre", verificacion.Id_Cliente);
            return View(verificacion);
        }

        // GET: Verificacions/Edit/5
        public ActionResult Edit(int? id)
        {
            //Linq para mostrar asesor asignado
            var busqueda = (from a in db.Verificacion join v in db.VendedorAsig on a.Cliente.Id_Corretaje equals v.Id_Corretaje into c from algo in c.DefaultIfEmpty() join ve in db.Vendedor on algo.Id_Vendedor equals ve.Id into ce from algoo in ce.DefaultIfEmpty() select new { a.Id, cliente = (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma), idven = ((algo.Id_Vendedor != null) ? algo.Id_Vendedor : 0), vendenombre = ((algoo.Vndr_Nombre != null) ? algoo.Vndr_Nombre : null), vendeapp = ((algoo.Vndr_Apellidopa != null) ? algoo.Vndr_Apellidopa : null), vendeapm = ((algoo.Vndr_Apellidoma != null) ? algoo.Vndr_Apellidoma : " ") }).ToList();

            foreach (var item in busqueda)
            {
                if (item.vendenombre != null)
                { 
                    if (item.Id == id)
                    {
                        ViewBag.Vendedor += item.vendenombre + " " + item.vendeapp + " " + item.vendeapm + "," + " ";
                    }
                }
            }

            //Selectlist calificacion vendedor
            var calificacion = new SelectList(new[]
            {
                    new {value = 0, text = "Seleccione una opción...."},
                    new {value = 1, text= "1"},
                    new {value = 2, text= "2"},
                    new {value = 3, text= "3"},
                    new {value = 4, text= "4"},
                    new {value = 5, text= "5"},
                    new {value = 6, text= "6"},
                    new {value = 7, text= "7"},
                    new {value = 8, text= "8"},
                    new {value = 9, text= "9"},
                    new {value = 10, text= "10"}
                }, "value", "text", 0);

            ViewData["Calificacion"] = calificacion;

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Verificacion verificacion = db.Verificacion.Find(id);
            if (verificacion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Gral_Nombre", verificacion.Id_Cliente);
            return View(verificacion);
        }

        // POST: Verificacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Vfn_Persona_fisica,Vfn_Visto_persona,Vfn_Tiempo_estimado,Vfn_Tiempo,Vfn_Tiene_costo,Vfn_Costo,Vfn_Trato_asesor,Vfn_Observaciones,Id_Cliente,Vfn_ProgresoForm,Id_Usuario")] Verificacion verificacion)
        {
            int puntaje, vendedor_id;
            CalificacionVendedor calificacionVendedor = new CalificacionVendedor();
            CalificacionVendedorsController calificacionVendedorsController;

            //Vendedores
            var busqueda = (from a in db.Verificacion join v in db.VendedorAsig on a.Cliente.Id_Corretaje equals v.Id_Corretaje into c from algo in c.DefaultIfEmpty() join ve in db.Vendedor on algo.Id_Vendedor equals ve.Id into ce from algoo in ce.DefaultIfEmpty() select new { a.Id, cliente = (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma), idven = ((algo.Id_Vendedor != null) ? algo.Id_Vendedor : 0), vendenombre = ((algoo.Vndr_Nombre != null) ? algoo.Vndr_Nombre : null), vendeapp = ((algoo.Vndr_Apellidopa != null) ? algoo.Vndr_Apellidopa : null), vendeapm = ((algoo.Vndr_Apellidoma != null) ? algoo.Vndr_Apellidoma : " "), casa = a.Cliente.Id_Corretaje }).ToList();
           
            if (verificacion.Vfn_Persona_fisica == null) verificacion.Vfn_Persona_fisica = false;
            if (verificacion.Vfn_Visto_persona == null) verificacion.Vfn_Visto_persona = false;
            if (verificacion.Vfn_Tiempo_estimado == null) verificacion.Vfn_Tiempo_estimado = false;
            if (verificacion.Vfn_Tiene_costo == null) verificacion.Vfn_Tiene_costo = false;
            //if (verificacion.Vfn_Trato_asesor == null) verificacion.Vfn_Trato_asesor = 0;


            if (ModelState.IsValid)
            {
                db.Entry(verificacion).State = EntityState.Modified;
                db.SaveChanges();

                //var corretaje_id = from a in db.Verificacion where a.Id_Cliente == a.Cliente.Id select new {corretaje = a.Cliente.Id_Corretaje };
                if (verificacion.Vfn_Trato_asesor.HasValue)
                {
                    puntaje = verificacion.Vfn_Trato_asesor.Value;

                    //Seleccionar vendedores asignados al cliente 
                    foreach (var item in busqueda)
                    {
                        if (item.vendenombre != null)
                        {
                            if (item.Id == verificacion.Id)
                            {
                                vendedor_id = item.idven.Value;
                                calificacionVendedorsController = new CalificacionVendedorsController();
                                calificacionVendedorsController.CalificacionVendedor(item.casa.Value, puntaje, vendedor_id);
                            }
                        }
                    }
                }

                return RedirectToAction("Index");
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Gral_Nombre", verificacion.Id_Cliente);
            return View(verificacion);
        }

        // GET: Verificacions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Verificacion" || Session["Tipo"].ToString() == "Administrador")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Verificacion verificacion = db.Verificacion.Find(id);
                if (verificacion == null)
                {
                    return HttpNotFound();
                }
                return View(verificacion);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // POST: Verificacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Verificacion verificacion = db.Verificacion.Find(id);
            db.Verificacion.Remove(verificacion);
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

        [HttpPost]
        public string VerfificacionCreate(Verificacion verificacion, int cliente_id)
        {
            CasasRedEntities CS = new CasasRedEntities();
            Verificacion verificacion_obj = new Verificacion
            {
                Vfn_Persona_fisica = false,
                Vfn_Visto_persona = false,
                Vfn_Tiempo_estimado = false,
                Vfn_Tiene_costo = false,
                Id_Cliente = cliente_id,
                Vfn_ProgresoForm = 0,
                Vfn_Trato_asesor = 0,
                Vfn_Costo = 0,
                Vfn_FechaAlta = DateTime.Now,
            };

            CS.Verificacion.Add(verificacion_obj);
            CS.SaveChanges();

            return "String si se pudo...";
        }

        public JsonResult BuscarVerificaciones(string filtro = "", int pagina =1, int registrosPagina = 15)
        {
            if (filtro == "")
            {
                int totalPaginas = (int)Math.Ceiling((double)db.Verificacion.Count() / registrosPagina);
                //var busqueda = (from a in db.Verificacion select new { a.Id, cliente = (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma), asesor = (a.Cliente.Corretaje.Vendedor.Vndr_Nombre + " " + a.Cliente.Corretaje.Vendedor.Vndr_Apellidopa + " " + a.Cliente.Corretaje.Vendedor.Vndr_Apellidoma), a.Vfn_ProgresoForm, total = totalPaginas }).OrderBy(a => a.cliente).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                var busqueda = (from a in db.Verificacion join v in db.VendedorAsig on a.Cliente.Id_Corretaje equals v.Id_Corretaje into c from algo in c.DefaultIfEmpty() join ve in db.Vendedor on algo.Id_Vendedor equals ve.Id into ce from algoo in ce.DefaultIfEmpty() select new { a.Id, cliente = (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma), idven = ((algo.Id_Vendedor != null) ? algo.Id_Vendedor : 0), vendenombre = ((algoo.Vndr_Nombre != null) ? algoo.Vndr_Nombre : "Sin asignar"), vendeapp = ((algoo.Vndr_Apellidopa != null) ? algoo.Vndr_Apellidopa : "Sin asignar"), vendeapm = ((algoo.Vndr_Apellidoma != null) ? algoo.Vndr_Apellidoma : " "), a.Vfn_ProgresoForm, total = totalPaginas }).OrderBy(a => a.cliente).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();

                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Verificacion where (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma).Contains(filtro) || (a.Cliente.Vendedor.Vndr_Nombre + " " + a.Cliente.Vendedor.Vndr_Apellidopa + " " + a.Cliente.Vendedor.Vndr_Apellidoma).Contains(filtro) select a).Count() / registrosPagina);
                //var busqueda = (from a in db.Verificacion where (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma).Contains(filtro) || (a.Cliente.Vendedor.Vndr_Nombre + " " + a.Cliente.Vendedor.Vndr_Apellidopa + " " + a.Cliente.Vendedor.Vndr_Apellidoma).Contains(filtro) select new { a.Id, cliente = (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma), asesor = (a.Cliente.Vendedor.Vndr_Nombre + " " + a.Cliente.Vendedor.Vndr_Apellidopa + " " + a.Cliente.Vendedor.Vndr_Apellidoma), a.Vfn_ProgresoForm, total = totalPaginas }).OrderBy(a => a.cliente).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                var busqueda = (from a in db.Verificacion join v in db.VendedorAsig on a.Cliente.Id_Corretaje equals v.Id_Corretaje into c from algo in c.DefaultIfEmpty() join ve in db.Vendedor on algo.Id_Vendedor equals ve.Id into ce from algoo in ce.DefaultIfEmpty() select new { a.Id, cliente = (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma), idven = ((algo.Id_Vendedor != null) ? algo.Id_Vendedor : 0), vendenombre = ((algoo.Vndr_Nombre != null) ? algoo.Vndr_Nombre : "Sin asignar"), vendeapp = ((algoo.Vndr_Apellidopa != null) ? algoo.Vndr_Apellidopa : "Sin asignar"), vendeapm = ((algoo.Vndr_Apellidoma != null) ? algoo.Vndr_Apellidoma : " "), a.Vfn_ProgresoForm, total = totalPaginas }).OrderBy(a => a.cliente).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

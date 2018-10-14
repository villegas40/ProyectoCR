using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CasasRed_Nuevo3_.Models;
//El bueno que se subira a Github y Bitbucket
namespace CasasRed_Nuevo3_.Controllers
{
    public class ClientesController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: Clientes
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "ApoyoGestion" || Session["Tipo"].ToString() == "Gestion" || Session["Tipo"].ToString() == "Administrador")
            {
                var cliente = db.Cliente.Include(c => c.Corretaje).Include(c => c.Verificacion).Include(c => c.Vendedor);
                return View(cliente.ToList());
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }

        }

        // GET: Clientes/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "ApoyoGestion" || Session["Tipo"].ToString() == "Gestion" || Session["Tipo"].ToString() == "Administrador")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cliente cliente = db.Cliente.Find(id);
                if (cliente == null)
                {
                    return HttpNotFound();
                }
                return View(cliente);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Clientes/Create
        public ActionResult Create()
        {
            //Select List de las casas
            //var listaCasas = db.Corretaje.ToList();
            //SelectList listItems = new SelectList(listaCasas, "Id");

            //ViewData["Posicion"] = listaCasas;
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "ApoyoGestion" || Session["Tipo"].ToString() == "Gestion" || Session["Tipo"].ToString() == "Administrador")
            {
                ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Direccion");

                var estadocivil = new SelectList(new[] {
                    new {value = "No seleccionado", text = "Seleccione una opción..."},
                    new {value = "Soltero", text = "Soltero"},
                    new {value = "Casado", text = "Casado"},
                    new {value = "Divorsiado", text = "Divorsiado"},
                    new { value = "Viudo", text = "Viudo" }
                }, "value", "text", 0);

                ViewData["Posicion"] = ViewBag.Id_Corretaje;

                ViewBag.Id_Vendedor = new SelectList(db.Vendedor, "Id", "Vndr_Nombre");
                ViewData["Posicion2"] = ViewBag.Id_Vendedor;

                ViewData["EstadoCivil"] = estadocivil;
                return View();
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        public string CrearFolio(int longitud)
        {
            string caracteres = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < longitud--)
            {
                res.Append(caracteres[rnd.Next(caracteres.Length)]);
            }
            return res.ToString();
        }

        public string ValidarFolioDuplicado()
        {
            var folio = CrearFolio(10);
            List<Cliente> clientes = new List<Cliente>();
            clientes = db.Cliente.ToList();

            foreach (var item in clientes)
            {
                if (item.Grlal_Folio == folio)
                {
                    ValidarFolioDuplicado();
                }
            }
            return folio;
        }
    
        // POST: Clientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Gral_Nombre,Gral_Apellidopa,Gral_Apellidoma,Gral_Fechanac,Gral_Nss,Gral_Curp,Gral_Rfc,Gral_Lugarnac,Gral_Calle,Gral_Numero,Gral_Cp,Gral_Colonia,Gral_Municipio,Gral_Estado,Gral_Celular,Gral_Tel_casa,Gral_Estado_civil,Gral_Regimen_matrimonial,Gral_Ocupacion,Gral_Teltrabajo,Gral_Correo,Gral_Identificacion,Gral_No_identificacion,Gral_Ref_nombre1,Gral_Ref_cel_1,Gral_Ref_nombre2,Gral_Ref_cel_2,Cyg_Nombre,Cyg_Apellidopa,Cyg_Apellidoma,Gyg_Fechanac,Cyg_Nss,Cyg_Curp,Cyg_Rfc,Cyg_Lugarnac,Cyg_Celular,Cyg_Tel_casa,Cyg_Ocupacion,Cyg_Tel_trabajo,Cyg_Correo,Cyg_Identificacion,Cyg_No_identificacoion,Gral_Fechaalta,Id_Corretaje,Gral_ProgresoForm,Grlal_Folio,Id_Vendedor,Id_Usuario")] Cliente cliente)
        {
            int cliente_id;
            int corretaje_id;
            string telefono, correo;

            //Obtener los correos de los usuarios de hablitacion y contaduria
            var usuarios = (from usu in db.Usuario where usu.usu_tipo == "2" || usu.usu_tipo == "6" select new { usu.usu_correo }).ToArray();

            var gestion_controller = new GestionsController();
            var gestion = new Gestion();
            
            var verificacion_controller = new VerificacionsController();
            var verificacion = new Verificacion();
            var foliogenerado = ValidarFolioDuplicado();

            //SMS y Correo
            var sms = new SmsController();
            var correo_controlador = new CorreoController();

            var estadocivil = new SelectList(new[] {
                    new {value = "No seleccionado", text = "Seleccione una opción..."},
                    new {value = "Soltero", text = "Soltero"},
                    new {value = "Casado", text = "Casado"},
                    new {value = "Divorsiado", text = "Divorsiado"},
                    new { value = "Viudo", text = "Viudo" }
                }, "value", "text", 0);

            if (ModelState.IsValid)
            {
                cliente.Gral_Fechaalta = DateTime.Now;
                cliente.Grlal_Folio = foliogenerado;
                //Guardar Usuario que creo el registro
                cliente.Id_Usuario = int.Parse(Session["UsuarioID"].ToString());

                db.Cliente.Add(cliente);
                db.SaveChanges();

                //Mandar a llamar el método para crear un formulario vacío
                cliente_id = cliente.Id;
                corretaje_id = cliente.Id_Corretaje.Value; //Preguntar si lo dejo así o corretaje_id = cliente.Id_Corretaje.HasValue ? cliente.Id_Corretaje.Value:0
                //telefono = cliente.Gral_Celular.ToString();
                correo = cliente.Gral_Correo;
                if (cliente.Id_Corretaje != null)
                {
                Corretaje cr = db.Corretaje.Find(cliente.Id_Corretaje);
                cr.Crt_Status = "Venta";
                    db.SaveChanges();
                }

                verificacion_controller.VerfificacionCreate(verificacion, cliente_id);

               
                gestion_controller.GestionCrear(gestion, cliente_id, corretaje_id);

                //Si el cliente es eliminado esto hace que se le asigne un nuevo cliente y aparezca en gestion
                //Gestion gs = db.Gestion.Find(cliente.Id_Corretaje); <---- Ver  despues
                //gs.Id_Cliente = cliente.Id;
                //db.Enrty(gs).State = EntityState.Modified
                //db.SaveChanges();

                //sms.SendSms(telefono); Comentado porque gasta dinero
                if (cliente.Gral_Correo != null)
                {
                    correo_controlador.sendmail(correo);
                }

                //Enviar correo de alta de casa a los demás departamentos
                foreach (var item in usuarios)
                {
                    if (item != null)
                    {
                        correo_controlador.sendMailGestion(item.usu_correo);
                    }
                }

                return RedirectToAction("Index");
            }

            ViewData["EstadoCivil"] = estadocivil;
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Direccion", cliente.Id_Corretaje);
            ViewData["Posicion"] = ViewBag.Id_Corretaje;

            ViewBag.Id_Vendedor = new SelectList(db.Vendedor, "Id", "Vndr_Nombre", cliente.Id_Vendedor);
            ViewData["Posicion2"] = ViewBag.Id_Vendedor;

            return View(cliente);
        }

        // GET: Clientes/Edit/5
        public ActionResult Edit(int? id)
        {
            //Obteber las casas disponibles para 
            var casas = (from c in db.Corretaje where c.Crt_Status == "Disponible" select new { c.Id, c.Crt_Direccion }).ToList();

            var dictionary = new Dictionary<int, string>();

            dictionary.Add(0, "No asignado");
            foreach (var item in casas)
            {
                dictionary.Add(item.Id, item.Crt_Direccion);
            }

            ViewBag.SelectList = new SelectList(dictionary, "Key", "Value", 0);


            var estadocivil = new SelectList(new[] {
                    new {value = "No seleccionado", text = "Seleccione una opción..."},
                    new {value = "Soltero", text = "Soltero"},
                    new {value = "Casado", text = "Casado"},
                    new {value = "Divorsiado", text = "Divorsiado"},
                    new { value = "Viudo", text = "Viudo" }
                }, "value", "text", 0);

           

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return HttpNotFound();
            }

            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Direccion", cliente.Id_Corretaje);

            ViewBag.Id_Vendedor = new SelectList(db.Vendedor, "Id", "Vndr_Nombre", cliente.Id_Vendedor);
            ViewData["Posicion2"] = ViewBag.Id_Vendedor;
            ViewData["EstadoCivil"] = estadocivil;
            ViewData["Posicion3"] = ViewBag.Id_Corretaje;
            return View(cliente);
        }

        // POST: Clientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Gral_Nombre,Gral_Apellidopa,Gral_Apellidoma,Gral_Fechanac,Gral_Nss,Gral_Curp,Gral_Rfc,Gral_Lugarnac,Gral_Calle,Gral_Numero,Gral_Cp,Gral_Colonia,Gral_Municipio,Gral_Estado,Gral_Celular,Gral_Tel_casa,Gral_Estado_civil,Gral_Regimen_matrimonial,Gral_Ocupacion,Gral_Teltrabajo,Gral_Correo,Gral_Identificacion,Gral_No_identificacion,Gral_Ref_nombre1,Gral_Ref_cel_1,Gral_Ref_nombre2,Gral_Ref_cel_2,Cyg_Nombre,Cyg_Apellidopa,Cyg_Apellidoma,Gyg_Fechanac,Cyg_Nss,Cyg_Curp,Cyg_Rfc,Cyg_Lugarnac,Cyg_Celular,Cyg_Tel_casa,Cyg_Ocupacion,Cyg_Tel_trabajo,Cyg_Correo,Cyg_Identificacion,Cyg_No_identificacoion,Gral_Fechaalta,Id_Corretaje,Gral_ProgresoForm,Id_Usuario, Grlal_Folio,Id_Vendedor")] Cliente cliente)
        {
            //SelectList para seleccionar casa
            //List<SelectListItem> idCasa = new List<SelectListItem>();
            //var casas = (from c in db.Corretaje where c.Crt_Status == "Disponible" select new { c.Id, c.Crt_Direccion }).ToList();
            //idCasa.Add(new SelectListItem { Text = "Seleccione una opción...", Value = "" });
            //foreach (var item in casas)
            //{
            //    idCasa.Add(new SelectListItem { Text = item.Crt_Direccion, Value = item.Id.ToString() });
            //}
            //var select = new SelectList(idCasa, "value", "text", 0);
            //ViewData["Casa"] = select;

            var estadocivil = new SelectList(new[] {
                    new {value = "No seleccionado", text = "Seleccione una opción..."},
                    new {value = "Soltero", text = "Soltero"},
                    new {value = "Casado", text = "Casado"},
                    new {value = "Divorsiado", text = "Divorsiado"},
                    new { value = "Viudo", text = "Viudo" }
                }, "value", "text", 0);

            //Que guarde nulo en el campo de corretaje
            if (cliente.Id_Corretaje != null)
            {
                cliente.Id_Corretaje = (cliente.Id_Corretaje == 0) ? null : cliente.Id_Corretaje;
            }
             

            if (ModelState.IsValid)
            {
                db.Entry(cliente).State = EntityState.Modified;
                db.SaveChanges();

                if (cliente.Id_Corretaje != null)
                {
                    Corretaje cr = db.Corretaje.Find(cliente.Id_Corretaje);
                    cr.Crt_Status = "Venta";
                    db.SaveChanges();
                    //Si el cliente es eliminado esto hace que se le asigne un nuevo cliente y aparezca en gestion
                    //Gestion gs = db.Gestion.Find(cliente.Id_Corretaje); <---- Ver  despues
                    //gs.Id_Cliente = cliente.Id;
                    //db.Enrty(gs).State = EntityState.Modified
                    //db.SaveChanges();

                }

                return RedirectToAction("Index");
            }

            

            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Direccion", cliente.Id_Corretaje);

            ViewBag.Id_Vendedor = new SelectList(db.Vendedor, "Id", "Vndr_Nombre", cliente.Id_Vendedor);
            ViewData["Posicion2"] = ViewBag.Id_Vendedor;
            ViewData["EstadoCivil"] = estadocivil;
            return View(cliente);
        }

        // GET: Clientes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "ApoyoGestion" || Session["Tipo"].ToString() == "Gestion" || Session["Tipo"].ToString() == "Administrador")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Cliente cliente = db.Cliente.Find(id);
                if (cliente == null)
                {
                    return HttpNotFound();
                }
                return View(cliente);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // POST: Clientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);

            if (cliente.Id_Corretaje != null)
            {
                Corretaje cr = db.Corretaje.Find(cliente.Id_Corretaje);
                cr.Crt_Status = "Disponible";
                db.SaveChanges();
            }

            db.Cliente.Remove(cliente);
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

        public JsonResult BuscarCliente(string filtro = "", int pagina = 1, int registrosPagina = 15)
        {
            if (filtro == "")
            {
                int totalPaginas = (int)Math.Ceiling((double)db.Cliente.Count() / registrosPagina);
                var busqueda = (from a in db.Cliente select new { progeso = a.Gral_ProgresoForm,a.Id_Corretaje,a.Id, direccion = (a.Corretaje.Crt_Direccion == null) ? "Casa no asignada" :a.Corretaje.Crt_Direccion, cliente = (a.Gral_Nombre + " " + a.Gral_Apellidopa + " " + a.Gral_Apellidoma), fecha = SqlFunctions.DateName("year", a.Gral_Fechaalta).Trim() + "/" + SqlFunctions.StringConvert((double)a.Gral_Fechaalta.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("day", a.Gral_Fechaalta).Trim(), total = totalPaginas }).OrderBy(a => a.cliente).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Cliente where (a.Gral_Nombre + " " + a.Gral_Apellidopa + " " + a.Gral_Apellidoma).Contains(filtro) || a.Corretaje.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Cliente where (a.Gral_Nombre + " " + a.Gral_Apellidopa + " " + a.Gral_Apellidoma).Contains(filtro) || a.Corretaje.Crt_Direccion.Contains(filtro) select new { progeso = a.Gral_ProgresoForm,a.Id_Corretaje, a.Id, direccion = a.Corretaje.Crt_Direccion, cliente = (a.Gral_Nombre + " " + a.Gral_Apellidopa + " " + a.Gral_Apellidoma), fecha = SqlFunctions.DateName("year", a.Gral_Fechaalta).Trim() + "/" + SqlFunctions.StringConvert((double)a.Gral_Fechaalta.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("day", a.Gral_Fechaalta).Trim(), total = totalPaginas }).OrderBy(a => a.cliente).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
        }

        public class DetailsViewModel
        {
            public List<Cliente> clientes { get; set; }
            public List<Corretaje> corretajes = new List<Corretaje>();
            public List<Contaduria> contadurias { get; set; }
            public List<Gestion> gestions = new List<Gestion>();
            public List<Habilitacion> habilitacions = new List<Habilitacion>();
            public List<Verificacion> verificacions { get; set; }
            public List<Comentarios> comentarios = new List<Comentarios>();
        }

        public ActionResult Folioview(string folio)
        {
            DetailsViewModel folioc = new DetailsViewModel();

            int? idcliente = 0;
            int? idcorretaje = 0;
            int? idgestion = 0;
            int? idhabilitacion = 0;

            folioc.clientes = db.Cliente.ToList();
            folioc.corretajes = db.Corretaje.ToList();
            folioc.gestions = db.Gestion.ToList();
            folioc.habilitacions = db.Habilitacion.ToList();

            foreach (var item in folioc.clientes)
            {
                if (folio == item.Grlal_Folio) 
                {
                    idcliente = item.Id;
                    idcorretaje = item.Id_Corretaje;

                    foreach (var item1 in folioc.gestions)
                    {
                        if (item1.Id_Cliente == idcliente)
                        {
                            idgestion = item1.Id;
                            break;
                        }
                    }
                    foreach (var item2 in folioc.habilitacions)
                    {
                        if (item2.Id_Corretaje == idcorretaje)
                        {
                            idhabilitacion = item2.Id;
                            break;
                        }
                    }
                    break;
                }
            }

            folioc.clientes.Clear();
            folioc.corretajes.Clear();
            folioc.gestions.Clear();
            folioc.habilitacions.Clear();
         
            folioc.clientes.Add(db.Cliente.Find(idcliente));
            folioc.gestions.Add(db.Gestion.Find(idgestion));
            folioc.corretajes.Add(db.Corretaje.Find(idcorretaje));
            folioc.habilitacions.Add(db.Habilitacion.Find(idhabilitacion));
            folioc.comentarios = db.Comentarios.ToList();

            ViewBag.folios = folio;
            return View(folioc);
        }
    }
    public class MyViewModel
    {
        public Cliente Cliente { get; set; }
        public IEnumerable<SelectList> Corretaje { get; set; }
    }
}

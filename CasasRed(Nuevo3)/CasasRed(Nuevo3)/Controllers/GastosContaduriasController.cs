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
        public ActionResult Details(int? idcontaduria)
        {
            //Perdon por mis listas , para soporte $$ skype: jose.armando316 

            if (idcontaduria == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DetailsViewModel GASTOS = new DetailsViewModel();

            /*Ingresar datos a listas para compararlos*/
            int idCorretaje = (int)db.Contaduria.Find(idcontaduria).Id_Corretaje;
            GASTOS.gastosContadurias = db.GastosContaduria.ToList();
            GASTOS.contadurias.Add(db.Contaduria.Find(idcontaduria));
            GASTOS.usuarios = db.Usuario.ToList();
            
            //Recordar apoyogestion
            GASTOS.tipoUsuarios = db.TipoUsuario.ToList();

            //Verificar con dani si de aqui puedo sacar la informacion
            GASTOS.casaInventarios = db.CasaInventario.Where(e => e.ci_corretaje_id == idCorretaje).ToList();
            GASTOS.existencias = db.Existencias.ToList();


            int? idcorretaje = 0;


            List<int?> IDS = new List<int?>();
            List<String> GSTCON_CONCE = new List<string>();
            List<decimal?> GSTCON_MONTO = new List<decimal?>();
            List<String> GSTCON_DESCRIPCION = new List<string>();
            List<DateTime?> GSTCON_FECHA = new List<DateTime?>();
            List<int?> IDSCORRETAJE = new List<int?>();
            List<int?> IDSUAURIOS = new List<int?>();
            var GGASTOS = (from g in db.GastosContaduria select new { g.Id,g.GstCon_Concepto,g.GstCon_Monto,g.GstCon_Descripcion,g.GstCon_Fecha,g.Id_Corretaje,g.Id_Usuario }).ToList();
            var Usuarios = (from u in db.Usuario join t in db.TipoUsuario on u.Id_TipoUsiario equals t.Id select new { u.Id, t.tipusu_descricion }).ToList();

            /*Time to play*/
            //Listas para corretaje
            List<int?> IDSC = new List<int?>();
            List<String> GSTCON_CONCEC = new List<string>();
            List<decimal?> GSTCON_MONTOC = new List<decimal?>();
            List<String> GSTCON_DESCRIPCIONC = new List<string>();
            List<DateTime?> GSTCON_FECHAC = new List<DateTime?>();
            List<int?> IDSCORRETAJEC = new List<int?>();
            List<int?> IDSUAURIOSC = new List<int?>();
            
            //Listas para gestion
            List<int?> IDSG = new List<int?>();
            List<String> GSTCON_CONCEG = new List<string>();
            List<decimal?> GSTCON_MONTOG = new List<decimal?>();
            List<String> GSTCON_DESCRIPCIONG = new List<string>();
            List<DateTime?> GSTCON_FECHAG = new List<DateTime?>();
            List<int?> IDSCORRETAJEG = new List<int?>();
            List<int?> IDSUAURIOSG = new List<int?>();

            //Listas para administrador
            List<int?> IDSA = new List<int?>();
            List<String> GSTCON_CONCEA = new List<string>();
            List<decimal?> GSTCON_MONTOA = new List<decimal?>();
            List<String> GSTCON_DESCRIPCIONA = new List<string>();
            List<DateTime?> GSTCON_FECHAA = new List<DateTime?>();
            List<int?> IDSCORRETAJEA = new List<int?>();
            List<int?> IDSUAURIOSA = new List<int?>();

            //Listas para contaduria
            List<int?> IDSCC = new List<int?>();
            List<String> GSTCON_CONCECC = new List<string>();
            List<decimal?> GSTCON_MONTOCC = new List<decimal?>();
            List<String> GSTCON_DESCRIPCIONCC = new List<string>();
            List<DateTime?> GSTCON_FECHACC = new List<DateTime?>();
            List<int?> IDSCORRETAJECC = new List<int?>();
            List<int?> IDSUAURIOSCC = new List<int?>();


            //Listas para inventarios
            //ic.ci_Id,ic.ci_articulo_id,ic.ci_cantidadAsignada,ic.ci_corretaje_id,e.ext_precioUnitario
            List<int?> IDCI = new List<int?>();
            List<string> CI_ARTICULO_ID = new List<string>();
            List<decimal> CI_CANTIDADASIGNADA = new List<decimal>();
            List<int?> CI_CORRETAJE_ID = new List<int?>();
            List<decimal?> EXT_PRECIOUNITARIO = new List<decimal?>();

            foreach (var item in GASTOS.contadurias)
            {
                idcorretaje = item.Id_Corretaje;
            }
            //filtrar primero todos los gastos de la casa , ya sea por inventario/gastos
            //sacar cuanto se saco de inventario para la casa en articulos y pasarlo a $$
            //lograr hacer un general y que tenga desgloce de datos


            //Separacion de gastos por departamento se filtra en vista por depto
            foreach (var item in GGASTOS)
            {
                foreach (var item2 in Usuarios)
                {
                    if (item.Id_Usuario == item2.Id)
                    {
                        if (item2.tipusu_descricion == "Gestion")
                        {
                              IDSG.Add(item.Id);
                              GSTCON_CONCEG.Add(item.GstCon_Concepto);
                              GSTCON_MONTOG.Add(item.GstCon_Monto);
                              GSTCON_DESCRIPCIONG.Add(item.GstCon_Descripcion);
                              GSTCON_FECHAG.Add(item.GstCon_Fecha);
                              IDSCORRETAJEG.Add(item.Id_Corretaje);
                              IDSUAURIOSG.Add(item.Id_Usuario);
                        }
                        else if (item2.tipusu_descricion == "Corretaje") 
                        {
                            IDSC.Add(item.Id);
                            GSTCON_CONCEC.Add(item.GstCon_Concepto);
                            GSTCON_MONTOC.Add(item.GstCon_Monto);
                            GSTCON_DESCRIPCIONC.Add(item.GstCon_Descripcion);
                            GSTCON_FECHAC.Add(item.GstCon_Fecha);
                            IDSCORRETAJEC.Add(item.Id_Corretaje);
                            IDSUAURIOSC.Add(item.Id_Usuario);
                        }
                        else if(item2.tipusu_descricion=="Contabilidad")
                        {
                            IDSCC.Add(item.Id);
                            GSTCON_CONCECC.Add(item.GstCon_Concepto);
                            GSTCON_MONTOCC.Add(item.GstCon_Monto);
                            GSTCON_DESCRIPCIONCC.Add(item.GstCon_Descripcion);
                            GSTCON_FECHACC.Add(item.GstCon_Fecha);
                            IDSCORRETAJECC.Add(item.Id_Corretaje);
                            IDSUAURIOSCC.Add(item.Id_Usuario);
                        }
                        else
                        {
                            IDSA.Add(item.Id);
                            GSTCON_CONCEA.Add(item.GstCon_Concepto);
                            GSTCON_MONTOA.Add(item.GstCon_Monto);
                            GSTCON_DESCRIPCIONA.Add(item.GstCon_Descripcion);
                            GSTCON_FECHAA.Add(item.GstCon_Fecha);
                            IDSCORRETAJEA.Add(item.Id_Corretaje);
                            IDSUAURIOSA.Add(item.Id_Usuario);
                        }
                    }
                }
            }

            //Inventario desgloce ? , asi como total del inventario
            //var Usuarios = (from u in db.Usuario join t in db.TipoUsuario on u.Id_TipoUsiario equals t.Id select new { u.Id, t.tipusu_descricion }).ToList();
            var Inventarios = (from ic in db.CasaInventario 
                               join ha in db.HistorialAsignacion on ic.ci_Id equals ha.ha_casaInventario
                               join e in db.Existencias on ha.ha_existencia_id equals e.Id
                               where ic.ci_corretaje_id == idcorretaje
                               select new { ic.ci_Id, ic.ci_articulo_id, ic.ci_cantidadAsignada, ic.ci_corretaje_id, e.ext_precioUnitario }).ToList();
            decimal preciototal = 0;
            foreach (var item in Inventarios)
            {  
                preciototal += (decimal)item.ext_precioUnitario * item.ci_cantidadAsignada;
                IDCI.Add(item.ci_Id);
                CI_ARTICULO_ID.Add(item.ci_articulo_id);
                CI_CANTIDADASIGNADA.Add(item.ci_cantidadAsignada);
                CI_CORRETAJE_ID.Add(item.ci_corretaje_id);
                EXT_PRECIOUNITARIO.Add(item.ext_precioUnitario);                
            }

            //Inventario
            ViewBag.PRECIOTOTAL = preciototal;
            ViewBag.IDCI = IDCI;
            ViewBag.CIR_ARTICULO_ID = CI_ARTICULO_ID;
            ViewBag.CI_CANTIDADASIGNADA = CI_CANTIDADASIGNADA;
            ViewBag.CI_CORRETAJE_ID = CI_CORRETAJE_ID;
            ViewBag.EXT_PRECIOUNITARIO = EXT_PRECIOUNITARIO;

            
            //GESTION
            ViewBag.IDSG = IDSG;
            ViewBag.GSTCON_CONCEG = GSTCON_CONCEG;
            ViewBag.GSTCON_MONTOG = GSTCON_MONTOG;
            ViewBag.GSTCON_DESCRIPCIONG = GSTCON_DESCRIPCIONG;
            ViewBag.GSTCON_FECHAG = GSTCON_FECHAG;
            ViewBag.IDSCORRETAJEG = IDSCORRETAJEG;
            ViewBag.IDSUSUARIOSG = IDSUAURIOSG;
            
            //CORRETAJE
            ViewBag.IDSC = IDSC;
            ViewBag.GSTCON_CONCEC = GSTCON_CONCEC;
            ViewBag.GSTCON_MONTOC = GSTCON_MONTOC;
            ViewBag.GSTCON_DESCRIPCIONC = GSTCON_DESCRIPCIONC;
            ViewBag.GSTCON_FECHAC = GSTCON_FECHAC;
            ViewBag.IDSCORRETAJEC = IDSCORRETAJEC;
            ViewBag.IDSUSUARIOSC = IDSUAURIOSC;

            //Contabilidad
            
            ViewBag.IDSCC = IDSCC;
            ViewBag.GSTCON_CONCECC = GSTCON_CONCECC;
            ViewBag.GSTCON_MONTOCC = GSTCON_MONTOCC;
            ViewBag.GSTCON_DESCRIPCIONCC = GSTCON_DESCRIPCIONCC;
            ViewBag.GSTCON_FECHACC = GSTCON_FECHACC;
            ViewBag.IDSCORRETAJECC = IDSCORRETAJECC;
            ViewBag.IDSUSUARIOSCC = IDSUAURIOSCC;

            //Administrador
            ViewBag.IDSA= IDSA;
            ViewBag.GSTCON_CONCEA = GSTCON_CONCEA;
            ViewBag.GSTCON_MONTOA = GSTCON_MONTOA;
            ViewBag.GSTCON_DESCRIPCIONA = GSTCON_DESCRIPCIONA;
            ViewBag.GSTCON_FECHAA = GSTCON_FECHAA;
            ViewBag.IDSCORRETAJEA = IDSCORRETAJEA;
            ViewBag.IDSUSUARIOSA = IDSUAURIOSA;



            /* a ver que sale*/
            foreach (var item in GGASTOS)
            {
                IDS.Add(item.Id);
                GSTCON_CONCE.Add(item.GstCon_Concepto);
                GSTCON_MONTO.Add(item.GstCon_Monto);
                GSTCON_DESCRIPCION.Add(item.GstCon_Descripcion);
                GSTCON_FECHA.Add(item.GstCon_Fecha);
                IDSCORRETAJE.Add(item.Id_Corretaje);
                IDSUAURIOS.Add(item.Id_Usuario);
            }

            ViewBag.IDS = IDS;
            ViewBag.GSTCON_CONCE = GSTCON_CONCE;
            ViewBag.GSTCON_MONTO = GSTCON_MONTO;
            ViewBag.GSTCON_DESCRIPCION = GSTCON_DESCRIPCION;
            ViewBag.GSTCON_FECHA = GSTCON_FECHA;
            ViewBag.IDSCORRETAJE = IDSCORRETAJE;
            ViewBag.IDSUSUARIOS = IDSUAURIOS;

            ViewBag.GastoMaterial = Math.Round(preciototal,2);

            return View(GASTOS);
        }
        public class DetailsViewModel
        {
            public List<GastosContaduria> gastosContadurias = new List<GastosContaduria>();
            public List<Cliente> clientes = new List<Cliente>();
            public List<Corretaje> corretajes = new List<Corretaje>();
            public List<Contaduria> contadurias = new List<Contaduria>();
            public List<Gestion> gestions = new List<Gestion>();
            public List<Habilitacion> habilitacions = new List<Habilitacion>();
            public List<Usuario> usuarios = new List<Usuario>();
            public List<TipoUsuario> tipoUsuarios = new List<TipoUsuario>();
            
            //Inventario
            public List<CasaInventario> casaInventarios = new List<CasaInventario>();
            public List<Existencias> existencias = new List<Existencias>();


        }

        // GET: GastosContadurias/Create
        public ActionResult Create(int id)
        {
            ViewBag.casa = id;
            ViewBag.Tipo = Session["Tipo"].ToString();

            //Selectlist conceptos de Corretaje
            var listaCorr = new SelectList(new[] {
                new {value = "-", text = "Seleccione un concepto...."},
                new {value = "CESPT", text = "CESPT"},
                new {value = "CFE", text = "CFE"},
                new {value = "Otros", text = "Otros"}
            }, "value", "text", 0);

            //Selectlist conceptos de Gestion
            var listaGest= new SelectList(new[] {
                new {value = "-", text = "Seleccione un concepto...."},
                new {value = "Escrituras", text = "Escrituras"},
                new {value = "Planta Cartográfica", text = "Planta Cartográfica"},
                new {value = "Acta de Nacimiento", text = "Acta de Nacimiento"},
                new {value = "Número Oficial", text = "Solicitud de Retención de Infonavit"},
                new {value = "Certificado de Hipoteca", text = "Certificado de Hipoteca"},
                new {value = "Certificado de Fiscal", text = "Certificado de Fiscal"},
                new {value = "Solicitud Estado", text = "Solicitud Estado"},
                new {value = "Junta URBI", text = "Junta URBI"},
                new {value = "Certificado Cartográfico", text = "Certificado Cartográfico"},
                new {value = "Avalúo", text = "Avalúo"},
                new {value = "Notaría", text = "Notaría"},
                new {value = "Firma Escituras", text = "Firma Escituras"},
                new {value = "Gestión Infonavit", text = "Gestión Infonavit"},
                new {value = "Otros", text = "Otros"}
            }, "value", "text", 0);

            //Selectlist Administrador
            var listaAdmin = new SelectList(new[] {
                new {value = "-", text = "Seleccione un concepto...."},
                new {value = "CESPT", text = "CESPT"},
                new {value = "CFE", text = "CFE"},
                new {value = "Escrituras", text = "Escrituras"},
                new {value = "Planta Cartográfica", text = "Planta Cartográfica"},
                new {value = "Acta de Nacimiento", text = "Acta de Nacimiento"},
                new {value = "Número Oficial", text = "Solicitud de Retención de Infonavit"},
                new {value = "Certificado de Hipoteca", text = "Certificado de Hipoteca"},
                new {value = "Certificado de Fiscal", text = "Certificado de Fiscal"},
                new {value = "Sol Estado", text = "Sol Estado"},
                new {value = "Junta URBI", text = "Junta URBI"},
                new {value = "Certificado Cartográfico", text = "Certificado Cartográfico"},
                new {value = "Avalúo", text = "Avalúo"},
                new {value = "Notaría", text = "Notaría"},
                new {value = "Firma Escituras", text = "Firma Escituras"},
                new {value = "Gestión Infonavit", text = "Gestión Infonavit"},
                new {value = "Otros", text = "Otros"}
            }, "value", "text", 0);

            //Selectlist conceptos de Contaduria
            var listaCont = new SelectList(new[] {
                new {value = "No seleccionado", text = "Seleccione un concepto...."},
                new {value = "Vigilancia", text = "Vigilancia"},
                new {value = "Devol. Mensualidades", text = "Devol. Mensualidades"},
                new {value = "Mano de Obra", text = "Mano de Obra"},
                new {value = "Otros", text = "Otros"}
            }, "value", "text", 0);

            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status");
            ViewData["Corretaje"] = listaCorr;
            ViewData["Gestion"] = listaGest;
            ViewData["Administrador"] = listaAdmin;
            ViewData["Contador"] = listaCont;

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
                new {value = "No seleccionado", text = "Seleccione un concepto...."},
                new {value = "CESPT", text = "CESPT"},
                new {value = "CFE", text = "CFE"},
            }, "value", "text", 0);

            //Selectlist conceptos de Gestion
            var listaGest = new SelectList(new[] {
                new {value = "No seleccionado", text = "Seleccione un concepto...."},
                new {value = "CESPT", text = "CESPT"},
                new {value = "CFE", text = "CFE"},
            }, "value", "text", 0);

            //Selectlist conceptos de Contaduria
            var listaCont = new SelectList(new[] {
                new {value = "No seleccionado", text = "Seleccione un concepto...."},
                new {value = "CESPT", text = "CESPT"},
                new {value = "CFE", text = "CFE"},
            }, "value", "text", 0);

            if (ModelState.IsValid)
            {
                //Guardar campos automaticps
                gastosContaduria.Id_Usuario = int.Parse(Session["UsuarioID"].ToString());
                gastosContaduria.GstCon_Fecha = DateTime.Now;

                db.GastosContaduria.Add(gastosContaduria);
                db.SaveChanges();
                return Redirect("/GastosContadurias/Index/" + gastosContaduria.Id_Corretaje);
            }

            ViewData["Corretaje"] = listaCorr;
            ViewData["Gestion"] = listaGest;
            ViewData["Contador"] = listaCont;
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
            int? idc = gastosContaduria.Id_Corretaje;
            db.GastosContaduria.Remove(gastosContaduria);
            db.SaveChanges();
            return Redirect("/GastosContadurias/Index/" + idc);
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

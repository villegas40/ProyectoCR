using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebGrease;
using CasasRed_Nuevo3_.Models;

namespace CasasRed_Nuevo3_.Controllers
{
    public class GerenteController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: Gerente
        public ViewResult Index()
        {
            var gen = new FooViewModel();
            gen.gestions = db.Gestion.ToList();
            gen.corretajes = db.Corretaje.ToList();
            List<string> nombres = new List<string>();
            List<int?> ids = new List<int?>();
          
            var ge = ((from g in db.Gestion select new { g.Id_Corretaje, g.Cliente.Gral_Nombre }).ToList());



            foreach (var XD in ge)
            {
               nombres.Add(XD.Gral_Nombre);
               ids.Add(XD.Id_Corretaje);
            }

            //ViewBag.test = (from g in db.Gestion select new {g.Id_Corretaje, g.Cliente.Gral_Nombre}).ToList();
            ViewBag.listanombres = nombres;
            ViewBag.listaids = ids;

            return View(gen);
            
        }
        public class FooViewModel
        {
            public IEnumerable<Gestion> gestions { get; set; }
            public IEnumerable<Corretaje> corretajes { get; set; }
        }

        public class DetailsViewModel
        {
            public List<Cliente> clientes { get; set; }
            public List<Corretaje> corretajes = new List<Corretaje>();
            public List<Contaduria> contadurias { get; set; }
            public List<Gestion> gestions = new List<Gestion>();
            public List<Habilitacion> habilitacions = new List<Habilitacion>();
            public List<Verificacion> verificacions { get; set; }


        }

        public ActionResult DeleteDetails(int?idc,int? idg)
        {
            if (idc == null && idg == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (idg == 0)
            {
                DetailsViewModel kappa1 = new DetailsViewModel();
                kappa1.corretajes.Add(db.Corretaje.Find(idc));
                kappa1.habilitacions = (db.Habilitacion.ToList());
                return View(kappa1);
            }

            DetailsViewModel kappa = new DetailsViewModel();

            kappa.corretajes.Add(db.Corretaje.Find(idc));
            kappa.gestions.Add(db.Gestion.Find(idg));
            kappa.verificacions = (db.Verificacion.ToList());
            kappa.habilitacions = (db.Habilitacion.ToList());

            var xd = ((from x in db.Verificacion select new { x.Id, x.Vfn_Trato_asesor, x.Id_Cliente }).ToList());

            List<int?> idv = new List<int?>();
            List<int?> vfntrato = new List<int?>();
            List<int?> IDclien = new List<int?>();
            foreach (var test3 in xd)
            {
                idv.Add(test3.Id);
                vfntrato.Add(test3.Vfn_Trato_asesor);
                IDclien.Add(test3.Id_Cliente);
            }
            ViewBag.idvs = idv;
            ViewBag.vfntratos = vfntrato;
            ViewBag.idcliente = IDclien;

            return View(kappa);
        }

        // GET: Clientes/Delete/5
        public ActionResult DeleteCliente(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Gestion" || Session["Tipo"].ToString() == "Administrador")
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
        [HttpPost, ActionName("DeleteCliente")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            db.Cliente.Remove(cliente);
            db.SaveChanges();
            return RedirectToAction("/Index");
        }

        // GET: Corretajes/Delete/5
        public ActionResult DeleteCorretaje(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Corretaje" || Session["Tipo"].ToString() == "Administrador")
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Corretaje corretaje = db.Corretaje.Find(id);
                if (corretaje == null)
                {
                    return HttpNotFound();
                }
                return View(corretaje);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // POST: Corretajes/Delete/5
        [HttpPost, ActionName("DeleteCorretaje")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmedd(int id)
        {
            Corretaje corretaje = db.Corretaje.Find(id);
            db.Corretaje.Remove(corretaje);
            db.SaveChanges();
            return RedirectToAction("/Index");
        }


        public ActionResult Details(int? idc, int? idg)
        {
            if (idc == null && idg == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (idg == 0)
            {
                DetailsViewModel kappa1 = new DetailsViewModel();
                kappa1.corretajes.Add(db.Corretaje.Find(idc));
                kappa1.habilitacions = (db.Habilitacion.ToList());
                kappa1.verificacions = (db.Verificacion.ToList());
                kappa1.contadurias = (db.Contaduria.ToList());

                return View(kappa1);
            }

            DetailsViewModel kappa = new DetailsViewModel();

            kappa.corretajes.Add(db.Corretaje.Find(idc));
            kappa.gestions.Add(db.Gestion.Find(idg));
            kappa.verificacions = (db.Verificacion.ToList());
            kappa.habilitacions = (db.Habilitacion.ToList());
            kappa.contadurias = (db.Contaduria.ToList());

            var xd = ((from x in db.Verificacion select new { x.Id, x.Vfn_Trato_asesor, x.Id_Cliente }).ToList());

            List<int?> idv = new List<int?>();
            List<int?> vfntrato = new List<int?>();
            List<int?> IDclien = new List<int?>();
            foreach (var test3 in xd)
            {
                idv.Add(test3.Id);
                vfntrato.Add(test3.Vfn_Trato_asesor);
                IDclien.Add(test3.Id_Cliente);
            }
            ViewBag.idvs = idv;
            ViewBag.vfntratos = vfntrato;
            ViewBag.idcliente = IDclien;

            return View(kappa);
        }

        /*EDITAR CORRETAJE */
        // GET: Corretajes/Edit/5
        public ActionResult Editc(int? id)
        {
            var posicion = new SelectList(new[] {
                new { value = 0, text = "Selecciona una opción.."},
                new { value = 1, text = "Soltero" },
                new { value = 2, text = "Casado" },
                new { value = 3, text = "Viudo" },
                new { value = 4, text = "Divorciado" }
            }, "value", "text", 0);

            ViewData["Posicion"] = posicion;

            //Estatus de la casa
            var estatus = new SelectList(new[] {
                new { value = 0, text = "Selecciona una opción.."},
                new { value = 1, text = "Venta" },
                new { value = 2, text = "Disponible" },
            }, "value", "text", 0);

            ViewData["Estatus"] = estatus;


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corretaje corretaje = db.Corretaje.Find(id);
            if (corretaje == null)
            {
                return HttpNotFound();
            }

            return View(corretaje);
        }

        // POST: Corretajes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editc(Corretaje corretaje, HttpPostedFileBase Crt_ReciboAgua, HttpPostedFileBase Crt_ReciboLuz, HttpPostedFileBase Crt_Recibo_predial_digital, HttpPostedFileBase Crt_Otros)
        {
            if (corretaje.Crt_Ine_Titu == null) corretaje.Crt_Ine_Titu = false;
            if (corretaje.Crt_Ine_Conyu == null) corretaje.Crt_Ine_Conyu = false;
            if (corretaje.Crt_ActaNacTitu == null) corretaje.Crt_ActaNacTitu = false;
            if (corretaje.Crt_ActaNacConyu == null) corretaje.Crt_ActaNacConyu = false;
            if (corretaje.Crt_ActaMatr == null) corretaje.Crt_ActaMatr = false;
            if (corretaje.Crt_EscrCert == null) corretaje.Crt_EscrCert = false;
            if (corretaje.Crt_Acuerdo == null) corretaje.Crt_Acuerdo = false;
            if (corretaje.Crt_CartaDesPre == null) corretaje.Crt_CartaDesPre = false;
            if (corretaje.Crt_Escritura_Simple == null) corretaje.Crt_Escritura_Simple = false;

            // Imagenes
            if (Crt_ReciboAgua != null)
            {
                corretaje.Crt_ReciboAgua = "data:image/jpg;base64," + convertTo64(Crt_ReciboAgua);
            }
            if (Crt_ReciboLuz != null)
            {
                corretaje.Crt_ReciboLuz = "data:image/jpg;base64," + convertTo64(Crt_ReciboLuz);
            }

            if (Crt_Recibo_predial_digital != null)
            {
                corretaje.Crt_Recibo_predial_digital = "data:image/jpg;base64," + convertTo64(Crt_Recibo_predial_digital);
            }

            if (Crt_Otros != null)
            {
                corretaje.Crt_Otros = "data:image/jpg;base64," + convertTo64(Crt_Otros);
            }
            //
            // Mini hack
            DateTime aux = new DateTime();
            corretaje.Crt_Status = (corretaje.Crt_Status == null) ? "" : corretaje.Crt_Status;
            corretaje.Crt_Cliente_Nombre = (corretaje.Crt_Cliente_Nombre == null) ? "" : corretaje.Crt_Cliente_Nombre;
            corretaje.Crt_Cliente_ApMat = (corretaje.Crt_Cliente_ApMat == null) ? "" : corretaje.Crt_Cliente_ApMat;
            corretaje.Crt_Cliente_ApPat = (corretaje.Crt_Cliente_ApPat == null) ? "" : corretaje.Crt_Cliente_ApPat;
            corretaje.Crt_Direccion = (corretaje.Crt_Direccion == null) ? "" : corretaje.Crt_Direccion;
            corretaje.Crt_Precio = (corretaje.Crt_Precio == null) ? "" : corretaje.Crt_Precio;
            corretaje.Crt_Gasto = (corretaje.Crt_Gasto == null) ? "" : corretaje.Crt_Gasto;
            corretaje.Crt_Tipo_Vivienda = (corretaje.Crt_Tipo_Vivienda == null) ? "" : corretaje.Crt_Tipo_Vivienda;
            corretaje.Crt_Nivel = (corretaje.Crt_Nivel == null) ? 0 : corretaje.Crt_Nivel;
            corretaje.Crt_Num_Habitaciones = (corretaje.Crt_Num_Habitaciones == null) ? 0 : corretaje.Crt_Num_Habitaciones;
            corretaje.Crt_Planta = (corretaje.Crt_Planta == null) ? 0 : corretaje.Crt_Planta;
            corretaje.Crt_Ano_compra = (corretaje.Crt_Ano_compra == null) ? "" : corretaje.Crt_Ano_compra;
            corretaje.Crt_Num_Credito_Infonavit = (corretaje.Crt_Num_Credito_Infonavit == null) ? "" : corretaje.Crt_Num_Credito_Infonavit;
            corretaje.Crt_Saldo_infonavit = (corretaje.Crt_Saldo_infonavit == null) ? 0 : corretaje.Crt_Saldo_infonavit;
            corretaje.Crt_Fec_Nac = (corretaje.Crt_Fec_Nac == null) ? aux : corretaje.Crt_Fec_Nac;
            corretaje.Crt_Tel_Celular = (corretaje.Crt_Tel_Celular == null) ? "" : corretaje.Crt_Tel_Celular;
            corretaje.Crt_Estado_Civil = (corretaje.Crt_Estado_Civil == null) ? "" : corretaje.Crt_Estado_Civil;
            corretaje.Crt_Tel_Casa = (corretaje.Crt_Tel_Casa == null) ? "" : corretaje.Crt_Tel_Casa;
            corretaje.Crt_Tel_Trabajo = (corretaje.Crt_Tel_Trabajo == null) ? "" : corretaje.Crt_Tel_Trabajo;
            corretaje.Crt_Tel_Ref1 = (corretaje.Crt_Tel_Ref1 == null) ? "" : corretaje.Crt_Tel_Ref1;
            corretaje.Crt_Tel_Ref2 = (corretaje.Crt_Tel_Ref2 == null) ? "" : corretaje.Crt_Tel_Ref2;
            corretaje.Crt_Tel_Ref = (corretaje.Crt_Tel_Ref == null) ? "" : corretaje.Crt_Tel_Ref;
            corretaje.Crt_Clave_Catastral = (corretaje.Crt_Clave_Catastral == null) ? "" : corretaje.Crt_Clave_Catastral;
            corretaje.Crt_Adeudo_predial = (corretaje.Crt_Adeudo_predial == null) ? 0 : corretaje.Crt_Adeudo_predial;
            corretaje.Crt_Num_servicio_luz = (corretaje.Crt_Num_servicio_luz == null) ? "" : corretaje.Crt_Num_servicio_luz;
            corretaje.Crt_Adeudo_luz = (corretaje.Crt_Adeudo_luz == null) ? 0 : corretaje.Crt_Adeudo_luz;
            corretaje.Crt_NombreC_Titular_luz = (corretaje.Crt_NombreC_Titular_luz == null) ? "" : corretaje.Crt_NombreC_Titular_luz;
            corretaje.Crt_No_cuenta_agua = (corretaje.Crt_No_cuenta_agua == null) ? "" : corretaje.Crt_No_cuenta_agua;
            corretaje.Crt_Adeudo_agua = (corretaje.Crt_Adeudo_agua == null) ? 0 : corretaje.Crt_Adeudo_agua;
            corretaje.Crt_Status_Muestra = (corretaje.Crt_Status_Muestra == null) ? "" : corretaje.Crt_Status_Muestra;
            corretaje.Crt_Obervaciones = (corretaje.Crt_Obervaciones == null) ? "" : corretaje.Crt_Obervaciones;


            if (ModelState.IsValid)
            {
                db.Entry(corretaje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("/Index");
            }
            return View(corretaje);
        }

        /*EDITAR GESTION */
        // GET: Gestions/Edit/5
        public ActionResult Editg(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Gestion gestion = db.Gestion.Find(id);
            if (gestion == null)
            {
                return HttpNotFound();
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Gral_Nombre", gestion.Id_Cliente);
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", gestion.Id_Corretaje);
            return View(gestion);
        }

        // POST: Gestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editg([Bind(Include = "Id,Gtn_Escrituras,Gtn_Planta_Cartografica,Gtn_Predial,Gtn_Recibo_Luz,Gtn_Recibo_Agua,Gtn_Acta_Nacimiento,Gtn_IFE_Copia,Gtn_Sol_Ret_Ifo,Gtn_Cert_Hip,Gtn_Cert_Fiscal,Gtn_Sol_Estado,Gtn_Junta_URBI,Gtn_Agua_Pago_Inf,Gtn_Cert_Cartogr,Gtn_No_Oficial,Gtn_Avaluo,Gtn_CT_Banco,Gtn_Aviso_Suspension,Gtn_Formato_Infonavit,Gtn_Fotos_Propiedad,Gtn_Evaluo_Contacto,Gtn_Planeacion_Fianza,Gtn_Urbanizacion,Gtn_Credito_INFONAVIT,Gtn_Notaria,Gtn_Firma_Escrituras,Gtm_Aviso_Susp,Id_Corretaje,Id_Cliente,Gtn_ProgresoForm")] Gestion gestion)
        {
            gestion.Gtn_Escrituras = ((gestion.Gtn_Escrituras == null) ? false : gestion.Gtn_Escrituras);
            gestion.Gtn_Planta_Cartografica = ((gestion.Gtn_Planta_Cartografica == null) ? false : gestion.Gtn_Planta_Cartografica);
            gestion.Gtn_Predial = ((gestion.Gtn_Predial == null) ? false : gestion.Gtn_Predial);
            gestion.Gtn_Recibo_Luz = ((gestion.Gtn_Recibo_Luz == null) ? false : gestion.Gtn_Recibo_Luz);
            gestion.Gtn_Recibo_Agua = ((gestion.Gtn_Recibo_Agua == null) ? false : gestion.Gtn_Recibo_Agua);
            gestion.Gtn_Acta_Nacimiento = ((gestion.Gtn_Acta_Nacimiento == null) ? false : gestion.Gtn_Acta_Nacimiento);
            gestion.Gtn_IFE_Copia = ((gestion.Gtn_IFE_Copia == null) ? false : gestion.Gtn_IFE_Copia);
            gestion.Gtn_Sol_Ret_Ifo = ((gestion.Gtn_Sol_Ret_Ifo == null) ? false : gestion.Gtn_Sol_Ret_Ifo);
            gestion.Gtn_Cert_Hip = ((gestion.Gtn_Cert_Hip == null) ? false : gestion.Gtn_Cert_Hip);
            gestion.Gtn_Cert_Fiscal = ((gestion.Gtn_Cert_Fiscal == null) ? false : gestion.Gtn_Cert_Fiscal);
            gestion.Gtn_Sol_Estado = ((gestion.Gtn_Sol_Estado == null) ? false : gestion.Gtn_Sol_Estado);
            gestion.Gtn_Junta_URBI = ((gestion.Gtn_Junta_URBI == null) ? false : gestion.Gtn_Junta_URBI);
            gestion.Gtn_Agua_Pago_Inf = ((gestion.Gtn_Agua_Pago_Inf == null) ? false : gestion.Gtn_Agua_Pago_Inf);
            gestion.Gtn_Cert_Cartogr = ((gestion.Gtn_Cert_Cartogr == null) ? false : gestion.Gtn_Cert_Cartogr);
            gestion.Gtn_No_Oficial = ((gestion.Gtn_No_Oficial == null) ? false : gestion.Gtn_No_Oficial);
            gestion.Gtn_Avaluo = ((gestion.Gtn_Avaluo == null) ? false : gestion.Gtn_Avaluo);
            gestion.Gtn_CT_Banco = ((gestion.Gtn_CT_Banco == null) ? false : gestion.Gtn_CT_Banco);
            gestion.Gtn_Aviso_Suspension = ((gestion.Gtn_Aviso_Suspension == null) ? false : gestion.Gtn_Aviso_Suspension);
            gestion.Gtn_Formato_Infonavit = ((gestion.Gtn_Formato_Infonavit == null) ? false : gestion.Gtn_Formato_Infonavit);
            gestion.Gtn_Fotos_Propiedad = ((gestion.Gtn_Fotos_Propiedad == null) ? false : gestion.Gtn_Fotos_Propiedad);
            gestion.Gtn_Evaluo_Contacto = ((gestion.Gtn_Evaluo_Contacto == null) ? false : gestion.Gtn_Evaluo_Contacto);
            gestion.Gtn_Planeacion_Fianza = ((gestion.Gtn_Planeacion_Fianza == null) ? false : gestion.Gtn_Planeacion_Fianza);
            gestion.Gtn_Urbanizacion = ((gestion.Gtn_Urbanizacion == null) ? false : gestion.Gtn_Urbanizacion);
            gestion.Gtn_Credito_INFONAVIT = ((gestion.Gtn_Credito_INFONAVIT == null) ? false : gestion.Gtn_Credito_INFONAVIT);
            gestion.Gtn_Notaria = ((gestion.Gtn_Notaria == null) ? false : gestion.Gtn_Notaria);
            gestion.Gtn_Firma_Escrituras = ((gestion.Gtn_Firma_Escrituras == null) ? false : gestion.Gtn_Firma_Escrituras);

            if (ModelState.IsValid)
            {
                db.Entry(gestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("/Index");
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Gral_Nombre", gestion.Id_Cliente);
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", gestion.Id_Corretaje);
            return View(gestion);
        }

        /*EDITAR HABILITACION */
        // GET: Habilitacions/Edit/5
        public ActionResult Edith(int? id)
        {
            bool continuar = false;
            int idh = 0;
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }



            List<Habilitacion> habilitacions = new List<Habilitacion>();
            habilitacions = (db.Habilitacion.ToList());
            foreach (var searchid in habilitacions)
            {
                if (searchid.Id_Corretaje == id)
                {
                    idh = searchid.Id;
                    continuar = true;
                    break;
                }
                else
                {
                    continuar = false;
                }

            }
            if (continuar == true)
            {
                Habilitacion habilitacion = db.Habilitacion.Find(idh);
                if (habilitacion == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", habilitacion.Id_Corretaje);
                return View(habilitacion);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // POST: Habilitacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edith([Bind(Include = "Id,Hbt_Puertas,Hbt_Chapas,Hbt_Marcos_puertas,Hbt_Bisagras,Hbt_Taza,Hbt_Lavamanos,Hbt_Bastago,Hbt_Chapeton,Hbt_Maneral,Hbt_Regadera_completa,Hbt_Kit_lavamanos,Hbt_Kit_taza,Hbt_Rosetas,Hbt_Apagador_sencillo,Hbt_Conector_sencillo,Hbt_Apagador_doble,Hbt_Conector_apagador,Hbt_Domo,Hbt_Ventanas,Hbt_Cableado,Hbt_Calibre_cableado,Hbt_Break_interior,Hbt_Break_medidor,Hbt_Pinturas,Hbt_AvisoSusp,Id_Corretaje, Hbt_ProgresoForm")] Habilitacion habilitacion)
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
                return RedirectToAction("/Index");
            }
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", habilitacion.Id_Corretaje);
            return View(habilitacion);
        }



        /*EDITAR VERIFICACION */
        // GET: Verificacions/Edit/5
        public ActionResult Editv(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            List<Verificacion> verificacions = new List<Verificacion>();
            List<Gestion> gestions = new List<Gestion>();

            bool continuar = false;
            int? idv = 0;
            int? idcliente = 0;

            gestions = db.Gestion.ToList();
            verificacions = db.Verificacion.ToList();
            Gestion gestion = db.Gestion.Find(id);
            idcliente = gestion.Id_Cliente;
            foreach (var searchidv in verificacions)
            {
                if (searchidv.Id_Cliente == idcliente)
                {
                    idv = searchidv.Id_Cliente;
                    continuar = true;
                    break;
                }
            }
            if (continuar == true)
            {

                Verificacion verificacion = db.Verificacion.Find(idv);
                if (verificacion == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Gral_Nombre", verificacion.Id_Cliente);
                return View(verificacion);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }
        }

        // POST: Verificacions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editv([Bind(Include = "Id,Vfn_Persona_fisica,Vfn_Visto_persona,Vfn_Tiempo_estimado,Vfn_Tiempo,Vfn_Tiene_costo,Vfn_Costo,Vfn_Trato_asesor,Vfn_Observaciones,Id_Cliente,Vfn_ProgresoForm")] Verificacion verificacion)
        {
            if (verificacion.Vfn_Persona_fisica == null) verificacion.Vfn_Persona_fisica = false;
            if (verificacion.Vfn_Visto_persona == null) verificacion.Vfn_Visto_persona = false;
            if (verificacion.Vfn_Tiempo_estimado == null) verificacion.Vfn_Tiempo_estimado = false;
            if (verificacion.Vfn_Tiene_costo == null) verificacion.Vfn_Tiene_costo = false;

            if (ModelState.IsValid)
            {
                db.Entry(verificacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("/Index");
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Gral_Nombre", verificacion.Id_Cliente);
            return View(verificacion);
        }


        /*EDITAR CONTABILIDAD */
        // GET: Contadurias/Edit/5
        public ActionResult Editconta(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //List<Gastos> gastos = new List<Gastos>(); <--------------------------------------------Jose Aqui Mero
            List<Contaduria> contadurias = new List<Contaduria>();

            bool continuar = false;

            int? idconta = 0;


            //gastos = db.Gastos.ToList(); <--------------------------------------------Jose Aqui Mero
            contadurias = db.Contaduria.ToList();

            foreach (var searchconta in contadurias)
            {
                if (searchconta.Id_Corretaje == id)
                {
                    idconta = searchconta.Id;
                    continuar = true;
                    break;
                }
            }
           

            if (continuar == true)
            {

                Contaduria contaduria = db.Contaduria.Find(idconta);
                if (contaduria == null)
                {
                    return HttpNotFound();
                }
                //ViewBag.Id_Gastos = new SelectList(db.Gastos, "Id", "Gst_Concepto", contaduria.Id_Gastos); <--------------------------------------------Jose Aqui Mero
                //ViewBag.Id_GastosContaduria = new SelectList(db.GastosContaduria, "Id", "Id", contaduria.Id_GastosContaduria); <--------------------------------------------Jose Aqui Mero
                return View(contaduria);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
        }

        // POST: Contadurias/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editconta([Bind(Include = "Id,Cnt_Presupuesto_gestion,Cnt_Presupuesto_corretaje,Cnt_Presupuesto_habilitacion,Cnt_Presupuesto,Id_Gastos,Id_GastosContaduria,Id_Corretaje")] Contaduria contaduria)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contaduria).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("/Index");
            }
            //ViewBag.Id_Gastos = new SelectList(db.Gastos, "Id", "Gst_Concepto", contaduria.Id_Gastos); <--------------------------------------------Jose Aqui Mero
            //ViewBag.Id_GastosContaduria = new SelectList(db.GastosContaduria, "Id", "Id", contaduria.Id_GastosContaduria); <--------------------------------------------Jose Aqui Mero
            return View(contaduria);
        }

        public JsonResult Buscarnombre(string SearchString = "",string Status="",string ArribaYAbajo = "")
        {
            if (ArribaYAbajo == "Ascendente")
            {
                if (SearchString == "" && Status == "Todas")
                {
                    var search = (from s in db.Corretaje join xd in db.Gestion on s.Id equals xd.Id_Corretaje into c from algo in c.DefaultIfEmpty() select new { corre = s.Id.ToString(), Cliente = s.Crt_Cliente_Nombre.ToString(),clienteapp= s.Crt_Cliente_ApPat.ToString(),clienteapm = s.Crt_Cliente_ApMat.ToString(), dire = s.Crt_Direccion.ToString(), status = s.Crt_Status.ToString(), compra = ((algo != null) ? algo.Cliente.Gral_Nombre.ToString() : "No Asignado"), compraapp = ((algo != null) ? algo.Cliente.Gral_Apellidopa.ToString() : "No Asignado"), compraapm = ((algo != null) ? algo.Cliente.Gral_Apellidoma.ToString() : "No Asignado"), idg = ((algo != null) ? algo.Id.ToString() : "No Asignado"),progresocorre = s.Crt_ProgresoForm,progresogesti = algo.Gtn_ProgresoForm }).OrderBy(s => s.corre).ToList();
                    //var search = (from s in db.Gestion select new {corre = ((s.Corretaje!=null)?s.Corretaje.Id.ToString():"No Asignado"), s.Id,Cliente= ((s.Corretaje != null) ? s.Corretaje.Crt_Cliente_Nombre.ToString() : "No Asignado"),direccion= ((s.Corretaje != null) ? s.Corretaje.Crt_Direccion.ToString() : "No Asignado")}).OrderBy(s => s.Id).ToList();
                    return Json(search, JsonRequestBehavior.AllowGet);
                }

                else if (SearchString == "")
                {
                    var search = (from s in db.Corretaje join xd in db.Gestion on s.Id equals xd.Id_Corretaje into c from algo in c.DefaultIfEmpty() where s.Crt_Status == Status select new { corre = s.Id.ToString(), Cliente = s.Crt_Cliente_Nombre.ToString(), clienteapp = s.Crt_Cliente_ApPat.ToString(), clienteapm = s.Crt_Cliente_ApMat.ToString(), dire = s.Crt_Direccion.ToString(), status = s.Crt_Status.ToString(), compra = ((algo != null) ? algo.Cliente.Gral_Nombre.ToString() : "No Asignado"), compraapp = ((algo != null) ? algo.Cliente.Gral_Apellidopa.ToString() : "No Asignado"), compraapm = ((algo != null) ? algo.Cliente.Gral_Apellidoma.ToString() : "No Asignado"), idg = ((algo != null) ? algo.Id.ToString() : "No Asignado"), progresocorre = s.Crt_ProgresoForm, progresogesti = algo.Gtn_ProgresoForm }).OrderBy(s => s.corre).ToList();
                    //var search = (from s in db.Gestion select new {corre = ((s.Corretaje!=null)?s.Corretaje.Id.ToString():"No Asignado"), s.Id,Cliente= ((s.Corretaje != null) ? s.Corretaje.Crt_Cliente_Nombre.ToString() : "No Asignado"),direccion= ((s.Corretaje != null) ? s.Corretaje.Crt_Direccion.ToString() : "No Asignado")}).OrderBy(s => s.Id).ToList();
                    return Json(search, JsonRequestBehavior.AllowGet);

                }

                else if (Status == "Todas" && SearchString != "")
                {
                    var search = (from s in db.Corretaje join xd in db.Gestion on s.Id equals xd.Id_Corretaje into c from algo in c.DefaultIfEmpty() where algo.Cliente.Gral_Nombre.Contains(SearchString) || s.Crt_Cliente_Nombre.Contains(SearchString) select new { corre = s.Id.ToString(), Cliente = s.Crt_Cliente_Nombre.ToString(), clienteapp = s.Crt_Cliente_ApPat.ToString(), clienteapm = s.Crt_Cliente_ApMat.ToString(), dire = s.Crt_Direccion.ToString(), status = s.Crt_Status.ToString(), compra = ((algo != null) ? algo.Cliente.Gral_Nombre.ToString() : "No Asignado"), compraapp = ((algo != null) ? algo.Cliente.Gral_Apellidopa.ToString() : "No Asignado"), compraapm = ((algo != null) ? algo.Cliente.Gral_Apellidoma.ToString() : "No Asignado"), idg = ((algo != null) ? algo.Id.ToString() : "No Asignado"), progresocorre = s.Crt_ProgresoForm, progresogesti = algo.Gtn_ProgresoForm }).OrderBy(s => s.corre).ToList();
                    return Json(search, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var search = (from s in db.Corretaje join xd in db.Gestion on s.Id equals xd.Id_Corretaje into c from algo in c.DefaultIfEmpty() where algo.Cliente.Gral_Nombre.Contains(SearchString) || s.Crt_Cliente_Nombre.Contains(SearchString) && s.Crt_Status == Status select new { corre = s.Id.ToString(), Cliente = s.Crt_Cliente_Nombre.ToString(), clienteapp = s.Crt_Cliente_ApPat.ToString(), clienteapm = s.Crt_Cliente_ApMat.ToString(), dire = s.Crt_Direccion.ToString(), status = s.Crt_Status.ToString(), compra = ((algo != null) ? algo.Cliente.Gral_Nombre.ToString() : "No Asignado"), compraapp = ((algo != null) ? algo.Cliente.Gral_Apellidopa.ToString() : "No Asignado"), compraapm = ((algo != null) ? algo.Cliente.Gral_Apellidoma.ToString() : "No Asignado"), idg = ((algo != null) ? algo.Id.ToString() : "No Asignado"), progresocorre = s.Crt_ProgresoForm, progresogesti = algo.Gtn_ProgresoForm }).OrderBy(s => s.corre).ToList();
                    return Json(search, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                if (SearchString == "" && Status == "Todas")
                {
                    var search = (from s in db.Corretaje join xd in db.Gestion on s.Id equals xd.Id_Corretaje into c from algo in c.DefaultIfEmpty() select new { corre = s.Id.ToString(), Cliente = s.Crt_Cliente_Nombre.ToString(), clienteapp = s.Crt_Cliente_ApPat.ToString(), clienteapm = s.Crt_Cliente_ApMat.ToString(), dire = s.Crt_Direccion.ToString(), status = s.Crt_Status.ToString(), compra = ((algo != null) ? algo.Cliente.Gral_Nombre.ToString() : "No Asignado"), compraapp = ((algo != null) ? algo.Cliente.Gral_Apellidopa.ToString() : "No Asignado"), compraapm = ((algo != null) ? algo.Cliente.Gral_Apellidoma.ToString() : "No Asignado"), idg = ((algo != null) ? algo.Id.ToString() : "No Asignado"), progresocorre = s.Crt_ProgresoForm, progresogesti = algo.Gtn_ProgresoForm }).OrderByDescending(s => s.corre).ToList();
                    //var search = (from s in db.Gestion select new {corre = ((s.Corretaje!=null)?s.Corretaje.Id.ToString():"No Asignado"), s.Id,Cliente= ((s.Corretaje != null) ? s.Corretaje.Crt_Cliente_Nombre.ToString() : "No Asignado"),direccion= ((s.Corretaje != null) ? s.Corretaje.Crt_Direccion.ToString() : "No Asignado")}).OrderBy(s => s.Id).ToList();
                    return Json(search, JsonRequestBehavior.AllowGet);
                }

                else if (SearchString == "")
                {
                    var search = (from s in db.Corretaje join xd in db.Gestion on s.Id equals xd.Id_Corretaje into c from algo in c.DefaultIfEmpty() where s.Crt_Status == Status select new { corre = s.Id.ToString(), Cliente = s.Crt_Cliente_Nombre.ToString(), clienteapp = s.Crt_Cliente_ApPat.ToString(), clienteapm = s.Crt_Cliente_ApMat.ToString(), dire = s.Crt_Direccion.ToString(), status = s.Crt_Status.ToString(), compra = ((algo != null) ? algo.Cliente.Gral_Nombre.ToString() : "No Asignado"), compraapp = ((algo != null) ? algo.Cliente.Gral_Apellidopa.ToString() : "No Asignado"), compraapm = ((algo != null) ? algo.Cliente.Gral_Apellidoma.ToString() : "No Asignado"), idg = ((algo != null) ? algo.Id.ToString() : "No Asignado"), progresocorre = s.Crt_ProgresoForm, progresogesti = algo.Gtn_ProgresoForm }).OrderByDescending(s => s.corre).ToList();
                    //var search = (from s in db.Gestion select new {corre = ((s.Corretaje!=null)?s.Corretaje.Id.ToString():"No Asignado"), s.Id,Cliente= ((s.Corretaje != null) ? s.Corretaje.Crt_Cliente_Nombre.ToString() : "No Asignado"),direccion= ((s.Corretaje != null) ? s.Corretaje.Crt_Direccion.ToString() : "No Asignado")}).OrderBy(s => s.Id).ToList();
                    return Json(search, JsonRequestBehavior.AllowGet);

                }

                else if (Status == "Todas" && SearchString != "")
                {
                    var search = (from s in db.Corretaje join xd in db.Gestion on s.Id equals xd.Id_Corretaje into c from algo in c.DefaultIfEmpty() where algo.Cliente.Gral_Nombre.Contains(SearchString) || s.Crt_Cliente_Nombre.Contains(SearchString) select new { corre = s.Id.ToString(), Cliente = s.Crt_Cliente_Nombre.ToString(), clienteapp = s.Crt_Cliente_ApPat.ToString(), clienteapm = s.Crt_Cliente_ApMat.ToString(), dire = s.Crt_Direccion.ToString(), status = s.Crt_Status.ToString(), compra = ((algo != null) ? algo.Cliente.Gral_Nombre.ToString() : "No Asignado"), compraapp = ((algo != null) ? algo.Cliente.Gral_Apellidopa.ToString() : "No Asignado"), compraapm = ((algo != null) ? algo.Cliente.Gral_Apellidoma.ToString() : "No Asignado"), idg = ((algo != null) ? algo.Id.ToString() : "No Asignado"), progresocorre = s.Crt_ProgresoForm, progresogesti = algo.Gtn_ProgresoForm }).OrderByDescending(s => s.corre).ToList();
                    return Json(search, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var search = (from s in db.Corretaje join xd in db.Gestion on s.Id equals xd.Id_Corretaje into c from algo in c.DefaultIfEmpty() where algo.Cliente.Gral_Nombre.Contains(SearchString) || s.Crt_Cliente_Nombre.Contains(SearchString) && s.Crt_Status == Status select new { corre = s.Id.ToString(), Cliente = s.Crt_Cliente_Nombre.ToString(), clienteapp = s.Crt_Cliente_ApPat.ToString(), clienteapm = s.Crt_Cliente_ApMat.ToString(), dire = s.Crt_Direccion.ToString(), status = s.Crt_Status.ToString(), compra = ((algo != null) ? algo.Cliente.Gral_Nombre.ToString() : "No Asignado"), compraapp = ((algo != null) ? algo.Cliente.Gral_Apellidopa.ToString() : "No Asignado"), compraapm = ((algo != null) ? algo.Cliente.Gral_Apellidoma.ToString() : "No Asignado"), idg = ((algo != null) ? algo.Id.ToString() : "No Asignado"), progresocorre = s.Crt_ProgresoForm, progresogesti = algo.Gtn_ProgresoForm }).OrderByDescending(s => s.corre).ToList();
                    return Json(search, JsonRequestBehavior.AllowGet);
                }
            }
           
        }

        public List<SelectListItem> EstCivil()
        {
            return new List<SelectListItem>() {
                new SelectListItem()
            {
                Text = "Soltero",
                Value = "Soltero"
            },
            new SelectListItem()
            {
                Text = "Casado",
                Value = "Casado"
            },
            new SelectListItem()
            {
                Text = "Viudo",
                Value = "Viudo"
            },
            new SelectListItem()
            {
                Text = "Divorsiado",
                Value = "Divorsiado"
            }
        };

        }

        public string convertTo64(HttpPostedFileBase cvFile)
        {
            if (cvFile == null)
            {
                return " ";
            }
            byte[] fileInBytes = new byte[cvFile.ContentLength];
            using (BinaryReader theReader = new BinaryReader(cvFile.InputStream))
            {
                fileInBytes = theReader.ReadBytes(cvFile.ContentLength);
            }
            string fileAsString = Convert.ToBase64String(fileInBytes);
            return fileAsString;
        }
    }
}
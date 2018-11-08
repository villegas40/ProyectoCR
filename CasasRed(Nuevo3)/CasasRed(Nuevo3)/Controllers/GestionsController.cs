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
    public class GestionsController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: Gestions
        public ActionResult Index()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Gestion" || Session["Tipo"].ToString() == "Administrador")
            {
                var gestion = db.Gestion.Include(g => g.Cliente).Include(g => g.Corretaje);
                return View(gestion.ToList());
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Gestions/Details/5
        public ActionResult Details(int? id)
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
                Gestion gestion = db.Gestion.Find(id);
                if (gestion == null)
                {
                    return HttpNotFound();
                }
                return View(gestion);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }           
        }

        // GET: Gestions/Create
        public ActionResult Create()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Gestion" || Session["Tipo"].ToString() == "Administrador")
            {
                ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Gral_Nombre");
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

        // POST: Gestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Gtn_Escrituras,Gtn_Planta_Cartografica,Gtn_Predial,Gtn_Recibo_Luz,Gtn_Recibo_Agua,Gtn_Acta_Nacimiento,Gtn_IFE_Copia,Gtn_Sol_Ret_Ifo,Gtn_Cert_Hip,Gtn_Cert_Fiscal,Gtn_Sol_Estado,Gtn_Junta_URBI,Gtn_Agua_Pago_Inf,Gtn_Cert_Cartogr,Gtn_No_Oficial,Gtn_Avaluo,Gtn_CT_Banco,Gtn_Aviso_Suspension,Gtn_Formato_Infonavit,Gtn_Fotos_Propiedad,Gtn_Evaluo_Contacto,Gtn_Planeacion_Fianza,Gtn_Urbanizacion,Gtn_Credito_INFONAVIT,Gtn_Notaria,Gtn_Firma_Escrituras,Id_Corretaje,Id_Cliente,Gtn_ProgresoForm,Id_Usuario,Gtn_ReciboActualizado,Gtn_Taller,Gtn_CuentaBancaria,Gtn_Acta_Nacim_Cony,Gtn_Acta_Matrimonio,Gtn_DatosGnrl_Comp,Gtn_Comp_Domicilio,Gtn_Recibo_Nomina,Gtn_RFC_Comprador,Gtn_CURP_Comprador,Gtn_RFC_Conyugue,Gtn_CURP_Conyugue,Gtn_Inscp_INFONAVIT,Gtn_Acta_Nac_Ven,Gtn_Acta_Nac_Cony_Ven,Gtn_Acta_Matrimonio_Ven,Gtn_IFE_Copia_Ven,Gtn_RFC_Ven,Gtn_CURP_Ven,Gtn_RFC_Conyu_Ven,Gtn_CURP_Conyu_Ven,Gtn_Entrega_Vivienda,Gtn_FechaAlta,Gtn_Aviso_Ret")] Gestion gestion)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Gestion" || Session["Tipo"].ToString() == "Administrador")
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
                gestion.Gtn_Acta_Nacim_Cony = ((gestion.Gtn_Acta_Nacim_Cony == null) ? false : gestion.Gtn_Acta_Nacim_Cony);
                gestion.Gtn_Acta_Matrimonio = ((gestion.Gtn_Acta_Matrimonio == null) ? false : gestion.Gtn_Acta_Matrimonio);
                gestion.Gtn_DatosGnrl_Comp = ((gestion.Gtn_DatosGnrl_Comp == null) ? false : gestion.Gtn_DatosGnrl_Comp);
                gestion.Gtn_Comp_Domicilio = ((gestion.Gtn_Comp_Domicilio == null) ? false : gestion.Gtn_Comp_Domicilio);
                gestion.Gtn_Recibo_Nomina = ((gestion.Gtn_Recibo_Nomina == null) ? false : gestion.Gtn_Recibo_Nomina);
                gestion.Gtn_RFC_Comprador = ((gestion.Gtn_RFC_Comprador == null) ? false : gestion.Gtn_RFC_Comprador);
                gestion.Gtn_CURP_Comprador = ((gestion.Gtn_CURP_Comprador == null) ? false : gestion.Gtn_CURP_Comprador);
                gestion.Gtn_RFC_Conyugue = ((gestion.Gtn_RFC_Conyugue == null) ? false : gestion.Gtn_RFC_Conyugue);
                gestion.Gtn_CURP_Conyugue = ((gestion.Gtn_CURP_Conyugue == null) ? false : gestion.Gtn_CURP_Conyugue);
                gestion.Gtn_Inscp_INFONAVIT = ((gestion.Gtn_Inscp_INFONAVIT == null) ? false : gestion.Gtn_Inscp_INFONAVIT);
                gestion.Gtn_Acta_Nac_Ven = ((gestion.Gtn_Acta_Nac_Ven == null) ? false : gestion.Gtn_Acta_Nac_Ven);
                gestion.Gtn_Acta_Nac_Cony_Ven = ((gestion.Gtn_Acta_Nac_Cony_Ven == null) ? false : gestion.Gtn_Acta_Nac_Cony_Ven);
                gestion.Gtn_Acta_Matrimonio_Ven = ((gestion.Gtn_Acta_Matrimonio_Ven == null) ? false : gestion.Gtn_Acta_Matrimonio_Ven);
                gestion.Gtn_IFE_Copia_Ven = ((gestion.Gtn_IFE_Copia_Ven == null) ? false : gestion.Gtn_IFE_Copia_Ven);
                gestion.Gtn_RFC_Ven = ((gestion.Gtn_RFC_Ven == null) ? false : gestion.Gtn_RFC_Ven);
                gestion.Gtn_CURP_Ven = ((gestion.Gtn_CURP_Ven == null) ? false : gestion.Gtn_CURP_Ven);
                gestion.Gtn_RFC_Conyu_Ven = ((gestion.Gtn_RFC_Conyu_Ven == null) ? false : gestion.Gtn_RFC_Conyu_Ven);
                gestion.Gtn_CURP_Conyu_Ven = ((gestion.Gtn_CURP_Conyu_Ven == null) ? false : gestion.Gtn_CURP_Conyu_Ven);
                gestion.Gtn_Entrega_Vivienda = ((gestion.Gtn_Entrega_Vivienda == null) ? false : gestion.Gtn_Entrega_Vivienda);
                gestion.Gtn_Taller = ((gestion.Gtn_Taller == null) ? false : gestion.Gtn_Taller);
                gestion.Gtn_CuentaBancaria = ((gestion.Gtn_CuentaBancaria == null) ? false : gestion.Gtn_CuentaBancaria);
                gestion.Gtn_ReciboActualizado = ((gestion.Gtn_ReciboActualizado == null) ? false : gestion.Gtn_ReciboActualizado);
                gestion.Gtn_Aviso_Ret = ((gestion.Gtn_Aviso_Ret == null) ? false : gestion.Gtn_Aviso_Ret);

                if (ModelState.IsValid)
                {
                    gestion.Gtn_FechaAlta = DateTime.Now;
                    db.Gestion.Add(gestion);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Gral_Nombre", gestion.Id_Cliente);
                ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", gestion.Id_Corretaje);
                return View(gestion);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Gestions/Edit/5
        public ActionResult Edit(int? id)
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
                Gestion gestion = db.Gestion.Find(id);
                if (gestion == null)
                {
                    return HttpNotFound();
                }
                ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Gral_Nombre", gestion.Id_Cliente);
                ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", gestion.Id_Corretaje);
                return View(gestion);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
           
        }

        // POST: Gestions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Gtn_Escrituras,Gtn_Planta_Cartografica,Gtn_Predial,Gtn_Recibo_Luz,Gtn_Recibo_Agua,Gtn_Acta_Nacimiento,Gtn_IFE_Copia,Gtn_Sol_Ret_Ifo,Gtn_Cert_Hip,Gtn_Cert_Fiscal,Gtn_Sol_Estado,Gtn_Junta_URBI,Gtn_Agua_Pago_Inf,Gtn_Cert_Cartogr,Gtn_No_Oficial,Gtn_Avaluo,Gtn_CT_Banco,Gtn_Aviso_Suspension,Gtn_Formato_Infonavit,Gtn_Fotos_Propiedad,Gtn_Evaluo_Contacto,Gtn_Planeacion_Fianza,Gtn_Urbanizacion,Gtn_Credito_INFONAVIT,Gtn_Notaria,Gtn_Firma_Escrituras,Id_Corretaje,Id_Cliente,Gtn_ProgresoForm,Id_Usuario,Gtn_ReciboActualizado,Gtn_Taller,Gtn_CuentaBancaria,Gtn_Acta_Nacim_Cony,Gtn_Acta_Matrimonio,Gtn_DatosGnrl_Comp,Gtn_Comp_Domicilio,Gtn_Recibo_Nomina,Gtn_RFC_Comprador,Gtn_CURP_Comprador,Gtn_RFC_Conyugue,Gtn_CURP_Conyugue,Gtn_Inscp_INFONAVIT,Gtn_Acta_Nac_Ven,Gtn_Acta_Nac_Cony_Ven,Gtn_Acta_Matrimonio_Ven,Gtn_IFE_Copia_Ven,Gtn_RFC_Ven,Gtn_CURP_Ven,Gtn_RFC_Conyu_Ven,Gtn_CURP_Conyu_Ven,Gtn_Entrega_Vivienda,Gtn_FechaAlta,Gtn_Aviso_Ret")] Gestion gestion)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Gestion" || Session["Tipo"].ToString() == "Administrador")
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
                gestion.Gtn_Acta_Nacim_Cony = ((gestion.Gtn_Acta_Nacim_Cony == null) ? false : gestion.Gtn_Acta_Nacim_Cony);
                gestion.Gtn_Acta_Matrimonio = ((gestion.Gtn_Acta_Matrimonio == null) ? false : gestion.Gtn_Acta_Matrimonio);
                gestion.Gtn_DatosGnrl_Comp = ((gestion.Gtn_DatosGnrl_Comp == null) ? false : gestion.Gtn_DatosGnrl_Comp);
                gestion.Gtn_Comp_Domicilio = ((gestion.Gtn_Comp_Domicilio == null) ? false : gestion.Gtn_Comp_Domicilio);
                gestion.Gtn_Recibo_Nomina = ((gestion.Gtn_Recibo_Nomina == null) ? false : gestion.Gtn_Recibo_Nomina);
                gestion.Gtn_RFC_Comprador = ((gestion.Gtn_RFC_Comprador == null) ? false : gestion.Gtn_RFC_Comprador);
                gestion.Gtn_CURP_Comprador = ((gestion.Gtn_CURP_Comprador == null) ? false : gestion.Gtn_CURP_Comprador);
                gestion.Gtn_RFC_Conyugue = ((gestion.Gtn_RFC_Conyugue == null) ? false : gestion.Gtn_RFC_Conyugue);
                gestion.Gtn_CURP_Conyugue = ((gestion.Gtn_CURP_Conyugue == null) ? false : gestion.Gtn_CURP_Conyugue);
                gestion.Gtn_Inscp_INFONAVIT = ((gestion.Gtn_Inscp_INFONAVIT == null) ? false : gestion.Gtn_Inscp_INFONAVIT);
                gestion.Gtn_Acta_Nac_Ven = ((gestion.Gtn_Acta_Nac_Ven == null) ? false : gestion.Gtn_Acta_Nac_Ven);
                gestion.Gtn_Acta_Nac_Cony_Ven = ((gestion.Gtn_Acta_Nac_Cony_Ven == null) ? false : gestion.Gtn_Acta_Nac_Cony_Ven);
                gestion.Gtn_Acta_Matrimonio_Ven = ((gestion.Gtn_Acta_Matrimonio_Ven == null) ? false : gestion.Gtn_Acta_Matrimonio_Ven);
                gestion.Gtn_IFE_Copia_Ven = ((gestion.Gtn_IFE_Copia_Ven == null) ? false : gestion.Gtn_IFE_Copia_Ven);
                gestion.Gtn_RFC_Ven = ((gestion.Gtn_RFC_Ven == null) ? false : gestion.Gtn_RFC_Ven);
                gestion.Gtn_CURP_Ven = ((gestion.Gtn_CURP_Ven == null) ? false : gestion.Gtn_CURP_Ven);
                gestion.Gtn_RFC_Conyu_Ven = ((gestion.Gtn_RFC_Conyu_Ven == null) ? false : gestion.Gtn_RFC_Conyu_Ven);
                gestion.Gtn_CURP_Conyu_Ven = ((gestion.Gtn_CURP_Conyu_Ven == null) ? false : gestion.Gtn_CURP_Conyu_Ven);
                gestion.Gtn_Entrega_Vivienda = ((gestion.Gtn_Entrega_Vivienda == null) ? false : gestion.Gtn_Entrega_Vivienda);
                gestion.Gtn_Taller = ((gestion.Gtn_Taller == null) ? false : gestion.Gtn_Taller);
                gestion.Gtn_CuentaBancaria = ((gestion.Gtn_CuentaBancaria == null) ? false : gestion.Gtn_CuentaBancaria);
                gestion.Gtn_ReciboActualizado = ((gestion.Gtn_ReciboActualizado == null) ? false : gestion.Gtn_ReciboActualizado);
                gestion.Gtn_Aviso_Ret = ((gestion.Gtn_Aviso_Ret == null) ? false : gestion.Gtn_Aviso_Ret);
                gestion.Gtn_FechaAlta = ((gestion.Gtn_FechaAlta == null) ? DateTime.Now : gestion.Gtn_FechaAlta);

                if (ModelState.IsValid)
                {
                    db.Entry(gestion).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Gral_Nombre", gestion.Id_Cliente);
                ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", gestion.Id_Corretaje);
                return View(gestion);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Gestions/Delete/5
        public ActionResult Delete(int? id)
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
                Gestion gestion = db.Gestion.Find(id);
                if (gestion == null)
                {
                    return HttpNotFound();
                }
                return View(gestion);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // POST: Gestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Gestion" || Session["Tipo"].ToString() == "Administrador")
            {
                Gestion gestion = db.Gestion.Find(id);
                db.Gestion.Remove(gestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                LoginController lc = new LoginController();
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

        //Crear registro vacio al dar de alta cliente
        public string GestionCrear(Gestion gestion, int cliente_id, int corretaje_id)
        {
            
            CasasRedEntities CS = new CasasRedEntities();
            var gestio = (from a in db.Gestion where corretaje_id == a.Id_Corretaje select a).FirstOrDefault();
            if (gestio != null)
            {
                gestio.Id_Cliente = cliente_id;
                db.SaveChanges();
            }
            else
            {
                //Crea nuevo registro
                Gestion gestion_obj = new Gestion
                {
                    Gtn_Escrituras = false,
                    Gtn_Planta_Cartografica = false,
                    Gtn_Predial = false,
                    Gtn_Recibo_Luz = false,
                    Gtn_Recibo_Agua = false,
                    Gtn_Acta_Nacimiento = false,
                    Gtn_IFE_Copia = false,
                    Gtn_Sol_Ret_Ifo = false,
                    Gtn_Cert_Hip = false,
                    Gtn_Cert_Fiscal = false,
                    Gtn_Sol_Estado = false,
                    Gtn_Junta_URBI = false,
                    Gtn_Agua_Pago_Inf = false,
                    Gtn_Cert_Cartogr = false,
                    Gtn_No_Oficial = false,
                    Gtn_Avaluo = false,
                    Gtn_CT_Banco = false,
                    Gtn_Aviso_Suspension = false,
                    Gtn_Formato_Infonavit = false,
                    Gtn_Fotos_Propiedad = false,
                    Gtn_Evaluo_Contacto = false,
                    Gtn_Planeacion_Fianza = false,
                    Gtn_Urbanizacion = false,
                    Gtn_Credito_INFONAVIT = false,
                    Gtn_Notaria = false,
                    Gtn_Firma_Escrituras = false,
                    Id_Cliente = cliente_id,
                    Id_Corretaje = corretaje_id,
                    Gtn_ProgresoForm = 0,
                    Gtn_FechaAlta = DateTime.Now,
                    Gtn_CuentaBancaria = false,
                    Gtn_ReciboActualizado = false,
                    Gtn_Taller = false,
                    Gtn_Acta_Nacim_Cony = false,
                    Gtn_Acta_Matrimonio = false,
                    Gtn_DatosGnrl_Comp = false,
                    Gtn_Comp_Domicilio = false,
                    Gtn_Recibo_Nomina = false,
                    Gtn_RFC_Comprador = false,
                    Gtn_CURP_Comprador = false,
                    Gtn_RFC_Conyugue = false,
                    Gtn_CURP_Conyugue = false,
                    Gtn_Inscp_INFONAVIT = false,
                    Gtn_Acta_Nac_Ven = false,
                    Gtn_Acta_Nac_Cony_Ven = false,
                    Gtn_Acta_Matrimonio_Ven = false,
                    Gtn_IFE_Copia_Ven = false,
                    Gtn_RFC_Ven = false,
                    Gtn_CURP_Ven = false,
                    Gtn_RFC_Conyu_Ven = false,
                    Gtn_CURP_Conyu_Ven = false,
                    Gtn_Entrega_Vivienda = false,
                    Gtn_Aviso_Ret = false,
                };

                CS.Gestion.Add(gestion_obj);
                CS.SaveChanges();
            }


            return "String que funciona...";
        }

        //Mostrado en pantalla 
        //Obteber las casas
        public JsonResult BuscarGestion(string filtro = "", int pagina = 1, int registrosPagina = 15,int mes = 0 , int ano = 0)
        {
            if (filtro == "" && mes == 0 && ano == 0)
            {
                int totalPaginas = (int)Math.Ceiling((double)db.Gestion.Count() / registrosPagina);
                var busqueda = (from a in db.Gestion select new { a.Id_Corretaje, a.Id, casa = a.Corretaje.Crt_Direccion, cliente = (a.Cliente.Gral_Nombre == null) ? "Cliente no asignado" : a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma, fecha = SqlFunctions.DateName("year", a.Cliente.Gral_Fechaalta).Trim() + "/" + SqlFunctions.StringConvert((double)a.Cliente.Gral_Fechaalta.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("day", a.Cliente.Gral_Fechaalta).Trim(), a.Corretaje.Crt_Status, a.Gtn_ProgresoForm, total = totalPaginas }).OrderBy(a => a.Id).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();

                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            //solo filtro
            else if (filtro != "" && mes == 0 && ano == 0)
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Gestion where (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma).Contains(filtro) || a.Cliente.Corretaje.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Gestion where (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma).Contains(filtro) || a.Cliente.Corretaje.Crt_Direccion.Contains(filtro) select new { a.Id_Corretaje, a.Id, a.Cliente.Corretaje.Crt_Direccion, fecha = SqlFunctions.DateName("year", a.Cliente.Gral_Fechaalta).Trim() + "/" + SqlFunctions.StringConvert((double)a.Cliente.Gral_Fechaalta.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("day", a.Cliente.Gral_Fechaalta).Trim(), a.Cliente.Corretaje.Crt_Status, cliente = (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma), a.Gtn_ProgresoForm, total = totalPaginas }).OrderBy(a => a.Crt_Direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            //solo mes sin año y sin filtro
            else if (mes != 0 && filtro == "" && ano == 0)
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Gestion where (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma).Contains(filtro) || a.Cliente.Corretaje.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Gestion where a.Gtn_FechaAlta.Value.Month.Equals(mes) select new { a.Id_Corretaje, a.Id, a.Cliente.Corretaje.Crt_Direccion, fecha = SqlFunctions.DateName("year", a.Cliente.Gral_Fechaalta).Trim() + "/" + SqlFunctions.StringConvert((double)a.Cliente.Gral_Fechaalta.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("day", a.Cliente.Gral_Fechaalta).Trim(), a.Cliente.Corretaje.Crt_Status, cliente = (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma), a.Gtn_ProgresoForm, total = totalPaginas }).OrderBy(a => a.Crt_Direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            //solo ano sin mes y sin filtro
            else if (ano != 0 && filtro == "" && mes == 0)
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Gestion where (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma).Contains(filtro) || a.Cliente.Corretaje.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Gestion where a.Gtn_FechaAlta.Value.Year.Equals(ano) select new { a.Id_Corretaje, a.Id, a.Cliente.Corretaje.Crt_Direccion, fecha = SqlFunctions.DateName("year", a.Cliente.Gral_Fechaalta).Trim() + "/" + SqlFunctions.StringConvert((double)a.Cliente.Gral_Fechaalta.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("day", a.Cliente.Gral_Fechaalta).Trim(), a.Cliente.Corretaje.Crt_Status, cliente = (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma), a.Gtn_ProgresoForm, total = totalPaginas }).OrderBy(a => a.Crt_Direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            //ano y filtro sin mes 
            else if (ano != 0 && filtro != "" && mes == 0)
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Gestion where (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma).Contains(filtro) || a.Cliente.Corretaje.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Gestion where ((a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma).Contains(filtro) || a.Cliente.Corretaje.Crt_Direccion.Contains(filtro)) && a.Gtn_FechaAlta.Value.Year.Equals(ano) select new { a.Id_Corretaje, a.Id, a.Cliente.Corretaje.Crt_Direccion, fecha = SqlFunctions.DateName("year", a.Cliente.Gral_Fechaalta).Trim() + "/" + SqlFunctions.StringConvert((double)a.Cliente.Gral_Fechaalta.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("day", a.Cliente.Gral_Fechaalta).Trim(), a.Cliente.Corretaje.Crt_Status, cliente = (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma), a.Gtn_ProgresoForm, total = totalPaginas }).OrderBy(a => a.Crt_Direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }

            //mes y filtro sin a;o
            else if (filtro != "" && mes != 0 && ano == 0)
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Gestion where (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma).Contains(filtro) || a.Cliente.Corretaje.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Gestion where ((a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma).Contains(filtro) || a.Cliente.Corretaje.Crt_Direccion.Contains(filtro)) && a.Gtn_FechaAlta.Value.Month.Equals(mes) select new { a.Id_Corretaje, a.Id, a.Cliente.Corretaje.Crt_Direccion, fecha = SqlFunctions.DateName("year", a.Cliente.Gral_Fechaalta).Trim() + "/" + SqlFunctions.StringConvert((double)a.Cliente.Gral_Fechaalta.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("day", a.Cliente.Gral_Fechaalta).Trim(), a.Cliente.Corretaje.Crt_Status, cliente = (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma), a.Gtn_ProgresoForm, total = totalPaginas }).OrderBy(a => a.Crt_Direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            //all
            else
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Gestion where (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma).Contains(filtro) || a.Cliente.Corretaje.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Gestion where ((a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma).Contains(filtro) || a.Cliente.Corretaje.Crt_Direccion.Contains(filtro)) && a.Gtn_FechaAlta.Value.Month.Equals(mes) && a.Gtn_FechaAlta.Value.Year.Equals(ano) select new { a.Id_Corretaje, a.Id, a.Cliente.Corretaje.Crt_Direccion, fecha = SqlFunctions.DateName("year", a.Cliente.Gral_Fechaalta).Trim() + "/" + SqlFunctions.StringConvert((double)a.Cliente.Gral_Fechaalta.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("day", a.Cliente.Gral_Fechaalta).Trim(), a.Cliente.Corretaje.Crt_Status, cliente = (a.Cliente.Gral_Nombre + " " + a.Cliente.Gral_Apellidopa + " " + a.Cliente.Gral_Apellidoma), a.Gtn_ProgresoForm, total = totalPaginas }).OrderBy(a => a.Crt_Direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using CasasRed_Nuevo3_.Models;
//Agregado
using Hangfire;

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

        /*RECORDATORIOS*/
        //Vista para mostrar recordatorios
        public ActionResult Recordatorios(int id)
        {
            var recordatorios = (from c in db.Recordatorio where c.Rcd_Id_Gestion == id select new RecordatoriosViewModel {
                Id = c.Rcd_Id,
                Titulo = c.Rcd_Titulo,
                Casa = c.Gestion.Corretaje.Crt_Direccion,
                Anio = c.Rcd_Anio.Value,
                Mes = c.Rcd_Mes.Value,
                Dia = c.Rcd_Dia.Value,
                Hora = c.Rcd_Hora.Value,
                Minuto = c.Rcd_Minuto.Value

            }).ToList();

            //Este id es el que se mandará a create
            ViewBag.id = id;
            ViewBag.Recordatorios = recordatorios;

            return View();
        }

        //ViewModel para retornar los recordatorios utilizando una consulta
        public class RecordatoriosViewModel
        {
            public int Id { get; set; }
            public string Titulo { get; set; }
            public string Casa { get; set; }
            public int Anio { get; set; }
            public int Mes { get; set; }
            public int Dia { get; set; }
            public int Hora { get; set; }
            public int Minuto { get; set; }
        }


        //Vista para dar de Alta recordatorios
        public ActionResult CrearRecordatorios(int id)
        {
            //Id del registro de gestion
            ViewBag.id = id;

            //Selectlist Titulos de Recordatorio
            var listaGest = new SelectList(new[] {
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

            //Select list mes
            var listaMes = new SelectList(new[] {
                new {value = 0, text = "Seleccione mes...."},
                new {value = 1, text = "Enero"},
                new {value = 2, text = "Febrero"},
                new {value = 3, text = "Marzo"},
                new {value = 4, text = "Abril"},
                new {value = 5, text = "Mayo"},
                new {value = 6, text = "Junio"},
                new {value = 7, text = "Julio"},
                new {value = 8, text = "Agosto"},
                new {value = 9, text = "Septiembre"},
                new {value = 10, text = "Octubre"},
                new {value = 11, text = "Noviembre"},
                new {value = 12, text = "Diciembre"},
            }, "value", "text", 0);

            //Select list hora
            var listaHora = new SelectList(new[] {
                new {value = 0, text = "00"},
                new {value = 1, text = "01"},
                new {value = 2, text = "02"},
                new {value = 3, text = "03"},
                new {value = 4, text = "04"},
                new {value = 5, text = "05"},
                new {value = 6, text = "06"},
                new {value = 7, text = "07"},
                new {value = 8, text = "08"},
                new {value = 9, text = "09"},
                new {value = 10, text = "10"},
                new {value = 11, text = "11"},
                new {value = 12, text = "12"},
                new {value = 13, text = "13"},
                new {value = 14, text = "14"},
                new {value = 15, text = "15"},
                new {value = 16, text = "16"},
                new {value = 17, text = "17"},
                new {value = 18, text = "18"},
                new {value = 19, text = "19"},
                new {value = 20, text = "20"},
                new {value = 21, text = "21"},
                new {value = 22, text = "22"},
                new {value = 23, text = "23"},
                new {value = 24, text = "24"},
            }, "value", "text", 0);

            //Select list minutos
            var listaMinutos = new SelectList(new[] {
                new {value = 0, text = "00"},
                new {value = 1, text = "01"},
                new {value = 2, text = "02"},
                new {value = 3, text = "03"},
                new {value = 4, text = "04"},
                new {value = 5, text = "05"},
                new {value = 6, text = "06"},
                new {value = 7, text = "07"},
                new {value = 8, text = "08"},
                new {value = 9, text = "09"},
                new {value = 10, text = "10"},
                new {value = 11, text = "11"},
                new {value = 12, text = "12"},
                new {value = 13, text = "13"},
                new {value = 14, text = "14"},
                new {value = 15, text = "15"},
                new {value = 16, text = "16"},
                new {value = 17, text = "17"},
                new {value = 18, text = "18"},
                new {value = 19, text = "19"},
                new {value = 20, text = "20"},
                new {value = 21, text = "21"},
                new {value = 22, text = "22"},
                new {value = 23, text = "23"},
                new {value = 24, text = "24"},
                new {value = 25, text = "25"},
                new {value = 26, text = "26"},
                new {value = 27, text = "27"},
                new {value = 28, text = "28"},
                new {value = 29, text = "29"},
                new {value = 30, text = "30"},
                new {value = 31, text = "31"},
                new {value = 32, text = "32"},
                new {value = 33, text = "33"},
                new {value = 34, text = "34"},
                new {value = 35, text = "35"},
                new {value = 36, text = "36"},
                new {value = 37, text = "37"},
                new {value = 38, text = "38"},
                new {value = 39, text = "39"},
                new {value = 40, text = "40"},
                new {value = 41, text = "41"},
                new {value = 42, text = "42"},
                new {value = 43, text = "43"},
                new {value = 44, text = "44"},
                new {value = 45, text = "45"},
                new {value = 46, text = "46"},
                new {value = 47, text = "47"},
                new {value = 48, text = "48"},
                new {value = 49, text = "49"},
                new {value = 50, text = "50"},
                new {value = 51, text = "51"},
                new {value = 52, text = "52"},
                new {value = 53, text = "53"},
                new {value = 54, text = "54"},
                new {value = 55, text = "55"},
                new {value = 56, text = "56"},
                new {value = 57, text = "57"},
                new {value = 58, text = "58"},
                new {value = 59, text = "59"},
            }, "value", "text", 0);

            ViewData["Minutos"] = listaMinutos;
            ViewData["Hora"] = listaHora;
            ViewData["Mes"] = listaMes;
            ViewData["Gestion"] = listaGest;

            return View();
        }

        //Alta recordatorios
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CrearRecordatorios(Recordatorio recordatorio)
        {
            recordatorio.Rcd_Enviado = (recordatorio.Rcd_Enviado == null) ? false : recordatorio.Rcd_Enviado;
            recordatorio.Rcd_Listado = (recordatorio.Rcd_Listado == null) ? false : recordatorio.Rcd_Listado;
            recordatorio.Rcd_Id_Usuario = int.Parse(Session["UsuarioID"].ToString());
            //recordatorio.Rcd_Anio = DateTime.Now.Year;

            if (ModelState.IsValid)
            {
                db.Recordatorio.Add(recordatorio);
                db.SaveChanges();
                return Redirect("/Gestions/Recordatorios/" + recordatorio.Rcd_Id_Gestion);
            }

            return View(recordatorio);
        }

        //Construir el correo
        public async static Task CorreoRecordatorio(string title, string body, int recordarorio_id)
        {
            GestionsController gestionController = new GestionsController();

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("casasredposventa@gmail.com", "casasred123");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("casasredposventa@gmail.com");
            mail.To.Add("villegaspro64@gmail.com");
            mail.Subject = "Casas Red Recordatorio - " + title;
            mail.IsBodyHtml = true;
            mail.Body = body + "\n" + "<a href=\"https://www.casasredposventa.com\">Casas Red Posventa</a>";

            try
            {
                await client.SendMailAsync(mail);
                gestionController.UpdateCorreo(recordarorio_id);
            }
            catch (SmtpException x)
            {
                Console.Write(x.InnerException.Message);
            }
        }


        //Programar Recordatorios
        public async Task<ActionResult> ChecarRecordatorios()
        {
            //Traer todos los recordatorios que no han sido listados
            var recordatorio = (from c in db.Recordatorio where c.Rcd_Listado == false select c).ToList();

            foreach (var item in recordatorio)
            {
                //Verifica que el día y mes no sean cero
                //if (item.Rcd_Mes != 0 && item.Rcd_Dia == 0)
                //{
                    await AgregarRecordatorio(item.Rcd_Titulo, item.Rcd_Descripcion, item.Rcd_Anio.Value, item.Rcd_Mes.Value, item.Rcd_Dia.Value, item.Rcd_Hora.Value, item.Rcd_Minuto.Value, item.Rcd_Id);
                    var recordatorioUpdate = (from c in db.Recordatorio where c.Rcd_Id == item.Rcd_Id select c).FirstOrDefault();
                    recordatorioUpdate.Rcd_Listado = true;
                    db.SaveChanges();
                //}
                //else
                //{
                //    var recordatorioUpdate = (from c in db.Recordatorio where c.Rcd_Id == item.Rcd_Id select c).FirstOrDefault();
                //    recordatorioUpdate.Rcd_Listado = true;
                //    db.SaveChanges();
                //}
            }

            return null;
        }

        public async static Task AgregarRecordatorio(string title, string body, int year, int month, int day, int hour, int minutes, int recordarorio_id)
        {
            BackgroundJob.Schedule(
            () => CorreoRecordatorio(title, body, recordarorio_id),
                new DateTime(year, month, day, hour, minutes, 00));
        }

        public void UpdateCorreo(int recordatorio_id)
        {
            var recordatorioUpdate = (from c in db.Recordatorio where c.Rcd_Id == recordatorio_id select c).FirstOrDefault();
            recordatorioUpdate.Rcd_Enviado = true;
            db.SaveChanges();
        }
    }
}

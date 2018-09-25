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
    public class GestionsController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: Gestions
        public ActionResult Index()
        {
            //if (Session["Usuario"] == null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}
            //else if (Session["Tipo"].ToString() == "Gestion" || Session["Tipo"].ToString() == "Administrador")
            //{
                var gestion = db.Gestion.Include(g => g.Cliente).Include(g => g.Corretaje);
                return View(gestion.ToList());
            //}
            //else
            //{
            //    LoginController lc = new LoginController();
            //    string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
            //    return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            //}
            
        }

        // GET: Gestions/Details/5
        public ActionResult Details(int? id)
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

        // GET: Gestions/Create
        public ActionResult Create()
        {
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Gral_Nombre");
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status");
            return View();
        }

        // POST: Gestions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Gtn_Escrituras,Gtn_Planta_Cartografica,Gtn_Predial,Gtn_Recibo_Luz,Gtn_Recibo_Agua,Gtn_Acta_Nacimiento,Gtn_IFE_Copia,Gtn_Sol_Ret_Ifo,Gtn_Cert_Hip,Gtn_Cert_Fiscal,Gtn_Sol_Estado,Gtn_Junta_URBI,Gtn_Agua_Pago_Inf,Gtn_Cert_Cartogr,Gtn_No_Oficial,Gtn_Avaluo,Gtn_CT_Banco,Gtn_Aviso_Suspension,Gtn_Formato_Infonavit,Gtn_Fotos_Propiedad,Gtn_Evaluo_Contacto,Gtn_Planeacion_Fianza,Gtn_Urbanizacion,Gtn_Credito_INFONAVIT,Gtn_Notaria,Gtn_Firma_Escrituras,Gtm_Aviso_Susp,Id_Corretaje,Id_Cliente,Gtn_ProgresoForm,Id_Usuario")] Gestion gestion)
        {
            gestion.Gtn_Escrituras = ((gestion.Gtn_Escrituras == null) ? false: gestion.Gtn_Escrituras);
            gestion.Gtn_Planta_Cartografica = ((gestion.Gtn_Planta_Cartografica == null) ? false: gestion.Gtn_Planta_Cartografica);
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
                db.Gestion.Add(gestion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Gral_Nombre", gestion.Id_Cliente);
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", gestion.Id_Corretaje);
            return View(gestion);
        }

        // GET: Gestions/Edit/5
        public ActionResult Edit(int? id)
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
        public ActionResult Edit([Bind(Include = "Id,Gtn_Escrituras,Gtn_Planta_Cartografica,Gtn_Predial,Gtn_Recibo_Luz,Gtn_Recibo_Agua,Gtn_Acta_Nacimiento,Gtn_IFE_Copia,Gtn_Sol_Ret_Ifo,Gtn_Cert_Hip,Gtn_Cert_Fiscal,Gtn_Sol_Estado,Gtn_Junta_URBI,Gtn_Agua_Pago_Inf,Gtn_Cert_Cartogr,Gtn_No_Oficial,Gtn_Avaluo,Gtn_CT_Banco,Gtn_Aviso_Suspension,Gtn_Formato_Infonavit,Gtn_Fotos_Propiedad,Gtn_Evaluo_Contacto,Gtn_Planeacion_Fianza,Gtn_Urbanizacion,Gtn_Credito_INFONAVIT,Gtn_Notaria,Gtn_Firma_Escrituras,Gtm_Aviso_Susp,Id_Corretaje,Id_Cliente,Gtn_ProgresoForm,Id_Usuario")] Gestion gestion)
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
                return RedirectToAction("Index");
            }
            ViewBag.Id_Cliente = new SelectList(db.Cliente, "Id", "Gral_Nombre", gestion.Id_Cliente);
            ViewBag.Id_Corretaje = new SelectList(db.Corretaje, "Id", "Crt_Status", gestion.Id_Corretaje);
            return View(gestion);
        }

        // GET: Gestions/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Gestions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Gestion gestion = db.Gestion.Find(id);
            db.Gestion.Remove(gestion);
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

        //Crear registro vacio al dar de alta cliente
        public string GestionCrear(Gestion gestion, int cliente_id, int corretaje_id)
        {
            
            CasasRedEntities CS = new CasasRedEntities();

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
                Gtn_ProgresoForm = 0
            };

            CS.Gestion.Add(gestion_obj);
            CS.SaveChanges();

            return "String que funciona...";
        }
    }
}

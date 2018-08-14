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
            var gestion = db.Gestion.Include(g => g.Cliente).Include(g => g.Corretaje);
            return View(gestion.ToList());
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
        public ActionResult Create([Bind(Include = "Id,Gtn_Escrituras,Gtn_Planta_Cartografica,Gtn_Predial,Gtn_Recibo_Luz,Gtn_Recibo_Agua,Gtn_Acta_Nacimiento,Gtn_IFE_Copia,Gtn_Sol_Ret_Ifo,Gtn_Cert_Hip,Gtn_Cert_Fiscal,Gtn_Sol_Estado,Gtn_Junta_URBI,Gtn_Agua_Pago_Inf,Gtn_Cert_Cartogr,Gtn_No_Oficial,Gtn_Avaluo,Gtn_CT_Banco,Gtn_Aviso_Suspension,Gtn_Formato_Infonavit,Gtn_Fotos_Propiedad,Gtn_Evaluo_Contacto,Gtn_Planeacion_Fianza,Gtn_Urbanizacion,Gtn_Credito_INFONAVIT,Gtn_Notaria,Gtn_Firma_Escrituras,Gtm_Aviso_Susp,Id_Corretaje,Id_Cliente")] Gestion gestion)
        {
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
        public ActionResult Edit([Bind(Include = "Id,Gtn_Escrituras,Gtn_Planta_Cartografica,Gtn_Predial,Gtn_Recibo_Luz,Gtn_Recibo_Agua,Gtn_Acta_Nacimiento,Gtn_IFE_Copia,Gtn_Sol_Ret_Ifo,Gtn_Cert_Hip,Gtn_Cert_Fiscal,Gtn_Sol_Estado,Gtn_Junta_URBI,Gtn_Agua_Pago_Inf,Gtn_Cert_Cartogr,Gtn_No_Oficial,Gtn_Avaluo,Gtn_CT_Banco,Gtn_Aviso_Suspension,Gtn_Formato_Infonavit,Gtn_Fotos_Propiedad,Gtn_Evaluo_Contacto,Gtn_Planeacion_Fianza,Gtn_Urbanizacion,Gtn_Credito_INFONAVIT,Gtn_Notaria,Gtn_Firma_Escrituras,Gtm_Aviso_Susp,Id_Corretaje,Id_Cliente")] Gestion gestion)
        {
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
        public string GestionCrear(Gestion gestion, int cliente_id)
        {
            int gestion_id;
            var verificacion_controller = new VerificacionsController();
            var verificacion = new Verificacion();

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
                Id_Cliente = cliente_id
            };

            CS.Gestion.Add(gestion_obj);
            CS.SaveChanges();

            gestion_id = gestion_obj.Id;

            verificacion_controller.VerfificacionCreate(verificacion, gestion_id);

            return "String que funciona...";
        }
    }
}

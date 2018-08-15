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
    public class CorretajesController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: Corretajes
        public ActionResult Index()
        {
            return View(db.Corretaje.ToList());
        }

        // GET: Corretajes/Details/5
        public ActionResult Details(int? id)
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

        // GET: Corretajes/Create
        public ActionResult Create()
        {
            var posicion = new SelectList(new[] {
                new { value = 0, text = "Selecciona una opción.."},
                new { value = 1, text = "Soltero" },
                new { value = 2, text = "Casado" }
            }, "value", "text", 0);

            ViewData["Posicion"] = posicion;

            return View();
        }

        // POST: Corretajes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Crt_Status,Crt_Cliente_Nombre,Crt_Cliente_ApMat,Crt_Cliente_ApPat,Crt_Direccion,Crt_Precio,Crt_Gasto,Crt_Tipo_Vivienda,Crt_Nivel,Crt_Num_Habitaciones,Crt_Planta,Crt_Ano_compra,Crt_Num_Credito_Infonavit,Crt_Saldo_infonavit,Crt_Fec_Nac,Crt_Tel_Celular,Crt_Estado_Civil,Crt_Tel_Casa,Crt_Tel_Trabajo,Crt_Tel_Ref1,Crt_Tel_Ref2,Crt_Tel_Ref,Crt_Recibo_predial_digital,Crt_Clave_Catastral,Crt_Adeudo_predial,Crt_Num_servicio_luz,Crt_Adeudo_luz,Crt_NombreC_Titular_luz,Crt_No_cuenta_agua,Crt_Adeudo_agua,Crt_Ine_Titu,Crt_Ine_Conyu,Crt_Escritura_Simple,Crt_Acuerdo,Crt_ActaNacTitu,Crt_ActaNacConyu,Crt_ActaMatr,Crt_EscrCert,Crt_CartaDesPre,Crt_ReciboLuz,Crt_ReciboAgua,Crt_Otros,Crt_Status_Muestra,Crt_Obervaciones,Crt_GastosServicios")] Corretaje corretaje)
        {
            int corretaje_id;
            var habilitacion = new Habilitacion();
            var habilitacion_controller = new HabilitacionsController();

            var contaduria = new Contaduria();
            var contaduria_controller = new ContaduriasController();

            var posicion = new SelectList(new[] {
                new { value = 0, text = "Selecciona una opción.."},
                new { value = 1, text = "Soltero" },
                new { value = 2, text = "Casado" }
            }, "value", "text", 0);

            if (ModelState.IsValid)
            {
                db.Corretaje.Add(corretaje);
                db.SaveChanges();

                //Borrar si no sirve
                corretaje_id = corretaje.Id;
                //Borrar si no sirve
                habilitacion_controller.CrearHabilitacion(habilitacion, corretaje_id);

                contaduria_controller.CrearContaduria(contaduria, corretaje_id); 

                return RedirectToAction("Index");
            }

            ViewData["Posicion"] = posicion;
            return View(corretaje);
        }

        // GET: Corretajes/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: Corretajes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Crt_Status,Crt_Cliente_Nombre,Crt_Cliente_ApMat,Crt_Cliente_ApPat,Crt_Direccion,Crt_Precio,Crt_Gasto,Crt_Tipo_Vivienda,Crt_Nivel,Crt_Num_Habitaciones,Crt_Planta,Crt_Ano_compra,Crt_Num_Credito_Infonavit,Crt_Saldo_infonavit,Crt_Fec_Nac,Crt_Tel_Celular,Crt_Estado_Civil,Crt_Tel_Casa,Crt_Tel_Trabajo,Crt_Tel_Ref1,Crt_Tel_Ref2,Crt_Tel_Ref,Crt_Recibo_predial_digital,Crt_Clave_Catastral,Crt_Adeudo_predial,Crt_Num_servicio_luz,Crt_Adeudo_luz,Crt_NombreC_Titular_luz,Crt_No_cuenta_agua,Crt_Adeudo_agua,Crt_Ine_Titu,Crt_Ine_Conyu,Crt_Escritura_Simple,Crt_Acuerdo,Crt_ActaNacTitu,Crt_ActaNacConyu,Crt_ActaMatr,Crt_EscrCert,Crt_CartaDesPre,Crt_ReciboLuz,Crt_ReciboAgua,Crt_Otros,Crt_Status_Muestra,Crt_Obervaciones,Crt_GastosServicios")] Corretaje corretaje)
        {
            if (ModelState.IsValid)
            {
                db.Entry(corretaje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(corretaje);
        }

        // GET: Corretajes/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Corretajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Corretaje corretaje = db.Corretaje.Find(id);
            db.Corretaje.Remove(corretaje);
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

        //Select List
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
    }
}

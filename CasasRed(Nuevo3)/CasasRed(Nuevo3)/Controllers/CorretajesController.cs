using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.IO;
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
            string[] pos = new string[] { "Soltero", "Casado", "Viudo", "Divorsiado" };

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Corretaje corretaje = db.Corretaje.Find(id);
            int a = Convert.ToInt16(corretaje.Crt_Estado_Civil);
            ViewBag.civil = pos[a];
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
                new { value = 2, text = "Casado" },
                new { value = 3, text = "Viudo" },
                new { value = 4, text = "Divorciado" }
            }, "value", "text", 0);

            ViewData["Posicion"] = posicion;

            return View();
        }

        // POST: Corretajes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public ActionResult Create([Bind(Include = "Id,Crt_Status,Crt_Cliente_Nombre,Crt_Cliente_ApMat,Crt_Cliente_ApPat,Crt_Direccion,Crt_Precio,Crt_Gasto,Crt_Tipo_Vivienda,Crt_Nivel,Crt_Num_Habitaciones,Crt_Planta,Crt_Ano_compra,Crt_Num_Credito_Infonavit,Crt_Saldo_infonavit,Crt_Fec_Nac,Crt_Tel_Celular,Crt_Estado_Civil,Crt_Tel_Casa,Crt_Tel_Trabajo,Crt_Tel_Ref1,Crt_Tel_Ref2,Crt_Tel_Ref,Crt_Recibo_predial_digital,Crt_Clave_Catastral,Crt_Adeudo_predial,Crt_Num_servicio_luz,Crt_Adeudo_luz,Crt_NombreC_Titular_luz,Crt_No_cuenta_agua,Crt_Adeudo_agua,Crt_Ine_Titu,Crt_Ine_Conyu,Crt_Escritura_Simple,Crt_Acuerdo,Crt_ActaNacTitu,Crt_ActaNacConyu,Crt_ActaMatr,Crt_EscrCert,Crt_CartaDesPre,Crt_ReciboLuz,Crt_ReciboAgua,Crt_Otros,Crt_Status_Muestra,Crt_Obervaciones,Crt_GastosServicios")] Corretaje corretaje, HttpPostedFileBase agua, HttpPostedFileBase luz, HttpPostedFileBase predial, HttpPostedFileBase otro)
        public ActionResult Create(Corretaje corretaje, HttpPostedFileBase Crt_ReciboAgua, HttpPostedFileBase Crt_ReciboLuz, HttpPostedFileBase Crt_Recibo_predial_digital, HttpPostedFileBase Crt_Otros)
        {
            var a = corretaje.Id;
            int corretaje_id;
            var habilitacion = new Habilitacion();
            var habilitacion_controller = new HabilitacionsController();

            var contaduria = new Contaduria();
            var contaduria_controller = new ContaduriasController();

            var posicion = new SelectList(new[] {
                new { value = 0, text = "Selecciona una opción.."},
                new { value = 1, text = "Soltero" },
                new { value = 2, text = "Casado" },
                new { value = 3, text = "Viudo" },
                new { value = 4, text = "Divorciado" }
            }, "value", "text", 0);


            // Imagenes
            corretaje.Crt_ReciboAgua = "data:image/jpg;base64," + convertTo64(Crt_ReciboAgua);
            corretaje.Crt_ReciboLuz = "data:image/jpg;base64," + convertTo64(Crt_ReciboLuz);
            corretaje.Crt_Recibo_predial_digital = "data:image/jpg;base64," + convertTo64(Crt_Recibo_predial_digital);
            corretaje.Crt_Otros = "data:image/jpg;base64," + convertTo64(Crt_Otros);
            //

            //Documentos checkbox
            if (corretaje.Crt_Ine_Titu == null) corretaje.Crt_Ine_Titu = false;
            if (corretaje.Crt_Ine_Conyu == null) corretaje.Crt_Ine_Conyu = false;
            if (corretaje.Crt_ActaNacTitu == null) corretaje.Crt_ActaNacTitu = false;
            if (corretaje.Crt_ActaNacConyu == null) corretaje.Crt_ActaNacConyu = false;
            if (corretaje.Crt_ActaMatr == null) corretaje.Crt_ActaMatr = false;
            if (corretaje.Crt_EscrCert == null) corretaje.Crt_EscrCert = false;
            if (corretaje.Crt_Acuerdo == null) corretaje.Crt_Acuerdo = false;
            if (corretaje.Crt_CartaDesPre == null) corretaje.Crt_CartaDesPre = false;
            if (corretaje.Crt_Escritura_Simple == null) corretaje.Crt_Escritura_Simple = false;
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


        // GET: Corretajes/Edit/5
        public ActionResult Edit(int? id)
        {
            var posicion = new SelectList(new[] {
                new { value = 0, text = "Selecciona una opción.."},
                new { value = 1, text = "Soltero" },
                new { value = 2, text = "Casado" },
                new { value = 3, text = "Viudo" },
                new { value = 4, text = "Divorciado" }
            }, "value", "text", 0);

            ViewData["Posicion"] = posicion;

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
        public ActionResult Edit(Corretaje corretaje, HttpPostedFileBase Crt_ReciboAgua, HttpPostedFileBase Crt_ReciboLuz, HttpPostedFileBase Crt_Recibo_predial_digital, HttpPostedFileBase Crt_Otros)
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
            corretaje.Crt_ReciboAgua = "data:image/jpg;base64," + convertTo64(Crt_ReciboAgua);
            corretaje.Crt_ReciboLuz = "data:image/jpg;base64," + convertTo64(Crt_ReciboLuz);
            corretaje.Crt_Recibo_predial_digital = "data:image/jpg;base64," + convertTo64(Crt_Recibo_predial_digital);
            corretaje.Crt_Otros = "data:image/jpg;base64," + convertTo64(Crt_Otros);
            //

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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
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
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Corretaje" || Session["Tipo"].ToString() == "Administrador")
            {
                Corretaje corretaje = new Corretaje();
                string[] posestatus = new string[] { "Ninguna", "Venta", "Disponible","Cancelado","Firmado" };
                int id = Convert.ToInt16(corretaje.Crt_Status);
                ViewBag.estatus = posestatus[id];
                return View(db.Corretaje.ToList());
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Corretajes/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Corretaje" || Session["Tipo"].ToString() == "Administrador")
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
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Corretajes/Create
        public ActionResult Create()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Corretaje" || Session["Tipo"].ToString() == "Administrador")
            {
                //Select List para estatus de casa
                var estatus = new SelectList(new[] {
                new { value = "-", text = "Selecciona una opción.."},
                new { value = "Venta", text = "Venta" },
                new { value = "Disponible", text = "Disponible" },
                new {value = "Cancelado",text="Cancelado"},
                new {value = "Firmado" ,text="Firmado"}
                }, "value", "text", 0);

                var posicion = new SelectList(new[] {
                new { value = 0, text = "Selecciona una opción.."},
                new { value = 1, text = "Soltero" },
                new { value = 2, text = "Casado" },
                new { value = 3, text = "Viudo" },
                new { value = 4, text = "Divorciado" }
                }, "value", "text", 0);

                var vivienda = new SelectList(new[] {
                    new {value = "-", text = "Seleccione una opción"},
                    new {value = "Casa", text = "Casa"},
                    new {value = "Departamento", text = "Departamento"}                
                }, "value", "text", 0);


                ViewBag.Id_Vendedor = new SelectList(db.Vendedor, "Id", "Vndr_Nombre");
                ViewData["Vendedor"] = ViewBag.Id_Vendedor;

                ViewData["Vivienda"] = vivienda;
                ViewData["Posicion"] = posicion;
                ViewData["Estatus"] = estatus;
           
                
                return View();
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // POST: Corretajes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        // public ActionResult Create([Bind(Include = "Id,Crt_Status,Crt_Cliente_Nombre,Crt_Cliente_ApMat,Crt_Cliente_ApPat,Crt_Direccion,Crt_Precio,Crt_Gasto,Crt_Tipo_Vivienda,Crt_Nivel,Crt_Num_Habitaciones,Crt_Planta,Crt_Ano_compra,Crt_Num_Credito_Infonavit,Crt_Saldo_infonavit,Crt_Fec_Nac,Crt_Tel_Celular,Crt_Estado_Civil,Crt_Tel_Casa,Crt_Tel_Trabajo,Crt_Tel_Ref1,Crt_Tel_Ref2,Crt_Tel_Ref,Crt_Recibo_predial_digital,Crt_Clave_Catastral,Crt_Adeudo_predial,Crt_Num_servicio_luz,Crt_Adeudo_luz,Crt_NombreC_Titular_luz,Crt_No_cuenta_agua,Crt_Adeudo_agua,Crt_Ine_Titu,Crt_Ine_Conyu,Crt_Escritura_Simple,Crt_Acuerdo,Crt_ActaNacTitu,Crt_ActaNacConyu,Crt_ActaMatr,Crt_EscrCert,Crt_CartaDesPre,Crt_ReciboLuz,Crt_ReciboAgua,Crt_Otros,Crt_Status_Muestra,Crt_Obervaciones,Crt_GastosServicios")] Corretaje corretaje, HttpPostedFileBase agua, HttpPostedFileBase luz, HttpPostedFileBase predial, HttpPostedFileBase otro)
        public ActionResult Create(Corretaje corretaje, HttpPostedFileBase Crt_ReciboAgua, HttpPostedFileBase Crt_ReciboLuz, HttpPostedFileBase Crt_Recibo_predial_digital, HttpPostedFileBase Crt_Otros)
        {
            //Obtener los correos de los usuarios de hablitacion y contaduria
            var usuarios = (from usu in db.Usuario where usu.usu_tipo == "4" || usu.usu_tipo == "5" select new { usu.usu_correo }).ToArray();

            //Crear objeto del controlador de correo para llamar al metodo
            var correo = new CorreoController();

            var a = corretaje.Id;
            int corretaje_id;
            var habilitacion = new Habilitacion();
            var habilitacion_controller = new HabilitacionsController();

            var contaduria = new Contaduria();
            var contaduria_controller = new ContaduriasController();

            //Select List para estatus de casa
            var estatus = new SelectList(new[] {
                new { value = "No seleccionado", text = "Selecciona una opción.."}, //Esto puede ser con 0 o 1
                new { value = "Venta", text = "Venta" },
                new { value = "Disponible", text = "Disponible" },
                new { value = "Cancelado",text= "Cancelado"},
                new {value = "Firmado",text = "Firmado"}
            }, "value", "text", 0);


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
            corretaje.Crt_Nss = (corretaje.Crt_Nss == null) ? "" : corretaje.Crt_Nss;

            if (ModelState.IsValid)
            {
                corretaje.Crt_FechaAlta = DateTime.Now;
                db.Corretaje.Add(corretaje);
                db.SaveChanges();

                //Borrar si no sirve
                corretaje_id = corretaje.Id;

                //Crea registro vacío en habilitación
                habilitacion_controller.CrearHabilitacion(habilitacion, corretaje_id);
                //Crea registro vacío en contaduría
                contaduria_controller.CrearContaduria(contaduria, corretaje_id);

                //Enviar correo de alta de casa a los demás departamentos
                foreach (var item in usuarios)
                {
                    if (item != null)
                    {
                        correo.sendMailCorretaje(item.usu_correo);
                    }
                }

                return RedirectToAction("Index");
            }

            ViewData["Estatus"] = estatus;
            ViewData["Posicion"] = posicion;

            ViewBag.Id_Vendedor = new SelectList(db.Vendedor, "Id", "Vndr_Nombre", corretaje.Id_Vendedor);
            ViewData["Vendedor"] = ViewBag.Id_Vendedor;

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
            //Estatus de la casa
            var estatus = new SelectList(new[] {
                new { value = "No seleccionado", text = "Selecciona una opción.."},
                new { value = "Venta", text = "Venta" },
                new { value = "Disponible", text = "Disponible" },
                new {value = "Cancelado" , text ="Cancelado"},
                new {value = "Firmado" , text = "Firmado"}
            }, "value", "text", 0);

            
            var posicion = new SelectList(new[] {
                new { value = 0, text = "Selecciona una opción.."},
                new { value = 1, text = "Soltero" },
                new { value = 2, text = "Casado" },
                new { value = 3, text = "Viudo" },
                new { value = 4, text = "Divorciado" }
            }, "value", "text", 0);

            var vivienda = new SelectList(new[] {
                    new {value = "-", text = "Seleccione una opción"},
                    new {value = "Casa", text = "Casa"},
                    new {value = "Departamento", text = "Departamento"}
                }, "value", "text", 0);

            ViewData["Vivienda"] = vivienda;
            ViewData["Posicion"] = posicion;
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
            corretaje.Crt_Nss = (corretaje.Crt_Nss == null) ? "" : corretaje.Crt_Nss;

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

        //Obteber las casas
        public JsonResult BuscarCorretaje(string filtro = "", int pagina = 1, int registrosPagina = 15, int mes = 0, int ano = 0)
        {
            if (filtro == "" && mes == 0 && ano == 0)
            {
                int totalPaginas = (int)Math.Ceiling((double)db.Corretaje.Count() / registrosPagina);
                var busqueda = (from a in db.Corretaje select new { a.Id, a.Crt_Direccion, fecha = SqlFunctions.DateName("year", a.Crt_FechaAlta).Trim() + "/" + SqlFunctions.StringConvert((double)a.Crt_FechaAlta.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("day", a.Crt_FechaAlta).Trim(), a.Crt_Status, cliente = (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat), a.Crt_ProgresoForm, total = totalPaginas }).OrderBy(a => a.Crt_Direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            //solo filtro
            else if (filtro != "" && mes == 0 && ano == 0)
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Corretaje where (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat).Contains(filtro) || a.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Corretaje where (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat).Contains(filtro) || a.Crt_Direccion.Contains(filtro) select new { a.Id, a.Crt_Direccion, fecha = SqlFunctions.DateName("year", a.Crt_FechaAlta).Trim() + "/" + SqlFunctions.StringConvert((double)a.Crt_FechaAlta.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("day", a.Crt_FechaAlta).Trim(), a.Crt_Status, cliente = (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat), a.Crt_ProgresoForm, total = totalPaginas }).OrderBy(a => a.Crt_Direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            //solo mes sin año y sin filtro
            else if (mes != 0 && filtro == "" && ano == 0)
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Corretaje where (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat).Contains(filtro) || a.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Corretaje where a.Crt_FechaAlta.Value.Month.Equals(mes) select new { a.Id, a.Crt_Direccion, fecha = SqlFunctions.DateName("year", a.Crt_FechaAlta).Trim() + "/" + SqlFunctions.StringConvert((double)a.Crt_FechaAlta.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("day", a.Crt_FechaAlta).Trim(), a.Crt_Status, cliente = (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat), a.Crt_ProgresoForm, total = totalPaginas }).OrderBy(a => a.Crt_Direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            //solo ano sin mes y sin filtro
            else if (ano != 0 && filtro == "" && mes == 0)
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Corretaje where (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat).Contains(filtro) || a.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Corretaje where a.Crt_FechaAlta.Value.Year.Equals(ano) select new { a.Id, a.Crt_Direccion, fecha = SqlFunctions.DateName("year", a.Crt_FechaAlta).Trim() + "/" + SqlFunctions.StringConvert((double)a.Crt_FechaAlta.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("day", a.Crt_FechaAlta).Trim(), a.Crt_Status, cliente = (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat), a.Crt_ProgresoForm, total = totalPaginas }).OrderBy(a => a.Crt_Direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            //ano y filtro sin mes 
            else if (ano != 0 && filtro != "" && mes == 0)
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Corretaje where (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat).Contains(filtro) || a.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Corretaje where ((a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat).Contains(filtro) || a.Crt_Direccion.Contains(filtro)) && a.Crt_FechaAlta.Value.Year.Equals(ano) select new { a.Id, a.Crt_Direccion, fecha = SqlFunctions.DateName("year", a.Crt_FechaAlta).Trim() + "/" + SqlFunctions.StringConvert((double)a.Crt_FechaAlta.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("day", a.Crt_FechaAlta).Trim(), a.Crt_Status, cliente = (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat), a.Crt_ProgresoForm, total = totalPaginas }).OrderBy(a => a.Crt_Direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            //mes y filtro sin a;o
            else if (filtro != "" && mes != 0 && ano == 0)
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Corretaje where (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat).Contains(filtro) || a.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Corretaje where ((a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat).Contains(filtro) || a.Crt_Direccion.Contains(filtro)) && a.Crt_FechaAlta.Value.Month.Equals(mes) select new { a.Id, a.Crt_Direccion, fecha = SqlFunctions.DateName("year", a.Crt_FechaAlta).Trim() + "/" + SqlFunctions.StringConvert((double)a.Crt_FechaAlta.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("day", a.Crt_FechaAlta).Trim(), a.Crt_Status, cliente = (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat), a.Crt_ProgresoForm, total = totalPaginas }).OrderBy(a => a.Crt_Direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            //all
            else
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Corretaje where (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat).Contains(filtro) || a.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Corretaje where ((a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat).Contains(filtro) || a.Crt_Direccion.Contains(filtro)) && a.Crt_FechaAlta.Value.Month.Equals(mes) && a.Crt_FechaAlta.Value.Year.Equals(ano) select new { a.Id, a.Crt_Direccion, fecha = SqlFunctions.DateName("year", a.Crt_FechaAlta).Trim() + "/" + SqlFunctions.StringConvert((double)a.Crt_FechaAlta.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("day", a.Crt_FechaAlta).Trim(), a.Crt_Status, cliente = (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat), a.Crt_ProgresoForm, total = totalPaginas }).OrderBy(a => a.Crt_Direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult BuscarCorretaje2(string filtro = "", int pagina = 1, int registrosPagina = 15, int mes = 0, int ano = 0)
        {
            if (filtro == "" && mes == 0 && ano == 0)
            {
                int totalPaginas = (int)Math.Ceiling((double)db.Corretaje.Count() / registrosPagina);
                var busqueda = (from a in db.Corretaje where a.Crt_Status == "Disponible" select new { a.Id, a.Crt_Direccion, a.Crt_Ano_compra, a.Crt_Status, cliente = (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat), a.Crt_ProgresoForm, total = totalPaginas }).OrderBy(a => a.Crt_Direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            //solo filtro
            else if (filtro != "" && mes == 0 && ano == 0)
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Corretaje where (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat).Contains(filtro) || a.Crt_Ano_compra.Contains(filtro) || a.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Corretaje where a.Crt_Status == "Disponible" && (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat).Contains(filtro) || a.Crt_Ano_compra.Contains(filtro) || a.Crt_Direccion.Contains(filtro) select new { a.Id, a.Crt_Direccion, a.Crt_Ano_compra, a.Crt_Status, cliente = (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat), a.Crt_ProgresoForm, total = totalPaginas }).OrderBy(a => a.Crt_Direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int totalPaginas = (int)Math.Ceiling((double)(from a in db.Corretaje where (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat).Contains(filtro) || a.Crt_Ano_compra.Contains(filtro) || a.Crt_Direccion.Contains(filtro) select a).Count() / registrosPagina);
                var busqueda = (from a in db.Corretaje where a.Crt_Status == "Disponible" && (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat).Contains(filtro) || a.Crt_Ano_compra.Contains(filtro) || a.Crt_Direccion.Contains(filtro) select new { a.Id, a.Crt_Direccion, a.Crt_Ano_compra, a.Crt_Status, cliente = (a.Crt_Cliente_Nombre + " " + a.Crt_Cliente_ApPat + " " + a.Crt_Cliente_ApMat), a.Crt_ProgresoForm, total = totalPaginas }).OrderBy(a => a.Crt_Direccion).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

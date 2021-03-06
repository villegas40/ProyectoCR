﻿using CasasRed_Nuevo3_.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiApp.Models;

namespace WebApiApp.Controllers
{
    public class ClientesController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/Clientes
        public IQueryable<Cliente> GetCliente()
        {
            return db.Cliente;
        }

        // GET: api/Clientes/5
        [Route("api/Clientes/{id}")]
        public IHttpActionResult GetCliente(int id)
        {
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
            }

            return Ok(cliente);
        }

        // PUT: api/Clientes/5
        [Route("api/Clientes/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCliente(int id, Cliente cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cliente.Id)
            {
                return BadRequest();
            }

            db.Entry(cliente).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Clientes
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult PostCliente(Cliente cliente)
        {

            int cliente_id, corretaje_id;
            string correo, telefono;

            //Obtener los correos de los usuarios de hablitacion y contaduria
            var usuarios = (from usu in db.Usuario where usu.usu_tipo == "2" || usu.usu_tipo == "6" select new { usu.usu_correo }).ToArray();

            //Objeto de Gestion
            var gestion_controller = new GestionsController();

            //Objeto Verificacion
            var verificacion_controller = new VerificacionsController();

            //Correo
            var correo_controller = new CorreoController();

            //SMS
            var sms_controller = new SmsController();

            //Folio
            var foliogenerado = ValidarFolioDuplicado();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            cliente.Gral_Fechaalta = DateTime.Now;
            cliente.Grlal_Folio = foliogenerado;

            db.Cliente.Add(cliente);
            db.SaveChanges();

            //Tomar valores
            cliente_id = cliente.Id;
            corretaje_id = cliente.Id_Corretaje.Value; //Preguntar si lo dejo así o corretaje_id = cliente.Id_Corretaje.HasValue ? cliente.Id_Corretaje.Value:0
                                                       //telefono = cliente.Gral_Celular.ToString();
                                                       //correo = cliente.Gral_Correo;

            correo = cliente.Gral_Correo;

            //Actualizar status de la casa
            if (cliente.Id_Corretaje != null)
            {
                Corretaje cr = db.Corretaje.Find(cliente.Id_Corretaje);
                cr.Crt_Status = "Venta";
                db.SaveChanges();
            }

            //Funciones
            gestion_controller.CreateGestions(cliente_id, corretaje_id);
            verificacion_controller.CreateVerificacions(cliente_id);
            //sms_controller.SendSms(telefono); Estan comentadas porque cuestan dinero xd

            //Llamado de función para enviar correo
            if (cliente.Gral_Correo != null || cliente.Gral_Correo != "")
            {
                correo_controller.sendmail(correo);
            }

            //Enviar correo de alta de casa a los demás departamentos
            foreach (var item in usuarios)
            {
                if (item != null)
                {
                    correo_controller.sendMailGestion(item.usu_correo);
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cliente.Id }, cliente);

        }

        //Metodo Folio
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

        // DELETE: api/Clientes/5
        [Route("api/Clientes/{id}")]
        [ResponseType(typeof(Cliente))]
        public IHttpActionResult DeleteCliente(int id)
        {
            //Consulta para desactivar las casillas de gestion
            var gestion = (from a in db.Gestion where id == a.Id_Cliente select a).FirstOrDefault();

            Cliente cliente = db.Cliente.Find(id);

            if (cliente == null)
            {
                return NotFound();
            }

            //Actualizar Gestion
            if (gestion != null)
            {
                gestion.Gtn_Acta_Nacimiento = false;
                gestion.Gtn_Acta_Nacim_Cony = false;
                gestion.Gtn_Acta_Matrimonio = false;
                gestion.Gtn_DatosGnrl_Comp = false;
                gestion.Gtn_Comp_Domicilio = false;
                gestion.Gtn_Recibo_Nomina = false;
                gestion.Gtn_RFC_Comprador = false;
                gestion.Gtn_CURP_Comprador = false;
                gestion.Gtn_RFC_Conyugue = false;
                gestion.Gtn_CURP_Conyugue = false;
                gestion.Gtn_Inscp_INFONAVIT = false;
                gestion.Gtn_Taller = false;
                gestion.Gtn_Avaluo = false;
                gestion.Gtn_Notaria = false;
                gestion.Gtn_Aviso_Ret = false;
                gestion.Gtn_Firma_Escrituras = false;
                db.SaveChanges();
            }

            //Actualizar el status de la casa
            if (cliente.Id_Corretaje != null)
            {
                Corretaje cr = db.Corretaje.Find(cliente.Id_Corretaje);
                cr.Crt_Status = "Disponible";
                db.SaveChanges();
            }

            db.Cliente.Remove(cliente);
            db.SaveChanges();

            return Ok(cliente);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClienteExists(int id)
        {
            return db.Cliente.Count(e => e.Id == id) > 0;
        }
    }
}
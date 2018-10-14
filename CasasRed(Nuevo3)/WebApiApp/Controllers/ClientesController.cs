using CasasRed_Nuevo3_.Controllers;
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

            //Funciones
            gestion_controller.CreateGestions(cliente_id, corretaje_id);
            verificacion_controller.CreateVerificacions(cliente_id);
            //sms_controller.SendSms(telefono); Estan comentadas porque cuestan dinero xd
            //correo_controller.sendmail(correo);

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
            Cliente cliente = db.Cliente.Find(id);
            if (cliente == null)
            {
                return NotFound();
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
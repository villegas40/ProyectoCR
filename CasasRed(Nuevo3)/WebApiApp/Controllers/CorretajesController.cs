using CasasRed_Nuevo3_.Controllers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebApiApp.Models;

namespace WebApiApp.Controllers
{
    public class CorretajesController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/Corretajes
        public IQueryable<Corretaje> GetCorretaje()
        {
            return db.Corretaje;
        }

        // GET: api/Corretajes/5
        [Route("api/Corretajes/{id}")]
        [ResponseType(typeof(Corretaje))]
        public IHttpActionResult GetCorretaje(int id)
        {
            Corretaje corretaje = db.Corretaje.Find(id);
            if (corretaje == null)
            {
                return NotFound();
            }

            return Ok(corretaje);
        }

        // PUT: api/Corretajes/5
        [Route("api/Corretajes/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCorretaje(int id, Corretaje corretaje)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != corretaje.Id)
            {
                return BadRequest();
            }

            db.Entry(corretaje).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CorretajeExists(id))
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

        // POST: api/Corretajes
        [ResponseType(typeof(Corretaje))]
        public IHttpActionResult PostCorretaje(Corretaje corretaje)
        {
            int corretaje_id;

            //Objeto para acceder a los metodos de correo
            var correo_controller = new CorreoController();

            //Obtener los correos de los usuarios de hablitacion y contaduria
            var usuarios = (from usu in db.Usuario where usu.usu_tipo == "4" || usu.usu_tipo == "5" select new { usu.usu_correo }).ToArray();

            //Habilitacion
            //var habilitacion = new Habilitacion();
            var habilitacion_controller = new HabilitacionsController();

            //Contaduria
            //var contaduria = new Contaduria();
            var contadiria_controller = new ContaduriasController();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //Fecha de alta del la casa al sistema
            corretaje.Crt_FechaAlta = DateTime.Now;

            db.Corretaje.Add(corretaje);
            db.SaveChanges();

            //Variables
            corretaje_id = corretaje.Id;

            //Funciones
            habilitacion_controller.CreateHabilitacions(corretaje_id);
            contadiria_controller.CreateContadurias(corretaje_id);

            //Enviar correo de alta de casa a los demás departamentos
            foreach (var item in usuarios)
            {
                if (item != null)
                {
                    correo_controller.sendMailCorretaje(item.usu_correo);
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = corretaje.Id }, corretaje);
        }

        // DELETE: api/Corretajes/5
        [Route("api/Corretajes/{id}")]
        [ResponseType(typeof(Corretaje))]
        public IHttpActionResult DeleteCorretaje(int id)
        {
            Corretaje corretaje = db.Corretaje.Find(id);
            if (corretaje == null)
            {
                return NotFound();
            }

            db.Corretaje.Remove(corretaje);
            db.SaveChanges();

            return Ok(corretaje);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CorretajeExists(int id)
        {
            return db.Corretaje.Count(e => e.Id == id) > 0;
        }

    }
}
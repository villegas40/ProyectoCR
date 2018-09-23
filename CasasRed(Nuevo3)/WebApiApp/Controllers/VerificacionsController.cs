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
    public class VerificacionsController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/Verificacions
        public IQueryable<Verificacion> GetVerificacion()
        {
            return db.Verificacion;
        }

        // GET: api/Verificacions/5
        [Route("api/Verificacions/{id}")]
        [ResponseType(typeof(Verificacion))]
        public IHttpActionResult GetVerificacion(int id)
        {
            Verificacion verificacion = db.Verificacion.Find(id);
            if (verificacion == null)
            {
                return NotFound();
            }

            return Ok(verificacion);
        }

        // PUT: api/Verificacions/5
        [Route("api/Verificacions/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVerificacion(int id, Verificacion verificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != verificacion.Id)
            {
                return BadRequest();
            }

            db.Entry(verificacion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VerificacionExists(id))
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

        // POST: api/Verificacions
        [ResponseType(typeof(Verificacion))]
        public IHttpActionResult PostVerificacion(Verificacion verificacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Verificacion.Add(verificacion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = verificacion.Id }, verificacion);
        }

        // DELETE: api/Verificacions/5
        [Route("api/Verificacions/{id}")]
        [ResponseType(typeof(Verificacion))]
        public IHttpActionResult DeleteVerificacion(int id)
        {
            Verificacion verificacion = db.Verificacion.Find(id);
            if (verificacion == null)
            {
                return NotFound();
            }

            db.Verificacion.Remove(verificacion);
            db.SaveChanges();

            return Ok(verificacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VerificacionExists(int id)
        {
            return db.Verificacion.Count(e => e.Id == id) > 0;
        }

        ////Funcion generar registro vacio relacionado con un cliente dado de alta
        public void CreateVerificacions(int cliente_id)
        {
            CasasRedEntities CS = new CasasRedEntities();
            Verificacion verificacion_obj = new Verificacion
            {
                Vfn_Costo = 0,
                Vfn_Persona_fisica = false,
                Vfn_Tiempo_estimado = false,
                Vfn_Tiene_costo = false,
                Vfn_Visto_persona = false
            };

            CS.Verificacion.Add(verificacion_obj);
            CS.SaveChanges();
        }
    }
}
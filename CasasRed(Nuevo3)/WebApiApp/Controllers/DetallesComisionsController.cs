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
    public class DetallesComisionsController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/DetallesComisions
        public IQueryable<DetallesComision> GetDetallesComision()
        {
            return db.DetallesComision;
        }

        // GET: api/DetallesComisions/5
        [Route("api/DetallesComisions/{id}")]
        [ResponseType(typeof(DetallesComision))]
        public IHttpActionResult GetDetallesComision(int id)
        {
            DetallesComision detallesComision = db.DetallesComision.Find(id);
            if (detallesComision == null)
            {
                return NotFound();
            }

            return Ok(detallesComision);
        }

        // PUT: api/DetallesComisions/5
        [Route("api/DetallesComisions/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDetallesComision(int id, DetallesComision detallesComision)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != detallesComision.Id)
            {
                return BadRequest();
            }

            db.Entry(detallesComision).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DetallesComisionExists(id))
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

        // POST: api/DetallesComisions
        [ResponseType(typeof(DetallesComision))]
        public IHttpActionResult PostDetallesComision(DetallesComision detallesComision)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DetallesComision.Add(detallesComision);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = detallesComision.Id }, detallesComision);
        }

        // DELETE: api/DetallesComisions/5
        [Route("api/DetallesComisions/{id}")]
        [ResponseType(typeof(DetallesComision))]
        public IHttpActionResult DeleteDetallesComision(int id)
        {
            DetallesComision detallesComision = db.DetallesComision.Find(id);
            if (detallesComision == null)
            {
                return NotFound();
            }

            db.DetallesComision.Remove(detallesComision);
            db.SaveChanges();

            return Ok(detallesComision);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DetallesComisionExists(int id)
        {
            return db.DetallesComision.Count(e => e.Id == id) > 0;
        }
    }
}
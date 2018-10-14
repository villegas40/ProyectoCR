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
    public class CalificacionVendedorsController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/CalificacionVendedors
        public IQueryable<CalificacionVendedor> GetCalificacionVendedor()
        {
            return db.CalificacionVendedor;
        }

        // GET: api/CalificacionVendedors/5
        [Route("api/CalificacionVendedors/{id}")]
        [ResponseType(typeof(CalificacionVendedor))]
        public IHttpActionResult GetCalificacionVendedor(int id)
        {
            CalificacionVendedor calificacionVendedor = db.CalificacionVendedor.Find(id);
            if (calificacionVendedor == null)
            {
                return NotFound();
            }

            return Ok(calificacionVendedor);
        }

        // PUT: api/CalificacionVendedors/5
        [Route("api/CalificacionVendedors/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCalificacionVendedor(int id, CalificacionVendedor calificacionVendedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != calificacionVendedor.Id)
            {
                return BadRequest();
            }

            db.Entry(calificacionVendedor).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CalificacionVendedorExists(id))
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

        // POST: api/CalificacionVendedors
        [ResponseType(typeof(CalificacionVendedor))]
        public IHttpActionResult PostCalificacionVendedor(CalificacionVendedor calificacionVendedor)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CalificacionVendedor.Add(calificacionVendedor);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = calificacionVendedor.Id }, calificacionVendedor);
        }

        // DELETE: api/CalificacionVendedors/5
        [Route("api/CalificacionVendedors/{id}")]
        [ResponseType(typeof(CalificacionVendedor))]
        public IHttpActionResult DeleteCalificacionVendedor(int id)
        {
            CalificacionVendedor calificacionVendedor = db.CalificacionVendedor.Find(id);
            if (calificacionVendedor == null)
            {
                return NotFound();
            }

            db.CalificacionVendedor.Remove(calificacionVendedor);
            db.SaveChanges();

            return Ok(calificacionVendedor);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CalificacionVendedorExists(int id)
        {
            return db.CalificacionVendedor.Count(e => e.Id == id) > 0;
        }
    }
}
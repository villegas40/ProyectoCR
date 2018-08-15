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
    public class GastosController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/Gastos
        public IQueryable<Gastos> GetGastos()
        {
            return db.Gastos;
        }

        // GET: api/Gastos/5
        [ResponseType(typeof(Gastos))]
        public IHttpActionResult GetGastos(int id)
        {
            Gastos gastos = db.Gastos.Find(id);
            if (gastos == null)
            {
                return NotFound();
            }

            return Ok(gastos);
        }

        // PUT: api/Gastos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGastos(int id, Gastos gastos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gastos.Id)
            {
                return BadRequest();
            }

            db.Entry(gastos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GastosExists(id))
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

        // POST: api/Gastos
        [ResponseType(typeof(Gastos))]
        public IHttpActionResult PostGastos(Gastos gastos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gastos.Add(gastos);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = gastos.Id }, gastos);
        }

        // DELETE: api/Gastos/5
        [ResponseType(typeof(Gastos))]
        public IHttpActionResult DeleteGastos(int id)
        {
            Gastos gastos = db.Gastos.Find(id);
            if (gastos == null)
            {
                return NotFound();
            }

            db.Gastos.Remove(gastos);
            db.SaveChanges();

            return Ok(gastos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GastosExists(int id)
        {
            return db.Gastos.Count(e => e.Id == id) > 0;
        }
    }
}
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
    public class CasaInventarios1Controller : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/CasaInventarios1
        public IQueryable<CasaInventario> GetCasaInventario()
        {
            return db.CasaInventario;
        }

        // GET: api/CasaInventarios1/5
        [Route("api/CasaInventarios1/{id}")]
        [ResponseType(typeof(CasaInventario))]
        public IHttpActionResult GetCasaInventario(int id)
        {
            CasaInventario casaInventario = db.CasaInventario.Find(id);
            if (casaInventario == null)
            {
                return NotFound();
            }

            return Ok(casaInventario);
        }

        // PUT: api/CasaInventarios1/5
        [Route("api/CasaInventarios1/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCasaInventario(int id, CasaInventario casaInventario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != casaInventario.ci_Id)
            {
                return BadRequest();
            }

            db.Entry(casaInventario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CasaInventarioExists(id))
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

        // POST: api/CasaInventarios1
        [ResponseType(typeof(CasaInventario))]
        public IHttpActionResult PostCasaInventario(CasaInventario casaInventario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CasaInventario.Add(casaInventario);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = casaInventario.ci_Id }, casaInventario);
        }

        // DELETE: api/CasaInventarios1/5
        [Route("api/CasaInventarios1/{id}")]
        [ResponseType(typeof(CasaInventario))]
        public IHttpActionResult DeleteCasaInventario(int id)
        {
            CasaInventario casaInventario = db.CasaInventario.Find(id);
            if (casaInventario == null)
            {
                return NotFound();
            }

            db.CasaInventario.Remove(casaInventario);
            db.SaveChanges();

            return Ok(casaInventario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CasaInventarioExists(int id)
        {
            return db.CasaInventario.Count(e => e.ci_Id == id) > 0;
        }
    }
}
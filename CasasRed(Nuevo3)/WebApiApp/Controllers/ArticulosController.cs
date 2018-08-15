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
    public class ArticulosController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/Articulos
        public IQueryable<Articulos> GetArticulos()
        {
            return db.Articulos;
        }

        // GET: api/Articulos/5
        [ResponseType(typeof(Articulos))]
        public IHttpActionResult GetArticulos(string id)
        {
            Articulos articulos = db.Articulos.Find(id);
            if (articulos == null)
            {
                return NotFound();
            }

            return Ok(articulos);
        }

        // PUT: api/Articulos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutArticulos(string id, Articulos articulos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != articulos.art_id)
            {
                return BadRequest();
            }

            db.Entry(articulos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticulosExists(id))
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

        // POST: api/Articulos
        [ResponseType(typeof(Articulos))]
        public IHttpActionResult PostArticulos(Articulos articulos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Articulos.Add(articulos);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ArticulosExists(articulos.art_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = articulos.art_id }, articulos);
        }

        // DELETE: api/Articulos/5
        [ResponseType(typeof(Articulos))]
        public IHttpActionResult DeleteArticulos(string id)
        {
            Articulos articulos = db.Articulos.Find(id);
            if (articulos == null)
            {
                return NotFound();
            }

            db.Articulos.Remove(articulos);
            db.SaveChanges();

            return Ok(articulos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArticulosExists(string id)
        {
            return db.Articulos.Count(e => e.art_id == id) > 0;
        }
    }
}
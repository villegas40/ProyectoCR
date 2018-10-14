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
    public class ComentariosController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/Comentarios
        public IQueryable<Comentarios> GetComentarios()
        {
            return db.Comentarios;
        }

        // GET: api/Comentarios/5
        [Route("api/Comentarios/{id}")]
        [ResponseType(typeof(Comentarios))]
        public IHttpActionResult GetComentarios(int id)
        {
            Comentarios comentarios = db.Comentarios.Find(id);
            if (comentarios == null)
            {
                return NotFound();
            }

            return Ok(comentarios);
        }

        // PUT: api/Comentarios/5
        [Route("api/Comentarios/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutComentarios(int id, Comentarios comentarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comentarios.Id)
            {
                return BadRequest();
            }

            db.Entry(comentarios).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComentariosExists(id))
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

        // POST: api/Comentarios
        [ResponseType(typeof(Comentarios))]
        public IHttpActionResult PostComentarios(Comentarios comentarios)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Comentarios.Add(comentarios);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = comentarios.Id }, comentarios);
        }

        // DELETE: api/Comentarios/5
        [Route("api/Comentarios/{id}")]
        [ResponseType(typeof(Comentarios))]
        public IHttpActionResult DeleteComentarios(int id)
        {
            Comentarios comentarios = db.Comentarios.Find(id);
            if (comentarios == null)
            {
                return NotFound();
            }

            db.Comentarios.Remove(comentarios);
            db.SaveChanges();

            return Ok(comentarios);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ComentariosExists(int id)
        {
            return db.Comentarios.Count(e => e.Id == id) > 0;
        }
    }
}
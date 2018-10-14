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
    public class TipoUsuariosController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/TipoUsuarios
        public IQueryable<TipoUsuario> GetTipoUsuario()
        {
            return db.TipoUsuario;
        }

        // GET: api/TipoUsuarios/5
        [Route("api/TipoUsuarios/{id}")]
        [ResponseType(typeof(TipoUsuario))]
        public IHttpActionResult GetTipoUsuario(int id)
        {
            TipoUsuario tipoUsuario = db.TipoUsuario.Find(id);
            if (tipoUsuario == null)
            {
                return NotFound();
            }

            return Ok(tipoUsuario);
        }

        // PUT: api/TipoUsuarios/5
        [Route("api/TipoUsuarios/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipoUsuario(int id, TipoUsuario tipoUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipoUsuario.Id)
            {
                return BadRequest();
            }

            db.Entry(tipoUsuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TipoUsuarioExists(id))
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

        // POST: api/TipoUsuarios
        [ResponseType(typeof(TipoUsuario))]
        public IHttpActionResult PostTipoUsuario(TipoUsuario tipoUsuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TipoUsuario.Add(tipoUsuario);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipoUsuario.Id }, tipoUsuario);
        }

        // DELETE: api/TipoUsuarios/5
        [Route("api/TipoUsuarios/{id}")]
        [ResponseType(typeof(TipoUsuario))]
        public IHttpActionResult DeleteTipoUsuario(int id)
        {
            TipoUsuario tipoUsuario = db.TipoUsuario.Find(id);
            if (tipoUsuario == null)
            {
                return NotFound();
            }

            db.TipoUsuario.Remove(tipoUsuario);
            db.SaveChanges();

            return Ok(tipoUsuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TipoUsuarioExists(int id)
        {
            return db.TipoUsuario.Count(e => e.Id == id) > 0;
        }
    }
}
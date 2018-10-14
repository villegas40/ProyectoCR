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
    public class VendedorAsigsController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/VendedorAsigs
        public IQueryable<VendedorAsig> GetVendedorAsig()
        {
            return db.VendedorAsig;
        }

        // GET: api/VendedorAsigs/5
        [Route("api/VendedorAsigs/{id}")]
        [ResponseType(typeof(VendedorAsig))]
        public IHttpActionResult GetVendedorAsig(int id)
        {
            VendedorAsig vendedorAsig = db.VendedorAsig.Find(id);
            if (vendedorAsig == null)
            {
                return NotFound();
            }

            return Ok(vendedorAsig);
        }

        // PUT: api/VendedorAsigs/5
        [Route("api/VendedorAsigs/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVendedorAsig(int id, VendedorAsig vendedorAsig)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vendedorAsig.Id)
            {
                return BadRequest();
            }

            db.Entry(vendedorAsig).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VendedorAsigExists(id))
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

        // POST: api/VendedorAsigs
        [ResponseType(typeof(VendedorAsig))]
        public IHttpActionResult PostVendedorAsig(VendedorAsig vendedorAsig)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.VendedorAsig.Add(vendedorAsig);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vendedorAsig.Id }, vendedorAsig);
        }

        // DELETE: api/VendedorAsigs/5
        [Route("api/VendedorAsigs/{id}")]
        [ResponseType(typeof(VendedorAsig))]
        public IHttpActionResult DeleteVendedorAsig(int id)
        {
            VendedorAsig vendedorAsig = db.VendedorAsig.Find(id);
            if (vendedorAsig == null)
            {
                return NotFound();
            }

            db.VendedorAsig.Remove(vendedorAsig);
            db.SaveChanges();

            return Ok(vendedorAsig);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VendedorAsigExists(int id)
        {
            return db.VendedorAsig.Count(e => e.Id == id) > 0;
        }
    }
}
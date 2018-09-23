﻿using System;
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

            //Borrar si no sirve

            db.Corretaje.Add(corretaje);
            db.SaveChanges();

            //Variables
            corretaje_id = corretaje.Id;

            //Funciones
            habilitacion_controller.CreateHabilitacions(corretaje_id);
            contadiria_controller.CreateContadurias(corretaje_id);

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
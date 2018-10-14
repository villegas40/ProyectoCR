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
    public class ContaduriasController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/Contadurias
        public IQueryable<Contaduria> GetContaduria()
        {
            return db.Contaduria;
        }

        // GET: api/Contadurias/5
        [Route("api/Contadurias/{id}")]
        [ResponseType(typeof(Contaduria))]
        public IHttpActionResult GetContaduria(int id)
        {
            Contaduria contaduria = db.Contaduria.Find(id);
            if (contaduria == null)
            {
                return NotFound();
            }

            return Ok(contaduria);
        }

        // PUT: api/Contadurias/5
        [Route("api/Contadurias/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutContaduria(int id, Contaduria contaduria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != contaduria.Id)
            {
                return BadRequest();
            }

            db.Entry(contaduria).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContaduriaExists(id))
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

        // POST: api/Contadurias
        [ResponseType(typeof(Contaduria))]
        public IHttpActionResult PostContaduria(Contaduria contaduria)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Contaduria.Add(contaduria);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = contaduria.Id }, contaduria);
        }

        // DELETE: api/Contadurias/5
        [Route("api/Contadurias/{id}")]
        [ResponseType(typeof(Contaduria))]
        public IHttpActionResult DeleteContaduria(int id)
        {
            Contaduria contaduria = db.Contaduria.Find(id);
            if (contaduria == null)
            {
                return NotFound();
            }

            db.Contaduria.Remove(contaduria);
            db.SaveChanges();

            return Ok(contaduria);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ContaduriaExists(int id)
        {
            return db.Contaduria.Count(e => e.Id == id) > 0;
        }

        //Funcion crear registro vacio automantico cuando se da de alta una casa
        public void CreateContadurias(int corretaje_id)
        {
            CasasRedEntities CS = new CasasRedEntities();

            Contaduria contaduria_obj = new Contaduria
            {

                Cnt_M_Preguntar = 0,
                Cnt_Material = 0,
                Cnt_Tramites = 0,
                Cnt_Vigilancia = 0,
                Cnt_CESPT = 0,
                Cnt_CFE = 0,
                Id_Corretaje = corretaje_id,
                Cnt_DevMensualidad = 0
            };

            CS.Contaduria.Add(contaduria_obj);
            CS.SaveChanges();
        }
    }
}
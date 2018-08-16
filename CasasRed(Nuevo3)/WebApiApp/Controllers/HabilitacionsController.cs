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
    public class HabilitacionsController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/Habilitacions
        public IQueryable<Habilitacion> GetHabilitacion()
        {
            return db.Habilitacion;
        }

        // GET: api/Habilitacions/5
        [ResponseType(typeof(Habilitacion))]
        public IHttpActionResult GetHabilitacion(int id)
        {
            Habilitacion habilitacion = db.Habilitacion.Find(id);
            if (habilitacion == null)
            {
                return NotFound();
            }

            return Ok(habilitacion);
        }

        // PUT: api/Habilitacions/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHabilitacion(int id, Habilitacion habilitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != habilitacion.Id)
            {
                return BadRequest();
            }

            db.Entry(habilitacion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HabilitacionExists(id))
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

        // POST: api/Habilitacions
        [ResponseType(typeof(Habilitacion))]
        public IHttpActionResult PostHabilitacion(Habilitacion habilitacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Habilitacion.Add(habilitacion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = habilitacion.Id }, habilitacion);
        }

        // DELETE: api/Habilitacions/5
        [ResponseType(typeof(Habilitacion))]
        public IHttpActionResult DeleteHabilitacion(int id)
        {
            Habilitacion habilitacion = db.Habilitacion.Find(id);
            if (habilitacion == null)
            {
                return NotFound();
            }

            db.Habilitacion.Remove(habilitacion);
            db.SaveChanges();

            return Ok(habilitacion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HabilitacionExists(int id)
        {
            return db.Habilitacion.Count(e => e.Id == id) > 0;
        }

        //Funcion registro automatico
        public void CreateHabilitacions(int corretaje_id)
        {
            CasasRedEntities CS = new CasasRedEntities();
            Habilitacion habilitacion_obj = new Habilitacion
            {
                Hbt_Apagador_doble = false,
                Hbt_Apagador_sencillo = false,
                Hbt_AvisoSusp = false,
                Hbt_Bastago = false,
                Hbt_Bisagras = false,
                Hbt_Break_interior = false,
                Hbt_Break_medidor = false,
                Hbt_Cableado = false,
                Hbt_Chapas = false,
                Hbt_Chapeton = false,
                Hbt_Conector_apagador = false,
                Hbt_Conector_sencillo = false,
                Hbt_Domo = false,
                Hbt_Kit_lavamanos = false,
                Hbt_Kit_taza = false,
                Hbt_Lavamanos = false,
                Hbt_Maneral = false,
                Hbt_Marcos_puertas = false,
                Hbt_Pinturas = false,
                Hbt_Puertas = false,
                Hbt_Regadera_completa = false,
                Hbt_Rosetas = false,
                Hbt_Taza = false,
                Hbt_Ventanas = false,
                Id_Corretaje = corretaje_id
            };

            CS.Habilitacion.Add(habilitacion_obj);
            CS.SaveChanges();
        }
    }
}
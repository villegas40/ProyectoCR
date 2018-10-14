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
    public class GestionsController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: api/Gestions
        public IQueryable<Gestion> GetGestion()
        {
            return db.Gestion;
        }

        // GET: api/Gestions/5
        [Route("api/Gestions/{id}")]
        [ResponseType(typeof(Gestion))]
        public IHttpActionResult GetGestion(int id)
        {
            Gestion gestion = db.Gestion.Find(id);
            if (gestion == null)
            {
                return NotFound();
            }

            return Ok(gestion);
        }

        // PUT: api/Gestions/5
        [Route("api/Gestions/{id}")]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGestion(int id, Gestion gestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gestion.Id)
            {
                return BadRequest();
            }

            db.Entry(gestion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GestionExists(id))
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

        // POST: api/Gestions
        [ResponseType(typeof(Gestion))]
        public IHttpActionResult PostGestion(Gestion gestion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gestion.Add(gestion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = gestion.Id }, gestion);
        }

        // DELETE: api/Gestions/5
        [Route("api/Gestions/{id}")]
        [ResponseType(typeof(Gestion))]
        public IHttpActionResult DeleteGestion(int id)
        {
            Gestion gestion = db.Gestion.Find(id);
            if (gestion == null)
            {
                return NotFound();
            }

            db.Gestion.Remove(gestion);
            db.SaveChanges();

            return Ok(gestion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GestionExists(int id)
        {
            return db.Gestion.Count(e => e.Id == id) > 0;
        }

        //Funcion generar registro vacio relacionado con un cliente dado de alta
        public void CreateGestions(int cliente_id, int corretaje_id)
        {
            CasasRedEntities CS = new CasasRedEntities();

            //Consulta para buscar las casas existentes en la base de datos
            var gestio = (from a in db.Gestion where corretaje_id == a.Id_Corretaje select a).FirstOrDefault();

            //Si es distinto a null entonces la casa si esta registrada en gestion
            if (gestio != null)
            {
                gestio.Id_Cliente = cliente_id;
                db.SaveChanges();
            }
            else
            {
                Gestion gestion_obj = new Gestion
                {
                    Gtn_Acta_Nacimiento = false,
                    Gtn_Agua_Pago_Inf = false,
                    Gtn_Aviso_Suspension = false,
                    Gtn_Avaluo = false,
                    Gtn_Cert_Cartogr = false,
                    Gtn_Cert_Fiscal = false,
                    Gtn_Cert_Hip = false,
                    Gtn_Credito_INFONAVIT = false,
                    Gtn_CT_Banco = false,
                    Gtn_Escrituras = false,
                    Gtn_Evaluo_Contacto = false,
                    Gtn_Firma_Escrituras = false,
                    Gtn_Formato_Infonavit = false,
                    Gtn_Fotos_Propiedad = false,
                    Gtn_IFE_Copia = false,
                    Gtn_Junta_URBI = false,
                    Gtn_Notaria = false,
                    Gtn_No_Oficial = false,
                    Gtn_Planeacion_Fianza = false,
                    Gtn_Planta_Cartografica = false,
                    Gtn_Predial = false,
                    Gtn_Recibo_Agua = false,
                    Gtn_Recibo_Luz = false,
                    Gtn_Sol_Estado = false,
                    Gtn_Sol_Ret_Ifo = false,
                    Gtn_Urbanizacion = false,
                    Id_Cliente = cliente_id,
                    Id_Corretaje = corretaje_id
                };

                CS.Gestion.Add(gestion_obj);
                CS.SaveChanges();
            }
        }
    }
}
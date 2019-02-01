using Hangfire;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Threading.Tasks;
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
        [System.Web.Http.Route("api/Gestions/{id}")]
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
        [System.Web.Http.Route("api/Gestions/{id}")]
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
        [System.Web.Http.Route("api/Gestions/{id}")]
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
                    Gtn_Escrituras = false,
                    Gtn_Planta_Cartografica = false,
                    Gtn_Predial = false,
                    Gtn_Recibo_Luz = false,
                    Gtn_Recibo_Agua = false,
                    Gtn_Acta_Nacimiento = false,
                    Gtn_IFE_Copia = false,
                    Gtn_Sol_Ret_Ifo = false,
                    Gtn_Cert_Hip = false,
                    Gtn_Cert_Fiscal = false,
                    Gtn_Sol_Estado = false,
                    Gtn_Junta_URBI = false,
                    Gtn_Agua_Pago_Inf = false,
                    Gtn_Cert_Cartogr = false,
                    Gtn_No_Oficial = false,
                    Gtn_Avaluo = false,
                    Gtn_CT_Banco = false,
                    Gtn_Aviso_Suspension = false,
                    Gtn_Formato_Infonavit = false,
                    Gtn_Fotos_Propiedad = false,
                    Gtn_Evaluo_Contacto = false,
                    Gtn_Planeacion_Fianza = false,
                    Gtn_Urbanizacion = false,
                    Gtn_Credito_INFONAVIT = false,
                    Gtn_Notaria = false,
                    Gtn_Firma_Escrituras = false,
                    Id_Cliente = cliente_id,
                    Id_Corretaje = corretaje_id,
                    Gtn_ProgresoForm = 0,
                    Gtn_FechaAlta = DateTime.Now,
                    Gtn_CuentaBancaria = false,
                    Gtn_ReciboActualizado = false,
                    Gtn_Taller = false,
                    Gtn_Acta_Nacim_Cony = false,
                    Gtn_Acta_Matrimonio = false,
                    Gtn_DatosGnrl_Comp = false,
                    Gtn_Comp_Domicilio = false,
                    Gtn_Recibo_Nomina = false,
                    Gtn_RFC_Comprador = false,
                    Gtn_CURP_Comprador = false,
                    Gtn_RFC_Conyugue = false,
                    Gtn_CURP_Conyugue = false,
                    Gtn_Inscp_INFONAVIT = false,
                    Gtn_Acta_Nac_Ven = false,
                    Gtn_Acta_Nac_Cony_Ven = false,
                    Gtn_Acta_Matrimonio_Ven = false,
                    Gtn_IFE_Copia_Ven = false,
                    Gtn_RFC_Ven = false,
                    Gtn_CURP_Ven = false,
                    Gtn_RFC_Conyu_Ven = false,
                    Gtn_CURP_Conyu_Ven = false,
                    Gtn_Entrega_Vivienda = false,
                    Gtn_Aviso_Ret = false,
                };

                CS.Gestion.Add(gestion_obj);
                CS.SaveChanges();
            }
        }

        // GET: api/Recordatorios/5
        [System.Web.Http.Route("api/Recordatorios/{id}")]
        [ResponseType(typeof(Recordatorio))]
        public IHttpActionResult GetRecordatodio(int id)
        {
            //Recordatorio recordatorio = db.Recordatorio.Find(id);
            var recordatorio = (from c in db.Recordatorio
                                where c.Rcd_Id_Gestion == id
                                select new RecordatoriosViewModel
                                {
                                    Id = c.Rcd_Id,
                                    Titulo = c.Rcd_Titulo,
                                    //Casa = c.Gestion.Corretaje.Crt_Direccion,
                                    Anio = c.Rcd_Anio.Value,
                                    Mes = c.Rcd_Mes.Value,
                                    Dia = c.Rcd_Dia.Value,
                                    Hora = c.Rcd_Hora.Value,
                                    Minuto = c.Rcd_Minuto.Value

                                }).ToList();

            if (recordatorio == null)
            {
                return NotFound();
            }

            return Ok(recordatorio);
        }

        //ViewModel utilizado para la consulta
        public class RecordatoriosViewModel
        {
            public int Id { get; set; }
            public string Titulo { get; set; }
            public string Casa { get; set; }
            public int Anio { get; set; }
            public int Mes { get; set; }
            public int Dia { get; set; }
            public int Hora { get; set; }
            public int Minuto { get; set; }
        }

        //POST
        [System.Web.Http.Route("api/Recordatorios/{id}")]
        public HttpResponseMessage PostRecordatorios([FromBody]Recordatorio recordatorio)
        {
            try
            {
                using (CasasRedEntities entities = new CasasRedEntities())
                {
                    entities.Recordatorio.Add(recordatorio);
                    entities.SaveChanges();
                }

                var message = Request.CreateResponse(HttpStatusCode.Created, recordatorio);
                message.Headers.Location = new Uri(Request.RequestUri + recordatorio.Rcd_Id.ToString());
                return message;

            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        // POST: api/Recordatorios
        //[ResponseType(typeof(Recordatorio))]
        //public IHttpActionResult PostRecordatorios(Recordatorio recordatorio)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    recordatorio.Rcd_Enviado = (recordatorio.Rcd_Enviado == null) ? false : recordatorio.Rcd_Enviado;
        //    recordatorio.Rcd_Listado = (recordatorio.Rcd_Listado == null) ? false : recordatorio.Rcd_Listado;
        //    recordatorio.Rcd_Hora = 15;
        //    recordatorio.Rcd_Minuto = 0;
        //    db.Recordatorio.Add(recordatorio);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = recordatorio.Rcd_Id }, recordatorio);
        //}

        //Construir el correo
        public async static Task CorreoRecordatorio(string title, string body, int recordarorio_id)
        {
            GestionsController gestionController = new GestionsController();

            SmtpClient client = new SmtpClient();
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential("casasredposventa@gmail.com", "casasred123");
            client.Port = 587;
            client.Host = "smtp.gmail.com";
            client.EnableSsl = true;
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("casasredposventa@gmail.com");
            mail.To.Add("villegaspro64@gmail.com");
            mail.Subject = "Casas Red Recordatorio - " + title;
            mail.IsBodyHtml = true;
            mail.Body = body + "\n" + "<a href=\"https://www.casasredposventa.com\">Casas Red Posventa</a>";

            try
            {
                await client.SendMailAsync(mail);
                gestionController.UpdateCorreo(recordarorio_id);
            }
            catch (SmtpException x)
            {
                Console.Write(x.InnerException.Message);
            }
        }


        //Programar Recordatorios
        public async Task<IHttpActionResult> ChecarRecordatorios()
        {
            //Traer todos los recordatorios que no han sido listados
            var recordatorio = (from c in db.Recordatorio where c.Rcd_Listado == false select c).ToList();

            foreach (var item in recordatorio)
            {
                await AgregarRecordatorio(item.Rcd_Titulo, item.Rcd_Descripcion, item.Rcd_Anio.Value, item.Rcd_Mes.Value, item.Rcd_Dia.Value, item.Rcd_Hora.Value, item.Rcd_Minuto.Value, item.Rcd_Id);
                var recordatorioUpdate = (from c in db.Recordatorio where c.Rcd_Id == item.Rcd_Id select c).FirstOrDefault();
                recordatorioUpdate.Rcd_Listado = true;
                db.SaveChanges();
            }

            return null;
        }

        public async static Task AgregarRecordatorio(string title, string body, int year, int month, int day, int hour, int minutes, int recordarorio_id)
        {
            BackgroundJob.Schedule(
            () => CorreoRecordatorio(title, body, recordarorio_id),
                new DateTime(year, month, day, hour, minutes, 00));
        }

        public void UpdateCorreo(int recordatorio_id)
        {
            var recordatorioUpdate = (from c in db.Recordatorio where c.Rcd_Id == recordatorio_id select c).FirstOrDefault();
            recordatorioUpdate.Rcd_Enviado = true;
            db.SaveChanges();
        }
    }
}
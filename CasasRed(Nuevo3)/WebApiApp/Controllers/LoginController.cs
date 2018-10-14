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
    public class LoginController : ApiController
    {
        private CasasRedEntities db = new CasasRedEntities();
        // GET api/<controller>
        //public IEnumerable<string> Get(string status)
        //{
        //    if (db != null)
        //    {

        //    }
        //    return new string[] { "status", "aceptado" };
        //}

        // GET api/<controller>/5
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST api/<controller>
        [Route("api/Login")]
        [ResponseType(typeof(Usuario))]
        public IHttpActionResult PostLogin(Loginuser loginuser)
        {
            Usuario usr = new Usuario();

            if (ModelState.IsValid)
            {
                var userList = (from u in db.Usuario join ut in db.TipoUsuario on u.usu_tipo equals ut.Id.ToString() where (u.usu_username == loginuser.User || loginuser.User == u.usu_correo) && u.usu_password == loginuser.Password && u.usu_activo == true select new { u, ut }).FirstOrDefault();
                if (userList != null)
                {
                    return Ok(userList.u);  
                }
            }
            else
            {
                return NotFound();
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT api/<controller>/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        // DELETE api/<controller>/5
        //public void Delete(int id)
        //{
        //}

        public string Redireccionar(string tipo = "")
        {
            if (tipo == "")
            {
                // tipo = Session["Tipo"].ToString();
            }
            string direccion = "Home-Index";
            if (tipo == "Administrador")
            {
                direccion = "Home-Index";
            }
            else if (tipo == "Gestion")
            {
                direccion = "Gestions-Index";
            }
            else if (tipo == "Corretaje")
            {
                direccion = "Corretajes-Index";
            }
            else if (tipo == "Habilitacion")
            {
                direccion = "Habilitacions-Index";
            }
            else if (tipo == "Contabilidad")
            {
                direccion = "Contadurias-Index";
            }
            else if (tipo == "Verificacion")
            {
                direccion = "Verificacions-Index";
            }
            else if (tipo == "ApoyoGestion") direccion = "Clientes-Index";
            return direccion;
        }
    }

    public class Loginuser
    {
        private string user;
        private string password;
        
        public string User { get => user; set => user = value; }
        public string Password { get => password; set => password = value; }
    }
}
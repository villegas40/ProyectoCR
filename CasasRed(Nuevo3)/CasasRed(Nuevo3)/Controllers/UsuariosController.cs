using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CasasRed_Nuevo3_.Models;
//El bueno que va para bitbucket
namespace CasasRed_Nuevo3_.Controllers
{
    public class UsuariosController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();

        // GET: Usuarios
        public ActionResult Index()
        {

            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Administrador")
            {
                return View();
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Administrador")
            {
                if (id == null)
                {
                    return RedirectToAction("Usuarios", "Index");
                }
                Usuario usuario = db.Usuario.Find(id);
                if (usuario == null)
                {
                    return RedirectToAction("Usuarios","Index");
                }
                return View(usuario);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Administrador")
            {
                List<TipoUsuario> t = (from tu in db.TipoUsuario select tu).OrderBy(x => x.Id).ToList();
                List<string> desc = new List<string>();
                List<int> ids = new List<int>();
                foreach (TipoUsuario item in t)
                {
                    desc.Add(item.tipusu_descricion);
                    ids.Add(item.Id);
                }
                ViewBag.Descs = desc;
                ViewBag.Ids = ids;
                return View();
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,usu_username,usu_correo,usu_nombre,usu_password,usu_apellidoPa,usu_apellidoMa,usu_tipo,usu_activo,Id_TipoUsiario")] Usuario usuario)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Administrador")
            {
                if (ModelState.IsValid)
                {
                    usuario.Id_TipoUsiario = Convert.ToInt32(usuario.usu_tipo);
                    usuario.usu_alta = DateTime.Now;
                    db.Usuario.Add(usuario);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                //ViewBag.Id = new SelectList(db.TipoUsuario, "Id", "tipusu_descricion", usuario.Id);
                List<TipoUsuario> t = (from tu in db.TipoUsuario select tu).OrderBy(x => x.Id).ToList();
                List<string> desc = new List<string>();
                List<int> ids = new List<int>();
                foreach (TipoUsuario item in t)
                {
                    desc.Add(item.tipusu_descricion);
                    ids.Add(item.Id);
                }
                ViewBag.Descs = desc;
                ViewBag.Ids = ids;
                return View(usuario);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Administrador" || Session["UsuarioID"].ToString() == id.ToString())
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Usuario usuario = db.Usuario.Find(id);
                if (usuario == null)
                {
                    return RedirectToAction("Index");
                }
                //ViewBag.Id = new SelectList(db.TipoUsuario, "Id", "tipusu_descricion", usuario.Id);
                List<TipoUsuario> t = (from tu in db.TipoUsuario select tu).OrderBy(x => x.Id).ToList();
                List<string> desc = new List<string>();
                List<int> ids = new List<int>();
                foreach (TipoUsuario item in t)
                {
                    desc.Add(item.tipusu_descricion);
                    ids.Add(item.Id);
                }
                ViewBag.Descs = desc;
                ViewBag.Ids = ids;

                return View(usuario);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,usu_username,usu_correo,usu_nombre,usu_password,usu_apellidoPa,usu_apellidoMa,usu_alta,usu_tipo,usu_activo,Id_TipoUsiario")] Usuario usuario)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Administrador" || Session["UsuarioID"].ToString() == usuario.Id.ToString())
            {
                if (ModelState.IsValid)
                {
                    usuario.Id_TipoUsiario = Convert.ToInt32(usuario.usu_tipo);
                    db.Entry(usuario).State = EntityState.Modified;
                    db.SaveChanges();
                    if (Session["UsuarioID"].ToString() == usuario.Id.ToString())
                    {
                        Session["NombreCompleto"] = usuario.usu_nombre + " " + usuario.usu_apellidoPa + " " + usuario.usu_apellidoMa;
                    }
                    return RedirectToAction("Index");
                }
                //ViewBag.Id = new SelectList(db.TipoUsuario, "Id", "tipusu_descricion", usuario.Id);
                List<TipoUsuario> t = (from tu in db.TipoUsuario select tu).OrderBy(x => x.Id).ToList();
                List<string> desc = new List<string>();
                List<int> ids = new List<int>();
                foreach (TipoUsuario item in t)
                {
                    desc.Add(item.tipusu_descricion);
                    ids.Add(item.Id);
                }
                ViewBag.Descs = desc;
                ViewBag.Ids = ids;
                return View(usuario);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Administrador")
            {
                if (id == null)
                {
                    return RedirectToAction("Index");
                }
                Usuario usuario = db.Usuario.Find(id);
                if (usuario == null)
                {
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Administrador")
            {
                Usuario usuario = db.Usuario.Find(id);
                db.Usuario.Remove(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public JsonResult ComprobarUsuarios(string username = "", string correo = "")
        {
            bool existe = false;
            if (username == "" && correo == "")
            {
                existe = true;
            }
            else{
                if (username != "")
                {
            var us = (from u in db.Usuario where u.usu_username == username select u).FirstOrDefault();
            if (us != null)
            {
                existe = true;
            }
                }
                else if (correo != "")
                {
                    var us = (from u in db.Usuario where u.usu_correo == username select u).FirstOrDefault();
                    if (us != null)
                    {
                        existe = true;
                    }
                }
            }
            return Json(existe, JsonRequestBehavior.AllowGet);
        }
        public JsonResult BuscarUsuario(string filtro = "", int pagina =1, int registrosPagina = 15)
        {
            if (filtro == "")
            {
                double tot = (from u in db.Usuario select u).Count();
                int totalPagina = (int)Math.Ceiling((double)((tot / registrosPagina)));
                var busqueda = (from u in db.Usuario select new { u.Id, u.usu_username, nombre = u.usu_nombre + " " + u.usu_apellidoPa + " " + u.usu_apellidoMa, fecha = SqlFunctions.DateName("year", u.usu_alta).Trim() + "/" + SqlFunctions.StringConvert((double)u.usu_alta.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("day", u.usu_alta).Trim(), estatus = (((bool)u.usu_activo)? "Activo" : "Inactivo" ), desc = u.TipoUsuario.tipusu_descricion, total = totalPagina }).OrderBy(u => u.usu_username).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
            else
            {
                int totalPagina = (int)Math.Ceiling((double)(from u in db.Usuario where u.usu_username.Contains(filtro) || (u.usu_nombre + u.usu_apellidoPa + u.usu_apellidoMa).Contains(filtro) || u.TipoUsuario.tipusu_descricion.Contains(filtro) || u.usu_correo.Contains(filtro) select u).Count() / registrosPagina);
                var busqueda = (from u in db.Usuario where u.usu_username.Contains(filtro) || (u.usu_nombre + " " + u.usu_apellidoPa + " " + u.usu_apellidoMa).Contains(filtro) || u.TipoUsuario.tipusu_descricion.Contains(filtro) || u.usu_correo.Contains(filtro)
                                select new { u.Id, u.usu_username, nombre = u.usu_nombre + " " + u.usu_apellidoPa + " " + u.usu_apellidoMa, fecha = SqlFunctions.DateName("year", u.usu_alta).Trim() + "/" + SqlFunctions.StringConvert((double)u.usu_alta.Value.Month).TrimStart() + "/" + SqlFunctions.DateName("day", u.usu_alta).Trim(), estatus = (((bool)u.usu_activo) ? "Activo" : "Inactivo"), desc = u.TipoUsuario.tipusu_descricion, total = totalPagina }).OrderBy(u => u.usu_username).Skip((pagina - 1) * registrosPagina).Take(registrosPagina).ToList();
                return Json(busqueda, JsonRequestBehavior.AllowGet);
            }
        }
    }
}

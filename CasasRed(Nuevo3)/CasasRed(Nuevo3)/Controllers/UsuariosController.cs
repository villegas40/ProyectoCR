using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
            //var usuario = db.Usuario.Include(u => u.TipoUsuario);
            //return View(usuario.ToList());
            var usuario = db.Usuario.Include(u => u.TipoUsuario);
            return View(usuario.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            //ViewBag.Id = new SelectList(db.TipoUsuario, "Id", "tipusu_descricion");
            ViewBag.Id_TipoUsiario = new SelectList(db.TipoUsuario, "Id", "tipusu_descricion");
            return View();
        }

        // POST: Usuarios/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,usu_username,usu_correo,usu_nombre,usu_password,usu_apellidoPa,usu_apellidoMa,usu_alta,usu_tipo,usu_activo,Id_TipoUsiario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuario.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.Id = new SelectList(db.TipoUsuario, "Id", "tipusu_descricion", usuario.Id);
            ViewBag.Id_TipoUsiario = new SelectList(db.TipoUsuario, "Id", "tipusu_descricion", usuario.Id_TipoUsiario);
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            //ViewBag.Id = new SelectList(db.TipoUsuario, "Id", "tipusu_descricion", usuario.Id);
            ViewBag.Id_TipoUsiario = new SelectList(db.TipoUsuario, "Id", "tipusu_descricion", usuario.Id_TipoUsiario);
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,usu_username,usu_correo,usu_nombre,usu_password,usu_apellidoPa,usu_apellidoMa,usu_alta,usu_tipo,usu_activo,Id_TipoUsiario")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.Id = new SelectList(db.TipoUsuario, "Id", "tipusu_descricion", usuario.Id);
            ViewBag.Id_TipoUsiario = new SelectList(db.TipoUsuario, "Id", "tipusu_descricion", usuario.Id_TipoUsiario);
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuario.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuario.Find(id);
            db.Usuario.Remove(usuario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

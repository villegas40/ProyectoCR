using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CasasRed_Nuevo3_.Models;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;

namespace CasasRed_Nuevo3_.Controllers
{
    public class VendedorController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();
        // GET: Vendedor
        public ActionResult Index()
        {
            
            //Hacer vista para agregar vendedores/editarlos/Borrar/index
            if (Session["Usuario"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else if (Session["Tipo"].ToString() == "Corretaje" || Session["Tipo"].ToString() == "Administrador" || Session["Tipo"].ToString() == "Gestion")
            {

                DetailsViewModel VENDEDORES = new DetailsViewModel();

                VENDEDORES.vendedors = db.Vendedor.ToList();
                return View(VENDEDORES);
            }
            else
            {
                LoginController lc = new LoginController();
                string redireccion = lc.Redireccionar(Session["Tipo"].ToString());
                return RedirectToAction(redireccion.Split('-')[1], redireccion.Split('-')[0]);
            }
           
        }

        public class DetailsViewModel
        {
            public List<Vendedor> vendedors = new List<Vendedor>();
           
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Vendedor vendedor)
        {
            vendedor.Vndr_Nombre = (vendedor.Vndr_Nombre == null) ? "" : vendedor.Vndr_Nombre;
            vendedor.Vndr_Apellidopa = (vendedor.Vndr_Apellidopa == null) ? "" : vendedor.Vndr_Apellidopa;
            vendedor.Vndr_Apellidoma = (vendedor.Vndr_Apellidoma == null) ? "" : vendedor.Vndr_Apellidoma;

            if (ModelState.IsValid)
            {
                db.Vendedor.Add(vendedor);
                db.SaveChanges();
                return Redirect("/Vendedor/index");
            }


            return View();
        }
        public ActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vendedor vendedor = db.Vendedor.Find(id);
            return View(vendedor);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            Vendedor vendedor = db.Vendedor.Find(id);
            db.Vendedor.Remove(vendedor);
            db.SaveChanges();
            return Redirect("/Vendedor/Index");
        }

        public ActionResult Edith(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Vendedor vendedor = db.Vendedor.Find(id);
            if (vendedor == null)
            {
                return HttpNotFound();
            }

            return View(vendedor);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edith([Bind(Include = "Id,Vndr_Nombre,Vndr_Apellidopa,Vndr_Apellidoma")]Vendedor vendedor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vendedor).State = EntityState.Modified;
                db.SaveChanges();
                return Redirect("/Vendedor/Index");
            }

            return View(vendedor);
        }


    }
}
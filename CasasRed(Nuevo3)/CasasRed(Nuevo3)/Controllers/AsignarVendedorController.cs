using CasasRed_Nuevo3_.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CasasRed_Nuevo3_.Controllers
{
    public class AsignarVendedorController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();
        public static int? idcorretaje1 = 0;
        public static string departamento1 = "";
        // GET: AsignarVendedor
        public ActionResult Index(int? idcorretaje,string departamento="")
        {

            if (idcorretaje == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            }

            if (departamento=="corretaje")
            {
                DetailsViewModel VENDEDORES = new DetailsViewModel();

                VENDEDORES.vendedorAsigs = db.VendedorAsig.ToList();
                VENDEDORES.corretajes = db.Corretaje.ToList();
                VENDEDORES.vendedors = db.Vendedor.ToList();

                var Vende = ((from v in db.VendedorAsig select new { v.Id,v.Id_Corretaje,v.Id_Vendedor,v.VndAsig_Departamento }).ToList());
                List<int?> IDS = new List<int?>();
                List<int?> IDSCORRETAJE = new List<int?>();
                List<int?> IDSVENDEDOR = new List<int?>();
                List<string> DEPARTAMENTOS = new List<string>();

                foreach (var item in Vende)
                {
                    IDS.Add(item.Id);
                    IDSCORRETAJE.Add(item.Id_Corretaje);
                    IDSVENDEDOR.Add(item.Id_Vendedor);
                    DEPARTAMENTOS.Add(item.VndAsig_Departamento);
                    
                }

                ViewBag.IDS = IDS;
                ViewBag.IDSCORRETAJE = IDSCORRETAJE;
                ViewBag.IDSVENDEDOR = IDSVENDEDOR;
                ViewBag.DEPARTAMENTOS = DEPARTAMENTOS;
                ViewBag.DEPARTAMENTO = "corretaje";
                ViewBag.idreal = idcorretaje;
                return View(VENDEDORES);
            }

            else if(departamento=="gestion")
            {
                DetailsViewModel VENDEDORES = new DetailsViewModel();

                VENDEDORES.vendedorAsigs = db.VendedorAsig.ToList();
                VENDEDORES.corretajes = db.Corretaje.ToList();
                VENDEDORES.vendedors = db.Vendedor.ToList();


                var Vende = ((from v in db.VendedorAsig select new { v.Id, v.Id_Corretaje, v.Id_Vendedor, v.VndAsig_Departamento }).ToList());
                List<int?> IDS = new List<int?>();
                List<int?> IDSCORRETAJE = new List<int?>();
                List<int?> IDSVENDEDOR = new List<int?>();
                List<string> DEPARTAMENTOS = new List<string>();

                foreach (var item in Vende)
                {
                    IDS.Add(item.Id);
                    IDSCORRETAJE.Add(item.Id_Corretaje);
                    IDSVENDEDOR.Add(item.Id_Vendedor);
                    DEPARTAMENTOS.Add(item.VndAsig_Departamento);

                }

                ViewBag.IDS = IDS;
                ViewBag.IDSCORRETAJE = IDSCORRETAJE;
                ViewBag.IDSVENDEDOR = IDSVENDEDOR;
                ViewBag.DEPARTAMENTOS = DEPARTAMENTOS;
                ViewBag.DEPARTAMENTO = "gestion";
                ViewBag.idreal = idcorretaje;

                return View(VENDEDORES);
            }
            return View();
        }
        public ActionResult Create(int? idcorretaje, string departamento="")
        {
            if (idcorretaje == 0 )
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Id_Vendedor = new SelectList(db.Vendedor, "Id", "Vndr_Nombre");

            idcorretaje1 = idcorretaje;
            departamento1 = departamento; 
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VendedorAsig vendedorAsig)
        {
            vendedorAsig.Id_Vendedor = (vendedorAsig.Id_Vendedor == 0) ? 0 : vendedorAsig.Id_Vendedor;
            vendedorAsig.Id_Corretaje = idcorretaje1;
            vendedorAsig.VndAsig_Departamento = departamento1;
            if (ModelState.IsValid)
            {
                db.VendedorAsig.Add(vendedorAsig);
                db.SaveChanges();
                return Redirect("/AsignarVendedor/Index" + "?idcorretaje=" + idcorretaje1 + "&departamento=" + departamento1);
            }
            return View();
        }
        public ActionResult Delete(int? id)
        {
            DetailsViewModel DELETEVENDEDOR = new DetailsViewModel();
            DELETEVENDEDOR.vendedorAsigs.Add(db.VendedorAsig.Find(id));
            DELETEVENDEDOR.vendedors = db.Vendedor.ToList();
            return View(DELETEVENDEDOR);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? id)
        {
            VendedorAsig vendedorAsig = db.VendedorAsig.Find(id);
            int? idcorretaje = vendedorAsig.Id_Corretaje;
            string departamento = vendedorAsig.VndAsig_Departamento;
            db.VendedorAsig.Remove(vendedorAsig);

            db.SaveChanges();

            return Redirect("/AsignarVendedor/Index?idcorretaje=" + idcorretaje + "&departamento=" + departamento);
        }


        public class DetailsViewModel
        {
            public List<Vendedor> vendedors = new List<Vendedor>();
            public List<Cliente> clientes = new List<Cliente>();
            public List<Corretaje> corretajes = new List<Corretaje>();       
            public List<VendedorAsig> vendedorAsigs = new List<VendedorAsig>();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CasasRed_Nuevo3_.Models;
using System.Web.Mvc;
using System.Net;
using System.IO;

namespace CasasRed_Nuevo3_.Controllers
{
    public class GaleriaController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();
        public static int idkappa = 0;
        // GET: Galeria
        public ActionResult Index(int? idhabilitacion)
        {
            if(idhabilitacion==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DetailsViewModel FOTOS = new DetailsViewModel();
            int? idfotos = 0;
            
            FOTOS.fotoshabilitacion = db.FotosHabilitacion.ToList();
            FOTOS.habilitacions = db.Habilitacion.ToList();

            foreach (var item in FOTOS.fotoshabilitacion)
            {
                if (item.fh_habilitacion == idhabilitacion)
                {
                    idfotos = item.fh_id;
                }
            }
          
            FOTOS.habilitacions.Clear();
            FOTOS.habilitacions.Add(db.Habilitacion.Find(idhabilitacion));

            List<string> fnombre = new List<string>();
            List<string> farchivo = new List<string>();
            List<int?> fid = new List<int?>();
            var cfotos = ((from f in db.FotosHabilitacion select new {f.fh_habilitacion, f.fh_nombre, f.fh_archivo }).ToList());

            foreach (var item in cfotos)
            {
                fid.Add(item.fh_habilitacion);
                fnombre.Add(item.fh_nombre);
                farchivo.Add(item.fh_archivo);
            }

            ViewBag.listfid = fid;
            ViewBag.listfhnombre = fnombre;
            ViewBag.listfharchivo = farchivo;


            return View(FOTOS);
        }
     
        public ActionResult Create(int idhabilitacion)
        {
            idkappa = idhabilitacion;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(FotosHabilitacion fotosHabilitacion, HttpPostedFileBase fh_archivo)
        {
            fotosHabilitacion.fh_archivo = "data:image/jpg;base64," + convertTo64(fh_archivo);
            fotosHabilitacion.fh_nombre = (fotosHabilitacion.fh_nombre == null) ? "" : fotosHabilitacion.fh_nombre;
            fotosHabilitacion.fh_habilitacion = idkappa;
            if (ModelState.IsValid)
            {
                db.FotosHabilitacion.Add(fotosHabilitacion);
                db.SaveChanges();

                // return RedirectToRoute("/index" + "?" + "idhabilitacion=" + idkappa);
                //return View("/index"+"?idhabilitacion="+idkappa);
                return RedirectToAction("/index"+"?idhabilitacion="+idkappa);
            }

            return View(fotosHabilitacion);
        }

        public string convertTo64(HttpPostedFileBase cvFile)
        {
            if (cvFile == null)
            {
                return " ";
            }
            byte[] fileInBytes = new byte[cvFile.ContentLength];
            using (BinaryReader theReader = new BinaryReader(cvFile.InputStream))
            {
                fileInBytes = theReader.ReadBytes(cvFile.ContentLength);
            }
            string fileAsString = Convert.ToBase64String(fileInBytes);
            return fileAsString;
        }

        public class DetailsViewModel
        {
            public List<Cliente> clientes { get; set; }
            public List<Corretaje> corretajes = new List<Corretaje>();
            public List<Contaduria> contadurias { get; set; }
            public List<Gestion> gestions = new List<Gestion>();
            public List<Habilitacion> habilitacions = new List<Habilitacion>();
            public List<Verificacion> verificacions { get; set; }
            public List<FotosHabilitacion> fotoshabilitacion = new List<FotosHabilitacion>();

        }
    }
}
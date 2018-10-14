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
          //  int? idfotos = 0;
            
            FOTOS.fotoshabilitacion = db.FotosHabilitacion.ToList();
            FOTOS.habilitacions.Add(db.Habilitacion.Find(idhabilitacion));

            //foreach (var item in FOTOS.fotoshabilitacion)
            //{
            //    if (item.fh_habilitacion == idhabilitacion)
            //    {
            //        idfotos = item.fh_id;
            //    }
            //}
          
            //FOTOS.habilitacions.Clear();
            //FOTOS.habilitacions.Add(db.Habilitacion.Find(idhabilitacion));

            List<string> fnombre = new List<string>();
            List<string> farchivo = new List<string>();
            List<int?> fid = new List<int?>();
            List<int?> ids = new List<int?>();

            var cfotos = ((from f in db.FotosHabilitacion select new {f.fh_id,f.fh_habilitacion, f.fh_nombre, f.fh_archivo }).ToList());

            foreach (var item in cfotos)
            {
                ids.Add(item.fh_id);
                fid.Add(item.fh_habilitacion);
                fnombre.Add(item.fh_nombre);
                farchivo.Add(item.fh_archivo);
            }
            ViewBag.IDS = ids;
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

              
                return Redirect("/Galeria/index"+"?idhabilitacion="+idkappa);
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
        //Terminar metodos
       public ActionResult Delete(int? idfoto,int? idhabi)
        {
            if (idfoto == 0 || idfoto == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            FotosHabilitacion fotosHabilitacion = db.FotosHabilitacion.Find(idfoto);


            return View(fotosHabilitacion);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? idfoto,int? idhabi)
        {
            FotosHabilitacion fotosHabilitacion = db.FotosHabilitacion.Find(idfoto);
            db.FotosHabilitacion.Remove(fotosHabilitacion);
            db.SaveChanges();
            return Redirect("/Galeria/index" + "?idhabilitacion=" + idhabi);
        }

    }
}
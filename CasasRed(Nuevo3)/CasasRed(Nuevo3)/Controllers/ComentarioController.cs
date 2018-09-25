using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CasasRed_Nuevo3_.Models;
using System.Web.Mvc;
using System.Net;

namespace CasasRed_Nuevo3_.Controllers
{
    public class ComentarioController : Controller
    {
        private CasasRedEntities db = new CasasRedEntities();
        public static int idcliente2 = 0;

        // GET: Comentario
        public ActionResult Index(int? idcliente)
        {
            if (idcliente==0)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            DetailsViewModel COMENTARIOS = new DetailsViewModel();

            int? idcomentarios = 0;

            COMENTARIOS.comentarios = db.Comentarios.ToList();
            COMENTARIOS.clientes = db.Cliente.ToList();

            foreach (var item in COMENTARIOS.comentarios)
            {
                if (item.Id_Cliente == idcliente)
                {
                    idcomentarios = item.Id;
                }
            }
            COMENTARIOS.clientes.Clear();
            COMENTARIOS.clientes.Add(db.Cliente.Find(idcliente));

            List<String> cmt_titulo = new List<string>();
            List<String> Comentario = new List<string>();
            List<int?> idclientes = new List<int?>();


            var Ccomentario = ((from c in db.Comentarios select new { c.Cmt_Nota, c.Cmt_Titulo, c.Id_Cliente }).ToList());

            foreach (var item in Ccomentario)
            {
                cmt_titulo.Add(item.Cmt_Titulo);
                Comentario.Add(item.Cmt_Nota);
                idclientes.Add(item.Id_Cliente);
            }

            ViewBag.CMT_TITULOS = cmt_titulo;
            ViewBag.COMENTARIOS = Comentario;
            ViewBag.IDCLIENTES = idclientes;

       
            return View(COMENTARIOS);
        }

        public ActionResult Create(int idcliente)
        {
            idcliente2 = idcliente;

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Comentarios comentarios)
        {
            comentarios.Cmt_Titulo = (comentarios.Cmt_Titulo == null) ? "" : comentarios.Cmt_Titulo;
            comentarios.Cmt_Nota = (comentarios.Cmt_Nota == null) ? "" : comentarios.Cmt_Nota;
            comentarios.Id_Cliente = idcliente2;
            if (ModelState.IsValid)
            {
                comentarios.Cmt_Fecha = DateTime.Now;
                db.Comentarios.Add(comentarios);
                db.SaveChanges();
                return Redirect("/Comentario/Index"+"?idcliente="+idcliente2);
            }


            return View();
        }


        public class DetailsViewModel
        {
            public List<Cliente> clientes = new List<Cliente>();
            public List<Comentarios> comentarios = new List<Comentarios>();
            public List<Corretaje> corretajes = new List<Corretaje>();
            public List<Contaduria> contadurias { get; set; }
            public List<Gestion> gestions = new List<Gestion>();
            public List<Habilitacion> habilitacions = new List<Habilitacion>();
            public List<Verificacion> verificacions { get; set; }
            public List<FotosHabilitacion> fotoshabilitacion = new List<FotosHabilitacion>();

        }

        //Terminar metodos
        public ActionResult Delete(int? idcomentario)
        {
            if (idcomentario == 0 || idcomentario == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            return View();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int? idcomentario)
        {
            return View();
        }

        public ActionResult Edit(int? idcomentario)
        {
            if (idcomentario == 0 || idcomentario == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Comentarios comentarios)
        {
            return View();
        }

       
    }


}
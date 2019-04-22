using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using universidadContoso.DAL;
using universidadContoso.ViewModels;



namespace universidadContoso.Controllers
{
    public class HomeController : Controller
    {
        private EscuelaContext db = new EscuelaContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            IQueryable<FechaInscripcionGroup> data = from Estudiante in db.Estudiantes
                                                     group Estudiante by Estudiante.FechaInscripcion into dateGroup
                                                   select new FechaInscripcionGroup
                                                   {
                                                       FechaInscripcion = dateGroup.Key,
                                                       EstudiantesContador= dateGroup.Count()
                                                   };
            return View(data.ToList());
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}
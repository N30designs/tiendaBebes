using System.Linq;
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
            IQueryable<FechaInscripcionGrupo> data = from estudiante in db.estudiantes
                                                   group estudiante by estudiante.fechaInscripcion  into dateGroup
                                                   select new FechaInscripcionGrupo()
                                                   {
                                                       fechaInscripcion = dateGroup.Key,
                                                       estudiantesContador = dateGroup.Count()
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
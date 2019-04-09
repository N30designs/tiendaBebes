using PagedList;
using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using universidadContoso.DAL;
using universidadContoso.Models;
using System.Data.Entity.Infrastructure;

namespace universidadContoso.Controllers
{
    public class EstudiantesController : Controller
    {
        private EscuelaContext db = new EscuelaContext();

        // GET: Estudiantes
        public ActionResult Index(string orden, string filtroactual, string busqueda, int? pagina)
        {
            ViewBag.ordenactual = orden;
            ViewBag.apellidoOrdenP = String.IsNullOrEmpty(orden) ? "apellido_desc" : "";
            ViewBag.fechaOrdenP = orden == "Fecha" ? "fecha_desc" : "Fecha";

            if(busqueda != null)
            {
                pagina = 1;
            }
            else
            {
                busqueda = filtroactual;
            }

            ViewBag.filtroactual = busqueda;

            var estudiantes = from s in db.estudiantes select s;

            if (!String.IsNullOrEmpty(busqueda)){
                estudiantes = estudiantes.Where(s => s.apellido.Contains(busqueda) || s.nombre.Contains(busqueda));
            }

            switch(orden)
            {
                case "apellido_desc":
                    estudiantes = estudiantes.OrderByDescending(s => s.apellido);
                    break;

                case "Fecha":
                    estudiantes = estudiantes.OrderBy(s => s.fechaInscripcion);
                    break;

                case "fecha_desc":
                    estudiantes = estudiantes.OrderByDescending(s => s.fechaInscripcion);
                    break;

                default:
                    estudiantes = estudiantes.OrderBy(s => s.apellido);
                    break;
            }

            int paginaTamaño = 3;
            int paginaNumero = (pagina ?? 1);
            
            return View(estudiantes.ToPagedList(paginaNumero, paginaTamaño));
        }

        // GET: Estudiantes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.estudiantes.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }

        // GET: Estudiantes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estudiantes/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "apellido,nombre,fechaInscripcion")] Estudiante estudiante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.estudiantes.Add(estudiante);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }catch(RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                ModelState.AddModelError("", "No se han podido guardar los cambios. Inténtelo de nuevo, y si el problema persiste contacte con un administrador.");
            }

           

            return View(estudiante);
        }

        // GET: Estudiantes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.estudiantes.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }

        // POST: Estudiantes/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var studentToUpdate = db.estudiantes.Find(id);
            if (TryUpdateModel(studentToUpdate, "",
               new string[] { "apellido", "nombre", "fechaInscripcion" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "No se han podido guardar los cambios. Inténtelo de nuevo, y si continua contacte con un administrador.");
                }
            }
            return View(studentToUpdate);
        }

        // GET: Estudiantes/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {


            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest,"No ha indicado el ID del estudiante");
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "La eliminación ha fallado. Inténtelo de nuevo, y si el problema persiste contacte con un administrador.";
            }
            Estudiante estudiante = db.estudiantes.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound("Ha indicado un ID incorrecto o el estudiante no existe.");
            }
            return View(estudiante);
        }

        // POST: Estudiantes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Estudiante estudiante = db.estudiantes.Find(id);
                db.estudiantes.Remove(estudiante);
                db.SaveChanges();
            }
            catch (RetryLimitExceededException/* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.
                return RedirectToAction("Delete", new { id = id, saveChangesError = true });
            }
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using universidadContoso.DAL;
using universidadContoso.Models;
using PagedList;
using System.Data.Entity.Infrastructure;

namespace universidadContoso.Controllers
{
    public class EstudianteController : Controller
    {
        private EscuelaContext db = new EscuelaContext();

        // GET: Estudiante
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "nombre_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Fecha" ? "fecha_desc" : "Fecha";
            var estudiantes = from s in db.Estudiantes
                           select s;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;


            if (!String.IsNullOrEmpty(searchString))
            {
                estudiantes = estudiantes.Where(s => s.Apellidos.Contains(searchString)
                                       || s.Nombre.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "nombre_desc":
                    estudiantes = estudiantes.OrderByDescending(s => s.Apellidos);
                    break;
                case "Fecha":
                    estudiantes = estudiantes.OrderBy(s => s.FechaInscripcion);
                    break;
                case "fecha_desc":
                    estudiantes = estudiantes.OrderByDescending(s => s.FechaInscripcion);
                    break;
                default:
                    estudiantes = estudiantes.OrderBy(s => s.Apellidos);
                    break;
            }
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(estudiantes.ToPagedList(pageNumber, pageSize));
        }

        // GET: Estudiante/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.Estudiantes.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }

        // GET: Estudiante/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estudiante/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Nombre,Apellidos,FechaInscripcion")] Estudiante estudiante)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Estudiantes.Add(estudiante);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "No ha sido posible guardar los cambios. Inténtelo de nuevo, y si el problema persiste por favor contacte con un administrador.");
            }
            
            return View(estudiante);
        }

        // GET: Estudiante/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Estudiante estudiante = db.Estudiantes.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }

        // POST: Estudiante/Edit/5
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
            var actualizarEstudiante = db.Estudiantes.Find(id);
            if (TryUpdateModel(actualizarEstudiante, "",
               new string[] { "Apellidos", "Nombre", "FechaInscripcion" }))
            {
                try
                {
                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
                }
            }
            return View(actualizarEstudiante);
        }

        // GET: Estudiante/Delete/5
        public ActionResult Delete(int? id, bool? saveChangesError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            if (saveChangesError.GetValueOrDefault())
            {
                ViewBag.ErrorMessage = "Se ha detectado un problema al eliminar. Por favor, inténtelo de nuevo y si el problema persiste contacte con un administrador.";
            }

            Estudiante estudiante = db.Estudiantes.Find(id);
            if (estudiante == null)
            {
                return HttpNotFound();
            }
            return View(estudiante);
        }

        // POST: Estudiante/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Estudiante estudiante = db.Estudiantes.Find(id);
                db.Estudiantes.Remove(estudiante);
                db.SaveChanges();                
            }
            catch (DataException/* dex */)
            {
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using universidadContoso.DAL;
using universidadContoso.Models;
using universidadContoso.ViewModels;

namespace universidadContoso.Controllers
{
    public class ProfesorController : Controller
    {
        private EscuelaContext db = new EscuelaContext();

        // GET: Profesor
        public ActionResult Index(int? id, int? courseID)
        {
            var viewModel = new ProfesorIndexData();
            viewModel.Profesores= db.Profesores
                .Include(i => i.DespachoAsignado)
                .Include(i => i.Cursos.Select(c => c.Departamento))
                .OrderBy(i => i.Apellidos);

            if (id != null)
            {
                ViewBag.InstructorID = id.Value;
                viewModel.Cursos = viewModel.Profesores.Where(
                    i => i.ID == id.Value).Single().Cursos;
            }

            if (courseID != null)
            {
                ViewBag.CourseID = courseID.Value;
                // Lazy loading
                //viewModel.Enrollments = viewModel.Courses.Where(
                //    x => x.CourseID == courseID).Single().Enrollments;
                // Explicit loading
                var selectedCourse = viewModel.Cursos.Where(x => x.CursoID== courseID).Single();
                db.Entry(selectedCourse).Collection(x => x.Inscripciones).Load();
                foreach (Inscripcion inscripcion in selectedCourse.Inscripciones)
                {
                    db.Entry(inscripcion).Reference(x => x.Estudiante).Load();
                }

                viewModel.Inscripciones= selectedCourse.Inscripciones;
            }

            return View(viewModel);
        }

        // GET: Profesor/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesor profesor = db.Profesores.Find(id);
            if (profesor == null)
            {
                return HttpNotFound();
            }
            return View(profesor);
        }

        // GET: Profesor/Create
        public ActionResult Create()
        {
            var profesor = new Profesor();
            profesor.Cursos = new List<Curso>();
            RellenarCursoAsignadoData(profesor);
            return View();
        }

        // POST: Profesor/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nombre,Apellidos,FechaContratacion,DespachoAsignado")] Profesor profesor, string[] cursosSeleccionados)
        {

            if (cursosSeleccionados != null)
            {
                profesor.Cursos = new List<Curso>();
                foreach (var curso in  cursosSeleccionados)
                {
                    var cursoAñadir = db.Cursos.Find(int.Parse(curso));
                    profesor.Cursos.Add(cursoAñadir);
                }
            }

            if (ModelState.IsValid)
            {
                db.Profesores.Add(profesor);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            RellenarCursoAsignadoData(profesor);
            return View(profesor);
        }

        // GET: Profesor/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesor profesor = db.Profesores
                .Include(i => i.DespachoAsignado)
                .Include(i => i.Cursos)
                .Where(i => i.ID == id)
                .Single();
            RellenarCursoAsignadoData(profesor);
            if (profesor == null)
            {
                return HttpNotFound();
            }
            ViewBag.ID = new SelectList(db.DespachosAsignados, "ProfesorID", "Ubicacion", profesor.ID);
            return View(profesor);
        }

        private void RellenarCursoAsignadoData(Profesor profesor)
        {
            var allCourses = db.Cursos;
            var instructorCourses = new HashSet<int>(profesor.Cursos.Select(c => c.CursoID));
            var viewModel = new List<CursoAsignadoData>();
            foreach (var course in allCourses)
            {
                viewModel.Add(new CursoAsignadoData
                {
                    CursoID = course.CursoID,
                    Nombre = course.Nombre,
                    Asignado = instructorCourses.Contains(course.CursoID)
                });
            }
            ViewBag.Courses = viewModel;
        }

        // POST: Profesor/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int? id, string[] selectedCourses)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var instructorToUpdate = db.Profesores
               .Include(i => i.DespachoAsignado)
               .Include(i => i.Cursos)
               .Where(i => i.ID == id)
               .Single();

            if (TryUpdateModel(instructorToUpdate, "",
            
                new string[] { "Apellidos", "Nombre", "FechaContratacion", "DespachoAsignado" }))
            {
                try
                {
                    if (String.IsNullOrWhiteSpace(instructorToUpdate.DespachoAsignado.Ubicacion))
                    {
                        instructorToUpdate.DespachoAsignado= null;
                    }

                    actualizarCursosProfesor(selectedCourses, instructorToUpdate);

                    db.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.
                    ModelState.AddModelError("", "No se han podido guardar los cambios. Inténtelo de nuevo, y si el problema continua contacte con un administrador.");
                }
            }
            RellenarCursoAsignadoData(instructorToUpdate);
            return View(instructorToUpdate);
        }

        private void actualizarCursosProfesor(string[] selectedCourses, Profesor instructorToUpdate)
        {
            if (selectedCourses == null)
            {
                instructorToUpdate.Cursos = new List<Curso>();
                return;
            }

            var selectedCoursesHS = new HashSet<string>(selectedCourses);
            var instructorCourses = new HashSet<int>
                (instructorToUpdate.Cursos.Select(c => c.CursoID));
            foreach (var course in db.Cursos)
            {
                if (selectedCoursesHS.Contains(course.CursoID.ToString()))
                {
                    if (!instructorCourses.Contains(course.CursoID))
                    {
                        instructorToUpdate.Cursos.Add(course);
                    }
                }
                else
                {
                    if (instructorCourses.Contains(course.CursoID))
                    {
                        instructorToUpdate.Cursos.Remove(course);
                    }
                }
            }
        }

        // GET: Profesor/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Profesor profesor = db.Profesores.Find(id);
            if (profesor == null)
            {
                return HttpNotFound();
            }
            return View(profesor);
        }

        // POST: Profesor/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Profesor profesor = db.Profesores
                .Include(i => i.DespachoAsignado)
                .Where(i => i.ID == id)
                .Single();

            db.Profesores.Remove(profesor);

            var departamento = db.Departamentos
                .Where(d => d.ProfesorID == id)
                .SingleOrDefault();
            if (departamento != null)
            {
                departamento.ProfesorID = null;
            }


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

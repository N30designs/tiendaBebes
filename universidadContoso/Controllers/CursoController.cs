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
using System.Data.Entity.Infrastructure;

namespace universidadContoso.Controllers
{
    public class CursoController : Controller
    {
        private EscuelaContext db = new EscuelaContext();

        // GET: Curso
        public ActionResult Index(int? departamentoSeleccionado)
        {
            var departamentos = db.Departamentos.OrderBy(q => q.Nombre).ToList();
            ViewBag.SelectedDepartment = new SelectList(departamentos, "DepartamentoID", "Nombre", departamentoSeleccionado);
            int departamentoID = departamentoSeleccionado.GetValueOrDefault();

            IQueryable<Curso> cursos = db.Cursos
                .Where(c => !departamentoSeleccionado.HasValue || c.DepartamentoID == departamentoID)
                .OrderBy(d => d.CursoID)
                .Include(d => d.Departamento);
           
            return View(cursos.ToList());
        }

        // GET: Curso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = db.Cursos.Find(id);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
        }

        // GET: Curso/Create
        public ActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        // POST: Curso/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CursoID,Nombre,Creditos,DepartamentoID")] Curso curso)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Cursos.Add(curso);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                //Log the error (uncomment dex variable name and add a line here to write a log.)
                ModelState.AddModelError("", "No es posible guardar los cambios. Inténtelo de nuevo y si el problmea persiste contacte con un administrador.");
            }
            PopulateDepartmentsDropDownList(curso.DepartamentoID);
            return View(curso);
        }

        // GET: Curso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = db.Cursos.Find(id);
            if (curso == null)
            {
                return HttpNotFound();
            }
            PopulateDepartmentsDropDownList(curso.DepartamentoID);
            return View(curso);
        }

        // POST: Curso/Edit/5
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
            var courseToUpdate = db.Cursos.Find(id);
            if (TryUpdateModel(courseToUpdate, "",
               new string[] { "Nombre", "Creditos", "DepartamentoID" }))
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
            PopulateDepartmentsDropDownList(courseToUpdate.DepartamentoID);
            return View(courseToUpdate);
        }
        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = from d in db.Departamentos
                                   orderby d.Nombre
                                   select d;
            ViewBag.DepartamentoID = new SelectList(departmentsQuery, "DepartamentoID", "Nombre", selectedDepartment);
        }

        // GET: Curso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Curso curso = db.Cursos.Find(id);
            if (curso == null)
            {
                return HttpNotFound();
            }
            return View(curso);
        }

        // POST: Curso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Curso curso = db.Cursos.Find(id);
            db.Cursos.Remove(curso);
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

        public ActionResult ActualizarCreditos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ActualizarCreditos(int? multiplicador)
        {
            if (multiplicador != null)
            {
                ViewBag.FilasAfectadas = db.Database.ExecuteSqlCommand("UPDATE Curso SET Creditos = Creditos * {0}", multiplicador);
            }
            return View();
        }
    }
}

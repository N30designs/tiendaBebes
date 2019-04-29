using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using universidadContoso.DAL;
using universidadContoso.Models;
using System.Data.Entity.Infrastructure;

namespace universidadContoso.Controllers
{
    public class DepartamentoController : Controller
    {
        private EscuelaContext db = new EscuelaContext();

        // GET: Departamento
        public async Task<ActionResult> Index()
        {
            var departamentos = db.Departamentos.Include(d => d.Administrador);
            return View(await departamentos.ToListAsync());
        }

        // GET: Departamento/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamento departamento = await db.Departamentos.FindAsync(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            return View(departamento);
        }

        // GET: Departamento/Create
        public ActionResult Create()
        {
            ViewBag.ProfesorID = new SelectList(db.Profesores, "ID", "NombreCompleto");
            return View();
        }

        // POST: Departamento/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "DepartamentoID,Nombre,Presupuesto,FechaInicio,ProfesorID")] Departamento departamento)
        {
            if (ModelState.IsValid)
            {
                db.Departamentos.Add(departamento);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ProfesorID = new SelectList(db.Profesores, "ID", "NombreCompleto", departamento.ProfesorID);
            return View(departamento);
        }

        // GET: Departamento/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamento departamento = await db.Departamentos.FindAsync(id);
            if (departamento == null)
            {
                return HttpNotFound();
            }
            ViewBag.ProfesorID = new SelectList(db.Profesores, "ID", "NombreCompleto", departamento.ProfesorID);
            return View(departamento);
        }

        // POST: Departamento/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int? id, byte[] rowVersion)
        {
            string[] fieldsToBind = new string[] { "Nombre", "Presupuesto", "FechaInicio", "ProfesorID", "RowVersion" };

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var departmentToUpdate = await db.Departamentos.FindAsync(id);
            if (departmentToUpdate == null)
            {
                Departamento deletedDepartment = new Departamento();
                TryUpdateModel(deletedDepartment, fieldsToBind);
                ModelState.AddModelError(string.Empty,
                    "No se pueden guardar los cambios. El departamento ha sido eliminado por otro usuario.");
                ViewBag.ProfesorID = new SelectList(db.Profesores, "ID", "NombreCompleto", deletedDepartment.ProfesorID);
                return View(deletedDepartment);
            }

            if (TryUpdateModel(departmentToUpdate, fieldsToBind))
            {
                try
                {
                    db.Entry(departmentToUpdate).OriginalValues["RowVersion"] = rowVersion;
                    await db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    var entry = ex.Entries.Single();
                    var clientValues = (Departamento)entry.Entity;
                    var databaseEntry = entry.GetDatabaseValues();
                    if (databaseEntry == null)
                    {
                        ModelState.AddModelError(string.Empty,
                            "No se pueden guardar los cambios. El departamento ha sido eliminado por otro usuario.");
                    }
                    else
                    {
                        var databaseValues = (Departamento)databaseEntry.ToObject();

                        if (databaseValues.Nombre != clientValues.Nombre)
                            ModelState.AddModelError("Nombre", "Valor actual: "
                                + databaseValues.Nombre);
                        if (databaseValues.Presupuesto != clientValues.Presupuesto)
                            ModelState.AddModelError("Presupuesto", "Valor actual: "
                                + String.Format("{0:c}", databaseValues.Presupuesto));
                        if (databaseValues.FechaInicio!= clientValues.FechaInicio)
                            ModelState.AddModelError("Fecha de Inicio", "Valor actual: "
                                + String.Format("{0:d}", databaseValues.FechaInicio));
                        if (databaseValues.ProfesorID != clientValues.ProfesorID)
                            ModelState.AddModelError("Administrador", "Valor actual: "
                                + db.Profesores.Find(databaseValues.ProfesorID).NombreCompleto);
                        ModelState.AddModelError(string.Empty, "La entrada que está intentando editar "
                            + "ha sido modificada por otro usuario después de que usted recibiese los "
                            + "datos actuales. La operación de modificación ha sido cancelada y se "
                            + "muestran los valores actuales en la base de datos. Si quiere editar "
                            + "esta entrada, haga click en el botón guardar de nuevo, de lo contrario "
                            + "haga click en el enlace 'VOLVER AL LISTADO' ");

                        departmentToUpdate.RowVersion = databaseValues.RowVersion;
                    }
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    //Log the error (uncomment dex variable name and add a line here to write a log.)
                    ModelState.AddModelError("", "No es posible guardar los cambios. Inténtelo de nuevo, y si el problema persiste, contacte con un administrador.");
                }
            }
            ViewBag.ProfesorID = new SelectList(db.Profesores, "ID", "NombreCompleto", departmentToUpdate.ProfesorID);
            return View(departmentToUpdate);
        }

        //GET: Departamento/Delete
        public async Task<ActionResult> Delete(int? id, bool? concurrencyError)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Departamento departamento= await db.Departamentos.FindAsync(id);
            if (departamento== null)
            {
                if (concurrencyError.GetValueOrDefault())
                {
                    return RedirectToAction("Index");
                }
                return HttpNotFound();
            }

            if (concurrencyError.GetValueOrDefault())
            {
                ViewBag.ConcurrencyErrorMessage = "La entrad que está intentando eliminar "
                    + "ha sido modificada por otro usario después de que se descargasen los "
                    + "valores originales de este formulario."
                    + "La operación de eliminación ha sido cancelada, y se muestran los "
                    + "valores actuales guardados en la base de datos. Si usted desea "
                    + "continuar con la eliminación de esta entrada, haga click en el "
                    + "boton Eliminar nuevamente, en caso contrario haga click en el "
                    + "enlace: Volver al Listado.";
            }

            return View(departamento);
        }

        // POST: Departamento/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Departamento departamento)
        {
            try
            {
                db.Entry(departamento).State = EntityState.Deleted;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                return RedirectToAction("Delete", new { concurrencyError = true, id = departamento.DepartamentoID});
            }
            catch (DataException /* dex */)
            {
                //Log the error (uncomment dex variable name after DataException and add a line here to write a log.
                ModelState.AddModelError(string.Empty, "No ha sido posible eliminar. Inténtelo de nuevo, y si el problema persiste contacte con un adminsitrador.");
                return View(departamento);
            }
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

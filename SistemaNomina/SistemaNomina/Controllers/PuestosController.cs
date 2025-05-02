using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SistemaNomina;

namespace SistemaNomina.Controllers
{
    public class PuestosController : Controller
    {
        private smartbuilding_rhEntities db = new smartbuilding_rhEntities();

        // GET: Puestos
        public ActionResult Index()
        {
            var puestos = db.Puestos.Include(p => p.Departamentos).Include(p => p.Horarios);
            return View(puestos.ToList());
        }

        // GET: Puestos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Puestos puestos = db.Puestos.Find(id);
            if (puestos == null)
            {
                return HttpNotFound();
            }
            return View(puestos);
        }

        // GET: Puestos/Create
        public ActionResult Create()
        {
            ViewBag.id_departamento = new SelectList(db.Departamentos, "id_departamento", "nombre");
            ViewBag.id_horario = new SelectList(db.Horarios, "id_horario", "nombre");
            return View();
        }

        // POST: Puestos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "nombre_puesto,id_departamento,salario_base,descripcion,horas_jornada,es_jefe,id_horario")] Puestos puestos)
        {
            try
            {
                // Asignar valores automáticos
                puestos.fecha_creacion = DateTime.Now;
                puestos.fecha_actualizacion = DateTime.Now;

                // Validación de claves foráneas
                if (!db.Departamentos.Any(d => d.id_departamento == puestos.id_departamento))
                {
                    ModelState.AddModelError("id_departamento", "El departamento seleccionado no existe");
                }

                if (puestos.id_horario.HasValue && !db.Horarios.Any(h => h.id_horario == puestos.id_horario.Value))
                {
                    ModelState.AddModelError("id_horario", "El horario seleccionado no existe");
                }

                if (ModelState.IsValid)
                {
                    db.Puestos.Add(puestos);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                // Capturar errores de validación de Entity Framework
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        ModelState.AddModelError(validationError.PropertyName,
                            $"Error en {validationError.PropertyName}: {validationError.ErrorMessage}");
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                // Manejar errores específicos de la base de datos
                HandleDbUpdateException(dbEx);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error inesperado: {ex.Message}");
                LogExceptionDetails(ex);
            }

            ViewBag.id_departamento = new SelectList(db.Departamentos, "id_departamento", "nombre", puestos.id_departamento);
            ViewBag.id_horario = new SelectList(db.Horarios, "id_horario", "nombre", puestos.id_horario);
            return View(puestos);
        }

        // GET: Puestos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Puestos puestos = db.Puestos.Find(id);
            if (puestos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_departamento = new SelectList(db.Departamentos, "id_departamento", "nombre", puestos.id_departamento);
            ViewBag.id_horario = new SelectList(db.Horarios, "id_horario", "nombre", puestos.id_horario);
            return View(puestos);
        }

        // POST: Puestos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_puesto,nombre_puesto,id_departamento,salario_base,descripcion,horas_jornada,es_jefe,id_horario,fecha_creacion,fecha_actualizacion")] Puestos puestos)
        {
            try
            {
                puestos.fecha_actualizacion = DateTime.Now;

                if (ModelState.IsValid)
                {
                    db.Entry(puestos).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationErrors in ex.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        ModelState.AddModelError(validationError.PropertyName,
                            $"Error en {validationError.PropertyName}: {validationError.ErrorMessage}");
                    }
                }
            }
            catch (DbUpdateException dbEx)
            {
                HandleDbUpdateException(dbEx);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error inesperado: {ex.Message}");
                LogExceptionDetails(ex);
            }

            ViewBag.id_departamento = new SelectList(db.Departamentos, "id_departamento", "nombre", puestos.id_departamento);
            ViewBag.id_horario = new SelectList(db.Horarios, "id_horario", "nombre", puestos.id_horario);
            return View(puestos);
        }

        // GET: Puestos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Puestos puestos = db.Puestos.Find(id);
            if (puestos == null)
            {
                return HttpNotFound();
            }
            return View(puestos);
        }

        // POST: Puestos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                Puestos puestos = db.Puestos.Find(id);
                db.Puestos.Remove(puestos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (DbUpdateException dbEx)
            {
                HandleDbUpdateException(dbEx);
                return View("Delete", db.Puestos.Find(id));
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error al eliminar: {ex.Message}");
                LogExceptionDetails(ex);
                return View("Delete", db.Puestos.Find(id));
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

        #region Métodos auxiliares para manejo de errores
        private void HandleDbUpdateException(DbUpdateException dbEx)
        {
            var innerException = dbEx.InnerException?.InnerException ?? dbEx.InnerException;

            if (innerException != null)
            {
                if (innerException.Message.Contains("FK_"))
                {
                    ModelState.AddModelError("", "No se puede realizar esta acción porque hay registros relacionados.");
                }
                else if (innerException.Message.Contains("IX_") || innerException.Message.Contains("UNIQUE"))
                {
                    ModelState.AddModelError("", "Ya existe un registro con estos valores (violación de restricción única).");
                }
                else
                {
                    ModelState.AddModelError("", $"Error de base de datos: {innerException.Message}");
                }
            }
            else
            {
                ModelState.AddModelError("", $"Error de base de datos: {dbEx.Message}");
            }
        }

        private void LogExceptionDetails(Exception ex)
        {
            // Aquí puedes implementar tu propio sistema de logging
            System.Diagnostics.Debug.WriteLine($"Error: {ex.Message}");
            System.Diagnostics.Debug.WriteLine($"StackTrace: {ex.StackTrace}");

            if (ex.InnerException != null)
            {
                System.Diagnostics.Debug.WriteLine($"Inner Exception: {ex.InnerException.Message}");
            }
        }
        #endregion
    }
}
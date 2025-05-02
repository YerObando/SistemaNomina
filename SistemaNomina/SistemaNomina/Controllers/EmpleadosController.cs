using System;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using SistemaNomina;

namespace SistemaNomina.Controllers
{
    public class EmpleadosController : Controller
    {
        private smartbuilding_rhEntities db = new smartbuilding_rhEntities();

        // GET: Empleados
        public ActionResult Index()
        {
            var empleados = db.Empleados.Include(e => e.EstadoCivil)
                                      .Include(e => e.Horarios)
                                      .Include(e => e.Puestos)
                                      .OrderBy(e => e.apellido1)
                                      .ThenBy(e => e.apellido2);
            return View(empleados.ToList());
        }

        // GET: Empleados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // GET: Empleados/Create
        public ActionResult Create()
        {
            CargarListas();
            var model = new Empleados
            {
                fecha_creacion = DateTime.Now,
                fecha_actualizacion = DateTime.Now,
                estado = "ACTIVO",
                fecha_ingreso = DateTime.Today,
                cantidad_hijos = 0
            };
            return View(model);
        }

        // POST: Empleados/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cedula,nombre1,nombre2,apellido1,apellido2,fecha_nacimiento,direccion,correo,telefono,id_estado_civil,cantidad_hijos,id_puesto,id_horario,fecha_ingreso,estado")] Empleados empleado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Configurar valores automáticos
                    empleado.fecha_creacion = DateTime.Now;
                    empleado.fecha_actualizacion = DateTime.Now;

                    // Manejar campos opcionales
                    empleado.nombre2 = string.IsNullOrWhiteSpace(empleado.nombre2) ? null : empleado.nombre2;
                    empleado.apellido2 = string.IsNullOrWhiteSpace(empleado.apellido2) ? null : empleado.apellido2;
                    empleado.direccion = string.IsNullOrWhiteSpace(empleado.direccion) ? null : empleado.direccion;
                    empleado.correo = string.IsNullOrWhiteSpace(empleado.correo) ? null : empleado.correo;
                    empleado.telefono = string.IsNullOrWhiteSpace(empleado.telefono) ? null : empleado.telefono;

                    db.Empleados.Add(empleado);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException?.InnerException ?? ex.InnerException ?? ex;
                ModelState.AddModelError("", $"Error al guardar: {innerException.Message}");
                System.Diagnostics.Debug.WriteLine($"Error al guardar empleado: {innerException.Message}");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error inesperado: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Error inesperado: {ex.Message}");
            }

            CargarListas();
            return View(empleado);
        }

        // GET: Empleados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            CargarListas(empleado);
            return View(empleado);
        }

        // POST: Empleados/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_empleado,cedula,nombre1,nombre2,apellido1,apellido2,fecha_nacimiento,direccion,correo,telefono,id_estado_civil,cantidad_hijos,id_puesto,id_horario,fecha_ingreso,estado,fecha_creacion")] Empleados empleado)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    // Actualizar fecha de modificación
                    empleado.fecha_actualizacion = DateTime.Now;

                    db.Entry(empleado).State = EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                ModelState.AddModelError("", "El registro que intentas editar fue modificado por otro usuario. Recarga la página para ver los cambios actualizados.");
            }
            catch (DbUpdateException ex)
            {
                var innerException = ex.InnerException?.InnerException ?? ex.InnerException ?? ex;
                ModelState.AddModelError("", $"Error al guardar: {innerException.Message}");
                System.Diagnostics.Debug.WriteLine($"Error al actualizar empleado: {innerException.Message}");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Error inesperado: {ex.Message}");
                System.Diagnostics.Debug.WriteLine($"Error inesperado: {ex.Message}");
            }

            CargarListas(empleado);
            return View(empleado);
        }

        // GET: Empleados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Empleados empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }
            return View(empleado);
        }

        // POST: Empleados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Empleados empleado = db.Empleados.Find(id);
            if (empleado == null)
            {
                return HttpNotFound();
            }

            // Cambiar estado a INACTIVO en lugar de eliminar
            empleado.estado = "INACTIVO";
            empleado.fecha_actualizacion = DateTime.Now;
            db.Entry(empleado).State = EntityState.Modified;
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

        private void CargarListas(Empleados empleado = null)
        {
            // Estados civiles ordenados
            ViewBag.id_estado_civil = new SelectList(db.EstadoCivil.OrderBy(e => e.id_estado_civil),
                                                  "id_estado_civil", "nombre",
                                                  empleado?.id_estado_civil);

            // Horarios ordenados
            ViewBag.id_horario = new SelectList(db.Horarios.OrderBy(h => h.id_horario),
                                             "id_horario", "nombre",
                                             empleado?.id_horario);

            // Puestos ordenados
            ViewBag.id_puesto = new SelectList(db.Puestos.OrderBy(p => p.id_puesto),
                                           "id_puesto", "nombre_puesto",
                                           empleado?.id_puesto);

            // Estados (Activo/Inactivo)
            ViewBag.estado = new SelectList(new[] { "ACTIVO", "INACTIVO" },
                                         empleado?.estado ?? "ACTIVO");
        }
    }
}
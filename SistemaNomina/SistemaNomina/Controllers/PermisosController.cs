using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaNomina;

namespace SistemaNomina.Controllers
{
    public class PermisosController : Controller
    {
        private smartbuilding_rhEntities db = new smartbuilding_rhEntities();

        // GET: Permisos
        public ActionResult Index()
        {
            var permisos = db.Permisos.Include(p => p.Empleados).Include(p => p.Estados).Include(p => p.Usuarios).Include(p => p.TiposPermiso);
            return View(permisos.ToList());
        }

        // GET: Permisos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permisos permisos = db.Permisos.Find(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            return View(permisos);
        }

        // GET: Permisos/Create
        public ActionResult Create()
        {
            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula");
            ViewBag.id_estado = new SelectList(db.Estados, "id_estado", "nombre");
            ViewBag.aprobado_por = new SelectList(db.Usuarios, "id_usuario", "usuario");
            ViewBag.id_tipo_permiso = new SelectList(db.TiposPermiso, "id_tipo_permiso", "nombre");
            return View();
        }

        // POST: Permisos/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_permiso,id_empleado,fecha,horas,id_tipo_permiso,motivo,id_estado,aprobado_por,fecha_creacion,fecha_actualizacion")] Permisos permisos)
        {
            if (ModelState.IsValid)
            {
                db.Permisos.Add(permisos);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula", permisos.id_empleado);
            ViewBag.id_estado = new SelectList(db.Estados, "id_estado", "nombre", permisos.id_estado);
            ViewBag.aprobado_por = new SelectList(db.Usuarios, "id_usuario", "usuario", permisos.aprobado_por);
            ViewBag.id_tipo_permiso = new SelectList(db.TiposPermiso, "id_tipo_permiso", "nombre", permisos.id_tipo_permiso);
            return View(permisos);
        }

        // GET: Permisos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permisos permisos = db.Permisos.Find(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula", permisos.id_empleado);
            ViewBag.id_estado = new SelectList(db.Estados, "id_estado", "nombre", permisos.id_estado);
            ViewBag.aprobado_por = new SelectList(db.Usuarios, "id_usuario", "usuario", permisos.aprobado_por);
            ViewBag.id_tipo_permiso = new SelectList(db.TiposPermiso, "id_tipo_permiso", "nombre", permisos.id_tipo_permiso);
            return View(permisos);
        }

        // POST: Permisos/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_permiso,id_empleado,fecha,horas,id_tipo_permiso,motivo,id_estado,aprobado_por,fecha_creacion,fecha_actualizacion")] Permisos permisos)
        {
            if (ModelState.IsValid)
            {
                db.Entry(permisos).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula", permisos.id_empleado);
            ViewBag.id_estado = new SelectList(db.Estados, "id_estado", "nombre", permisos.id_estado);
            ViewBag.aprobado_por = new SelectList(db.Usuarios, "id_usuario", "usuario", permisos.aprobado_por);
            ViewBag.id_tipo_permiso = new SelectList(db.TiposPermiso, "id_tipo_permiso", "nombre", permisos.id_tipo_permiso);
            return View(permisos);
        }

        // GET: Permisos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Permisos permisos = db.Permisos.Find(id);
            if (permisos == null)
            {
                return HttpNotFound();
            }
            return View(permisos);
        }

        // POST: Permisos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Permisos permisos = db.Permisos.Find(id);
            db.Permisos.Remove(permisos);
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

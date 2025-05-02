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
    public class IncapacidadesController : Controller
    {
        private smartbuilding_rhEntities db = new smartbuilding_rhEntities();

        // GET: Incapacidades
        public ActionResult Index()
        {
            var incapacidades = db.Incapacidades.Include(i => i.Empleados).Include(i => i.Estados).Include(i => i.TipoIncapacidades);
            return View(incapacidades.ToList());
        }

        // GET: Incapacidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incapacidades incapacidades = db.Incapacidades.Find(id);
            if (incapacidades == null)
            {
                return HttpNotFound();
            }
            return View(incapacidades);
        }

        // GET: Incapacidades/Create
        public ActionResult Create()
        {
            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula");
            ViewBag.id_estado = new SelectList(db.Estados, "id_estado", "nombre");
            ViewBag.id_tipo_incapacidad = new SelectList(db.TipoIncapacidades, "id_tipo", "nombre");
            return View();
        }

        // POST: Incapacidades/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_incapacidad,id_empleado,fecha_inicio,fecha_fin,numero_boleta,id_tipo_incapacidad,descripcion,id_estado,dias_incapacidad,fecha_registro")] Incapacidades incapacidades)
        {
            if (ModelState.IsValid)
            {
                db.Incapacidades.Add(incapacidades);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula", incapacidades.id_empleado);
            ViewBag.id_estado = new SelectList(db.Estados, "id_estado", "nombre", incapacidades.id_estado);
            ViewBag.id_tipo_incapacidad = new SelectList(db.TipoIncapacidades, "id_tipo", "nombre", incapacidades.id_tipo_incapacidad);
            return View(incapacidades);
        }

        // GET: Incapacidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incapacidades incapacidades = db.Incapacidades.Find(id);
            if (incapacidades == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula", incapacidades.id_empleado);
            ViewBag.id_estado = new SelectList(db.Estados, "id_estado", "nombre", incapacidades.id_estado);
            ViewBag.id_tipo_incapacidad = new SelectList(db.TipoIncapacidades, "id_tipo", "nombre", incapacidades.id_tipo_incapacidad);
            return View(incapacidades);
        }

        // POST: Incapacidades/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_incapacidad,id_empleado,fecha_inicio,fecha_fin,numero_boleta,id_tipo_incapacidad,descripcion,id_estado,dias_incapacidad,fecha_registro")] Incapacidades incapacidades)
        {
            if (ModelState.IsValid)
            {
                db.Entry(incapacidades).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula", incapacidades.id_empleado);
            ViewBag.id_estado = new SelectList(db.Estados, "id_estado", "nombre", incapacidades.id_estado);
            ViewBag.id_tipo_incapacidad = new SelectList(db.TipoIncapacidades, "id_tipo", "nombre", incapacidades.id_tipo_incapacidad);
            return View(incapacidades);
        }

        // GET: Incapacidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Incapacidades incapacidades = db.Incapacidades.Find(id);
            if (incapacidades == null)
            {
                return HttpNotFound();
            }
            return View(incapacidades);
        }

        // POST: Incapacidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Incapacidades incapacidades = db.Incapacidades.Find(id);
            db.Incapacidades.Remove(incapacidades);
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

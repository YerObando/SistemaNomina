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
    public class HorasExtrasController : Controller
    {
        private smartbuilding_rhEntities db = new smartbuilding_rhEntities();

        // GET: HorasExtras
        public ActionResult Index()
        {
            var horasExtras = db.HorasExtras.Include(h => h.Empleados).Include(h => h.Estados).Include(h => h.Usuarios).Include(h => h.TiposHoraExtra);
            return View(horasExtras.ToList());
        }

        // GET: HorasExtras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HorasExtras horasExtras = db.HorasExtras.Find(id);
            if (horasExtras == null)
            {
                return HttpNotFound();
            }
            return View(horasExtras);
        }

        // GET: HorasExtras/Create
        public ActionResult Create()
        {
            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula");
            ViewBag.id_estado = new SelectList(db.Estados, "id_estado", "nombre");
            ViewBag.aprobado_por = new SelectList(db.Usuarios, "id_usuario", "usuario");
            ViewBag.id_tipo = new SelectList(db.TiposHoraExtra, "id_tipo", "nombre");
            return View();
        }

        // POST: HorasExtras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_hora_extra,id_empleado,id_tipo,fecha,hora_inicio,hora_fin,horas,valor_hora,recargo,total,motivo,id_estado,aprobado_por,fecha_aprobacion,fecha_creacion,fecha_actualizacion")] HorasExtras horasExtras)
        {
            if (ModelState.IsValid)
            {
                db.HorasExtras.Add(horasExtras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula", horasExtras.id_empleado);
            ViewBag.id_estado = new SelectList(db.Estados, "id_estado", "nombre", horasExtras.id_estado);
            ViewBag.aprobado_por = new SelectList(db.Usuarios, "id_usuario", "usuario", horasExtras.aprobado_por);
            ViewBag.id_tipo = new SelectList(db.TiposHoraExtra, "id_tipo", "nombre", horasExtras.id_tipo);
            return View(horasExtras);
        }

        // GET: HorasExtras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HorasExtras horasExtras = db.HorasExtras.Find(id);
            if (horasExtras == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula", horasExtras.id_empleado);
            ViewBag.id_estado = new SelectList(db.Estados, "id_estado", "nombre", horasExtras.id_estado);
            ViewBag.aprobado_por = new SelectList(db.Usuarios, "id_usuario", "usuario", horasExtras.aprobado_por);
            ViewBag.id_tipo = new SelectList(db.TiposHoraExtra, "id_tipo", "nombre", horasExtras.id_tipo);
            return View(horasExtras);
        }

        // POST: HorasExtras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_hora_extra,id_empleado,id_tipo,fecha,hora_inicio,hora_fin,horas,valor_hora,recargo,total,motivo,id_estado,aprobado_por,fecha_aprobacion,fecha_creacion,fecha_actualizacion")] HorasExtras horasExtras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(horasExtras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula", horasExtras.id_empleado);
            ViewBag.id_estado = new SelectList(db.Estados, "id_estado", "nombre", horasExtras.id_estado);
            ViewBag.aprobado_por = new SelectList(db.Usuarios, "id_usuario", "usuario", horasExtras.aprobado_por);
            ViewBag.id_tipo = new SelectList(db.TiposHoraExtra, "id_tipo", "nombre", horasExtras.id_tipo);
            return View(horasExtras);
        }

        // GET: HorasExtras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HorasExtras horasExtras = db.HorasExtras.Find(id);
            if (horasExtras == null)
            {
                return HttpNotFound();
            }
            return View(horasExtras);
        }

        // POST: HorasExtras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HorasExtras horasExtras = db.HorasExtras.Find(id);
            db.HorasExtras.Remove(horasExtras);
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

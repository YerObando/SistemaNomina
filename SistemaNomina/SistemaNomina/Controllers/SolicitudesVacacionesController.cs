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
    public class SolicitudesVacacionesController : Controller
    {
        private smartbuilding_rhEntities db = new smartbuilding_rhEntities();

        // GET: SolicitudesVacaciones
        public ActionResult Index()
        {
            var solicitudesVacaciones = db.SolicitudesVacaciones.Include(s => s.Estados).Include(s => s.Usuarios).Include(s => s.Vacaciones);
            return View(solicitudesVacaciones.ToList());
        }

        // GET: SolicitudesVacaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudesVacaciones solicitudesVacaciones = db.SolicitudesVacaciones.Find(id);
            if (solicitudesVacaciones == null)
            {
                return HttpNotFound();
            }
            return View(solicitudesVacaciones);
        }

        // GET: SolicitudesVacaciones/Create
        public ActionResult Create()
        {
            ViewBag.id_estado = new SelectList(db.Estados, "id_estado", "nombre");
            ViewBag.aprobado_por = new SelectList(db.Usuarios, "id_usuario", "usuario");
            ViewBag.id_vacacion = new SelectList(db.Vacaciones, "id_vacacion", "periodo");
            return View();
        }

        // POST: SolicitudesVacaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_solicitud,id_vacacion,fecha_inicio,fecha_fin,fecha_solicitud,fecha_aprobacion,aprobado_por,comentario_solicitud,comentario_respuesta,id_estado,fecha_creacion,fecha_actualizacion")] SolicitudesVacaciones solicitudesVacaciones)
        {
            if (ModelState.IsValid)
            {
                db.SolicitudesVacaciones.Add(solicitudesVacaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_estado = new SelectList(db.Estados, "id_estado", "nombre", solicitudesVacaciones.id_estado);
            ViewBag.aprobado_por = new SelectList(db.Usuarios, "id_usuario", "usuario", solicitudesVacaciones.aprobado_por);
            ViewBag.id_vacacion = new SelectList(db.Vacaciones, "id_vacacion", "periodo", solicitudesVacaciones.id_vacacion);
            return View(solicitudesVacaciones);
        }

        // GET: SolicitudesVacaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudesVacaciones solicitudesVacaciones = db.SolicitudesVacaciones.Find(id);
            if (solicitudesVacaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_estado = new SelectList(db.Estados, "id_estado", "nombre", solicitudesVacaciones.id_estado);
            ViewBag.aprobado_por = new SelectList(db.Usuarios, "id_usuario", "usuario", solicitudesVacaciones.aprobado_por);
            ViewBag.id_vacacion = new SelectList(db.Vacaciones, "id_vacacion", "periodo", solicitudesVacaciones.id_vacacion);
            return View(solicitudesVacaciones);
        }

        // POST: SolicitudesVacaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_solicitud,id_vacacion,fecha_inicio,fecha_fin,fecha_solicitud,fecha_aprobacion,aprobado_por,comentario_solicitud,comentario_respuesta,id_estado,fecha_creacion,fecha_actualizacion")] SolicitudesVacaciones solicitudesVacaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudesVacaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_estado = new SelectList(db.Estados, "id_estado", "nombre", solicitudesVacaciones.id_estado);
            ViewBag.aprobado_por = new SelectList(db.Usuarios, "id_usuario", "usuario", solicitudesVacaciones.aprobado_por);
            ViewBag.id_vacacion = new SelectList(db.Vacaciones, "id_vacacion", "periodo", solicitudesVacaciones.id_vacacion);
            return View(solicitudesVacaciones);
        }

        // GET: SolicitudesVacaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudesVacaciones solicitudesVacaciones = db.SolicitudesVacaciones.Find(id);
            if (solicitudesVacaciones == null)
            {
                return HttpNotFound();
            }
            return View(solicitudesVacaciones);
        }

        // POST: SolicitudesVacaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SolicitudesVacaciones solicitudesVacaciones = db.SolicitudesVacaciones.Find(id);
            db.SolicitudesVacaciones.Remove(solicitudesVacaciones);
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

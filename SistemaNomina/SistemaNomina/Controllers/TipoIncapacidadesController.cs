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
    public class TipoIncapacidadesController : Controller
    {
        private smartbuilding_rhEntities db = new smartbuilding_rhEntities();

        // GET: TipoIncapacidades
        public ActionResult Index()
        {
            return View(db.TipoIncapacidades.ToList());
        }

        // GET: TipoIncapacidades/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoIncapacidades tipoIncapacidades = db.TipoIncapacidades.Find(id);
            if (tipoIncapacidades == null)
            {
                return HttpNotFound();
            }
            return View(tipoIncapacidades);
        }

        // GET: TipoIncapacidades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoIncapacidades/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_tipo,nombre,dias_maximos,pago_planilla,descripcion,fecha_creacion,fecha_actualizacion")] TipoIncapacidades tipoIncapacidades)
        {
            if (ModelState.IsValid)
            {
                db.TipoIncapacidades.Add(tipoIncapacidades);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoIncapacidades);
        }

        // GET: TipoIncapacidades/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoIncapacidades tipoIncapacidades = db.TipoIncapacidades.Find(id);
            if (tipoIncapacidades == null)
            {
                return HttpNotFound();
            }
            return View(tipoIncapacidades);
        }

        // POST: TipoIncapacidades/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_tipo,nombre,dias_maximos,pago_planilla,descripcion,fecha_creacion,fecha_actualizacion")] TipoIncapacidades tipoIncapacidades)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoIncapacidades).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoIncapacidades);
        }

        // GET: TipoIncapacidades/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoIncapacidades tipoIncapacidades = db.TipoIncapacidades.Find(id);
            if (tipoIncapacidades == null)
            {
                return HttpNotFound();
            }
            return View(tipoIncapacidades);
        }

        // POST: TipoIncapacidades/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoIncapacidades tipoIncapacidades = db.TipoIncapacidades.Find(id);
            db.TipoIncapacidades.Remove(tipoIncapacidades);
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

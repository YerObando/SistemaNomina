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
    public class TiposPermisoController : Controller
    {
        private smartbuilding_rhEntities db = new smartbuilding_rhEntities();

        // GET: TiposPermiso
        public ActionResult Index()
        {
            return View(db.TiposPermiso.ToList());
        }

        // GET: TiposPermiso/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposPermiso tiposPermiso = db.TiposPermiso.Find(id);
            if (tiposPermiso == null)
            {
                return HttpNotFound();
            }
            return View(tiposPermiso);
        }

        // GET: TiposPermiso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposPermiso/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_tipo_permiso,nombre,con_goce,descripcion,fecha_creacion,fecha_actualizacion")] TiposPermiso tiposPermiso)
        {
            if (ModelState.IsValid)
            {
                db.TiposPermiso.Add(tiposPermiso);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tiposPermiso);
        }

        // GET: TiposPermiso/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposPermiso tiposPermiso = db.TiposPermiso.Find(id);
            if (tiposPermiso == null)
            {
                return HttpNotFound();
            }
            return View(tiposPermiso);
        }

        // POST: TiposPermiso/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_tipo_permiso,nombre,con_goce,descripcion,fecha_creacion,fecha_actualizacion")] TiposPermiso tiposPermiso)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiposPermiso).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tiposPermiso);
        }

        // GET: TiposPermiso/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposPermiso tiposPermiso = db.TiposPermiso.Find(id);
            if (tiposPermiso == null)
            {
                return HttpNotFound();
            }
            return View(tiposPermiso);
        }

        // POST: TiposPermiso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TiposPermiso tiposPermiso = db.TiposPermiso.Find(id);
            db.TiposPermiso.Remove(tiposPermiso);
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

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
    public class TiposHoraExtraController : Controller
    {
        private smartbuilding_rhEntities db = new smartbuilding_rhEntities();

        // GET: TiposHoraExtra
        public ActionResult Index()
        {
            return View(db.TiposHoraExtra.ToList());
        }

        // GET: TiposHoraExtra/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposHoraExtra tiposHoraExtra = db.TiposHoraExtra.Find(id);
            if (tiposHoraExtra == null)
            {
                return HttpNotFound();
            }
            return View(tiposHoraExtra);
        }

        // GET: TiposHoraExtra/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TiposHoraExtra/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_tipo,nombre,recargo,descripcion,fecha_creacion,fecha_actualizacion")] TiposHoraExtra tiposHoraExtra)
        {
            if (ModelState.IsValid)
            {
                db.TiposHoraExtra.Add(tiposHoraExtra);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tiposHoraExtra);
        }

        // GET: TiposHoraExtra/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposHoraExtra tiposHoraExtra = db.TiposHoraExtra.Find(id);
            if (tiposHoraExtra == null)
            {
                return HttpNotFound();
            }
            return View(tiposHoraExtra);
        }

        // POST: TiposHoraExtra/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_tipo,nombre,recargo,descripcion,fecha_creacion,fecha_actualizacion")] TiposHoraExtra tiposHoraExtra)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tiposHoraExtra).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tiposHoraExtra);
        }

        // GET: TiposHoraExtra/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TiposHoraExtra tiposHoraExtra = db.TiposHoraExtra.Find(id);
            if (tiposHoraExtra == null)
            {
                return HttpNotFound();
            }
            return View(tiposHoraExtra);
        }

        // POST: TiposHoraExtra/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TiposHoraExtra tiposHoraExtra = db.TiposHoraExtra.Find(id);
            db.TiposHoraExtra.Remove(tiposHoraExtra);
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

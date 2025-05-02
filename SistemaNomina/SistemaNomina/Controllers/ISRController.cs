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
    public class ISRController : Controller
    {
        private smartbuilding_rhEntities db = new smartbuilding_rhEntities();

        // GET: ISR
        public ActionResult Index()
        {
            return View(db.ISR.ToList());
        }

        // GET: ISR/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ISR iSR = db.ISR.Find(id);
            if (iSR == null)
            {
                return HttpNotFound();
            }
            return View(iSR);
        }

        // GET: ISR/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ISR/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_isr,anio,limite_inferior,limite_superior,porcentaje,exceso,credito_hijo,credito_conyuge,descripcion,fecha_creacion,fecha_actualizacion")] ISR iSR)
        {
            if (ModelState.IsValid)
            {
                db.ISR.Add(iSR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(iSR);
        }

        // GET: ISR/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ISR iSR = db.ISR.Find(id);
            if (iSR == null)
            {
                return HttpNotFound();
            }
            return View(iSR);
        }

        // POST: ISR/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_isr,anio,limite_inferior,limite_superior,porcentaje,exceso,credito_hijo,credito_conyuge,descripcion,fecha_creacion,fecha_actualizacion")] ISR iSR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(iSR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(iSR);
        }

        // GET: ISR/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ISR iSR = db.ISR.Find(id);
            if (iSR == null)
            {
                return HttpNotFound();
            }
            return View(iSR);
        }

        // POST: ISR/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ISR iSR = db.ISR.Find(id);
            db.ISR.Remove(iSR);
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

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
    public class FeriadosController : Controller
    {
        private smartbuilding_rhEntities db = new smartbuilding_rhEntities();

        // GET: Feriados
        public ActionResult Index()
        {
            return View(db.Feriados.ToList());
        }

        // GET: Feriados/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feriados feriados = db.Feriados.Find(id);
            if (feriados == null)
            {
                return HttpNotFound();
            }
            return View(feriados);
        }

        // GET: Feriados/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Feriados/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_feriado,nombre,fecha,pago_obligatorio,recargo,descripcion,fecha_creacion,fecha_actualizacion")] Feriados feriados)
        {
            if (ModelState.IsValid)
            {
                db.Feriados.Add(feriados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(feriados);
        }

        // GET: Feriados/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feriados feriados = db.Feriados.Find(id);
            if (feriados == null)
            {
                return HttpNotFound();
            }
            return View(feriados);
        }

        // POST: Feriados/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_feriado,nombre,fecha,pago_obligatorio,recargo,descripcion,fecha_creacion,fecha_actualizacion")] Feriados feriados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(feriados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(feriados);
        }

        // GET: Feriados/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feriados feriados = db.Feriados.Find(id);
            if (feriados == null)
            {
                return HttpNotFound();
            }
            return View(feriados);
        }

        // POST: Feriados/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Feriados feriados = db.Feriados.Find(id);
            db.Feriados.Remove(feriados);
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

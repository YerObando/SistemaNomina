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
    public class AguinaldoController : Controller
    {
        private smartbuilding_rhEntities db = new smartbuilding_rhEntities();

        // GET: Aguinaldo
        public ActionResult Index()
        {
            var aguinaldo = db.Aguinaldo.Include(a => a.Empleados);
            return View(aguinaldo.ToList());
        }

        // GET: Aguinaldo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aguinaldo aguinaldo = db.Aguinaldo.Find(id);
            if (aguinaldo == null)
            {
                return HttpNotFound();
            }
            return View(aguinaldo);
        }

        // GET: Aguinaldo/Create
        public ActionResult Create()
        {
            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula");
            return View();
        }

        // POST: Aguinaldo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_aguinaldo,id_empleado,monto_total,meses_laborados,anio,fecha_creacion,fecha_actualizacion")] Aguinaldo aguinaldo)
        {
            if (ModelState.IsValid)
            {
                db.Aguinaldo.Add(aguinaldo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula", aguinaldo.id_empleado);
            return View(aguinaldo);
        }

        // GET: Aguinaldo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aguinaldo aguinaldo = db.Aguinaldo.Find(id);
            if (aguinaldo == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula", aguinaldo.id_empleado);
            return View(aguinaldo);
        }

        // POST: Aguinaldo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_aguinaldo,id_empleado,monto_total,meses_laborados,anio,fecha_creacion,fecha_actualizacion")] Aguinaldo aguinaldo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aguinaldo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula", aguinaldo.id_empleado);
            return View(aguinaldo);
        }

        // GET: Aguinaldo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Aguinaldo aguinaldo = db.Aguinaldo.Find(id);
            if (aguinaldo == null)
            {
                return HttpNotFound();
            }
            return View(aguinaldo);
        }

        // POST: Aguinaldo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Aguinaldo aguinaldo = db.Aguinaldo.Find(id);
            db.Aguinaldo.Remove(aguinaldo);
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

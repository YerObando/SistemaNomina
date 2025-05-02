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
    public class LiquidacionesController : Controller
    {
        private smartbuilding_rhEntities db = new smartbuilding_rhEntities();

        // GET: Liquidaciones
        public ActionResult Index()
        {
            var liquidaciones = db.Liquidaciones.Include(l => l.Empleados).Include(l => l.TipoLiquidacion);
            return View(liquidaciones.ToList());
        }

        // GET: Liquidaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liquidaciones liquidaciones = db.Liquidaciones.Find(id);
            if (liquidaciones == null)
            {
                return HttpNotFound();
            }
            return View(liquidaciones);
        }

        // GET: Liquidaciones/Create
        public ActionResult Create()
        {
            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula");
            ViewBag.id_tipo = new SelectList(db.TipoLiquidacion, "id_tipo", "nombre");
            return View();
        }

        // POST: Liquidaciones/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_liquidacion,id_empleado,id_tipo,fecha_salida,preaviso,cesantia,vacaciones_pendientes,dias_vacaciones_pendientes,aguinaldo_proporcional,total_liquidacion,isr_liquidacion,css_liquidacion,ivm_liquidacion,fecha_creacion,fecha_actualizacion")] Liquidaciones liquidaciones)
        {
            if (ModelState.IsValid)
            {
                db.Liquidaciones.Add(liquidaciones);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula", liquidaciones.id_empleado);
            ViewBag.id_tipo = new SelectList(db.TipoLiquidacion, "id_tipo", "nombre", liquidaciones.id_tipo);
            return View(liquidaciones);
        }

        // GET: Liquidaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liquidaciones liquidaciones = db.Liquidaciones.Find(id);
            if (liquidaciones == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula", liquidaciones.id_empleado);
            ViewBag.id_tipo = new SelectList(db.TipoLiquidacion, "id_tipo", "nombre", liquidaciones.id_tipo);
            return View(liquidaciones);
        }

        // POST: Liquidaciones/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_liquidacion,id_empleado,id_tipo,fecha_salida,preaviso,cesantia,vacaciones_pendientes,dias_vacaciones_pendientes,aguinaldo_proporcional,total_liquidacion,isr_liquidacion,css_liquidacion,ivm_liquidacion,fecha_creacion,fecha_actualizacion")] Liquidaciones liquidaciones)
        {
            if (ModelState.IsValid)
            {
                db.Entry(liquidaciones).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula", liquidaciones.id_empleado);
            ViewBag.id_tipo = new SelectList(db.TipoLiquidacion, "id_tipo", "nombre", liquidaciones.id_tipo);
            return View(liquidaciones);
        }

        // GET: Liquidaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Liquidaciones liquidaciones = db.Liquidaciones.Find(id);
            if (liquidaciones == null)
            {
                return HttpNotFound();
            }
            return View(liquidaciones);
        }

        // POST: Liquidaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Liquidaciones liquidaciones = db.Liquidaciones.Find(id);
            db.Liquidaciones.Remove(liquidaciones);
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

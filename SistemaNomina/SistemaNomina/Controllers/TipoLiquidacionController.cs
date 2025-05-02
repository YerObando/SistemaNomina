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
    public class TipoLiquidacionController : Controller
    {
        private smartbuilding_rhEntities db = new smartbuilding_rhEntities();

        // GET: TipoLiquidacion
        public ActionResult Index()
        {
            return View(db.TipoLiquidacion.ToList());
        }

        // GET: TipoLiquidacion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoLiquidacion tipoLiquidacion = db.TipoLiquidacion.Find(id);
            if (tipoLiquidacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoLiquidacion);
        }

        // GET: TipoLiquidacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TipoLiquidacion/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_tipo,nombre,descripcion,fecha_creacion,fecha_actualizacion")] TipoLiquidacion tipoLiquidacion)
        {
            if (ModelState.IsValid)
            {
                db.TipoLiquidacion.Add(tipoLiquidacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tipoLiquidacion);
        }

        // GET: TipoLiquidacion/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoLiquidacion tipoLiquidacion = db.TipoLiquidacion.Find(id);
            if (tipoLiquidacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoLiquidacion);
        }

        // POST: TipoLiquidacion/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_tipo,nombre,descripcion,fecha_creacion,fecha_actualizacion")] TipoLiquidacion tipoLiquidacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tipoLiquidacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tipoLiquidacion);
        }

        // GET: TipoLiquidacion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TipoLiquidacion tipoLiquidacion = db.TipoLiquidacion.Find(id);
            if (tipoLiquidacion == null)
            {
                return HttpNotFound();
            }
            return View(tipoLiquidacion);
        }

        // POST: TipoLiquidacion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TipoLiquidacion tipoLiquidacion = db.TipoLiquidacion.Find(id);
            db.TipoLiquidacion.Remove(tipoLiquidacion);
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

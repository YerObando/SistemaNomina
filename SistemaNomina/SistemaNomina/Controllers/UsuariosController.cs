using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using SistemaNomina;

namespace SistemaNomina.Controllers
{
    public class UsuariosController : Controller
    {
        private smartbuilding_rhEntities db = new smartbuilding_rhEntities();

        // GET: Usuarios/Login
        public ActionResult Login()
        {
            return View();
        }

        // POST: Usuarios/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string usuario, string contrasena)
        {
            if (string.IsNullOrEmpty(usuario) || string.IsNullOrEmpty(contrasena))
            {
                ViewBag.Error = "Debe ingresar el usuario y la contraseña.";
                return View();
            }

            var usuarioDB = db.Usuarios.FirstOrDefault(u => u.usuario != null && u.usuario.ToLower() == usuario.ToLower());

            if (usuarioDB != null)
            {
                if (usuarioDB.contrasena == contrasena)
                {
                    FormsAuthentication.SetAuthCookie(usuarioDB.usuario, false);

                    if (usuarioDB.primer_ingreso == true)
                    {
                        return RedirectToAction("CambiarContrasena", "Usuarios", new { id = usuarioDB.id_usuario });
                    }

                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Error = "Contraseña incorrecta.";
                }
            }
            else
            {
                ViewBag.Error = "El usuario no existe.";
            }

            return View();
        }

        // GET: Usuarios
        public ActionResult Index()
        {
            var usuarios = db.Usuarios.Include(u => u.Empleados).Include(u => u.Roles);
            return View(usuarios.ToList());
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // GET: Usuarios/Create
        public ActionResult Create()
        {
            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula");
            ViewBag.id_rol = new SelectList(db.Roles, "id_rol", "nombre");
            return View();
        }

        // POST: Usuarios/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id_usuario,id_empleado,usuario,contrasena,id_rol,primer_ingreso,fecha_creacion,fecha_actualizacion")] Usuarios usuarios)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuarios);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula", usuarios.id_empleado);
            ViewBag.id_rol = new SelectList(db.Roles, "id_rol", "nombre", usuarios.id_rol);
            return View(usuarios);
        }

        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula", usuarios.id_empleado);
            ViewBag.id_rol = new SelectList(db.Roles, "id_rol", "nombre", usuarios.id_rol);
            return View(usuarios);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id_usuario,id_empleado,usuario,contrasena,id_rol,primer_ingreso,fecha_creacion,fecha_actualizacion")] Usuarios usuarios)
        {
            var errores = ModelState.Values.SelectMany(v => v.Errors).ToList();

            if (ModelState.IsValid)
            {
                db.Entry(usuarios).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.id_empleado = new SelectList(db.Empleados, "id_empleado", "cedula", usuarios.id_empleado);
            ViewBag.id_rol = new SelectList(db.Roles, "id_rol", "nombre", usuarios.id_rol);
            return View(usuarios);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuarios usuarios = db.Usuarios.Find(id);
            if (usuarios == null)
            {
                return HttpNotFound();
            }
            return View(usuarios);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuarios usuarios = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuarios);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // GET: Usuarios/Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }

        // GET: Usuarios/CambiarContrasena
        [Authorize]
        public ActionResult CambiarContrasena(int id)
        {
            var usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }

            ViewBag.id_usuario = usuario.id_usuario;
            ViewBag.usuario = usuario.usuario;
            return View();
        }

        // POST: Usuarios/CambiarContrasena
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CambiarContrasena(int id, string nuevaContrasena, string confirmarContrasena)
        {
            if (nuevaContrasena != confirmarContrasena)
            {
                ViewBag.Error = "Las contraseñas no coinciden.";
                ViewBag.id_usuario = id;
                return View();
            }

            if (nuevaContrasena.Length < 6)
            {
                ViewBag.Error = "La contraseña debe tener al menos 6 caracteres.";
                ViewBag.id_usuario = id;
                return View();
            }

            var usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }

            usuario.contrasena = nuevaContrasena;
            usuario.primer_ingreso = false;
            usuario.fecha_actualizacion = DateTime.Now;

            db.Entry(usuario).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", "Home");
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


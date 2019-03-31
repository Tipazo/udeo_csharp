using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Parcial1_PrograIX.Models;

namespace Parcial1_PrograIX.Controllers
{
    public class Registros_empleadosWebController : Controller
    {
        private DB_Proga_IX db = new DB_Proga_IX();

        // GET: Registros_empleadosWeb
        public ActionResult Index()
        {
           //var registros_empleados = db.Registros_empleados.Include(r => r.Estudiante);
            //return View(registros_empleados.ToList());
            return Json("dfdfdf");
        }

        // GET: Registros_empleadosWeb/Details/5
        public ActionResult Details(int id)
        {
            if (id.ToString() == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Estudiante es = db.Estudiantes.Find(id);

            var registros_empleados = db.Registros_empleados
                            .Where(s => s.estudiante_id == id)
                            .ToList();
                            //.FirstOrDefault<Registros_empleados>();

            if (registros_empleados == null)
            {
                return HttpNotFound();
            }
            return View(registros_empleados);
            
        }

        private ActionResult NotFound()
        {
            throw new NotImplementedException();
        }

        // GET: Registros_empleadosWeb/Create
        public ActionResult Create()
        {
            ViewBag.estudiante_id = new SelectList(db.Estudiantes, "Id", "name");
            return View();
        }

        // POST: Registros_empleadosWeb/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "tipo,estudiante_id,fecha,hora_registro")] Registros_empleados registros_empleados)
        {
            if (ModelState.IsValid)
            {
                db.Registros_empleados.Add(registros_empleados);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.estudiante_id = new SelectList(db.Estudiantes, "Id", "name", registros_empleados.estudiante_id);
            return View(registros_empleados);
        }

        // GET: Registros_empleadosWeb/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registros_empleados registros_empleados = db.Registros_empleados.Find(id);
            if (registros_empleados == null)
            {
                return HttpNotFound();
            }
            ViewBag.estudiante_id = new SelectList(db.Estudiantes, "Id", "name", registros_empleados.estudiante_id);
            return View(registros_empleados);
        }

        // POST: Registros_empleadosWeb/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "tipo,estudiante_id,fecha,hora_registro")] Registros_empleados registros_empleados)
        {
            if (ModelState.IsValid)
            {
                db.Entry(registros_empleados).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.estudiante_id = new SelectList(db.Estudiantes, "Id", "name", registros_empleados.estudiante_id);
            return View(registros_empleados);
        }

        // GET: Registros_empleadosWeb/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Registros_empleados registros_empleados = db.Registros_empleados.Find(id);
            if (registros_empleados == null)
            {
                return HttpNotFound();
            }
            return View(registros_empleados);
        }

        // POST: Registros_empleadosWeb/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Registros_empleados registros_empleados = db.Registros_empleados.Find(id);
            db.Registros_empleados.Remove(registros_empleados);
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

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Parcial1_PrograIX.Models;

namespace Parcial1_PrograIX.Controllers
{
    public class Registros_empleadosController : ApiController
    {
        private DB_Proga_IX db = new DB_Proga_IX();

        // GET: api/Registros_empleados
        public IQueryable<Registros_empleados> GetRegistros_empleados()
        {
            return db.Registros_empleados;
        }

        // GET: api/Registros_empleados/5
        [ResponseType(typeof(Registros_empleados))]
        public IHttpActionResult GetRegistros_empleados(int id)
        {
            //Registros_empleados registros_empleados = db.Registros_empleados.Find(id);
            var cEmpleados = db.Registros_empleados
                .Where(s => s.estudiante_id == id)
                .ToList();
            if (cEmpleados == null)
            {
                return NotFound();
            }

            return Ok(cEmpleados);
        }

        // PUT: api/Registros_empleados/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutRegistros_empleados(string id, Registros_empleados registros_empleados)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != registros_empleados.tipo)
            {
                return BadRequest();
            }

            db.Entry(registros_empleados).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Registros_empleadosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Registros_empleados
        [ResponseType(typeof(Registros_empleados))]
        public IHttpActionResult PostRegistros_empleados(Registros_empleados registros_empleados)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //String equery = "estudiante_id = " + registros_empleados.estudiante_id.ToString() + " AND fecha = " + registros_empleados.fecha.ToString() + " AND tipo = entrada_labores";
            var cEmpleados = db.Registros_empleados
                .Where(s => s.estudiante_id == registros_empleados.estudiante_id)
                .Where(s => s.fecha == registros_empleados.fecha)
                .Where(s => s.tipo == "entrada_labores")
                .ToList();

            if (registros_empleados.tipo != "entrada_labores")
            {
                if (cEmpleados.Count <=0)
                {
                    return Json("Primero debe marcar su entrada a labor, posterior " + registros_empleados.tipo);
                }
            }

            if (registros_empleados.tipo == "salida_almuerzo")
            {
                 var cEmpleados2 = db.Registros_empleados
                .Where(s => s.estudiante_id == registros_empleados.estudiante_id)
                .Where(s => s.fecha == registros_empleados.fecha)
                .Where(s => s.tipo == "entrada_almuerzo")
                .ToList();

                if (cEmpleados2.Count <= 0)
                {
                    return Json("Primero debe marcar su entrada de almuerzo, posterior " + registros_empleados.tipo);
                }
            }

           
            if (registros_empleados.tipo == "entrada_almuerzo")
            {

                TimeSpan diff =  registros_empleados.hora_registro - cEmpleados.First().hora_registro;
                double hours = diff.TotalHours;

                if (hours < 3)
                {
                    return Json("No puede salir almorzar, debe trabajar almenos 3 horas.");
                }
   
            }

            db.Registros_empleados.Add(registros_empleados);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (Registros_empleadosExists(registros_empleados.tipo))
                {
                    return Json("Registro duplicado: " + registros_empleados.tipo);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = registros_empleados.tipo }, registros_empleados);
        }

        // DELETE: api/Registros_empleados/5
        [ResponseType(typeof(Registros_empleados))]
        public IHttpActionResult DeleteRegistros_empleados(string id)
        {
            Registros_empleados registros_empleados = db.Registros_empleados.Find(id);
            if (registros_empleados == null)
            {
                return NotFound();
            }

            db.Registros_empleados.Remove(registros_empleados);
            db.SaveChanges();

            return Ok(registros_empleados);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Registros_empleadosExists(string id)
        {
            return db.Registros_empleados.Count(e => e.tipo == id) > 0;
        }
    }
}
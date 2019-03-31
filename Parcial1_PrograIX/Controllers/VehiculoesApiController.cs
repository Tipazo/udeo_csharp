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
    public class VehiculoesApiController : ApiController
    {
        private DB_Proga_IX db = new DB_Proga_IX();

        // GET: api/VehiculoesApi
        public IQueryable<Vehiculo> GetVehiculos()
        {
            return db.Vehiculos;
        }

        // GET: api/VehiculoesApi/5
        [ResponseType(typeof(Vehiculo))]
        public IHttpActionResult GetVehiculo(int id)
        {
            Vehiculo vehiculo = db.Vehiculos.Find(id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            return Ok(vehiculo);
        }

        // PUT: api/VehiculoesApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVehiculo(int id, Vehiculo vehiculo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != vehiculo.Id)
            {
                return BadRequest();
            }

            db.Entry(vehiculo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VehiculoExists(id))
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

        // POST: api/VehiculoesApi
        [ResponseType(typeof(Vehiculo))]
        public IHttpActionResult PostVehiculo(Vehiculo vehiculo)
        {

            bool IsProductNameExist = db.Vehiculos.Any(x => x.placa == vehiculo.placa);
            if (IsProductNameExist == true)
            {
                ModelState.AddModelError("vehiculo.placa", "Ya existe en la bae de datos");
            }

            if (vehiculo.llantas <= 0)
            {
                ModelState.AddModelError("vehiculo.llantas", "Llantas debe ser mayor a 0");
            }

            if (vehiculo.puertas <= 0)
            {
                ModelState.AddModelError("vehiculo.puertas", "Puertas debe ser mayor a 0");
            }

            if (vehiculo.odometro <= 0)
            {
                ModelState.AddModelError("vehiculo.odometro", "Odometro debe ser mayor a 0");
            }

            DateTime now = DateTime.Today;
            int limitYear = Convert.ToInt32(now.ToString("yyyy")) - 10;

            if (vehiculo.anio < limitYear)
            {
                ModelState.AddModelError("vehiculo.anio", "Anio del vehiculo debe ser mayor a: " + limitYear.ToString());
            }

            if (vehiculo.anio == Convert.ToInt32(now.ToString("yyyy")))
            {
                ModelState.AddModelError("vehiculo.anio", "Anio del vehiculo no debe ser del anio actual: " + now.ToString("yyyy"));
            }

            if (vehiculo.costo_inicial <= 99999)
            {
                ModelState.AddModelError("vehiculo.costo_inicial", "Tiene que tener un valor 99,999.99" );
            }


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vehiculos.Add(vehiculo);
            
            try
            {
                db.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        // raise a new exception nesting
                        // the current instance as InnerException
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }


            return CreatedAtRoute("DefaultApi", new { id = vehiculo.Id }, vehiculo);
        }

        // DELETE: api/VehiculoesApi/5
        [ResponseType(typeof(Vehiculo))]
        public IHttpActionResult DeleteVehiculo(int id)
        {
            Vehiculo vehiculo = db.Vehiculos.Find(id);
            if (vehiculo == null)
            {
                return NotFound();
            }

            db.Vehiculos.Remove(vehiculo);
            db.SaveChanges();

            return Ok(vehiculo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VehiculoExists(int id)
        {
            return db.Vehiculos.Count(e => e.Id == id) > 0;
        }
    }
}
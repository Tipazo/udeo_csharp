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
    public class TarifasApiController : ApiController
    {
        private DB_Proga_IX db = new DB_Proga_IX();

        // GET: api/TarifasApi
        public IQueryable<Tarifa> GetTarifas()
        {
            return db.Tarifas;
        }

        // GET: api/TarifasApi/5
        [ResponseType(typeof(Tarifa))]
        public IHttpActionResult GetTarifa(int id)
        {
            Tarifa tarifa = db.Tarifas.Find(id);
            if (tarifa == null)
            {
                return NotFound();
            }

            return Ok(tarifa);
        }

        // PUT: api/TarifasApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTarifa(int id, Tarifa tarifa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tarifa.Id)
            {
                return BadRequest();
            }

            db.Entry(tarifa).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarifaExists(id))
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

        // POST: api/TarifasApi
        [ResponseType(typeof(Tarifa))]
        public IHttpActionResult PostTarifa(Tarifa tarifa)
        {
            bool IsProductNameExist = db.Tarifas.Any(x => x.nombre == tarifa.nombre);
            if (IsProductNameExist == true)
            {
                ModelState.AddModelError("tarifa.nombre", "Ya existe en la base de datos");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tarifas.Add(tarifa);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tarifa.Id }, tarifa);
        }

        // DELETE: api/TarifasApi/5
        [ResponseType(typeof(Tarifa))]
        public IHttpActionResult DeleteTarifa(int id)
        {
            Tarifa tarifa = db.Tarifas.Find(id);
            if (tarifa == null)
            {
                return NotFound();
            }

            db.Tarifas.Remove(tarifa);
            db.SaveChanges();

            return Ok(tarifa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TarifaExists(int id)
        {
            return db.Tarifas.Count(e => e.Id == id) > 0;
        }
    }
}
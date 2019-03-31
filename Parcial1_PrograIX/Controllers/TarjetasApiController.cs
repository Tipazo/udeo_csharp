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
    public class TarjetasApiController : ApiController
    {
        private DB_Proga_IX db = new DB_Proga_IX();

        // GET: api/TarjetasApi
        public IQueryable<Tarjeta> GetTarjetas()
        {
            return db.Tarjetas;
        }

        // GET: api/TarjetasApi/5
        [ResponseType(typeof(Tarjeta))]
        public IHttpActionResult GetTarjeta(int id)
        {
            Tarjeta tarjeta = db.Tarjetas.Find(id);
            if (tarjeta == null)
            {
                return NotFound();
            }

            return Ok(tarjeta);
        }

        // PUT: api/TarjetasApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTarjeta(int id, Tarjeta tarjeta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tarjeta.Id)
            {
                return BadRequest();
            }

            if (tarjeta.minuto_tarjeta > 120 || tarjeta.minuto_tarjeta < 0)
            {
                return Json("Minuto no valido de la tarjeta");
            }

            if (tarjeta.tipo_tarjeta != "amarilla" || tarjeta.tipo_tarjeta != "roja")
            {
                return Json("tipo de tarjeta no valida: " + tarjeta.tipo_tarjeta);
            }

            db.Entry(tarjeta).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarjetaExists(id))
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

        // POST: api/TarjetasApi
        [ResponseType(typeof(Tarjeta))]
        public IHttpActionResult PostTarjeta(Tarjeta tarjeta)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (tarjeta.minuto_tarjeta > 120 || tarjeta.minuto_tarjeta < 0)
            {
                return Json("Minuto no valido de la tarjeta");
            }

            if (tarjeta.tipo_tarjeta != "amarilla" || tarjeta.tipo_tarjeta != "roja")
            {
                return Json("tipo de tarjeta no valida: " + tarjeta.tipo_tarjeta);
            }

            db.Tarjetas.Add(tarjeta);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tarjeta.Id }, tarjeta);
        }

        // DELETE: api/TarjetasApi/5
        [ResponseType(typeof(Tarjeta))]
        public IHttpActionResult DeleteTarjeta(int id)
        {
            Tarjeta tarjeta = db.Tarjetas.Find(id);
            if (tarjeta == null)
            {
                return NotFound();
            }

            db.Tarjetas.Remove(tarjeta);
            db.SaveChanges();

            return Ok(tarjeta);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TarjetaExists(int id)
        {
            return db.Tarjetas.Count(e => e.Id == id) > 0;
        }
    }
}
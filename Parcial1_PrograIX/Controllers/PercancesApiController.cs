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
using Newtonsoft.Json.Linq;
using Parcial1_PrograIX.Models;

namespace Parcial1_PrograIX.Controllers
{
    public class PercancesApiController : ApiController
    {
        private DB_Proga_IX db = new DB_Proga_IX();

        // GET: api/PercancesApi
        public IQueryable<Percance> GetPercances()
        {
            return db.Percances;
        }

        // GET: api/PercancesApi/5
        [ResponseType(typeof(Percance))]
        public IHttpActionResult GetPercance(int id)
        {
            Percance percance = db.Percances.Find(id);
            if (percance == null)
            {
                return NotFound();
            }

            return Ok(percance);
        }

        // PUT: api/PercancesApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPercance(int id, Percance percance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != percance.Id)
            {
                return BadRequest();
            }

            db.Entry(percance).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PercanceExists(id))
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

        // POST: api/PercancesApi
        [ResponseType(typeof(Percance))]
        public IHttpActionResult PostPercance(Percance percance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //var jsonObj = JObject.Parse(percance.json_resultado);
            //var c_nuevo = (int)jsonObj["costo_nuevo"];

            //Vehiculo v = db.Vehiculos.Find(percance.vehiculo_id);
            //v.costo_variacion = c_nuevo;
          
            db.Percances.Add(percance);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = percance.Id }, percance);
        }

        // DELETE: api/PercancesApi/5
        [ResponseType(typeof(Percance))]
        public IHttpActionResult DeletePercance(int id)
        {
            Percance percance = db.Percances.Find(id);
            if (percance == null)
            {
                return NotFound();
            }

            db.Percances.Remove(percance);
            db.SaveChanges();

            return Ok(percance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PercanceExists(int id)
        {
            return db.Percances.Count(e => e.Id == id) > 0;
        }
    }
}
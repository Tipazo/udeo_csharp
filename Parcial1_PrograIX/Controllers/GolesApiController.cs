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
    public class GolesApiController : ApiController
    {
        private DB_Proga_IX db = new DB_Proga_IX();

        // GET: api/GolesApi
        public IQueryable<Gole> GetGoles()
        {
            return db.Goles;
        }

        // GET: api/GolesApi/5
        [ResponseType(typeof(Gole))]
        public IHttpActionResult GetGole(int id)
        {
            Gole gole = db.Goles.Find(id);
            if (gole == null)
            {
                return NotFound();
            }

            return Ok(gole);
        }

        // PUT: api/GolesApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutGole(int id, Gole gole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gole.Id)
            {
                return BadRequest();
            }

            if (gole.minuto_gol > 120 || gole.minuto_gol < 0)
            {
                return Json("Gol no permitido a mas de 120 minutos ");
            }

            db.Entry(gole).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GoleExists(id))
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

        // POST: api/GolesApi
        [ResponseType(typeof(Gole))]
        public IHttpActionResult PostGole(Gole gole)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (gole.minuto_gol > 120 || gole.minuto_gol < 0)
            {
                return Json("Gol no permitido a mas de 120 minutos ");
            }

            db.Goles.Add(gole);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = gole.Id }, gole);
        }

        // DELETE: api/GolesApi/5
        [ResponseType(typeof(Gole))]
        public IHttpActionResult DeleteGole(int id)
        {
            Gole gole = db.Goles.Find(id);
            if (gole == null)
            {
                return NotFound();
            }

            db.Goles.Remove(gole);
            db.SaveChanges();

            return Ok(gole);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GoleExists(int id)
        {
            return db.Goles.Count(e => e.Id == id) > 0;
        }
    }
}
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
    public class Asignacion_jugadoresApiController : ApiController
    {
        private DB_Proga_IX db = new DB_Proga_IX();

        // GET: api/Asignacion_jugadoresApi
        public IQueryable<Asignacion_jugadores> GetAsignacion_jugadores()
        {
            return db.Asignacion_jugadores;
        }

        // GET: api/Asignacion_jugadoresApi/5
        [ResponseType(typeof(Asignacion_jugadores))]
        public IHttpActionResult GetAsignacion_jugadores(int id)
        {
            Asignacion_jugadores asignacion_jugadores = db.Asignacion_jugadores.Find(id);
            if (asignacion_jugadores == null)
            {
                return NotFound();
            }

            return Ok(asignacion_jugadores);
        }

        // PUT: api/Asignacion_jugadoresApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAsignacion_jugadores(int id, Asignacion_jugadores asignacion_jugadores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != asignacion_jugadores.Id)
            {
                return BadRequest();
            }

            db.Entry(asignacion_jugadores).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Asignacion_jugadoresExists(id))
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

        // POST: api/Asignacion_jugadoresApi
        [ResponseType(typeof(Asignacion_jugadores))]
        public IHttpActionResult PostAsignacion_jugadores(Asignacion_jugadores asignacion_jugadores)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Asignacion_jugadores.Add(asignacion_jugadores);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = asignacion_jugadores.Id }, asignacion_jugadores);
        }

        // DELETE: api/Asignacion_jugadoresApi/5
        [ResponseType(typeof(Asignacion_jugadores))]
        public IHttpActionResult DeleteAsignacion_jugadores(int id)
        {
            Asignacion_jugadores asignacion_jugadores = db.Asignacion_jugadores.Find(id);
            if (asignacion_jugadores == null)
            {
                return NotFound();
            }

            db.Asignacion_jugadores.Remove(asignacion_jugadores);
            db.SaveChanges();

            return Ok(asignacion_jugadores);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Asignacion_jugadoresExists(int id)
        {
            return db.Asignacion_jugadores.Count(e => e.Id == id) > 0;
        }
    }
}
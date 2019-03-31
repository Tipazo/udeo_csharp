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
    public class JugadoresApiController : ApiController
    {
        private DB_Proga_IX db = new DB_Proga_IX();

        // GET: api/JugadoresApi
        public IQueryable<Jugadore> GetJugadores()
        {
            return db.Jugadores;
        }

        // GET: api/JugadoresApi/5
        [ResponseType(typeof(Jugadore))]
        public IHttpActionResult GetJugadore(int id)
        {
            Jugadore jugadore = db.Jugadores.Find(id);
            if (jugadore == null)
            {
                return NotFound();
            }

            return Ok(jugadore);
        }

        // PUT: api/JugadoresApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutJugadore(int id, Jugadore jugadore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != jugadore.Id)
            {
                return BadRequest();
            }

            db.Entry(jugadore).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!JugadoreExists(id))
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

        // POST: api/JugadoresApi
        [ResponseType(typeof(Jugadore))]
        public IHttpActionResult PostJugadore(Jugadore jugadore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Jugadores.Add(jugadore);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = jugadore.Id }, jugadore);
        }

        // DELETE: api/JugadoresApi/5
        [ResponseType(typeof(Jugadore))]
        public IHttpActionResult DeleteJugadore(int id)
        {
            Jugadore jugadore = db.Jugadores.Find(id);
            if (jugadore == null)
            {
                return NotFound();
            }

            db.Jugadores.Remove(jugadore);
            db.SaveChanges();

            return Ok(jugadore);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool JugadoreExists(int id)
        {
            return db.Jugadores.Count(e => e.Id == id) > 0;
        }
    }
}
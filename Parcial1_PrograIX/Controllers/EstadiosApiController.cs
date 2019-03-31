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
    public class EstadiosApiController : ApiController
    {
        private DB_Proga_IX db = new DB_Proga_IX();

        // GET: api/EstadiosApi
        public IQueryable<Estadio> GetEstadios()
        {
            return db.Estadios;
        }

        // GET: api/EstadiosApi/5
        [ResponseType(typeof(Estadio))]
        public IHttpActionResult GetEstadio(int id)
        {
            Estadio estadio = db.Estadios.Find(id);
            if (estadio == null)
            {
                return NotFound();
            }

            return Ok(estadio);
        }

        // PUT: api/EstadiosApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutEstadio(int id, Estadio estadio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != estadio.Id)
            {
                return BadRequest();
            }

            db.Entry(estadio).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstadioExists(id))
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

        // POST: api/EstadiosApi
        [ResponseType(typeof(Estadio))]
        public IHttpActionResult PostEstadio(Estadio estadio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Estadios.Add(estadio);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = estadio.Id }, estadio);
        }

        // DELETE: api/EstadiosApi/5
        [ResponseType(typeof(Estadio))]
        public IHttpActionResult DeleteEstadio(int id)
        {
            Estadio estadio = db.Estadios.Find(id);
            if (estadio == null)
            {
                return NotFound();
            }

            db.Estadios.Remove(estadio);
            db.SaveChanges();

            return Ok(estadio);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool EstadioExists(int id)
        {
            return db.Estadios.Count(e => e.Id == id) > 0;
        }
    }
}
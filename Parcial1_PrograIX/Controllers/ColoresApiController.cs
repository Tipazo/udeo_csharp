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
    public class ColoresApiController : ApiController
    {
        private DB_Proga_IX db = new DB_Proga_IX();

        // GET: api/ColoresApi
        public IQueryable<Colore> GetColores()
        {
            return db.Colores;
        }

        // GET: api/ColoresApi/5
        [ResponseType(typeof(Colore))]
        public IHttpActionResult GetColore(int id)
        {
            Colore colore = db.Colores.Find(id);
            if (colore == null)
            {
                return NotFound();
            }

            return Ok(colore);
        }

        // PUT: api/ColoresApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutColore(int id, Colore colore)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != colore.Id)
            {
                return BadRequest();
            }

            db.Entry(colore).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ColoreExists(id))
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

        // POST: api/ColoresApi
        [ResponseType(typeof(Colore))]
        public IHttpActionResult PostColore(Colore colore)
        {
            bool IsProductNameExist = db.Colores.Any(x => x.name == colore.name);
            if (IsProductNameExist == true)
            {
                ModelState.AddModelError("colore.name", "Ya existe el tipo de color ingresado.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Colores.Add(colore);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = colore.Id }, colore);
        }

        // DELETE: api/ColoresApi/5
        [ResponseType(typeof(Colore))]
        public IHttpActionResult DeleteColore(int id)
        {
            Colore colore = db.Colores.Find(id);
            if (colore == null)
            {
                return NotFound();
            }

            db.Colores.Remove(colore);
            db.SaveChanges();

            return Ok(colore);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ColoreExists(int id)
        {
            return db.Colores.Count(e => e.Id == id) > 0;
        }
    }
}
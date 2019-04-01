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
    public class Tipos_gamaApiController : ApiController
    {
        private DB_Proga_IX db = new DB_Proga_IX();

        // GET: api/Tipos_gamaApi
        [Authorize]
        public IQueryable<Tipos_gama> GetTipos_gama()
        {
            return db.Tipos_gama;
        }

        // GET: api/Tipos_gamaApi/5
        [Authorize]
        [ResponseType(typeof(Tipos_gama))]
        public IHttpActionResult GetTipos_gama(int id)
        {
            Tipos_gama tipos_gama = db.Tipos_gama.Find(id);
            if (tipos_gama == null)
            {
                return NotFound();
            }

            return Ok(tipos_gama);
        }

        // PUT: api/Tipos_gamaApi/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTipos_gama(int id, Tipos_gama tipos_gama)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipos_gama.Id)
            {
                return BadRequest();
            }

            db.Entry(tipos_gama).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!Tipos_gamaExists(id))
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

        // POST: api/Tipos_gamaApi
        [Authorize]
        [ResponseType(typeof(Tipos_gama))]
        public IHttpActionResult PostTipos_gama(Tipos_gama tipos_gama)
        {
            bool IsProductNameExist = db.Tipos_gama.Any(x => x.name == tipos_gama.name);
            if (IsProductNameExist == true)
            {
                ModelState.AddModelError("tipos_gama.name", "Ya existe el tipo de gama ingresado");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tipos_gama.Add(tipos_gama);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipos_gama.Id }, tipos_gama);
        }

        // DELETE: api/Tipos_gamaApi/5
        [Authorize]
        [ResponseType(typeof(Tipos_gama))]
        public IHttpActionResult DeleteTipos_gama(int id)
        {
            Tipos_gama tipos_gama = db.Tipos_gama.Find(id);
            if (tipos_gama == null)
            {
                return NotFound();
            }

            db.Tipos_gama.Remove(tipos_gama);
            db.SaveChanges();

            return Ok(tipos_gama);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool Tipos_gamaExists(int id)
        {
            return db.Tipos_gama.Count(e => e.Id == id) > 0;
        }
    }
}
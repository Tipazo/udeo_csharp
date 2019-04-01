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
    public class FabricasApiController : ApiController
    {
        private DB_Proga_IX db = new DB_Proga_IX();

        // GET: api/FabricasApi
        [Authorize]
        public IQueryable<Fabrica> GetFabricas()
        {
            return db.Fabricas;
        }

        // GET: api/FabricasApi/5
        [Authorize]
        [ResponseType(typeof(Fabrica))]
        public IHttpActionResult GetFabrica(int id)
        {
            Fabrica fabrica = db.Fabricas.Find(id);
            if (fabrica == null)
            {
                return NotFound();
            }

            return Ok(fabrica);
        }

        // PUT: api/FabricasApi/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutFabrica(int id, Fabrica fabrica)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != fabrica.Id)
            {
                return BadRequest();
            }

            db.Entry(fabrica).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FabricaExists(id))
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

        // POST: api/FabricasApi
        [Authorize]
        [ResponseType(typeof(Fabrica))]
        public IHttpActionResult PostFabrica(Fabrica fabrica)
        {
            bool IsProductNameExist = db.Fabricas.Any(x => x.name == fabrica.name);
            if (IsProductNameExist == true)
            {
                ModelState.AddModelError("fabrica.name", "Ya existe fabrica ingresada con el mismo nombre.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Fabricas.Add(fabrica);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = fabrica.Id }, fabrica);
        }

        // DELETE: api/FabricasApi/5
        [Authorize]
        [ResponseType(typeof(Fabrica))]
        public IHttpActionResult DeleteFabrica(int id)
        {
            Fabrica fabrica = db.Fabricas.Find(id);
            if (fabrica == null)
            {
                return NotFound();
            }

            db.Fabricas.Remove(fabrica);
            db.SaveChanges();

            return Ok(fabrica);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FabricaExists(int id)
        {
            return db.Fabricas.Count(e => e.Id == id) > 0;
        }
    }
}
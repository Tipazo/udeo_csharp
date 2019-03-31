using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using System.Web.Http.Results;
using System.Web.Mvc;
using Parcial1_PrograIX.Models;

namespace Parcial1_PrograIX.Controllers
{
    public class PilotoesApiController : ApiController
    {
        private DB_Proga_IX db = new DB_Proga_IX();


        // GET: api/PilotoesAp
        public IQueryable<Piloto> GetPilotos()
        {
            return db.Pilotos;
        }

        // GET: api/PilotoesApi/5
        [ResponseType(typeof(Piloto))]
        public IHttpActionResult GetPiloto(int id)
        {
            Piloto piloto = db.Pilotos.Find(id);
            if (piloto == null)
            {
                return NotFound();
            }

            return Ok(piloto);
        }

        // PUT: api/PilotoesApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPiloto(int id, Piloto piloto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != piloto.Id)
            {
                return BadRequest();
            }

            db.Entry(piloto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PilotoExists(id))
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

        // POST: api/PilotoesApi
        [ResponseType(typeof(Piloto))]
        public IHttpActionResult PostPiloto(Piloto piloto)
        {
            bool IsProductNameExist = db.Pilotos.Any(x => x.correo == piloto.correo);
            if (IsProductNameExist == true)
            {
                ModelState.AddModelError("piloto.correo", "Ya existe en la bae de datos");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pilotos.Add(piloto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = piloto.Id }, piloto);
        }

        // DELETE: api/PilotoesApi/5
        [ResponseType(typeof(Piloto))]
        public IHttpActionResult DeletePiloto(int id)
        {
            Piloto piloto = db.Pilotos.Find(id);
            if (piloto == null)
            {
                return NotFound();
            }

            db.Pilotos.Remove(piloto);
            db.SaveChanges();

            return Ok(piloto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PilotoExists(int id)
        {
            return db.Pilotos.Count(e => e.Id == id) > 0;
        }

        public JsonResult IsUserInDB(string correo)
        {
            return Json(!db.Pilotos.Any(user => user.correo == correo), JsonRequestBehavior.AllowGet);
        }

        public JsonResult ExisteoPilocoConCorreo(string correo)
        {
            var validateName = db.Pilotos.FirstOrDefault
                                (x => x.correo == correo);
            if (validateName != null)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
        }

        private JsonResult Json(bool v, JsonRequestBehavior allowGet)
        {
            throw new NotImplementedException();
        }
    }
}
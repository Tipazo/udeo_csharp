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
    public class DispositivoesApiController : ApiController
    {
        private DB_Proga_IX db = new DB_Proga_IX();

        // GET: api/DispositivoesApi
        [Authorize]
        public IQueryable<Dispositivo> GetDispositivos()
        {
            return db.Dispositivos;
        }

        // GET: api/DispositivoesApi/5
        [Authorize]
        [ResponseType(typeof(Dispositivo))]
        public IHttpActionResult GetDispositivo(int id)
        {
            Dispositivo dispositivo = db.Dispositivos.Find(id);
            if (dispositivo == null)
            {
                return NotFound();
            }

            return Ok(dispositivo);
        }

        // GET: api/DispositivoesApi/gama/5
        [Route("api/DispositivoesApi/gama/{id}")]
        [Authorize]
        public IQueryable<Dispositivo> GetDispositivoPorGama(int id)
        {
            return db.Dispositivos.Where(x => x.gama_id == id);
        }

        // PUT: api/DispositivoesApi/5
        [Authorize]
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDispositivo(int id, Dispositivo dispositivo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dispositivo.Id)
            {
                return BadRequest();
            }

            db.Entry(dispositivo).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DispositivoExists(id))
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

        // POST: api/DispositivoesApi
        [Authorize]
        [ResponseType(typeof(Dispositivo))]
        public IHttpActionResult PostDispositivo(Dispositivo dispositivo)
        {
            bool IsProductNameExist = db.Dispositivos.Any(x => x.name == dispositivo.name);
            if (IsProductNameExist == true)
            {
                //ModelState.AddModelError("dispositivo.name", "Ya existe dispositivo con el mismo nombre");
            }

            /*
             Dispositivo d = dispositivo;

            d.name = dispositivo.name;
            d.costo = dispositivo.costo;
            Fabrica fabrica = db.Fabricas.Find(dispositivo.fabrica_id);
            d.costo_con_incremento = dispositivo.costo + ((fabrica.costo_incremento / 100) * dispositivo.costo);
            d.fecha_registro = dispositivo.fecha_registro;
            d.gama_id = dispositivo.fabrica_id;
            d.fabrica_id = dispositivo.fabrica_id;
            d.color_id = dispositivo.color_id;
            d.created_at = dispositivo.created_at;
            */
            

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dispositivos.Add(dispositivo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dispositivo.Id }, dispositivo);
        }

        // DELETE: api/DispositivoesApi/5
        [Authorize]
        [ResponseType(typeof(Dispositivo))]
        public IHttpActionResult DeleteDispositivo(int id)
        {
            Dispositivo dispositivo = db.Dispositivos.Find(id);
            if (dispositivo == null)
            {
                return NotFound();
            }

            db.Dispositivos.Remove(dispositivo);
            db.SaveChanges();

            return Ok(dispositivo);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DispositivoExists(int id)
        {
            return db.Dispositivos.Count(e => e.Id == id) > 0;
        }
    }
}
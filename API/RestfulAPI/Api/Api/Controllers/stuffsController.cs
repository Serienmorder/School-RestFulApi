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
using Api.Models;

namespace Api.Controllers
{
    public class stuffsController : ApiController
    {
        private apiEntities db = new apiEntities();
        // GET: api/stuffs
        public IQueryable<stuff> Getstuffs()
        {
            var blah = db;
                return db.stuffs;
        }

        // GET: api/stuffs/5
        [ResponseType(typeof(stuff))]
        [Authorize]
        public IHttpActionResult Getstuff(int id)
        {
            stuff stuff = db.stuffs.Find(id);
            if (stuff == null)
            {
                return NotFound();
            }
         
                return Ok(stuff);
           
         
        }

        // PUT: api/stuffs/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putstuff(int id, stuff stuff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != stuff.id)
            {
                return BadRequest();
            }

            db.Entry(stuff).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!stuffExists(id))
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

        // POST: api/stuffs
        [ResponseType(typeof(stuff))]
        public IHttpActionResult Poststuff(stuff stuff)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.stuffs.Add(stuff);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = stuff.id }, stuff);
        }

        // DELETE: api/stuffs/5
        [ResponseType(typeof(stuff))]
        public IHttpActionResult Deletestuff(int id)
        {
            stuff stuff = db.stuffs.Find(id);
            if (stuff == null)
            {
                return NotFound();
            }

            db.stuffs.Remove(stuff);
            db.SaveChanges();

            return Ok(stuff);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool stuffExists(int id)
        {
            return db.stuffs.Count(e => e.id == id) > 0;
        }
    }
}
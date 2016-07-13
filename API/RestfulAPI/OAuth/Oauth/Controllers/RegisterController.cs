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
using Oauth.Models;
using Oauth.Helpers;

namespace Oauth.Controllers
{
    public class RegisterController : ApiController
    {
        private oauthConnection db = new oauthConnection();

        // GET: api/Register
        //public IQueryable<ident> Getidents()
        //{
        //    return db.idents;
        //}

        //// GET: api/Register/5
        //[ResponseType(typeof(ident))]
        //public IHttpActionResult Getident(int id)
        //{
        //    ident ident = db.idents.Find(id);
        //    if (ident == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(ident);
        //}

        // PUT: api/Register/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putident(int id, ident ident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ident.id)
            {
                return BadRequest();
            }

            db.Entry(ident).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!identExists(id))
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

        // POST: api/Register
        //[ResponseType(typeof(ident))]
        [HttpPost]
        public IHttpActionResult Postident([FromBody]ident ident)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ident.username = ident.username.ToLower();

            if(ident.password == "")
                return BadRequest("We do not allow blank passwords");
            if(ident.subject == "")
                return BadRequest("An email address must be specified.");

            var user = db.idents.Where(x => x.username == ident.username).ToList();

            if(user.Count > 0)
                return BadRequest("User already Exists");

            ident.password = HashHelper.Sha512(ident.username, ident.password);
            db.idents.Add(ident);
            db.SaveChanges();

            user = db.idents.Where(x => x.username == ident.username).ToList();
            //return Created("User Created", ident);
            return CreatedAtRoute("DefaultApi", new { id = ident.id }, ident);
        }

        //[ResponseType(typeof(ident))]
        //public IHttpActionResult Postident(string username, string password, string subject, string firstname, string lastname)
        //{
        //    ident ident = new ident
        //    { username = username,
        //        password = HashHelper.Sha512(username + password),
        //        subject = subject,
        //        claims = new claim[] { new claim { name = "Name", value = firstname + " " + lastname } }
        //    };
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }
        //    //ident.password = HashHelper.Sha512(ident.username + ident.password);
        //    //db.idents.Add(ident);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = ident.id }, ident);
        //}

        // DELETE: api/Register/5
        //[ResponseType(typeof(ident))]
        //public IHttpActionResult Deleteident(int id)
        //{
        //    ident ident = db.idents.Find(id);
        //    if (ident == null)
        //    {
        //        return NotFound();
        //    }

        //    db.idents.Remove(ident);
        //    db.SaveChanges();

        //    return Ok(ident);
        //}

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool identExists(int id)
        {
            return db.idents.Count(e => e.id == id) > 0;
        }
    }
}
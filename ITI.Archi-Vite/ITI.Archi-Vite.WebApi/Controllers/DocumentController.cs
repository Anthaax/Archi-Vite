using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ITI.Archi_Vite.DataBase;
using ITI.Archi_Vite.Core;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class DocumentController : ApiController
    {
        private ArchiViteContext _db = new ArchiViteContext();
        DocumentManager _doc;

        // GET: api/Document
        public IQueryable<User> GetUser()
        {
            return _db.User;
        }

        // GET: api/Document/?patientId=5&proId=5
        [ResponseType(typeof(DocumentSerializable))]
            public async Task<IHttpActionResult> GetDocument(int patientId, int proId)
        {
            _doc = new DocumentManager(_db);
            DocumentSerializable doc = _doc.SeeDocument(proId, patientId);
            if (doc == null)
            {
                return NotFound();
            }

            return Ok(doc);
        }
        // GET: api/Document/?patientId=5&proId=5
        [ResponseType(typeof(DocumentSerializable))]
        public async Task<IHttpActionResult> GetDocument(int patientId)
        {
            _doc = new DocumentManager(_db);
            DocumentSerializable doc = _doc.SeeDocument(proId, patientId);
            if (doc == null)
            {
                return NotFound();
            }

            return Ok(doc);
        }

        // PUT: api/Document/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutUser(int id, User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.UserId)
            {
                return BadRequest();
            }

            _db.Entry(user).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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

        // POST: api/Document
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> PostUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.User.Add(user);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = user.UserId }, user);
        }

        // DELETE: api/Document/5
        [ResponseType(typeof(User))]
        public async Task<IHttpActionResult> DeleteUser(int id)
        {
            User user = await _db.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _db.User.Remove(user);
            await _db.SaveChangesAsync();

            return Ok(user);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserExists(int id)
        {
            return _db.User.Count(e => e.UserId == id) > 0;
        }
    }
}
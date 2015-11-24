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
    public class MessageController : ApiController
    {
        private ArchiViteContext _db = new ArchiViteContext();
        private DocumentManager _doc;

        // GET: api/Message
        public IQueryable<Follower> GetFollower()
        {
            return _db.Follower;
        }

        // GET: api/Message/5
        [ResponseType(typeof(Follower))]
        public async Task<IHttpActionResult> GetFollower(int id)
        {
            Professional pro = _db.SelectRequest.SelectProfessional(id);
            Patient patient = _db.SelectRequest.SelectPatient(pro);
            DocumentSerializable doc = _doc.SeeDocument(pro, patient);
            return Ok(doc);
        }

        // PUT: api/Message/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFollower(int id, Follower follower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != follower.PatientId)
            {
                return BadRequest();
            }

            _db.Entry(follower).State = EntityState.Modified;

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowerExists(id))
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

        // POST: api/Message
        [ResponseType(typeof(Follower))]
        public async Task<IHttpActionResult> PostFollower(Follower follower)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Follower.Add(follower);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FollowerExists(follower.PatientId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = follower.PatientId }, follower);
        }

        // DELETE: api/Message/5
        [ResponseType(typeof(Follower))]
        public async Task<IHttpActionResult> DeleteFollower(int id)
        {
            Follower follower = await _db.Follower.FindAsync(id);
            if (follower == null)
            {
                return NotFound();
            }

            _db.Follower.Remove(follower);
            await _db.SaveChangesAsync();

            return Ok(follower);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool FollowerExists(int id)
        {
            return _db.Follower.Count(e => e.PatientId == id) > 0;
        }
    }
}
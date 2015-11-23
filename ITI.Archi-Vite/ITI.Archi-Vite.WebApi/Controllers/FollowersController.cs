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

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class FollowersController : ApiController
    {
        private ArchiViteContext _db = new ArchiViteContext();

        // GET: api/Followers
        public IQueryable<Follower> GetFollowers()
        {
            return _db.Follower;
        }

        // GET: api/Followers/5
        [ResponseType(typeof(Follower))]
        public async Task<IHttpActionResult> GetFollower(int id)
        {
            Follower follower = await _db.Follower.FindAsync(id);
            if (follower == null)
            {
                return NotFound();
            }

            return Ok(follower);
        }

        // PUT: api/Followers/5
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

        // POST: api/Followers
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

        // DELETE: api/Followers/5
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
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
    public class FollowersController : ApiController
    {
        private ArchiViteContext _db = new ArchiViteContext();
        DocumentManager _doc;

        // GET: api/Followers
        public IQueryable<Follower> GetFollowers()
        {
            return _db.Follower;
        }

        // GET: api/Followers/5
        [ResponseType(typeof(Follower))]
        public async Task<IHttpActionResult> GetFollower(int id, int proId)
        {
            Follower follower = _db.SelectRequest.SelectOneFollow(id, proId);
            if (follower == null)
            {
                return NotFound();
            }

            return Ok(follower);
        }

        // PUT: api/Followers/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFollower(FollowerCreation follower)
        {
            _doc = new DocumentManager(_db);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _doc.CreateEmptyFile(follower.PatientId + "$" + follower.ProfessionalId);
            _db.AddRequest.AddFollow(follower.PatientId, follower.ProfessionalId);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Followers/5
        [ResponseType(typeof(Follower))]
        public async Task<IHttpActionResult> DeleteFollower(int patientId, int proId)
        {
            _doc = new DocumentManager(_db);
            Follower follower = _db.SelectRequest.SelectOneFollow(patientId, proId);
            if (follower == null)
            {
                return NotFound();
            }
            _db.SuppressionRequest.FollowerSuppression(follower);
            _doc.DeleteFollowerFile(proId, patientId);
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
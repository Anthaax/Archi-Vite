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
        MessageService Do = new MessageService();

        // GET: api/Message
        public IQueryable<Follower> GetFollower()
        {
            return _db.Follower;
        }

        // GET: api/Message/5
        [ResponseType(typeof(Follower))]
        public async Task<IHttpActionResult> GetFollower(int id)
        {
            var doc = Do.getFollower(id);
            return Ok(doc);
        }

        // PUT: api/Message/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFollower(int patientId,int receiverId, DateTime date )
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
     
            try
            {
                Do.putFollower(patientId, receiverId, date);               
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Message
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostFollower(List<Professional> Receivers, Professional Sender, string Title, string Contents, Patient Patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
           
            try
            {
                Do.postFollower( Receivers,  Sender,  Title,  Contents, Patient);
            }
            catch (DbUpdateException)
            {
                    throw;
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Message/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> DeleteFollower(int reciverId, int patientid, DateTime date)
        {
            if (reciverId== null || patientid == null)
            {
                return NotFound();
            }
            Do.deleteFollower(reciverId, patientid, date);
            return StatusCode(HttpStatusCode.NoContent);
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
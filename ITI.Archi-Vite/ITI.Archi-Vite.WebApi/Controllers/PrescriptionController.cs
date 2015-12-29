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
    public class PrescriptionController : ApiController
    {
        private ArchiViteContext _db = new ArchiViteContext();
        DocumentManager _doc;
        PrescriptionService Do = new PrescriptionService();

        // GET: api/Prescription
        public IQueryable<Follower> GetFollower()
        {
            return _db.Follower;
        }

        // GET: api/Prescription/?patientId=5&proId=5
        [ResponseType(typeof(DocumentSerializable))]
        public async Task<IHttpActionResult> GetPrescrition(int patientId, int proId)
        {
            DocumentSerializable prescription = Do.getPrescription(patientId, proId);
            if ( prescription == null)
            {
                return NotFound();
            }

            return (Ok(prescription));
        }

        // PUT: api/Prescription/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutFollower(List<Professional> Receivers, User Sender, Patient Patient, string Title, string DocPath)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Do.putPrescription(Receivers, Sender, Patient, Title, DocPath);
            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Prescription
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostFollower(int reciverId, int patientid, DateTime date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            
            try
            {
                Do.postFollower(reciverId, patientid, date);
            }
            catch (DbUpdateException)
            {
                    throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Prescription/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> DeleteFollower(Prescription prescription, string FilePath)
        {
            if (prescription == null || FilePath == null)
            {
                return NotFound();
            }
            Do.deleteFollower(prescription, FilePath);
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
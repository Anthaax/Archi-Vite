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
        DocumentService Do = new DocumentService();

        // GET: api/Prescription
        public IQueryable<Follower> GetFollower()
        {
            return _db.Follower;
        }

        // PUT: api/Prescription/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPrescription(PrescriptionXML pres)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            FromXml xml = new FromXml();
            Prescription p = xml.CreatePrescription(pres);
            try
            {
                Do.putPrescription(p);
            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Prescription
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostPrescripton(int reciverId, int patientid, DateTime date)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            
            try
            {
                Do.postPrescripton(reciverId, patientid, date);
            }
            catch (DbUpdateException)
            {
                    throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Prescription/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> DeletePrescription(Prescription prescription, string FilePath)
        {
            if (prescription == null || FilePath == null)
            {
                return NotFound();
            }
            Do.deletePrescription(prescription, FilePath);
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
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
    public class PatientsController : ApiController
    {
        private ArchiViteContext _db = new ArchiViteContext();
        private DocumentManager _doc ;

        // GET: api/Patients
        public IQueryable<Patient> GetPatients()
        {
            return _db.Patient;
        }

        // GET: api/Patients/5
        [ResponseType(typeof(Patient))]
        public async Task<IHttpActionResult> GetPatient(int id)
        {
            Patient patient = _db.SelectRequest.SelectPatient(id);
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        // PUT: api/Patients/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPatient(PatientCreation newPatient)
        {
            _doc = new DocumentManager(_db);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.AddRequest.AddPatient(newPatient.User.FirstName, newPatient.User.LastName, newPatient.User.Birthdate, newPatient.User.Adress, newPatient.User.City, newPatient.User.Postcode, newPatient.User.PhoneNumber, newPatient.User.Pseudo, newPatient.User.Photo, newPatient.PathFile);
            _doc.CreateEmptyFile(newPatient.User.UserId.ToString());

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

        // DELETE: api/Patients/5
        [ResponseType(typeof(Patient))]
        public async Task<IHttpActionResult> DeletePatient(int id)
        {
            _doc = new DocumentManager(_db);
            Patient patient = _db.SelectRequest.SelectPatient(id);
            if (patient == null)
            {
                return NotFound();
            }
            List<Follower> follow = _db.SelectRequest.SelectFollowForPatient(id);
            foreach(var f in follow)
            {
                _doc.DeleteFollowerFile(f.Professionnal.ProfessionalId, patient.PatientId);
            }
            _doc.DeletePatientFile(patient.PatientId);
            _db.SuppressionRequest.PatientSuppression(patient);
            _db.Patient.Remove(patient);
            await _db.SaveChangesAsync();

            return Ok(patient);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PatientExists(int id)
        {
            return _db.Patient.Count(e => e.PatientId == id) > 0;
        }
    }
}
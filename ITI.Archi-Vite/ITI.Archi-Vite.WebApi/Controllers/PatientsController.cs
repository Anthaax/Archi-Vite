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
    public class PatientsController : ApiController
    {
        private ArchiViteContext _db = new ArchiViteContext();

        // GET: api/Patients
        public IQueryable<Patient> GetPatients()
        {
            return _db.Patient;
        }

        // GET: api/Patients/5
        [ResponseType(typeof(Patient))]
        public async Task<IHttpActionResult> GetPatient(int id)
        {
            Patient patient = await _db.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        // PUT: api/Patients/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPatient(int id, PatientCreation newPatient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != newPatient.User.UserId)
            {
                return BadRequest();
            }

            _db.Ar.AddPatient(newPatient.User.FirstName, newPatient.User.LastName, newPatient.User.Birthdate, newPatient.User.Adress, newPatient.User.City, newPatient.User.Postcode, newPatient.User.PhoneNumber, newPatient.User.Email, newPatient.User.Photo, newPatient.Referent, newPatient.PathFile);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PatientExists(id))
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

        // POST: api/Patients
        [ResponseType(typeof(Patient))]
        public async Task<IHttpActionResult> PostPatient(Patient patient)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Patient.Add(patient);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (PatientExists(patient.PatientId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = patient.PatientId }, patient);
        }

        // DELETE: api/Patients/5
        [ResponseType(typeof(Patient))]
        public async Task<IHttpActionResult> DeletePatient(int id)
        {
            Patient patient = await _db.Patient.FindAsync(id);
            if (patient == null)
            {
                return NotFound();
            }

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
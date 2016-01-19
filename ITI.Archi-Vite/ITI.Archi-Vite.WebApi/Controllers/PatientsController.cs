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
        private DocumentManager _doc ;
        PatientService Do = new PatientService();

        // GET: api/Patients
        public IQueryable<Patient> GetPatients()
        {
            return _db.Patient;
        }

        // GET: api/Patients/5
        [ResponseType(typeof(Patient))]
        public async Task<IHttpActionResult> GetPatient(int id)
        {
            Patient patient = Do.getPatient(id);
            if ( patient == null)
            {
                return NotFound();
            }

            return Ok(patient);
        }

        // PUT: api/Patients/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPatient(PatientCreation newPatient)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }            
            try
            {
                Do.putPatient(newPatient);
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
            Patient patient = Do.deletePatientCheck(id);
            if (patient == null)
            {
                return NotFound();
            }

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
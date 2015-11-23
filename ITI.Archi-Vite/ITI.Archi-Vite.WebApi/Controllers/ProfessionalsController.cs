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
    public class ProfessionalsController : ApiController
    {
        private ArchiViteContext _db = new ArchiViteContext();

        // GET: api/Professionals
        public IQueryable<Professional> GetProfessionals()
        {
            return _db.Professional;
        }

        // GET: api/Professionals/5
        [ResponseType(typeof(Professional))]
        public async Task<IHttpActionResult> GetProfessional(int id)
        {
            Professional professional = await _db.Professional.FindAsync(id);
            if (professional == null)
            {
                return NotFound();
            }

            return Ok(professional);
        }

        // PUT: api/Professionals/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProfessional(int id, ProfessionalCreation newProfessional)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != newProfessional.User.UserId)
            {
                return BadRequest();
            }

            _db.Ar.AddProfessional(newProfessional.User.FirstName, newProfessional.User.LastName, newProfessional.User.Birthdate, newProfessional.User.Adress, newProfessional.User.City, newProfessional.User.Postcode, newProfessional.User.PhoneNumber, newProfessional.User.Email, newProfessional.User.Photo, newProfessional.Role);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProfessionalExists(id))
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

        // POST: api/Professionals
        [ResponseType(typeof(Professional))]
        public async Task<IHttpActionResult> PostProfessional(Professional professional)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _db.Professional.Add(professional);

            try
            {
                await _db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (ProfessionalExists(professional.ProfessionalId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = professional.ProfessionalId }, professional);
        }

        // DELETE: api/Professionals/5
        [ResponseType(typeof(Professional))]
        public async Task<IHttpActionResult> DeleteProfessional(int id)
        {
            Professional professional = await _db.Professional.FindAsync(id);
            if (professional == null)
            {
                return NotFound();
            }

            _db.Professional.Remove(professional);
            await _db.SaveChangesAsync();

            return Ok(professional);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ProfessionalExists(int id)
        {
            return _db.Professional.Count(e => e.ProfessionalId == id) > 0;
        }
    }
}
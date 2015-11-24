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
    public class ProfessionalsController : ApiController
    {
        private ArchiViteContext _db = new ArchiViteContext();
        private DocumentManager _doc;


        // GET: api/Professionals
        public IQueryable<Professional> GetProfessionals()
        {
            return _db.Professional;
        }

        // GET: api/Professionals/5
        [ResponseType(typeof(Professional))]
        public async Task<IHttpActionResult> GetProfessional(int id)
        {
            Professional professional = _db.SelectRequest.SelectProfessional(id);
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

            _db.AddRequest.AddProfessional(newProfessional.User.FirstName, newProfessional.User.LastName, newProfessional.User.Birthdate, newProfessional.User.Adress, newProfessional.User.City, newProfessional.User.Postcode, newProfessional.User.PhoneNumber, newProfessional.User.Email, newProfessional.User.Photo, newProfessional.Role);

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


        // DELETE: api/Professionals/5
        [ResponseType(typeof(Professional))]
        public async Task<IHttpActionResult> DeleteProfessional(int id)
        {
            Professional professional = await _db.Professional.FindAsync(id);
            if (professional == null)
            {
                return NotFound();
            }
            _doc = new DocumentManager(_db);
            List<Follower> follow = _db.SelectRequest.SelectFollowForPro(id);
            foreach (var f in follow)
            {
                _doc.DeleteFile(professional, f.Patient);
            }
            _db.SuppressionRequest.ProfessionnalSuppression(professional);
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
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
        private DocumentManager _doc;
        public ProfessionalService Do = new ProfessionalService();


        // GET: api/Professionals
        public IQueryable<Professional> GetProfessionals()
        {
            return _db.Professional;
        }

        // GET: api/Professionals/5
        [ResponseType(typeof(Professional))]
        public async Task<IHttpActionResult> GetProfessional(int id)
        {
            Professional professional = Do.getProfessional(id);
            if (professional == null)
            {
                return NotFound();
            }

            return Ok(professional);
        }

        // PUT: api/Professionals/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutProfessional( ProfessionalCreation newProfessional)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                Do.putProfessional( newProfessional);
            }
            catch (DbUpdateConcurrencyException)
            {
                    throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        // DELETE: api/Professionals/5
        [ResponseType(typeof(Professional))]
        public async Task<IHttpActionResult> DeleteProfessional(int id)
        {
            Professional professional = Do.DeleteProfessionalCheck(id);
            if (professional == null)
            {
                return NotFound();
            }
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
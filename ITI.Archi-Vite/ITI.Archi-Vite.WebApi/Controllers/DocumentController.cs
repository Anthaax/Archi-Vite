using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using ITI.Archi_Vite.DataBase;
using ITI.Archi_Vite.Core;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class DocumentController : ApiController
    {
        private ArchiViteContext _db = new ArchiViteContext();
        DocumentManager _doc;

        // GET: api/Document
        public IQueryable<User> GetUser()
        {
            return _db.User;
        }

        // GET: api/Document/?patientId=5&proId=5
        [ResponseType(typeof(DocumentSerializable))]
            public async Task<IHttpActionResult> GetDocument(int patientId, int proId)
        {
            _doc = new DocumentManager(_db);
            DocumentSerializable doc = _doc.SeeDocument(proId, patientId);
            if (doc == null)
            {
                return NotFound();
            }

            return Ok(doc);
        }
        // GET: api/Document/?patientId=5&proId=5
        [ResponseType(typeof(DocumentSerializable))]
        public async Task<IHttpActionResult> GetDocument(int patientId)
        {
            _doc = new DocumentManager(_db);
            DocumentSerializable doc = _doc.SeeDocument(patientId);
            if (doc == null)
            {
                return NotFound();
            }

            return Ok(doc);
        }

        // PUT: api/Document/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDoc(MessageCreator newMessage)
        {
            _doc = new DocumentManager(_db);
            List<Professional> pro = new List<Professional>();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var proId in newMessage.ReceiverId)
            {
                pro.Add(_db.SelectRequest.SelectProfessional(proId));
            }
            _doc.CreateMessage(pro, _db.SelectRequest.SelectProfessional(newMessage.SenderId), newMessage.Title, newMessage.Content, _db.SelectRequest.SelectPatient(newMessage.PatientId));
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
        // PUT: api/Document/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDoc(PrescriptionCreator newPrescription)
        {
            _doc = new DocumentManager(_db);
            List<Professional> pro = new List<Professional>();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            foreach (var proId in newPrescription.Receivers)
            {
                pro.Add(_db.SelectRequest.SelectProfessional(proId));
            }
            _doc.CreatePrescription(pro, _db.SelectRequest.SelectProfessional(newPrescription.Sender), _db.SelectRequest.SelectPatient(newPrescription.Patient), newPrescription.Title, newPrescription.DocPath);
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

        // POST: api/Document
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PostDocument(ReciverModification newDoc)
        {
            _doc = new DocumentManager(_db);
            DocumentSerializable patientDoc = _doc.SeeDocument(newDoc.PatientId);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _doc.AddReciver(newDoc.RecieverId, newDoc.PatientId, newDoc.Date);

            await _db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE: api/Document/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> DeleteDoc(ReciverModification doc)
        {
            _doc = new DocumentManager(_db);
            DocumentSerializable patientDoc = _doc.SeeDocument(doc.PatientId);

            _doc.DeleteReciever(doc.RecieverId, doc.PatientId, doc.Date);

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

        private bool UserExists(int id)
        {
            return _db.User.Count(e => e.UserId == id) > 0;
        }
    }
}
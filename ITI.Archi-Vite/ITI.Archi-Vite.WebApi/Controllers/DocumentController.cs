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
        DocumentService Do = new DocumentService();

        // GET: api/Document
        public IQueryable<User> GetUser()
        {
            return _db.User;
        }

        // GET: api/Document/?patientId=5&proId=5
        [ResponseType(typeof(DocumentSerializable))]
            public async Task<IHttpActionResult> GetDocument(int patientId, int proId)
        {
            DocumentSerializable doc = Do.SeeDocument(proId, patientId);
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
            DocumentSerializable doc = Do.SeeDocument(patientId);
            if (doc == null)
            {
                return NotFound();
            }
            return Ok(doc);
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
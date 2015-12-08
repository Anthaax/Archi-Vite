using ITI.Archi_Vite.Core;
using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class MessageService
    {
        private ArchiViteContext _db = new ArchiViteContext();
        private DocumentManager _doc;

        public DocumentSerializable getFollower(int id)
        {
            Professional pro = _db.SelectRequest.SelectProfessional(id);
            Patient patient = _db.SelectRequest.SelectPatient(id);
            DocumentSerializable doc = _doc.SeeDocument(pro, patient);
            return doc;
        }

        public void putFollower(Follower follower)
        {
            _db.Entry(follower).State = EntityState.Modified;

        }

        public void postFollower(Follower follower)
        {
            _db.Follower.Add(follower);
        }

        public async System.Threading.Tasks.Task<Follower> deleteFollower(int id)
        {
            Follower follower = await _db.Follower.FindAsync(id);
            _db.Follower.Remove(follower);
            await _db.SaveChangesAsync();

            return follower;
        }
    }
}
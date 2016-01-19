using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class FollowerService
    {
        readonly ArchiViteContext _db;
        readonly DocumentManager _doc;

        public FollowerService()
        {
            _db = new ArchiViteContext();
            _doc = new DocumentManager(_db);
        }

        public Dictionary<Patient, Professional[]> getDocument(int id )
        {           
        var follower = _db.SelectRequest.SelectAllFollow(id);
        return follower;
        }

        public async void PutFollower(FollowerCreation follower)
        {
            _doc.CreateEmptyFile(follower.PatientId + "$" + follower.ProfessionalId);
            _db.AddRequest.AddFollow(follower.PatientId, follower.ProfessionalId);
        }

        public Follower DeleteFollowerCheck(int patientId, int proId)
        {
            Follower follower = _db.SelectRequest.SelectOneFollow(patientId, proId);
            if (follower != null) DeleteFollower(follower);
            return follower;

        }
        private void DeleteFollower(Follower follower)
        {
            _db.SuppressionRequest.FollowerSuppression(follower);
            _doc.DeleteFollowerFile(follower.ProfessionnalId, follower.PatientId);
        }
    }
}
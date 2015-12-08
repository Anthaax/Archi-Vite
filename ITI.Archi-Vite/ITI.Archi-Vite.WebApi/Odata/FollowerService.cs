using ITI.Archi_Vite.Core;
using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class FollowerService
    {
        private ArchiViteContext _db = new ArchiViteContext();
        DocumentManager _doc;

        public Dictionary<Patient, Professional[]> getDocument(int id)
        {           
        var follower = _db.SelectRequest.SelectAllFollow(id);
        return follower;
        }
        
        public void PutFollower(FollowerCreation follower)
        {
            _doc.CreateEmptyFile(follower.PatientId + "$" + follower.ProfessionalId);
            _db.AddRequest.AddFollow(follower.PatientId, follower.ProfessionalId);
        }

        public void DeleteFollower (int patientId, int proId)
        {
            Follower follower = _db.SelectRequest.SelectOneFollow(patientId, proId);
            _db.SuppressionRequest.FollowerSuppression(follower);
            _doc.DeleteFollowerFile(proId, patientId);
            _db.Follower.Remove(follower);
        }
    }
}
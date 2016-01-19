using ITI.Archi_Vite.Core;
using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class ProfessionalService
    {
        private ArchiViteContext _db = new ArchiViteContext();
        private DocumentManager _doc;

        public Professional getProfessional(int id)
        {
            Professional professional = _db.SelectRequest.SelectProfessional(id);
            return professional;
        }

        public void putProfessional(ProfessionalCreation newProfessional)
        {
            _db.AddRequest.AddProfessional(newProfessional.User, newProfessional.Role);
        }

        public Professional DeleteProfessionalCheck(int id)
        {
            Professional professional = _db.SelectRequest.SelectProfessional(id);
            if (professional != null) DeleteProfessional(professional, id);
            return professional;           
        }

        private void DeleteProfessional(Professional professional, int id)
        {
            _doc = new DocumentManager(_db);
            List<Follower> follow = _db.SelectRequest.SelectFollowForPro(id);
            foreach (var f in follow)
            {
                _doc.DeleteFollowerFile(professional.ProfessionalId, f.Patient.PatientId);
            }
            _db.SuppressionRequest.ProfessionnalSuppression(professional);
        }
    }
}
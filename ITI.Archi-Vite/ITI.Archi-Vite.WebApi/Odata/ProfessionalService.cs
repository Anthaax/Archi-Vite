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

        public void putProfessional(int id, ProfessionalCreation newProfessional)
        {
            _db.AddRequest.AddProfessional(newProfessional.User, newProfessional.Role);
        }

        public async System.Threading.Tasks.Task<Professional> DeleteProfessional(int id)
        {
            Professional professional = await _db.Professional.FindAsync(id);
            _doc = new DocumentManager(_db);
            List<Follower> follow = _db.SelectRequest.SelectFollowForPro(id);
            foreach (var f in follow)
            {
                _doc.DeleteFollowerFile(professional.ProfessionalId, f.Patient.PatientId);
            }
            _db.SuppressionRequest.ProfessionnalSuppression(professional);
            await _db.SaveChangesAsync();
            return professional;
        }
    }
}
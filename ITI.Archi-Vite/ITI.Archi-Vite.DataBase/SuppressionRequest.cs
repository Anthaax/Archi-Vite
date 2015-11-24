using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.DataBase
{
    public class SuppressionRequest
    {
        ArchiViteContext _context;
        public SuppressionRequest(ArchiViteContext context)
        {
            _context = context;
        }
        public void UserSuppression( User user )
        {
            _context.User.Remove(user);
            _context.SaveChanges();
        }
        public void PatientSuppression( Patient patient )
        {
            List<Follower> followPatient = _context.SelectRequest.SelectFollowForPatient(patient.PatientId);
            foreach(var follow in followPatient)
            {
                FollowerSuppression(follow);
            }
            _context.Patient.Remove(patient);
            _context.SaveChanges();
        }
        public void FollowerSuppression( Follower follower )
        {
            _context.Follower.Remove(follower);
            _context.SaveChanges();
        }
        public void ProfessionnalSuppression( Professional professional )
        {
            List<Follower> followPro = _context.SelectRequest.SelectFollowForPro(professional.ProfessionalId);
            foreach (var follow in followPro)
            {
                FollowerSuppression(follow);
            }
            _context.Professional.Remove(professional);
            _context.SaveChanges();
        }
    }
}

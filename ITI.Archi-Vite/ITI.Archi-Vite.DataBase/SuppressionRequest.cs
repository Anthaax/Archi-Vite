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
            _context.Professional.Remove(professional);
            _context.SaveChanges();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ITI.Archi_Vite.DataBase
{
    public class SelectRequest
    {
        ArchiViteContext _context;
        public SelectRequest(ArchiViteContext context)
        {
            _context = context;
        }
        public User SelectUser(int ID)
        {
            var selectQuery = _context.User
                                    .Where(t => t.UserId.Equals(ID))
                                    .FirstOrDefault();
            return selectQuery;
        }
        public User SelectUser(string pseudo, string password)
        {
                var selectQuery = _context.User
                                        .Where(t => t.Pseudo.Equals(pseudo) && t.Password.Equals(password))
                                        .FirstOrDefault();
                return selectQuery;
        }
        public Patient SelectPatient(int ID)
        {
            var selectQuery = _context.Patient
                                    .Include(c => c.User)
                                    .Where(t => t.PatientId.Equals(ID))
                                    .FirstOrDefault();
            return selectQuery;
        }
        public Patient SelectPatient(string Pseudo)
        {
            var selectQuery = _context.Patient
                                    .Include(c => c.User)
                                    .Where(t => t.PatientId.Equals(Pseudo))
                                    .FirstOrDefault();
            return selectQuery;
        }
        public Patient SelectPatient(string pseudo, string password)
        {
            var selectQuery = _context.Patient
                                    .Include(c => c.User)
                                    .Where(t => t.User.Pseudo.Equals(pseudo) && t.User.Password.Equals(password))
                                    .FirstOrDefault();
            return selectQuery;
        }
        public Professional SelectProfessional(int ID)
        {
            var selectQuery = _context.Professional
                                    .Include(c => c.User)
                                    .Where(t => t.ProfessionalId.Equals(ID))
                                    .FirstOrDefault();
            return selectQuery;
        }
        public Professional SelectProfessional(string pseudo)
        {
            var selectQuery = _context.Professional
                                    .Include(c => c.User)
                                    .Where(t => t.User.Pseudo.Equals(pseudo))
                                    .FirstOrDefault();
            return selectQuery;
        }
        public Professional SelectProfessional(string pseudo, string password)
        {
            var selectQuery = _context.Professional
                                    .Include(c => c.User)
                                    .Where(t => t.User.Pseudo.Equals(pseudo) && t.User.Password.Equals(password))
                                    .FirstOrDefault();
           return selectQuery;
        }

        public Follower SelectOneFollow(int IDPatient, int IDPro)
        {
            var senderFollow = _context.Follower
                                        .Include(c => c.Patient)
                                        .Include(c => c.Professionnal)
                                        .Include(c => c.Professionnal.User)
                                        .Include(c => c.Patient.User)
                                        .Where(t => t.Patient.PatientId.Equals(IDPatient) && t.ProfessionnalId.Equals(IDPro))
                                        .FirstOrDefault();
            return senderFollow;
        }
        public List<Follower> SelectFollowForPro(int IDPro)
        {
            var senderFollow = _context.Follower
                                        .Include(c => c.Patient)
                                        .Include(c => c.Professionnal)
                                        .Include(c => c.Professionnal.User)
                                        .Include(c => c.Patient.User)
                                        .Where(t => t.ProfessionnalId.Equals(IDPro))
                                        .ToList();
            return senderFollow;
        }
        public List<Follower> SelectFollowForPro(string pseudo)
        {
            var senderFollow = _context.Follower
                                        .Include(c => c.Patient)
                                        .Include(c => c.Professionnal)
                                        .Include(c => c.Professionnal.User)
                                        .Include(c => c.Patient.User)
                                        .Where(t => t.Professionnal.User.Pseudo.Equals(pseudo))
                                        .ToList();
            return senderFollow;
        }
        public List<Follower> SelectFollowForPro(string pseudo, string password)
        {
            var senderFollow = _context.Follower
                                        .Include(c => c.Patient)
                                        .Include(c => c.Professionnal)
                                        .Include(c => c.Professionnal.User)
                                        .Include(c => c.Patient.User)
                                        .Where(t => t.Professionnal.User.Pseudo.Equals(pseudo) && t.Professionnal.User.Password.Equals(password))
                                        .ToList();
            return senderFollow;
        }
        public List<Follower> SelectFollowForPatient(int IDPatient)
        {
            var senderFollow = _context.Follower
                                        .Include(c => c.Patient)
                                        .Include(c => c.Professionnal)
                                        .Include(c => c.Professionnal.User)
                                        .Include(c => c.Patient.User)
                                        .Where(t => t.PatientId.Equals(IDPatient))
                                        .ToList();
            return senderFollow;
        }
        public List<Follower> SelectFollowForPatient(string pseudo)
        {
            var senderFollow = _context.Follower
                                        .Include(c => c.Patient)
                                        .Include(c => c.Professionnal)
                                        .Include(c => c.Professionnal.User)
                                        .Include(c => c.Patient.User)
                                        .Where(t => t.Patient.User.Pseudo.Equals(pseudo))
                                        .ToList();
            return senderFollow;
        }
        public List<Follower> SelectFollowForPatient(string pseudo, string password)
        {
            var senderFollow = _context.Follower
                                        .Include(c => c.Patient)
                                        .Include(c => c.Professionnal)
                                        .Include(c => c.Professionnal.User)
                                        .Include(c => c.Patient.User)
                                        .Where(t => t.Patient.User.Pseudo.Equals(pseudo) && t.Professionnal.User.Password.Equals(password))
                                        .ToList();
            return senderFollow;
        }
    }
}

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
        public User SelectUser(string pseudo)
        {
                var selectQuery = _context.User
                                        .Where(t => t.Email.Equals(pseudo))
                                        .FirstOrDefault();
                return selectQuery;
        }
        public User SelectUser(string firstName, string lastName)
        {
            var selectQuery = _context.User
                                    .Where(t => t.FirstName.Equals(firstName) && t.LastName.Equals(lastName))
                                    .FirstOrDefault();
            return selectQuery;
        }
        public Patient SelectPatient(int ID)
        {
            var selectQuery = _context.Patient
                                    .Include(c => c.User)
                                    .Include(c => c.Referent)
                                    .Include(c => c.Referent.User)
                                    .Where(t => t.PatientId.Equals(ID))
                                    .FirstOrDefault();
            return selectQuery;
        }
        public Patient SelectPatient(Professional referent)
        {
            var selectQuery = _context.Patient
                                    .Include(c => c.User)
                                    .Include(c => c.Referent)
                                    .Include(c => c.Referent.User)
                                    .Where(t => t.Referent.ProfessionalId.Equals(referent.ProfessionalId))
                                    .FirstOrDefault();
            return selectQuery;
        }
        public Patient SelectPatient(string Pseudo)
        {
            var selectQuery = _context.Patient
                                    .Include(c => c.User)
                                    .Include(c => c.Referent)
                                    .Include(c => c.Referent.User)
                                    .Where(t => t.PatientId.Equals(Pseudo))
                                    .FirstOrDefault();
            return selectQuery;
        }
        public Patient SelectPatient(string firstName, string lastName)
        {
            var selectQuery = _context.Patient
                                    .Include(c => c.User)
                                    .Include(c => c.Referent)
                                    .Include(c => c.Referent.User)
                                    .Where(t => t.User.FirstName.Equals(firstName) && t.User.LastName.Equals(lastName))
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
                                    .Where(t => t.User.Email.Equals(pseudo))
                                    .FirstOrDefault();
            return selectQuery;
        }
        public Professional SelectProfessional(string firstName, string lastName)
        {
            var selectQuery = _context.Professional
                                    .Include(c => c.User)
                                    .Where(t => t.User.FirstName.Equals(firstName) && t.User.LastName.Equals(lastName))
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
                                        .Include(c => c.Patient.Referent)
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
                                        .Include(c => c.Patient.Referent)
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
                                        .Include(c => c.Patient.Referent)
                                        .Where(t => t.Professionnal.User.Email.Equals(pseudo))
                                        .ToList();
            return senderFollow;
        }
        public List<Follower> SelectFollowForPro(string firstName, string lastName)
        {
            var senderFollow = _context.Follower
                                        .Include(c => c.Patient)
                                        .Include(c => c.Professionnal)
                                        .Include(c => c.Professionnal.User)
                                        .Include(c => c.Patient.User)
                                        .Include(c => c.Patient.Referent)
                                        .Where(t => t.Professionnal.User.FirstName.Equals(firstName) && t.Professionnal.User.LastName.Equals(lastName))
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
                                        .Include(c => c.Patient.Referent)
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
                                        .Include(c => c.Patient.Referent)
                                        .Where(t => t.Patient.User.Email.Equals(pseudo))
                                        .ToList();
            return senderFollow;
        }
        public List<Follower> SelectFollowForPatient(string firstName, string lastName)
        {
            var senderFollow = _context.Follower
                                        .Include(c => c.Patient)
                                        .Include(c => c.Professionnal)
                                        .Include(c => c.Professionnal.User)
                                        .Include(c => c.Patient.User)
                                        .Include(c => c.Patient.Referent)
                                        .Where(t => t.Patient.User.FirstName.Equals(firstName) && t.Professionnal.User.LastName.Equals(lastName))
                                        .ToList();
            return senderFollow;
        }
    }
}

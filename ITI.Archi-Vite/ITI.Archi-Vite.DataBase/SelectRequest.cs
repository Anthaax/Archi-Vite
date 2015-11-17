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
        public User SelectUser(int ID)
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var selectQuery = context.User
                                        .Where(t => t.UserId.Equals(ID))
                                        .FirstOrDefault();
                return selectQuery;
            }
        }
        public User SelectUser(string Pseudo)
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var selectQuery = context.User
                                        .Where(t => t.Email.Equals(Pseudo))
                                        .FirstOrDefault();
                return selectQuery;
            }
        }
        public User SelectUser(string FirstName, string LastName)
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var selectQuery = context.User
                                        .Where(t => t.FirstName.Equals(FirstName) && t.LastName.Equals(LastName))
                                        .FirstOrDefault();
                return selectQuery;
            }
        }
        public Patient SelectPatient(int ID)
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var selectQuery = context.Patient
                                        .Include(c => c.User)
                                        .Include(c => c.Referent)
                                        .Include(c => c.Referent.User)
                                        .Where(t => t.PatientId.Equals(ID))
                                        .FirstOrDefault();
                return selectQuery;
            }
        }
        public Patient SelectPatient(string Pseudo)
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var selectQuery = context.Patient
                                        .Include(c => c.User)
                                        .Include(c => c.Referent)
                                        .Include(c => c.Referent.User)
                                        .Where(t => t.PatientId.Equals(Pseudo))
                                        .FirstOrDefault();
                return selectQuery;
            }
        }
        public Patient SelectPatient(string FirstName, string LastName)
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var selectQuery = context.Patient
                                        .Include(c => c.User)
                                        .Include(c => c.Referent)
                                        .Include(c => c.Referent.User)
                                        .Where(t => t.User.FirstName.Equals(FirstName) && t.User.LastName.Equals(LastName))
                                        .FirstOrDefault();
                return selectQuery;
            }
        }
        public Professional SelectProfessional(int ID)
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var selectQuery = context.Professional
                                        .Include(c => c.User)
                                        .Where(t => t.ProfessionalId.Equals(ID))
                                        .FirstOrDefault();
                return selectQuery;
            }
        }
        public Professional SelectProfessional(string Pseudo)
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var selectQuery = context.Professional
                                        .Include(c => c.User)
                                        .Where(t => t.User.Email.Equals(Pseudo))
                                        .FirstOrDefault();
                return selectQuery;
            }
        }
        public Professional SelectProfessional(string FirstName, string LastName)
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var selectQuery = context.Professional
                                        .Include(c => c.User)
                                        .Where(t => t.User.FirstName.Equals(FirstName) && t.User.LastName.Equals(LastName))
                                        .FirstOrDefault();
                return selectQuery;
            }
        }
        public Follower SelectOneFollow(int IDPatient, int IDPro)
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var senderFollow = context.Follower
                                            .Include(c => c.Patient)
                                            .Include(c => c.Professionnal)
                                            .Include(c => c.Professionnal.User)
                                            .Include(c => c.Patient.User)
                                            .Include(c => c.Patient.Referent)
                                            .Where(t => t.Patient.PatientId.Equals(IDPatient) && t.ProfessionnalId.Equals(IDPro))
                                            .FirstOrDefault();
                return senderFollow;
            }
        }
        public List<Follower> SelectFollowForPro(int IDPro)
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var senderFollow = context.Follower
                                            .Include(c => c.Patient)
                                            .Include(c => c.Professionnal)
                                            .Include(c => c.Professionnal.User)
                                            .Include(c => c.Patient.User)
                                            .Include(c => c.Patient.Referent)
                                            .Where(t => t.ProfessionnalId.Equals(IDPro))
                                            .ToList();
                return senderFollow;
            }
        }
        public List<Follower> SelectFollowForPro(string Pseudo)
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var senderFollow = context.Follower
                                            .Include(c => c.Patient)
                                            .Include(c => c.Professionnal)
                                            .Include(c => c.Professionnal.User)
                                            .Include(c => c.Patient.User)
                                            .Include(c => c.Patient.Referent)
                                            .Where(t => t.Professionnal.User.Email.Equals(Pseudo))
                                            .ToList();
                return senderFollow;
            }
        }
        public List<Follower> SelectFollowForPro(string FirstName, string LastName)
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var senderFollow = context.Follower
                                            .Include(c => c.Patient)
                                            .Include(c => c.Professionnal)
                                            .Include(c => c.Professionnal.User)
                                            .Include(c => c.Patient.User)
                                            .Include(c => c.Patient.Referent)
                                            .Where(t => t.Professionnal.User.FirstName.Equals(FirstName) && t.Professionnal.User.LastName.Equals(LastName))
                                            .ToList();
                return senderFollow;
            }
        }
    }
}

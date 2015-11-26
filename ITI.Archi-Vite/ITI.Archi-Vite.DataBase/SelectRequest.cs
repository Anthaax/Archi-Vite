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
        /// <summary>
        /// Select an User with an ID and return an User
        /// </summary>
        /// <param name="Id">Id of the user</param>
        /// <returns></returns>
        public User SelectUser(int Id)
        {
            var selectQuery = _context.User
                                    .Where(t => t.UserId.Equals(Id))
                                    .FirstOrDefault();
            return selectQuery;
        }
        /// <summary>
        /// Select an User with the pseudo and the password return an User
        /// </summary>
        /// <param name="pseudo">Pseudo of the user</param>
        /// <param name="password">Password of the user</param>
        /// <returns></returns>
        public User SelectUser(string pseudo, string password)
        {
                var selectQuery = _context.User
                                        .Where(t => t.Pseudo.Equals(pseudo) && t.Password.Equals(password))
                                        .FirstOrDefault();
                return selectQuery;
        }
        /// <summary>
        /// Select a patient with an Id and return a patient
        /// </summary>
        /// <param name="ID"> Id of the patient </param>
        /// <returns></returns>
        public Patient SelectPatient(int ID)
        {

            var selectQuery = _context.Patient
                                    .Include(c => c.User)
                                    .Where(t => t.PatientId.Equals(ID))
                                    .FirstOrDefault();
            return selectQuery;
        }
        /// <summary>
        /// Select a patient with a pseudo and a password and return a patient
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Patient SelectPatient(string pseudo, string password)
        {
            var selectQuery = _context.Patient
                                    .Include(c => c.User)
                                    .Where(t => t.User.Pseudo.Equals(pseudo) && t.User.Password.Equals(password))
                                    .FirstOrDefault();
            return selectQuery;
        }
        /// <summary>
        /// Select a professional with his Id and return a professional
        /// </summary>
        /// <param name="ID"></param>
        /// <returns></returns>
        public Professional SelectProfessional(int ID)
        {
            var selectQuery = _context.Professional
                                    .Include(c => c.User)
                                    .Where(t => t.ProfessionalId.Equals(ID))
                                    .FirstOrDefault();
            return selectQuery;
        }
        /// <summary>
        /// Select a professional with his a pseudo and a password and return a professional
        /// </summary>
        /// <param name="pseudo"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Professional SelectProfessional(string pseudo, string password)
        {
            var selectQuery = _context.Professional
                                    .Include(c => c.User)
                                    .Where(t => t.User.Pseudo.Equals(pseudo) && t.User.Password.Equals(password))
                                    .FirstOrDefault();
           return selectQuery;
        }
        /// <summary>
        /// Select a follower with an patientId and a proId and return a patient
        /// </summary>
        /// <param name="patientId"></param>
        /// <param name="proId"></param>
        /// <returns></returns>
        public Follower SelectOneFollow(int patientId, int proId)
        {
            var senderFollow = _context.Follower
                                        .Include(c => c.Patient)
                                        .Include(c => c.Professionnal)
                                        .Include(c => c.Professionnal.User)
                                        .Include(c => c.Patient.User)
                                        .Where(t => t.Patient.PatientId.Equals(patientId) && t.ProfessionnalId.Equals(proId))
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

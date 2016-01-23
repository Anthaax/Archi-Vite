using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace ITI.Archi_Vite.DataBase
{
    public class AddRequest
    {
        readonly ArchiViteContext _context;

        public AddRequest(ArchiViteContext context)
        {
            _context = context;        
        }
        public ArchiViteContext Context
        {
            get
            {
                return _context;
            }
        }
        /// <summary>
        /// Add a patient into the database 
        /// </summary>
        /// <param name="user"> user can't be null and all property of an user can be null exept userId who it must be null</param>
        /// <returns></returns>
        public Patient AddPatient(User user)
        {
            if (user == null) throw new ArgumentNullException("user", "user can't be null");
            if (user.Adress == null || user.Birthdate == null || user.City == null || user.FirstName == null || user.LastName == null || user.Password == null || user.PhoneNumber == 0 || user.Photo == null || user.Postcode == 0 || user.Pseudo == "null" || user.UserId != 0)
                throw new ArgumentException("All property of an user can't be null exept userId who it must be null");
            user.Password = CryptoMDP.GetMd5Hash(MD5.Create(), user.Password);
            Patient p = new Patient()
            {
                User = user
            };

            _context.User.Add(user);
            _context.Patient.Add(p);
            _context.SaveChanges();

            return p;
        }
        /// <summary>
        /// Add a patient into the database 
        /// </summary>
        /// <param name="user"> User can't be null and all property of an user can be null exept userId who it must be null </param>
        /// <param name="role"> Role can't be null and it's the role of the professional </param>
        /// <returns></returns>
        public Professional AddProfessional(User user, string role)
        {
            if (user == null || role == null) throw new ArgumentNullException("user or ", "user can't be null");
            if (user.Adress == null || user.Birthdate == null || user.City == null || user.FirstName == null || user.LastName == null || user.Password == null || user.PhoneNumber == 0 || user.Photo == null || user.Postcode == 0 || user.Pseudo == "null" || user.UserId != 0)
                throw new ArgumentException("All property of an user can't be null exept userId who it must be null");
            user.Password = CryptoMDP.GetMd5Hash(MD5.Create(), user.Password);
            Professional p = new Professional()
            {
                Role = role,
                User = user
            };

            _context.User.Add(user);
            _context.Professional.Add(p);
            _context.SaveChanges();

            return p;
        }
        /// <summary>
        /// Add a follow
        /// </summary>
        /// <param name="patientId"> Id of a patient </param>
        /// <param name="professionalId"> Id of a pro </param>
        public void AddFollow(int patientId, int professionalId)
        {
            if (patientId == 0 || professionalId == 0) throw new ArgumentNullException("Id can't be equal to 0");
            string filePath = patientId + "$" + professionalId;
            Follower f = new Follower()
            {
                Patient = _context.SelectRequest.SelectPatient(patientId),
                Professionnal = _context.SelectRequest.SelectProfessional(professionalId)
            };
            _context.Follower.Add(f);
            _context.SaveChanges();
        }
    }
}

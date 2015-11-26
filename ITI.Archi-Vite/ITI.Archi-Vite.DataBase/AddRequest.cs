using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Patient AddPatient(User user)
        {
            User u = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthdate = user.Birthdate,
                Adress = user.Adress,
                City = user.City,
                Postcode = user.Postcode,
                PhoneNumber = user.PhoneNumber,
                Pseudo = user.Pseudo,
                Password = user.Password,
                Photo = user.Photo
            };
            Patient p = new Patient()
            {
                User = u
            };

            _context.User.Add(u);
            _context.Patient.Add(p);
            _context.SaveChanges();

            return p;
        }
        public Professional AddProfessional(User user, string role)
        {
            User u = new User()
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthdate = user.Birthdate,
                Adress = user.Adress,
                City = user.City,
                Postcode = user.Postcode,
                PhoneNumber = user.PhoneNumber,
                Pseudo = user.Pseudo,
                Password = user.Password,
                Photo = user.Photo
            };
            Professional p = new Professional()
            {
                Role = role,
                User = u
            };

            _context.User.Add(u);
            _context.Professional.Add(p);
            _context.SaveChanges();

            return p;
        }
        public void AddFollow(int patientId, int professionalId)
        {
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

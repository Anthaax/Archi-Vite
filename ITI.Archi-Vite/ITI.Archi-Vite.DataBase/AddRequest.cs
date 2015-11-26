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
        readonly SelectRequest _sr;

        public AddRequest(ArchiViteContext context)
        {
            _context = context;
            _sr = new SelectRequest(context);            
        }
        public ArchiViteContext Context
        {
            get
            {
                return _context;
            }
        }

        public Patient AddPatient(string firstName, string lastName, DateTime birthDate, string adress, string city, int postCode, int phoneNumber, string pseudo, string password, string photo)
        {
            User u = new User()
            {
                FirstName = firstName,
                LastName = lastName,
                Birthdate = birthDate,
                Adress = adress,
                City = city,
                Postcode = postCode,
                PhoneNumber = phoneNumber,
                Pseudo = pseudo,
                Password = password,
                Photo = photo
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
        public void AddFollow(Patient patient, Professional professional)
        {
            string filePath = patient.PatientId + "$" + professional.ProfessionalId;
            Follower f = new Follower()
            {
                Patient = patient,
                Professionnal = _sr.SelectProfessional(professional.ProfessionalId)
            };
            _context.Follower.Add(f);
            _context.SaveChanges();
        }
    }
}

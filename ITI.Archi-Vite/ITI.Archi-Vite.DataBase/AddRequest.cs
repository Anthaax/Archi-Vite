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

        public Patient AddPatient(string firstName, string lastName, DateTime birthDate, string adress, string city, int postCode, int phoneNumber, string email, string photo, Professional referent, string pathFile )
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
                Email = email,
                Photo = photo
            };
            Patient p = new Patient()
            {
                PathFiles = pathFile,
                Referent = referent,
                User = u
            };

            _context.User.Add(u);
            _context.Patient.Add(p);
            _context.SaveChanges();
            AddFollow(p, referent);
            
            return p;
        }
        public Professional AddProfessional(string firstName, string lastName, DateTime birthDate, string adress, string city, int postCode, int phoneNumber, string email, string photo, string role)
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
                Email = email,
                Photo = photo
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
                FilePath = filePath,
                Professionnal = professional
            };
            _context.Follower.Add(f);
            _context.SaveChanges();
        }
    }
}

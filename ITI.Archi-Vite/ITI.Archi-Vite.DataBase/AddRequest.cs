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
        static ArchiViteContext _context;
        public AddRequest(ArchiViteContext context)
        {
            _context = context;
        }

        public static ArchiViteContext Context
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
            Context.User.Add(u);
            Context.Patient.Add(p);
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
            Context.User.Add(u);
            Context.Professional.Add(p);

            return p;
        }
        public void AddFollow(Patient Patient, Professional Professional)
        {
            string filePath = Patient.PatientId + "$" + Professional.ProfessionalId;
            Follower f = new Follower()
            {
                Patient = Patient,
                FilePath = filePath,
                Professionnal = Professional
            };
            Context.Follower.Add(f);
        }
    }
}

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
        public Patient AddPatient(string FirstName, string LastName, DateTime BirthDate, string Adress, string City, int PostCode, int PhoneNumber, string Email, string Photo, Professional Referent, string PathFile )
        {
            User u = new User()
            {
                FirstName = FirstName,
                LastName = LastName,
                Birthdate = BirthDate,
                Adress = Adress,
                City = City,
                Postcode = PostCode,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Photo = Photo
            };
            Patient p = new Patient()
            {
                PathFiles = PathFile,
                Referent = Referent,
                User = u
            };
            using (ArchiViteContext context = new ArchiViteContext())
            {
                context.User.Add(u);
                context.Patient.Add(p);
                context.SaveChanges();
            }
            return p;
        }
        public Professional AddProfessional(string FirstName, string LastName, DateTime BirthDate, string Adress, string City, int PostCode, int PhoneNumber, string Email, string Photo, string Role)
        {
            User u = new User()
            {
                FirstName = FirstName,
                LastName = LastName,
                Birthdate = BirthDate,
                Adress = Adress,
                City = City,
                Postcode = PostCode,
                PhoneNumber = PhoneNumber,
                Email = Email,
                Photo = Photo
            };
            Professional p = new Professional()
            {
                Role = Role,
                User = u
            };
            using (ArchiViteContext context = new ArchiViteContext())
            {
                context.User.Add(u);
                context.Professional.Add(p);
                context.SaveChanges();
            }
            return p;
        }
        public void AddFollow(Patient Patient, Professional Professional)
        {
            string filePath = Patient.PatientId + "/$" + Professional.ProfessionalId;
            Follower f = new Follower()
            {
                Patient = Patient,
                FilePath = filePath,
                Professionnal = Professional
            };
            using (ArchiViteContext context = new ArchiViteContext())
            {
                context.Follower.Add(f);
                context.SaveChanges();
            }

        }
    }
}

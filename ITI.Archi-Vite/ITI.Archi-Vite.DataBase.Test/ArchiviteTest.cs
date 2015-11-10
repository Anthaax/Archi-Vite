using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.Archi_Vite.Core;

namespace ITI.Archi_Vite.DataBase.Test
{
    [TestFixture]
    public class ArchiviteTest
    {
        [Test]
        public void CreateDatabase()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                context.Database.Create();
            }
        }
        [Test]
        public void InsertInDatabase()
        {
            User u = new User()
            {
                FirstName = "Guillaume",
                LastName = "Fimes",
                Adress = "72 avenue maurice thorez",
                Birthdate = DateTime.Now,
                City = "Ivry-sur-Seine",
                Email = "fimes@intechinfo.fr",
                PhoneNumber = 0662147351,
                Photo = "yolo",
                Postcode = 75015
            };
            User u1 = new User()
            {
                FirstName = "Clément",
                LastName = "Rousseau",
                Adress = "72 avenue maurice thorez",
                Birthdate = DateTime.Now,
                City = "Ivry-sur-Seine",
                Email = "crousseau@intechinfo.fr",
                PhoneNumber = 0606060606,
                Photo = "yolo",
                Postcode = 12452
            };
            User u2 = new User()
            {
                FirstName = "Simon",
                LastName = "Favraud",
                Adress = "72 avenue maurice thorez",
                Birthdate = DateTime.Now,
                City = "Ivry-sur-Seine",
                PhoneNumber = 0606066606,
                Photo = "yolo",
                Postcode = 12452
            };
            User u3 = new User()
            {
                FirstName = "Antoine",
                LastName = "Raquillet",
                Adress = "72 avenue maurice thorez",
                Birthdate = DateTime.Now,
                City = "Ivry-sur-Seine",
                Email = "sfavraud@intechinfo.fr",
                PhoneNumber = 0606066606,
                Photo = "yolo",
                Postcode = 12452
            };
            Professional pro1 = new Professional()
            {
                Role = "Infirmier",
                User = u2
            };
            Professional pro2 = new Professional()
            {
                Role = "Medecin",
                User = u1
            };
            Patient p = new Patient()
            {
                PathFiles = "abc",
                Referent = pro1,
                User =  u,

            };
            Follower f = new Follower()
            {
                Patient = p,
                Professionnal = pro1
            };
            using (ArchiViteContext context = new ArchiViteContext())
            {
                context.User.Add(u);
                context.User.Add(u1);
                context.User.Add(u2);
                context.User.Add(u3);
                context.Patient.Add(p);
                context.Follower.Add(f);
                context.SaveChanges();
            }
        }
        [Test]
        public void DeleteData()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                context.Database.Delete();
            }
        }
        [Test]
        public void SelectRequest()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var selectQuery = context.Patient.ToList();
                foreach(var user in selectQuery)
                {
                    Console.WriteLine("FirstName : {0} LastName : {1}", user.User.FirstName, user.User.LastName);
                }
                var selectQuery1 = context.Professional.Where(s => s.Role.Equals("Infirmier")).ToList();
                foreach (var user in selectQuery1)
                {
                    Console.WriteLine("FirstName : {0} LastName : {1}", user.User.FirstName, user.User.LastName, user.Role);
                }
            }
        }
        [Test]
        public void UpdateRequest()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var selectQuery = context.User.Where(s => s.FirstName.Equals("Guillaume")).FirstOrDefault();
                if(selectQuery != null)
                {
                    selectQuery.LastName = "Fist";
                }
                context.Entry(selectQuery).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        [Test]
        public void UpdateWithMethod()
        {
            User u = new User()
            {
                UserId = 1,
                FirstName = "Guillaume",
                LastName = "Fimes",
                Adress = "72 avenue maurice thorez",
                Birthdate = DateTime.Now,
                City = "Ivry-sur-Seine",
                Email = "fimes@intechinfo.fr",
                PhoneNumber = 0662147351,
                Photo = "yolo",
                Postcode = 75015
            };
            UserInfoUpdate info = new UserInfoUpdate(u);
            info.CheckInfo("Guillaume", "Fist", "13 rue des potiers", DateTime.Now, u.City, u.Email, u.Postcode, u.PhoneNumber);
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var user = context.User.Where(s => s.FirstName.Equals("Guillaume")).FirstOrDefault();
                Console.WriteLine("FirstName : {0} LastName : {1}  Adress : {2} BirthDate : {3}", user.FirstName, user.LastName, user.Adress, user.Birthdate);
            }
        }

        [Test]
        public void CreateFileForNewUser()
        {
            PatientManagement Account = new PatientManagement();
            Person person = new Person("Guillaume", "Fimes", DateTime.Now, "11 rue yolo", "Paris", 75015, 0603020104, "yolo@yolo", "Medecin", "coucou");
            Core.Patient patient = new Core.Patient("Clement", "Rousseau", DateTime.Now, "11 rue yolo", "Paris", 75015, 0603020104, "yolo@yolo", "coucou", person);
            Account.CreatePatient(patient);
        }
    }
}

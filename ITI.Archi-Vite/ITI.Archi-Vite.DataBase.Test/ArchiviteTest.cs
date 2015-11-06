using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.DataBase.Test
{
    [TestFixture]
    public class ArchiviteTest
    {
        [Test]
        public void CreateDatabase()
        {
            using (ArchiViteContexts context = new ArchiViteContexts())
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
                Role = "Patient",
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
                Role = "Medecin",
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
                Email = "sfavraud@intechinfo.fr",
                Role = "Archi'Mède",
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
                Role = "Archi'Mède",
                PhoneNumber = 0606066606,
                Photo = "yolo",
                Postcode = 12452
            };
            PatientFile p = new PatientFile()
            {
                PathFiles = "abc",
                Referent = 2,
                User =  u,

            };
            Follower f = new Follower()
            {
                User = u2,
                PatientFile = p
            };
            using (ArchiViteContexts context = new ArchiViteContexts())
            {
                context.User.Add(u);
                context.User.Add(u1);
                context.User.Add(u2);
                context.User.Add(u3);
                context.PatientFile.Add(p);
                context.Follower.Add(f);
                context.SaveChanges();
            }
        }
        [Test]
        public void DeleteData()
        {
            using (ArchiViteContexts context = new ArchiViteContexts())
            {
                context.Database.Delete();
            }
        }
        [Test]
        public void SelectRequest()
        {
            using (ArchiViteContexts context = new ArchiViteContexts())
            {
                List<User> selectQuery = context.User.Where(s => s.Role.Equals("Patient")).ToList<User>();
                foreach(var user in selectQuery)
                {
                    Console.WriteLine(user.FirstName);
                }
            }
        }
    }
}

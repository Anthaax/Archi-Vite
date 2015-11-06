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
                Adress = "11 rue saint charles",
                Birthdate = DateTime.Now,
                City = "Paris",
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
            PatientFile p = new PatientFile()
            {
                PathFiles = "abc",
                Referent = 2,
                User =  u,

            };
            using (ArchiViteContexts context = new ArchiViteContexts())
            {
                context.User.Add(u);
                context.User.Add(u1);
                context.PatientFile.Add(p);
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
    }
}

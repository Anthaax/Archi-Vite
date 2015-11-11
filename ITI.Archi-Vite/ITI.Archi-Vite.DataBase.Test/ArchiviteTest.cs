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
            DeleteData();
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
                Email = "sfavraud@intechinfo.fr",
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
            Professional pro3 = new Professional()
            {
                Role = "Medecin",
                User = u3
            };
            Patient p = new Patient()
            {
                PathFiles = "abc",
                Referent = pro2,
                User =  u

            };
            Follower f = new Follower()
            {
                Patient = p,
                FilePath = "asd",
                Professionnal = pro1
            };
            using (ArchiViteContext context = new ArchiViteContext())
            {
                context.User.Add(u);
                context.User.Add(u1);
                context.User.Add(u2);
                context.User.Add(u3);
                context.Professional.Add(pro1);
                context.Professional.Add(pro2);
                context.Professional.Add(pro3);
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
                var selectQuery1 = context.Follower.ToList();
                foreach( var follow in selectQuery1)
                {
                    context.Follower.Remove(follow);
                    context.SaveChanges();
                }
                var selectQuery2 = context.Patient.ToList();
                foreach (var patient in selectQuery2)
                {
                    context.Patient.Remove(patient);
                    context.SaveChanges();
                }
                var selectQuery3 = context.Professional.ToList();
                foreach (var pro in selectQuery3)
                {
                    context.Professional.Remove(pro);
                    context.SaveChanges();
                }
                var selectQuery4 = context.User.ToList();
                foreach (var user in selectQuery4)
                {
                    context.User.Remove(user);
                    context.SaveChanges();
                }
            }
        }
        [Test]
        public void UpdateUser()
        {
            
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var user = context.User.Where(s => s.FirstName.Equals("Guillaume")).FirstOrDefault();
                UpdateRequest update = new UpdateRequest();
                update.CheckUserInfo("Guillaume", "Fist", "13 rue des potiers", DateTime.Now, user.City, user.Email, user.Postcode, user.PhoneNumber, user);
                var u = context.User.Where(s => s.FirstName.Equals("Guillaume")).FirstOrDefault();
                Console.WriteLine("FirstName : {0} LastName : {1}  Adress : {2} BirthDate : {3}", u.FirstName, u.LastName, u.Adress, u.Birthdate);
            }
        }
        [Test]
        public void UpdatePatient()
        {
            UpdateRequest update = new UpdateRequest();
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var Pro = context.Professional.Where(t => t.ProfessionalId.Equals(2003)).FirstOrDefault();
                var Patient = context.Patient.Where(t => t.PatientId.Equals(2005)).FirstOrDefault();
                if(Pro != null && Patient != null ) update.CheckPatientInfo(Pro, Patient);


                var patient = context.Patient.Where(t => t.PatientId.Equals(2005)).FirstOrDefault();
                var user = context.User.Where(t => t.UserId.Equals(patient.PatientId)).FirstOrDefault();
                var selectpro = context.Professional.Where(t => t.ProfessionalId.Equals(patient.Referent.ProfessionalId)).FirstOrDefault();
                var pro = context.User.Where(t => t.UserId.Equals(selectpro.ProfessionalId)).FirstOrDefault();
                Console.WriteLine("FirstName : {0} LastName : {1} FisrtName Referent : {2}  LastName Referent : {3}", user.FirstName, user.LastName, pro.FirstName, pro.LastName);
            }
        }
        [Test]
        public void UpdatePro()
        {

            using (ArchiViteContext context = new ArchiViteContext())
            {
                var user = context.User.Where(s => s.FirstName.Equals("Guillaume")).FirstOrDefault();
                UpdateRequest update = new UpdateRequest();
                update.CheckUserInfo("Guillaume", "Fist", "13 rue des potiers", DateTime.Now, user.City, user.Email, user.Postcode, user.PhoneNumber, user);
                var u = context.User.Where(s => s.FirstName.Equals("Guillaume")).FirstOrDefault();
                Console.WriteLine("FirstName : {0} LastName : {1}  Adress : {2} BirthDate : {3}", u.FirstName, u.LastName, u.Adress, u.Birthdate);
            }
        }

        [Test]
        public void SelectRequest()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var selectQuery = context.Patient.ToList();
                foreach (var patient in selectQuery)
                {
                    var user = context.User.Where(t => t.UserId.Equals(patient.PatientId)).FirstOrDefault();
                    Console.WriteLine("FirstName : {0} LastName : {1}", user.FirstName, user.LastName);
                }
                var selectQuery1 = context.Professional.ToList();
                foreach (var professional in selectQuery1)
                {
                    var user = context.User.Where(t => t.UserId.Equals(professional.ProfessionalId)).FirstOrDefault();
                    Console.WriteLine("FirstName : {0} LastName : {1}", user.FirstName, user.LastName);
                }
            }
        }
        
        //[Test]
        //public void UpdateRequest()
        //{
        //    using (ArchiViteContext context = new ArchiViteContext())
        //    {
        //        var selectQuery = context.User.Where(s => s.FirstName.Equals("Guillaume")).FirstOrDefault();
        //        if(selectQuery != null)
        //        {
        //            selectQuery.LastName = "Fist";
        //        }
        //        context.Entry(selectQuery).State = System.Data.Entity.EntityState.Modified;
        //        context.SaveChanges();
        //    }
        //}
        

        //[Test]
        //public void CreateFileForNewUser()
        //{
        //    PatientManagement Account = new PatientManagement();
        //    Person person = new Person("Guillaume", "Fimes", DateTime.Now, "11 rue yolo", "Paris", 75015, 0603020104, "yolo@yolo", "Medecin", "coucou");
        //    Core.Patient patient = new Core.Patient("Clement", "Rousseau", DateTime.Now, "11 rue yolo", "Paris", 75015, 0603020104, "yolo@yolo", "coucou", person);
        //    Account.CreatePatient(patient);
        //}
    }
}

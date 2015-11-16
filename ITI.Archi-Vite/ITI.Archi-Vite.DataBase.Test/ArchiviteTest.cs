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
                User = u

            };
            DocumentManager dm = new DocumentManager();
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
                context.SaveChanges();
                var Patient = context.Patient.Include("User").Include("Referent").Where(t => t.User.FirstName.Equals("Guillaume")).FirstOrDefault();
                var pro = context.Professional.Include("User").Where(s => s.User.FirstName.Equals("Simon")).FirstOrDefault();
                Follower f = new Follower
                {
                    Patient = Patient,
                    Professionnal = pro,
                    FilePath = Patient.PatientId + "$" + pro.ProfessionalId
                };
                context.Follower.Add(f);
                dm.CreateEmptyFile(Patient.PatientId + "$" + pro.ProfessionalId);
                context.SaveChanges();
            }
        }
        [Test]
        public void DeleteData()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var selectQuery1 = context.Follower.ToList();
                foreach (var follow in selectQuery1)
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
                var Patient = context.Patient.Include("User").Include("Referent").Where(t => t.User.FirstName.Equals("Guillaume")).FirstOrDefault();
                var Pro = context.Professional.Include("User").Where(t => t.User.FirstName.Equals("Antoine")).FirstOrDefault();
                if (Pro != null && Patient != null) update.CheckPatientInfo(Pro, Patient);
                var NewPatient = context.Patient.Include("User").Include("Referent").Where(t => t.User.FirstName.Equals("Guillaume")).FirstOrDefault();
                Console.WriteLine("FirstName : {0} LastName : {1} FisrtName Referent : {2}  LastName Referent : {3}", NewPatient.User.FirstName, NewPatient.User.LastName, NewPatient.Referent.User.FirstName, NewPatient.Referent.User.LastName);
            }
        }
        [Test]
        public void UpdatePro()
        {
            UpdateRequest update = new UpdateRequest();
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var pro = context.Professional.Include("User").Where(s => s.User.FirstName.Equals("Simon")).FirstOrDefault();
                update.UpdateProInfo("Infirmier", pro);
                var newPro = context.Professional.Include("User").Where(s => s.User.FirstName.Equals("Simon")).FirstOrDefault();
                Console.WriteLine("FirstName : {0} LastName : {1} Role : {2}", newPro.User.FirstName, newPro.User.LastName, newPro.Role );
            }
        }

        [Test]
        public void SelectRequest()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var selectQuery = context.Patient.Include("User").Include("Referent").ToList();
                foreach (var patient in selectQuery)
                {
                    Console.WriteLine("FirstName : {0} LastName : {1}", patient.User.FirstName, patient.User.LastName);
                }
                var selectQuery1 = context.Professional.Include("User").ToList();
                foreach (var professional in selectQuery1)
                {
                    Console.WriteLine("FirstName : {0} LastName : {1}", professional.User.FirstName, professional.User.LastName);
                }
            }
        }
        [Test]
        public void CreatePatientAndPro()
        {
            AddRequest a = new AddRequest();
            Professional pro = a.AddProfessional("Yolo1", "Raquillet", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "sfavraud@intechinfo.fr", "yolo", "Medecin");
            Patient patient = a.AddPatient("Yolo", "Fimes", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "sfavraud@intechinfo.fr", "yolo", pro, "yolo");
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var selectQuery = context.Patient.Include("User").Include("Referent").Where(t => t.PatientId.Equals(patient.PatientId)).FirstOrDefault();
                Console.WriteLine("FirstName : {0} LastName : {1}", selectQuery.User.FirstName, selectQuery.User.LastName);
                var selectQueryPro = context.Professional.Include("User").Where(t => t.ProfessionalId.Equals(pro.ProfessionalId)).FirstOrDefault();
                Console.WriteLine("FirstName : {0} LastName : {1}", selectQueryPro.User.FirstName, selectQueryPro.User.LastName);
            }
        }
        [Test]
        public void CreateFollow()
        {
            AddRequest a = new AddRequest();
            DocumentManager dm = new DocumentManager();
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var patient = context.Patient.Include("User").Include("Referent").Where(t => t.User.FirstName.Equals("Guillaume")).FirstOrDefault();
                var professional = context.Professional.Include("User").Where(t => t.User.FirstName.Equals("Antoine")).FirstOrDefault();
                //a.AddFollow(patient, professional);
                var path = context.Follower.Include("Patient").Include("Professionnal").Where(t => t.FilePath.Equals(patient.PatientId + "/$" + professional.ProfessionalId)).FirstOrDefault();
                dm.CreateEmptyFile(patient.PatientId + "$" + professional.ProfessionalId);
                //var follows = context.Follower.Include("Patient").Include("Professional").Include("User").Include("Referent").ToList();
                //foreach (var f in follows)
                //{
                //    Console.WriteLine("FirstName : {0} LastName : {1} FirstNamePro : {2} LastNamePro : {3}", f.Patient.User.FirstName, f.Patient.User.LastName, f.Professionnal.User.FirstName, f.Professionnal.User.LastName);
                //}
            }
        }
        [Test]
        public void CreateMessage()
        {
            DocumentManager dm = new DocumentManager();
            using (ArchiViteContext context = new ArchiViteContext())
            {
                List<Professional> p = new List<Professional>();
                var Patient = context.Patient.Include("User").Include("Referent").Where(t => t.User.FirstName.Equals("Guillaume")).FirstOrDefault();
                var pro = context.Professional.Include("User").Where(s => s.User.FirstName.Equals("Simon")).FirstOrDefault();
                p.Add(pro);
                dm.CreateMessage(p, pro, "Coucou", "Y a un pb", Patient);
                
            }
        }
    }
}


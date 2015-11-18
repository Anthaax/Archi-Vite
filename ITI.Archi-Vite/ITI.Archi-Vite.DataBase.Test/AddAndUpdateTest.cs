using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.Archi_Vite.Core;
using System.Data.Entity;
using ITI.Archi_Vite.DataBase;

namespace ITI.Archi_Vite.DataBase.Test
{
    [TestFixture]
    public class AddAndUpdateTest
    {
        [Test]
        //[SetUp]
        public void CreatePatientAndPro()
        {
            AddRequest a = InitializeAddRequest();
            Professional pro = a.AddProfessional("Antoine", "Raquillet", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "sfavraud@intechinfo.fr", "yolo", "Medecin");
            Professional pro1 = a.AddProfessional("Simon", "Favraud", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "sfavraud@intechinfo.fr", "yolo", "Infirmier");
            Professional pro2 = a.AddProfessional("Clement", "Rousseau", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "sfavraud@intechinfo.fr", "yolo", "Medecin");
            Professional pro3 = a.AddProfessional("Olivier", "Spinelli", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "sfavraud@intechinfo.fr", "yolo", "Medecin");
            Patient patient = a.AddPatient("Guillaume", "Fimes", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "sfavraud@intechinfo.fr", "yolo", pro, "yolo");
            Patient patient1 = a.AddPatient("Maxime", "Coucou", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "sfavraud@intechinfo.fr", "yolo", pro2, "yolo");
            AddRequest.Context.SaveChanges();

                
            SelectRequest s = new SelectRequest();

            Professional antoine = s.SelectProfessional("Antoine", "Raquillet");
            Professional simon = s.SelectProfessional("Simon", "Favraud");
            Professional clement = s.SelectProfessional("Clement", "Rousseau");
            Professional olivier = s.SelectProfessional("Olivier", "Spinelli");

            Patient guillaume = s.SelectPatient("Guillaume", "Fimes");
            Patient maxime = s.SelectPatient("Maxime", "Coucou");

            Patient p = s.SelectPatient(guillaume.PatientId);
            Console.WriteLine("Nom : {0}   Prénom : {1}   Nom du Referent : {2}   Prenom du Referent : {3}", p.User.LastName, p.User.FirstName, p.Referent.User.LastName, p.Referent.User.FirstName);
            Assert.AreEqual(p.PatientId, guillaume.PatientId);
            Patient p1 = s.SelectPatient(maxime.PatientId);
            Console.WriteLine("Nom : {0}   Prénom : {1}   Nom du Referent : {2}   Prenom du Referent : {3}", p1.User.LastName, p1.User.FirstName, p1.Referent.User.LastName, p1.Referent.User.FirstName);
            Assert.AreEqual(p1.PatientId, maxime.PatientId);

            Professional Pro = s.SelectProfessional(antoine.ProfessionalId);
            Console.WriteLine("Nom : {0}   Prénom : {1}", Pro.User.LastName, Pro.User.FirstName);
            Assert.AreEqual(Pro.ProfessionalId, antoine.ProfessionalId);
            Professional Pro1 = s.SelectProfessional(simon.ProfessionalId);
            Console.WriteLine("Nom : {0}   Prénom : {1}", Pro1.User.LastName, Pro1.User.FirstName);
            Assert.AreEqual(Pro1.ProfessionalId, simon.ProfessionalId);
            Professional Pro2 = s.SelectProfessional(clement.ProfessionalId);
            Console.WriteLine("Nom : {0}   Prénom : {1}", Pro2.User.LastName, Pro2.User.FirstName);
            Assert.AreEqual(Pro2.ProfessionalId, clement.ProfessionalId);
            Professional Pro3 = s.SelectProfessional(olivier.ProfessionalId);
            Console.WriteLine("Nom : {0}   Prénom : {1}", Pro3.User.LastName, Pro3.User.FirstName);
            Assert.AreEqual(Pro3.ProfessionalId, olivier.ProfessionalId);

            Follower f = s.SelectOneFollow(guillaume.PatientId, guillaume.Referent.ProfessionalId);
            Console.WriteLine("PathFile : {0}", f.FilePath);
            Assert.AreEqual(f.FilePath, f.PatientId + "$" + f.ProfessionnalId);
            Follower f1 = s.SelectOneFollow(maxime.PatientId, maxime.Referent.ProfessionalId);
            Console.WriteLine("PathFile : {0}", f1.FilePath);
            Assert.AreEqual(f1.FilePath, f1.PatientId + "$" + f1.ProfessionnalId);

        }
        [Test]
        public void CreateFollow()
        {
            var context = AddRequest.Context;
            Assert.IsNotNull(context);
            DocumentManager dm = new DocumentManager();
            SelectRequest s = new SelectRequest();

            Patient patient = s.SelectPatient("Guillaume", "Fimes");
            Professional pro = s.SelectProfessional("Clement", "Rousseau");
            //a.AddFollow(patient, pro);

            Follower follow = s.SelectOneFollow(patient.PatientId, pro.ProfessionalId);
            Assert.AreEqual(follow.PatientId, patient.PatientId);
            Assert.AreEqual(follow.ProfessionnalId, pro.ProfessionalId);
            dm.CreateEmptyFile(follow.FilePath);
        }
        [Test]
        public void UpdateUser()
        {
            UpdateRequest ur = new UpdateRequest();
            SelectRequest s = new SelectRequest();

            User user = s.SelectUser("Guillaume", "Fimes");
            ur.CheckUserInfo(user.FirstName, user.LastName, user.Adress, user.Birthdate, "Paris", user.Email, user.Postcode, user.PhoneNumber, user.Photo, user);

            User NewUser = s.SelectUser(user.UserId);
            Assert.AreEqual(NewUser.City, "Paris");
            Assert.AreEqual(NewUser.LastName, "Fimes");
        }
        [Test]
        public void UpdatePatient()
        {
            UpdateRequest ur = new UpdateRequest();
            SelectRequest s = new SelectRequest();
            Patient patient = s.SelectPatient("Guillaume", "Fimes");
            Professional pro = s.SelectProfessional("Olivier", "Spinelli");

            ur.CheckPatientInfo(pro, patient);

            Patient newPatient = s.SelectPatient(patient.PatientId);
            Assert.AreEqual(newPatient.Referent.ProfessionalId, pro.ProfessionalId);

        }
        [Test]
        public void UpdatePro()
        {
            UpdateRequest ur = new UpdateRequest();
            SelectRequest s = new SelectRequest();
            Professional pro = s.SelectProfessional("Simon", "Favraud");
            
            ur.UpdateProInfo("Archi'Mède", pro);
            Professional newPro = s.SelectProfessional(pro.ProfessionalId);
            Assert.AreEqual(newPro.Role, "Archi'Mède");
        }

        [Test]//[TearDown]
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
                Assert.AreEqual(context.Follower.ToList().Count(), 0);
                Assert.AreEqual(context.Patient.ToList().Count(), 0);
                Assert.AreEqual(context.Professional.ToList().Count(), 0);
                Assert.AreEqual(context.User.ToList().Count(), 0);
            }
        }
        public AddRequest InitializeAddRequest()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                AddRequest a = new AddRequest(context);
                return a;
            }
        }
    }
}

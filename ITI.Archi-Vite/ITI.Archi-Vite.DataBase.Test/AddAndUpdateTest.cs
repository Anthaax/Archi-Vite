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
        ContextManager _archiVite;
        public AddAndUpdateTest()
        {
            _archiVite = new ContextManager();
        }
        [Test]
        public void CreatePatientAndPro()
        {
            using (ArchiViteContext context = _archiVite.Context)
            {
                AddRequest a = new AddRequest(context);
                DocumentManager dm = new DocumentManager(context);
                SelectRequest s = new SelectRequest(context);
                Professional pro = a.AddProfessional("Antoine", "Raquillet", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "sfavraud@intechinfo.fr", "yolo", "Medecin");
                Professional pro1 = a.AddProfessional("Simon", "Favraud", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "sfavraud@intechinfo.fr", "yolo", "Infirmier");
                Professional pro2 = a.AddProfessional("Clement", "Rousseau", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "sfavraud@intechinfo.fr", "yolo", "Medecin");
                Professional pro3 = a.AddProfessional("Olivier", "Spinelli", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "sfavraud@intechinfo.fr", "yolo", "Medecin");
                Patient patient = a.AddPatient("Guillaume", "Fimes", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "sfavraud@intechinfo.fr", "yolo", pro, "yolo");
                dm.CreateEmptyFile(patient.PatientId.ToString());
                dm.CreateEmptyFile(s.SelectOneFollow(patient.PatientId, patient.Referent.ProfessionalId).FilePath);
                Patient patient1 = a.AddPatient("Maxime", "Coucou", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "sfavraud@intechinfo.fr", "yolo", pro2, "yolo");
                dm.CreateEmptyFile(s.SelectOneFollow(patient1.PatientId, patient1.Referent.ProfessionalId).FilePath);
                dm.CreateEmptyFile(patient1.PatientId.ToString());


                a.AddFollow(patient, pro1);
                dm.CreateEmptyFile(s.SelectOneFollow(patient.PatientId, pro1.ProfessionalId).FilePath);

                a.AddFollow(patient, pro2);
                dm.CreateEmptyFile(s.SelectOneFollow(patient.PatientId, pro2.ProfessionalId).FilePath);

                a.AddFollow(patient, pro3);
                dm.CreateEmptyFile(s.SelectOneFollow(patient.PatientId, pro3.ProfessionalId).FilePath);

                Patient p = s.SelectPatient(patient.PatientId);
                Console.WriteLine("Nom : {0}   Prénom : {1}   Nom du Referent : {2}   Prenom du Referent : {3}", p.User.LastName, p.User.FirstName, p.Referent.User.LastName, p.Referent.User.FirstName);
                Assert.AreEqual(p.PatientId, patient.PatientId);
                Patient p1 = s.SelectPatient(patient1.PatientId);
                Console.WriteLine("Nom : {0}   Prénom : {1}   Nom du Referent : {2}   Prenom du Referent : {3}", p1.User.LastName, p1.User.FirstName, p1.Referent.User.LastName, p1.Referent.User.FirstName);
                Assert.AreEqual(p1.PatientId, patient1.PatientId);

                Professional Pro = s.SelectProfessional(pro.ProfessionalId);
                Console.WriteLine("Nom : {0}   Prénom : {1}", Pro.User.LastName, Pro.User.FirstName);
                Assert.AreEqual(Pro.ProfessionalId, pro.ProfessionalId);
                Professional Pro1 = s.SelectProfessional(pro1.ProfessionalId);
                Console.WriteLine("Nom : {0}   Prénom : {1}", Pro1.User.LastName, Pro1.User.FirstName);
                Assert.AreEqual(Pro1.ProfessionalId, pro1.ProfessionalId);
                Professional Pro2 = s.SelectProfessional(pro2.ProfessionalId);
                Console.WriteLine("Nom : {0}   Prénom : {1}", Pro2.User.LastName, Pro2.User.FirstName);
                Assert.AreEqual(Pro2.ProfessionalId, pro2.ProfessionalId);
                Professional Pro3 = s.SelectProfessional(pro3.ProfessionalId);
                Console.WriteLine("Nom : {0}   Prénom : {1}", Pro3.User.LastName, Pro3.User.FirstName);
                Assert.AreEqual(Pro3.ProfessionalId, pro3.ProfessionalId);

                Follower f = s.SelectOneFollow(patient.PatientId, patient.Referent.ProfessionalId);
                Console.WriteLine("PathFile : {0}", f.FilePath);
                Assert.AreEqual(f.FilePath, f.PatientId + "$" + f.ProfessionnalId);
                Follower f1 = s.SelectOneFollow(patient1.PatientId, patient1.Referent.ProfessionalId);
                Console.WriteLine("PathFile : {0}", f1.FilePath);
                Assert.AreEqual(f1.FilePath, f1.PatientId + "$" + f1.ProfessionnalId);
                context.SaveChanges();
            }
        }
        [Test]
        public void CreateFollow()
        {
            using (ArchiViteContext context = _archiVite.Context)
            {
                AddRequest a = new AddRequest(context);
                DocumentManager dm = new DocumentManager(context);
                SelectRequest s = new SelectRequest(context);

                Patient patient = s.SelectPatient("Guillaume", "Fimes");
                Professional pro = s.SelectProfessional("Clement", "Rousseau");
                a.AddFollow(patient, pro);

                Follower follow = s.SelectOneFollow(patient.PatientId, pro.ProfessionalId);
                Assert.AreEqual(follow.PatientId, patient.PatientId);
                Assert.AreEqual(follow.ProfessionnalId, pro.ProfessionalId);
                dm.CreateEmptyFile(follow.FilePath);
            }
        }
        [Test]
        public void UpdateUser()
        {
            using (ArchiViteContext context = _archiVite.Context)
            {
                UpdateRequest ur = new UpdateRequest(context);
                SelectRequest s = new SelectRequest(context);

                User user = s.SelectUser("Guillaume", "Fimes");
                ur.CheckUserInfo(user.FirstName, user.LastName, user.Adress, user.Birthdate, "Paris", user.Email, user.Postcode, user.PhoneNumber, user.Photo, user);

                User NewUser = s.SelectUser(user.UserId);
                Assert.AreEqual(NewUser.City, "Paris");
                Assert.AreEqual(NewUser.LastName, "Fimes");
            }
        }
        [Test]
        public void UpdatePatient()
        {
            using (ArchiViteContext context = _archiVite.Context)
            {
                UpdateRequest ur = new UpdateRequest(context);
                SelectRequest s = new SelectRequest(context);
                Patient patient = s.SelectPatient("Guillaume", "Fimes");
                Professional pro = s.SelectProfessional("Olivier", "Spinelli");

                ur.CheckPatientInfo(pro, patient);

                Patient newPatient = s.SelectPatient(patient.PatientId);
                Assert.AreEqual(newPatient.Referent.ProfessionalId, pro.ProfessionalId);
            }
        }
        [Test]
        public void UpdatePro()
        {
            using (ArchiViteContext context = _archiVite.Context)
            {
                UpdateRequest ur = new UpdateRequest(context);
                SelectRequest s = new SelectRequest(context);
                Professional pro = s.SelectProfessional("Simon", "Favraud");

                ur.UpdateProInfo("Archi'Mède", pro);
                Professional newPro = s.SelectProfessional(pro.ProfessionalId);
                Assert.AreEqual(newPro.Role, "Archi'Mède");
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

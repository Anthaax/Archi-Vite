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
        DocumentManager _doc;
        public AddAndUpdateTest()
        {
            _archiVite = new ContextManager();
            _doc = new DocumentManager(_archiVite.Context);
        }
        [Test]
        public void CreatePatientAndPro()
        {
            using (ArchiViteContext context = _archiVite.Context)
            {
                AddRequest a = new AddRequest(context);
                DocumentManager dm = new DocumentManager(context);
                SelectRequest s = new SelectRequest(context);
                Professional pro = a.AddProfessional("Antoine", "Raquillet", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "AntoineR", "AntoineR", "yolo", "Medecin");
                Professional pro1 = a.AddProfessional("Simon", "Favraud", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "SimonF", "SimonF", "yolo", "Infirmier");
                Professional pro2 = a.AddProfessional("Clement", "Rousseau", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "ClementC", "ClementC", "yolo", "Medecin");
                Professional pro3 = a.AddProfessional("Olivier", "Spinelli", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "OlivierS", "OlivierS", "yolo", "Medecin");
                Patient patient = a.AddPatient("Guillaume", "Fimes", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "GuillaumeF", "GuillaumeF", "yolo", pro);
                dm.CreateEmptyFile(patient.PatientId.ToString());
                dm.CreateEmptyFile(patient.PatientId + "$" + patient.Referent.ProfessionalId);
                Patient patient1 = a.AddPatient("Maxime", "Coucou", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "MaximeD", "MaximeD", "yolo", pro2);
                dm.CreateEmptyFile(patient1.PatientId + "$" + patient1.Referent.ProfessionalId);
                dm.CreateEmptyFile(patient1.PatientId.ToString());


                a.AddFollow(patient, pro1);
                dm.CreateEmptyFile(patient.PatientId + "$" + pro1.ProfessionalId);

                a.AddFollow(patient, pro2);
                dm.CreateEmptyFile(patient.PatientId + "$" + pro2.ProfessionalId);

                a.AddFollow(patient, pro3);
                dm.CreateEmptyFile(patient.PatientId + "$" + pro3.ProfessionalId);

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

                Patient patient = s.SelectPatient("GuillaumeF", "GuillaumeF");
                Professional pro = s.SelectProfessional("ClementR", "ClementR");
                a.AddFollow(patient, pro);

                Follower follow = s.SelectOneFollow(patient.PatientId, pro.ProfessionalId);
                Assert.AreEqual(follow.PatientId, patient.PatientId);
                Assert.AreEqual(follow.ProfessionnalId, pro.ProfessionalId);
                dm.CreateEmptyFile(patient.PatientId + "$" + pro.ProfessionalId);
            }
        }
        [Test]
        public void UpdateUser()
        {
            using (ArchiViteContext context = _archiVite.Context)
            {
                UpdateRequest ur = new UpdateRequest(context);
                SelectRequest s = new SelectRequest(context);

                User user = s.SelectUser("GuillaumeF", "GuillaumeF");
                ur.CheckUserInfo(user);

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
                Patient patient = s.SelectPatient("GuillaumeF", "GuillaumeF");
                Professional pro = s.SelectProfessional("OlivierS", "OlivierS");

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
                Professional pro = s.SelectProfessional("SimonF", "SimonF");

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
                    _doc.DeleteFollowerFile(follow.ProfessionnalId, follow.PatientId);
                    context.SuppressionRequest.FollowerSuppression(follow);
                    context.SaveChanges();
                }
                var selectQuery2 = context.Patient.ToList();
                foreach (var patient in selectQuery2)
                {
                    _doc.DeletePatientFile(patient.PatientId);
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
                Assert.IsFalse(context.Follower.Any());
                Assert.IsFalse(context.Patient.Any());
                Assert.IsFalse(context.Professional.Any());
                Assert.IsFalse(context.User.Any());
            }
        }
    }
}

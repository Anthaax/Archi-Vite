using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.Archi_Vite.Core;
using System.Data.Entity;
namespace ITI.Archi_Vite.DataBase.Test
{
    [TestFixture]
    public class MessageTest
    {

        [Test]
        public void CreatePatientAndPro()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                DocumentManager dm = new DocumentManager(context);
                User[] users = GetUsers();
                Professional pro = context.AddRequest.AddProfessional(users[0], "Medecin");
                Professional pro1 = context.AddRequest.AddProfessional(users[1], "Infirmier");
                Professional pro2 = context.AddRequest.AddProfessional(users[2], "Medecin");
                Professional pro3 = context.AddRequest.AddProfessional(users[3], "Medecin");
                Patient patient = context.AddRequest.AddPatient(users[4]);
                dm.CreateEmptyFile(patient.PatientId.ToString());
                Patient patient1 = context.AddRequest.AddPatient(users[5]);
                dm.CreateEmptyFile(patient1.PatientId.ToString());


                context.AddRequest.AddFollow(patient.PatientId, pro1.ProfessionalId);
                dm.CreateEmptyFile(patient.PatientId + "$" + pro1.ProfessionalId);

                context.AddRequest.AddFollow(patient.PatientId, pro2.ProfessionalId);
                dm.CreateEmptyFile(patient.PatientId + "$" + pro2.ProfessionalId);

                context.AddRequest.AddFollow(patient.PatientId, pro3.ProfessionalId);
                dm.CreateEmptyFile(patient.PatientId + "$" + pro3.ProfessionalId);

                Patient p = context.SelectRequest.SelectPatient(patient.PatientId);
                Console.WriteLine("Nom : {0}   Prénom : {1}", p.User.LastName, p.User.FirstName);
                Assert.AreEqual(p.PatientId, patient.PatientId);
                Patient p1 = context.SelectRequest.SelectPatient(patient1.PatientId);
                Console.WriteLine("Nom : {0}   Prénom : {1} ", p1.User.LastName, p1.User.FirstName);
                Assert.AreEqual(p1.PatientId, patient1.PatientId);

                Professional Pro = context.SelectRequest.SelectProfessional(pro.ProfessionalId);
                Console.WriteLine("Nom : {0}   Prénom : {1}", Pro.User.LastName, Pro.User.FirstName);
                Assert.AreEqual(Pro.ProfessionalId, pro.ProfessionalId);
                Professional Pro1 = context.SelectRequest.SelectProfessional(pro1.ProfessionalId);
                Console.WriteLine("Nom : {0}   Prénom : {1}", Pro1.User.LastName, Pro1.User.FirstName);
                Assert.AreEqual(Pro1.ProfessionalId, pro1.ProfessionalId);
                Professional Pro2 = context.SelectRequest.SelectProfessional(pro2.ProfessionalId);
                Console.WriteLine("Nom : {0}   Prénom : {1}", Pro2.User.LastName, Pro2.User.FirstName);
                Assert.AreEqual(Pro2.ProfessionalId, pro2.ProfessionalId);
                Professional Pro3 = context.SelectRequest.SelectProfessional(pro3.ProfessionalId);
                Console.WriteLine("Nom : {0}   Prénom : {1}", Pro3.User.LastName, Pro3.User.FirstName);
                Assert.AreEqual(Pro3.ProfessionalId, pro3.ProfessionalId);
            }
        }
        [Test]
        public void CreateMessage()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                DocumentManager dm = new DocumentManager(context);
                List<Professional> receivers = new List<Professional>();
                foreach (var f in context.SelectRequest.SelectFollowForPatient(context.SelectRequest.SelectPatient("GuillaumeF", "GuillaumeF").PatientId))
                {
                    receivers.Add(f.Professionnal);
                }
                dm.CreateMessage(receivers, context.SelectRequest.SelectProfessional("AntoineR", "AntoineR"), "Coucou", "J'ai un pb", context.SelectRequest.SelectPatient("GuillaumeF", "GuillaumeF"));
                DocumentSerializable document = dm.SeeDocument(context.SelectRequest.SelectProfessional(context.SelectRequest.SelectProfessional("AntoineR", "AntoineR").ProfessionalId), context.SelectRequest.SelectPatient("GuillaumeF", "GuillaumeF"));
                Assert.AreEqual(document.Messages.Count, 1);
                dm.CreateMessage(receivers, context.SelectRequest.SelectProfessional("AntoineR", "AntoineR"), "Salut", "Hey", context.SelectRequest.SelectPatient("GuillaumeF", "GuillaumeF"));
                DocumentSerializable documents = dm.SeeDocument(context.SelectRequest.SelectProfessional(context.SelectRequest.SelectProfessional("AntoineR", "AntoineR").ProfessionalId), context.SelectRequest.SelectPatient("GuillaumeF", "GuillaumeF"));
                Assert.AreEqual(documents.Messages.Count, 2);
            }
        }
        [Test]
        public void DeleteMessage()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                DocumentManager dm = new DocumentManager(context);
                SelectRequest s = new SelectRequest(context);
                dm.DeleteFollowerFile(s.SelectProfessional("SimonF", "SimonF").ProfessionalId, s.SelectPatient("GuillaumeF", "GuillaumeF").PatientId);
                context.SuppressionRequest.FollowerSuppression(s.SelectOneFollow(s.SelectPatient("GuillaumeF", "GuillaumeF").PatientId, s.SelectProfessional("SimonF", "SimonF").ProfessionalId));
                Assert.IsNull(s.SelectOneFollow(s.SelectPatient("GuillaumeF", "GuillaumeF").PatientId, s.SelectProfessional("SimonF", "SimonF").ProfessionalId));
            }
        }
        private User[] GetUsers()
        {
            User[] users = new User[6];
            User u0 = new User()
            {
                FirstName = "Antoine",
                LastName = "Raquillet",
                Birthdate = DateTime.Now,
                Adress = "72 avenue maurice thorez",
                City = "Ivry-sur-Seine",
                Postcode = 12345,
                PhoneNumber = 0606066606,
                Pseudo = "AntoineR",
                Password = "AntoineR",
                Photo = "yolo"
            };
            User u1 = new User()
            {
                FirstName = "Simon",
                LastName = "Favraud",
                Birthdate = DateTime.Now,
                Adress = "72 avenue maurice thorez",
                City = "Ivry-sur-Seine",
                Postcode = 12345,
                PhoneNumber = 0606066606,
                Pseudo = "SimonF",
                Password = "SimonF",
                Photo = "yolo"
            };
            User u2 = new User()
            {
                FirstName = "Clement",
                LastName = "Rousseau",
                Birthdate = DateTime.Now,
                Adress = "72 avenue maurice thorez",
                City = "Ivry-sur-Seine",
                Postcode = 12345,
                PhoneNumber = 0606066606,
                Pseudo = "ClementR",
                Password = "ClementR",
                Photo = "yolo"
            };
            User u3 = new User()
            {
                FirstName = "Olivier",
                LastName = "Spinelli",
                Birthdate = DateTime.Now,
                Adress = "72 avenue maurice thorez",
                City = "Ivry-sur-Seine",
                Postcode = 12345,
                PhoneNumber = 0606066606,
                Pseudo = "OlivierS",
                Password = "OlivierS",
                Photo = "yolo"
            };
            User u4 = new User()
            {
                FirstName = "Guillaume",
                LastName = "Fimes",
                Birthdate = DateTime.Now,
                Adress = "72 avenue maurice thorez",
                City = "Ivry-sur-Seine",
                Postcode = 12345,
                PhoneNumber = 0606066606,
                Pseudo = "GuillaumeF",
                Password = "GuillaumeF",
                Photo = "yolo"
            };
            User u5 = new User()
            {
                FirstName = "Maxime",
                LastName = "De Vogelas",
                Birthdate = DateTime.Now,
                Adress = "72 avenue maurice thorez",
                City = "Ivry-sur-Seine",
                Postcode = 12345,
                PhoneNumber = 0606066606,
                Pseudo = "MaximeD",
                Password = "MaximeD",
                Photo = "yolo"
            };
            users[0] = u0;
            users[1] = u1;
            users[2] = u2;
            users[3] = u3;
            users[4] = u4;
            users[5] = u5;
            return users;
        }
    }
}

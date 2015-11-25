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
        ContextManager _archiVite;
        public MessageTest()
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
                Professional pro = a.AddProfessional("Antoine", "Raquillet", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "AntoineR", "AntoineR", "yolo", "Medecin");
                Professional pro1 = a.AddProfessional("Simon", "Favraud", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "SimonF", "SimonF", "yolo", "Infirmier");
                Professional pro2 = a.AddProfessional("Clement", "Rousseau", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "ClementC", "ClementC", "yolo", "Medecin");
                Professional pro3 = a.AddProfessional("Olivier", "Spinelli", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "OlivierS", "OlivierS", "yolo", "Medecin");
                Patient patient = a.AddPatient("Guillaume", "Fimes", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "GuillaumeF", "GuillaumeF","yolo", pro);
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
        public void CreateMessage()
        {
            using (ArchiViteContext context = _archiVite.Context)
            {
                DocumentManager dm = new DocumentManager(context);
                SelectRequest s = new SelectRequest(context);
                List<Professional> receivers = new List<Professional>();
                foreach (var f in s.SelectFollowForPatient(s.SelectPatient("GuillaumeF", "GuillaumeF").PatientId))
                {
                    receivers.Add(f.Professionnal);
                }
                dm.CreateMessage(receivers, s.SelectProfessional("AntoineR", "AntoineR"), "Coucou", "J'ai un pb", s.SelectPatient("GuillaumeF", "GuillaumeF"));
                DocumentSerializable document = dm.SeeDocument(s.SelectProfessional(s.SelectPatient("GuillaumeF", "GuillaumeF").Referent.ProfessionalId), s.SelectPatient("GuillaumeF", "GuillaumeF"));
                Assert.AreEqual(document.Messages.Count, 1);
                dm.CreateMessage(receivers, s.SelectProfessional("AntoineR", "AntoineR"), "Salut", "Hey", s.SelectPatient("GuillaumeF", "GuillaumeF"));
                DocumentSerializable documents = dm.SeeDocument(s.SelectProfessional(s.SelectPatient("GuillaumeF", "GuillaumeF").Referent.ProfessionalId), s.SelectPatient("GuillaumeF", "GuillaumeF"));
                Assert.AreEqual(documents.Messages.Count, 2);
            }
        }
        [Test]
        public void DeleteMessage()
        {
            using (ArchiViteContext context = _archiVite.Context)
            {
                DocumentManager dm = new DocumentManager(context);
                SelectRequest s = new SelectRequest(context);
                dm.DeleteFollowerFile(s.SelectProfessional("SimonF", "SimonF"), s.SelectPatient("GuillaumeF", "GuillaumeF"));
                context.SuppressionRequest.FollowerSuppression(s.SelectOneFollow(s.SelectPatient("GuillaumeF", "GuillaumeF").PatientId, s.SelectProfessional("SimonF", "SimonF").ProfessionalId));
                Assert.IsNull(s.SelectOneFollow(s.SelectPatient("GuillaumeF", "GuillaumeF").PatientId, s.SelectProfessional("SimonF", "SimonF").ProfessionalId));
            }
        }
    }
}

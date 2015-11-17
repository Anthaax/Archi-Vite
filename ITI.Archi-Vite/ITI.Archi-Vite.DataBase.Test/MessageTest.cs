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
        public void CreatePatientAndPro()
        {
            AddRequest a = new AddRequest();
            SelectRequest s = new SelectRequest();
            Professional pro = a.AddProfessional("Antoine", "Raquillet", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "sfavraud@intechinfo.fr", "yolo", "Medecin");
            Professional pro1 = a.AddProfessional("Simon", "Favraud", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "sfavraud@intechinfo.fr", "yolo", "Infirmier");
            Professional pro2 = a.AddProfessional("Clement", "Rousseau", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "sfavraud@intechinfo.fr", "yolo", "Medecin");
            Professional pro3 = a.AddProfessional("Olivier", "Spinelli", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "sfavraud@intechinfo.fr", "yolo", "Medecin");
            Patient patient = a.AddPatient("Guillaume", "Fimes", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "sfavraud@intechinfo.fr", "yolo", pro, "yolo");
            Patient patient1 = a.AddPatient("Maxime", "Coucou", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12452, 0606066606, "sfavraud@intechinfo.fr", "yolo", pro2, "yolo");

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

        }
        [Test]
        public void CreateMessage()
        {
            DocumentManager dm = new DocumentManager();
        }
        [Test]
        public void GetMessage()
        {
            DocumentManager dm = new DocumentManager();
            Patient patient;
            using (ArchiViteContext context = new ArchiViteContext())
            {
                patient = context.Patient
                                .Include(c => c.User)
                                .Include(c => c.Referent)
                                .Include(c => c.Referent.User)
                                .Where(t => t.User.FirstName.Equals("Guillaume"))
                                .FirstOrDefault();
                var follower = context.Follower
                                    .Include(c => c.Patient)
                                    .Include(c => c.Professionnal)
                                    .Include(c => c.Professionnal.User)
                                    .Include(c => c.Patient.User)
                                    .Include(c => c.Patient.Referent)
                                    .Where(t => t.PatientId.Equals(patient.PatientId))
                                    .ToList();

            }
        }
    }
}

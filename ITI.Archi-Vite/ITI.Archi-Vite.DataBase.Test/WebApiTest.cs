using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.Archi_Vite.Core;
using System.Data.Entity;
using ITI.Archi_Vite.DataBase;
using ITI.Archi_Vite.WebApi.Controllers;

namespace ITI.Archi_Vite.DataBase.Test
{
    [TestFixture]
    class WebApiTest
    {
        DocumentManager _doc;
        UserService _userService;
        FollowerService _followerService;
        DocumentService _documentService;
        PatientService _patientService;
        ProfessionalService _professionalService;
        MessageService _messageService;
        public WebApiTest()
        {
            _professionalService = new ProfessionalService();
            _patientService = new PatientService();
            _userService = new UserService();
            _documentService = new DocumentService();
            _followerService = new FollowerService();
            _messageService = new MessageService();
        }

        [Test]
        public void GetUser()
        {

            User u = _userService.getUser("ClementR", "ClementR");
            Assert.AreEqual(u.LastName, "Rousseau");
            int id = u.UserId;

            User u2 = _userService.getUser(id);
            Assert.AreEqual(u.LastName, "Rousseau");
        }

        [Test]
        public void PostUser()
        {
            User u = _userService.getUser("ClementR", "ClementR");
            u.City = "Paris";
            _userService.PostUser(u);
            Assert.AreEqual(u.City, ("Paris"));
            u.City = "Ivry-sur-Seine";
            _userService.PostUser(u);
            Assert.AreEqual(u.City, ("Ivry-sur-Seine"));

        }

        [Test]
        public void GetFollower()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                Dictionary<Patient, Professional[]> expectedFollow = new Dictionary<Patient, Professional[]>();
                Professional[] proArray = new Professional[10];

                proArray.SetValue(context.SelectRequest.SelectProfessional("SimonF", "SimonF"), 0);
                proArray.SetValue(context.SelectRequest.SelectProfessional("ClementR", "ClementR"), 1);
                proArray.SetValue(context.SelectRequest.SelectProfessional("OlivierS", "OlivierS"), 2);

                expectedFollow.Add(context.SelectRequest.SelectPatient("GuillaumeF", "GuillaumeF"), proArray);
                Dictionary<Patient, Professional[]> allFollow = _followerService.getDocument(context.SelectRequest.SelectProfessional("ClementR", "ClementR").ProfessionalId);


                foreach (var pair in allFollow)
                {
                    Assert.AreEqual(pair.Key.PatientId, context.SelectRequest.SelectPatient("GuillaumeF", "GuillaumeF").PatientId);
                    for (int i = 0; i < 3; i++)
                    {
                        Assert.AreEqual(pair.Value[i].ProfessionalId, proArray[i].ProfessionalId);
                    }
                }
            }
        }

        [Test]
        public void PutFollower()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                _doc = new DocumentManager(context);
                FollowerCreation myNewFollow = new FollowerCreation(context.SelectRequest.SelectPatient("GuillaumeF", "GuillaumeF").PatientId,
                    context.SelectRequest.SelectProfessional("AntoineR", "AntoineR").ProfessionalId);
                _followerService.PutFollower(myNewFollow);

                Dictionary<Patient, Professional[]> allFollow = _followerService.getDocument(context.SelectRequest.SelectProfessional("ClementR", "ClementR").ProfessionalId);
                foreach (var pair in allFollow)
                {
                    Assert.AreEqual(pair.Value[0].ProfessionalId, context.SelectRequest.SelectProfessional("AntoineR", "AntoineR").ProfessionalId);
                }

                _doc.DeleteFollowerFile(context.SelectRequest.SelectProfessional("AntoineR","AntoineR").ProfessionalId, context.SelectRequest.SelectPatient("GuillaumeF", "GuillaumeF").PatientId);
                context.SuppressionRequest.FollowerSuppression(context.SelectRequest.SelectOneFollow(context.SelectRequest.SelectPatient("GuillaumeF", "GuillaumeF").PatientId, context.SelectRequest.SelectProfessional("AntoineR", "AntoineR").ProfessionalId));
                Assert.IsNull(context.SelectRequest.SelectOneFollow(context.SelectRequest.SelectPatient("GuillaumeF", "GuillaumeF").PatientId, context.SelectRequest.SelectProfessional("AntoineR", "AntoineR").ProfessionalId));
            }
        }

        [Test]
        public void DeleteFollower()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                _followerService.DeleteFollowerCheck(context.SelectRequest.SelectPatient("GuillaumeF", "GuillaumeF").PatientId, context.SelectRequest.SelectProfessional("ClementR", "ClementR").ProfessionalId);
                Assert.IsNull(context.SelectRequest.SelectOneFollow(context.SelectRequest.SelectPatient("GuillaumeF", "GuillaumeF").PatientId, context.SelectRequest.SelectProfessional("ClementR", "ClementR").ProfessionalId));

                FollowerCreation myNewFollow = new FollowerCreation(context.SelectRequest.SelectPatient("GuillaumeF", "GuillaumeF").PatientId,
                    context.SelectRequest.SelectProfessional("ClementR", "ClementR").ProfessionalId);
                _followerService.PutFollower(myNewFollow);

            }

        }

        [Test]
        public void GetPatient()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                Patient p = _patientService.getPatient(context.SelectRequest.SelectPatient("GuillaumeF", "GuillaumeF").PatientId);
                Assert.AreEqual(p.PatientId, context.SelectRequest.SelectPatient("GuillaumeF", "GuillaumeF").PatientId);
            }
        }

        [Test]
        public void PutAndDeletePatient()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                _doc = new DocumentManager(context);
                User userTest = new User()
                {
                    FirstName = "TEST",
                    LastName = "TEST",
                    Birthdate = DateTime.Now,
                    Adress = "72 avenue maurice thorez",
                    City = "Ivry-sur-Seine",
                    Postcode = 12345,
                    PhoneNumber = 0606066606,
                    Pseudo = "test",
                    Password = "mdp",
                    Photo = "yolo"
                };

                PatientCreation newPatient = new PatientCreation(userTest);
                _patientService.putPatient(newPatient);
                Assert.IsNotNull(context.SelectRequest.SelectPatient("test", "mdp"));

                _patientService.deletePatientCheck(context.SelectRequest.SelectPatient("test", "mdp").PatientId);
                Assert.IsNull(context.SelectRequest.SelectPatient("test", "mdp"));
            }
        }

        [Test]
        public void GetProfessional()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                Professional p = _professionalService.getProfessional(context.SelectRequest.SelectProfessional("ClementR", "ClementR").ProfessionalId);
                Assert.AreEqual(p.ProfessionalId, context.SelectRequest.SelectProfessional("ClementR", "ClementR").ProfessionalId);
            }
        }

        [Test]
        public void PutAndDeleteProfessional()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                _doc = new DocumentManager(context);
                User userTest = new User()
                {
                    FirstName = "TEST",
                    LastName = "TEST",
                    Birthdate = DateTime.Now,
                    Adress = "72 avenue maurice thorez",
                    City = "Ivry-sur-Seine",
                    Postcode = 12345,
                    PhoneNumber = 0606066606,
                    Pseudo = "test",
                    Password = "mdp",
                    Photo = "yolo"
                };

                ProfessionalCreation newPro = new ProfessionalCreation (userTest, "testeur");
                _professionalService.putProfessional(newPro);
                Assert.IsNotNull(context.SelectRequest.SelectProfessional("test", "mdp"));

                _professionalService.DeleteProfessionalCheck(context.SelectRequest.SelectProfessional("test", "mdp").ProfessionalId);
                Assert.IsNull(context.SelectRequest.SelectProfessional("test", "mdp"));
            }
        }

        [Test]
        public void GetDoc()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                DocumentSerializable doc = _messageService.getMessage(context.SelectRequest.SelectProfessional("ClementR","ClementR").ProfessionalId, context.SelectRequest.SelectPatient("GuillaumeF","GuillaumeF").PatientId);

                Assert.IsNotNull(doc);
            }
        }

        [Test]
        public void PutDoc()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                DocumentManager dm = new DocumentManager(context);
                List<Professional> listPro = new List<Professional>();
                listPro.Add(context.SelectRequest.SelectProfessional("ClementR", "ClementR"));
                listPro.Add(context.SelectRequest.SelectProfessional("SimonF", "SimonF"));
                Professional Sender = context.SelectRequest.SelectProfessional("AntoineR", "AntoineR");
                string Title = "My Title";
                string Contents = "My Contents";
                Patient P = context.SelectRequest.SelectPatient("GuillaumeF", "GuillaumeF");

                _messageService.putMessage(listPro, Sender, Title, Contents, P);

                DocumentSerializable document = dm.SeeDocument(context.SelectRequest.SelectProfessional("ClementR", "ClementR"), context.SelectRequest.SelectPatient("GuillaumeF", "GuillaumeF"));
                Assert.AreEqual(document.Messages.Count, 1);

                
            }
        }

        [Test]
        public void PostMessage()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                DocumentManager dm = new DocumentManager(context);
                List<Professional> listPro = new List<Professional>();
                listPro.Add(context.SelectRequest.SelectProfessional("ClementR", "ClementR"));
                listPro.Add(context.SelectRequest.SelectProfessional("SimonF", "SimonF"));
                Professional Sender = context.SelectRequest.SelectProfessional("AntoineR", "AntoineR");
                string Title = "My Title2";
                string Contents = "My Contents";
                Patient P = context.SelectRequest.SelectPatient("GuillaumeF", "GuillaumeF");

                _messageService.putMessage(listPro, Sender, Title, Contents, P);

                
                DocumentSerializable document = dm.SeeDocument(context.SelectRequest.SelectProfessional("ClementR", "ClementR"), context.SelectRequest.SelectPatient("GuillaumeF", "GuillaumeF"));

                foreach (var message in document.Messages)
                {
                    if (message.Title == "MyTitle2")
                    {
                        _messageService.postMessage(context.SelectRequest.SelectPatient("GuillaumF", "GuillaumeF").PatientId, context.SelectRequest.SelectProfessional("AntoineR", "AntoineR").ProfessionalId, message.Date);

                        Assert.That(message.Receivers.Count == 3);
                    }
                }
            }

        }
    }
}

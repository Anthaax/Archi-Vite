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
        public WebApiTest()
        {
            _userService = new UserService();
            _documentService = new DocumentService();
            _followerService = new FollowerService();
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
        public void getFollower()
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
        public void putFollower()
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
    }
}

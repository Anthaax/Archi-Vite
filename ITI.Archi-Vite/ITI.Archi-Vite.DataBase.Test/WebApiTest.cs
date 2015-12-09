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
        DocumentService _documentService;
        public WebApiTest()
        {
            _userService = new UserService();
            _documentService = new DocumentService();
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
        public void SeeDocumentPatient()
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
            }  
        }
    }
}

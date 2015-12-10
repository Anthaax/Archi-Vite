using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITI.Archi_Vite.DataBase;
using ITI.Archi_Vite.Core;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class UserService
    {
        private ArchiViteContext _db = new ArchiViteContext();
        DocumentManager _doc;
        public User getUser(int id)
        {
            User user = _db.SelectRequest.SelectUser(id);
            return user;
        }

        public User getUser(string pseudo, string password)
        {
            User user = _db.SelectRequest.SelectUser(pseudo, password);
            return user;
        }

        public void PostUser(User user)
        {
            _db.UpdateRequest.CheckUserInfo(user);
        }
    }
}
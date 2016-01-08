using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITI.Archi_Vite.Core;
using ITI.Archi_Vite.DataBase;


namespace ITI.Archi_Vite.WebApi
{
    public class Data
    {
        readonly DocumentSerializable _documents;
        readonly Dictionary<Patient, Professional[]> _followers;
        readonly User _user;
        public Data (DocumentSerializable doc, Dictionary<Patient,Professional[]> follow, User u)
        {
            _documents = doc;
            _followers = follow;
            _user = u;
        }

        public DocumentSerializable Documents
        {
            get
            {
                return _documents;
            }
        }

        public Dictionary<Patient, Professional[]> Followers
        {
            get
            {
                return _followers;
            }
        }

        public User User
        {
            get
            {
                return _user;
            }
        }
    }
}
using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class PatientCreation
    {
        User _user;
        int _referent;
        string _pathFile;

        public PatientCreation(User user, int referent, string pathFile)
        {
            _user = user;
            _referent = referent;
            _pathFile = pathFile;
        }

        public User User
        {
            get
            {
                return _user;
            }

            set
            {
                _user = value;
            }
        }

        public int Referent
        {
            get
            {
                return _referent;
            }

            set
            {
                _referent = value;
            }
        }

        public string PathFile
        {
            get
            {
                return _pathFile;
            }

            set
            {
                _pathFile = value;
            }
        }
    }
}
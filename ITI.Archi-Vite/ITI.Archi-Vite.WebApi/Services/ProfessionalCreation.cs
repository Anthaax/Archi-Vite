using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class ProfessionalCreation
    {
        readonly User _user;
        readonly string _role;

        public ProfessionalCreation(User user, string role)
        {
            _user = user;
            _role = role;
        }
        public string Role
        {
            get
            {
                return _role;
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

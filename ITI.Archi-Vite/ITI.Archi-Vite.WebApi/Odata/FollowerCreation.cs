using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.WebApi.Odata
{
    public class FollowerCreation
    {
        readonly Patient _patient;
        readonly Professional _professional;

        public FollowerCreation(Patient patient, Professional professional)
        {
            _patient = patient;
            _professional = professional;
        }

        public Patient Patient
        {
            get
            {
                return _patient;
            }
        }

        public Professional Professional
        {
            get
            {
                return _professional;
            }
        }
    }
}

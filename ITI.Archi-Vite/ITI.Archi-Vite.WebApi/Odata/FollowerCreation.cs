using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class FollowerCreation
    {
        readonly int _patientId;
        readonly int _professionalId;

        public FollowerCreation(int patient, int professionalId)
        {
            _patientId = patient;
            _professionalId = professionalId;
        }

        public int PatientId
        {
            get
            {
                return _patientId;
            }
        }

        public int ProfessionalId
        {
            get
            {
                return _professionalId;
            }
        }
    }
}

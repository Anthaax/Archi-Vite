using System;
using System.Collections.Generic;

namespace ITI.Archi_Vite.Forms
{
	public class Data
	{
        readonly List<Patient> patients = new List<Patient>();
        readonly List<Professional> pro = new List<Professional>();
		readonly User user = new User();
		public Data()
		{
			
		}

        public List<Patient> Patients
        {
            get
            {
                return patients;
            }
        }

        public List<Professional> Pro
        {
            get
            {
                return pro;
            }
        }

        public User User
        {
            get
            {
                return user;
            }
        }
    }
}


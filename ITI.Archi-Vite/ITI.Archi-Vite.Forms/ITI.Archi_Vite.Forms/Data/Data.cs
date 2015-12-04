using System;
using System.Collections.Generic;

namespace ITI.Archi_Vite.Forms
{
	public class Data
	{
        readonly Dictionary<Patient, List<Professional>> follow = new Dictionary<Patient, List<Professional>>();
        readonly User _user;
		public Data(User user)
		{
            _user = user;
		}

        public Dictionary<Patient, List<Professional>> Follow
        {
            get
            {
                return follow;
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


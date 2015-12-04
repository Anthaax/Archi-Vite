using System;
using System.Collections.Generic;

namespace ITI.Archi_Vite.Forms
{
	public class Data
	{
        readonly Dictionary<Patient, List<Professional>> _follow;
        readonly User _user;
		public Data(User user /*, Dictionary<Patient, List<Professional>> followers*/)
		{
            _user = user;
            //_follow = followers;
		}

        public Dictionary<Patient, List<Professional>> Follow
        {
            get
            {
                return _follow;
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


using System;
using System.Collections.Generic;

namespace ITI.Archi_Vite.Forms
{
	public class Data
	{
        readonly Dictionary<Patient, Professional[]> _follow;
        readonly User _user;
        DocumentSerializable _documents;
		public Data(User user , Dictionary<Patient, Professional[]> followers)
		{
            _user = user;
            _follow = followers;
		}

        public Dictionary<Patient, Professional[]> Follow
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


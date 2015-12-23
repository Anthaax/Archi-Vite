using System;
using System.Collections.Generic;
using System.IO;


namespace ITI.Archi_Vite.Forms
{
	public class Data
	{
        readonly Dictionary<Patient, Professional[]> _follow;
        readonly User _user;
        readonly DocumentSerializable _documents;
		public Data(User user , Dictionary<Patient, Professional[]> followers, DocumentSerializable documents)
		{
            _user = user;
            _follow = followers;
			_documents = documents;
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

        public DocumentSerializable Documents
        {
            get
            {
                return _documents;
            }
        }
    }
}


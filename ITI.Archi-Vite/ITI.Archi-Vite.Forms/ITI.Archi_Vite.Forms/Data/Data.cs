﻿using System;
using System.Collections.Generic;
using System.IO;


namespace ITI.Archi_Vite.Forms
{
	public class Data
	{
        readonly Dictionary<Patient, Professional[]> _follow;
        readonly User _user;
        readonly DocumentSerializable _documents;
        readonly DocumentSerializable _documentsAdded;
        bool _needUpdate;
        public Data(User user, Dictionary<Patient, Professional[]> followers, DocumentSerializable documents, DocumentSerializable documentsAdded)
        {
            _user = user;
            _follow = followers;
            _documents = documents;
            _documentsAdded = documentsAdded;
            _needUpdate = false;
        }
        public Data(User user, Dictionary<Patient, Professional[]> followers, DocumentSerializable documents, DocumentSerializable documentsAdded, bool needUpdate)
            : base()
        {
            _user = user;
            _follow = followers;
            _documents = documents;
            _documentsAdded = documentsAdded;
            _needUpdate = needUpdate;
        }
        public Data(User user , Dictionary<Patient, Professional[]> followers, DocumentSerializable documents)
		{
            _user = user;
            _follow = followers;
			_documents = documents;
            _documentsAdded = new DocumentSerializable(new List<Message>(), new List<Prescription>());
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

        public DocumentSerializable DocumentsAdded
        {
            get
            {
                return _documentsAdded;
            }
        }

        public bool NeedUpdate
        {
            get
            {
                return _needUpdate;
            }

            set
            {
                _needUpdate = value;
            }
        }
    }
}


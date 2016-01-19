using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Core
{
    [Serializable]
    public abstract class Document
    {
        readonly List<Professional> _receivers;
        readonly Patient _patient;
        readonly User _sender;
        readonly DateTime _date;
        readonly string _title;

        protected Document(User Sender, List<Professional> Receivers, Patient Patient, string Title)
        {
            _receivers = Receivers;
            _patient = Patient;
            _sender = Sender;
            _date = DateTime.Now;
            _title = Title;
        }

        public List<Professional> Receivers
        {
            get
            {
                return _receivers;
            }
        }

        public Patient Patient
        {
            get
            {
                return _patient;
            }
        }

        public User Sender
        {
            get
            {
                return _sender;
            }
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
        }
    }
}

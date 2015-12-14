using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
    public abstract class Documents
    {
        readonly List<Professional> _receivers;
        readonly Patient _patient;
        readonly Professional _sender;
        readonly DateTime _date;
        readonly string _title;
		readonly string _senderFullName;
        readonly string _patientFullName;


        protected Documents(Professional Sender, List<Professional> Receivers, Patient Patient, string Title)
        {
            _receivers = Receivers;
            _patient = Patient;
            _sender = Sender;
			_senderFullName = Sender.FirstName + " " + Sender.LastName;
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

		public string SenderName
		{
			get
			{
				return _senderFullName;
			}
		}
        
		public Patient Patient
        {
            get
            {
                return _patient;
            }
        }

        public Professional Sender
        {
            get
            {
                return _sender;
            }
        }

		public string Date
        {
            get
            {
				return _date.ToString();
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
        }
        public string PatientFullName
        {
            get
            {
                return _patientFullName;
            }
        }
    }
}

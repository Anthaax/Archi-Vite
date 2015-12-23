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
			_patientFullName = Patient.FirstName + " " + Patient.LastName;
            _date = DateTime.Now;
            _title = Title;
        }

        public List<Professional> Recievers
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
        public string PatientFullName
        {
            get
            {
                return _patientFullName;
            }
        }
    }
}

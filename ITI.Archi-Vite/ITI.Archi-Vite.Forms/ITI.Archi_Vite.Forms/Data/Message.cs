using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
    public class Message : Documents
    {
        readonly string _contents;
		readonly string _patientFullName;
        public Message(string Title, string Contents, User Sender, List<Professional> Receivers, Patient Patient)
            : base(Sender, Receivers, Patient, Title)
        {
            _contents = Contents;
			_patientFullName = Patient.FirstName + " " + Patient.LastName;
        }

        public string Contents
        {
            get
            {
                return _contents;
            }
        }
    }
}

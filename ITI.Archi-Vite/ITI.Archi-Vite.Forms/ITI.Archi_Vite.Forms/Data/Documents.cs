using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
    public abstract class Documents
    {
        readonly List<Professional> receivers;
        readonly Patient patient;
        readonly Professional sender;
        readonly DateTime date;
        readonly string title;

        protected Documents(Professional Sender, List<Professional> Receivers, Patient Patient, string Title)
        {
            receivers = Receivers;
            patient = Patient;
            sender = Sender;
            date = DateTime.Now;
            title = Title;
        }

        public List<Professional> Receivers
        {
            get
            {
                return receivers;
            }
        }

        public Patient Patient
        {
            get
            {
                return patient;
            }
        }

        public Professional Sender
        {
            get
            {
                return sender;
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
        }
    }
}

using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Core
{
    public class Message
    {
        readonly List<Professional> receivers;
        readonly Patient patient;
        readonly Professional sender;
        readonly DateTime date;
        readonly string title;
        readonly string contents;

        public Message(string Title, string Contents, Professional Sender, List<Professional> Receivers, Patient Patient)
        {
            title = Title;
            contents = Contents;
            receivers = Receivers;
            sender = Sender;
            date = DateTime.Now;
            patient = Patient;
            
        }
        public List<User> Receivers
        {
            get
            {
                return receivers;
            }
        }

        public User Sender
        {
            get
            {
                return sender;
            }
        }

        public string Title
        {
            get
            {
                return title;
            }
        }

        public string Contents
        {
            get
            {
                return contents;
            }
        }

        public DateTime Date
        {
            get
            {
                return date;
            }
        }
    }
}

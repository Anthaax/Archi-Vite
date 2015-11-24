using ITI.Archi_Vite.DataBase;
using System.Collections.Generic;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class MesssageCreator
    {
        readonly List<Professional> _receivers;
        readonly Professional _sender;
        readonly string _title;
        readonly string _contents;
        readonly Patient _patient; 

        public MesssageCreator(List<Professional> Receivers, Professional Sender, string Title, string Contents, Patient Patient)
        {
            _receivers = Receivers;
            _sender = Sender;
            _title = Title;
            _contents = Contents;
            _patient = Patient;
        }
        public List<Professional> Receivers
        {
            get
            {
                return _receivers;
            }
        }

        public Professional Sender
        {
            get
            {
                return _sender;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
        }

        public string Contents
        {
            get
            {
                return _contents;
            }
        }

        public Patient Patient
        {
            get
            {
                return _patient;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
    public class DocumentSerializable
    {
        List<Message> _messages;
        List<Prescription> _prescriptions;

        public DocumentSerializable(List<Message> messages, List<Prescription> prescription)
        {
            _prescriptions = prescription;
            _messages = messages;
        }

        public List<Message> Messages
        {
            get
            {
                return _messages;
            }
            set
            {
                _messages = value;
            }
        }

        public List<Prescription> Prescriptions
        {
            get
            {
                return _prescriptions;
            }
            set
            {
                _prescriptions = value;
            }
        }
    }
}

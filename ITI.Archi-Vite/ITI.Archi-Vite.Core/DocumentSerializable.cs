using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Core
{
    [Serializable]
    public class DocumentSerializable
    {
        readonly List<Message> _messages;
        readonly List<Prescription> _prescriptions;

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
        }

        public List<Prescription> Prescriptions
        {
            get
            {
                return _prescriptions;
            }
        }
    }
}

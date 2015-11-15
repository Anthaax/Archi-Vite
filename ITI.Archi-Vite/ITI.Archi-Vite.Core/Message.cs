using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Core
{
    [Serializable]
    public class Message : Document
    {
        readonly string contents;

        public Message(string Title, string Contents, Professional Sender, List<Professional> Receivers, Patient Patient)
            : base(Sender, Receivers, Patient, Title)
        {
            contents = Contents;
        }

        public string Contents
        {
            get
            {
                return contents;
            }
        }
    }
}

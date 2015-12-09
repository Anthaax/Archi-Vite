using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
    public class Prescription : Documents
    {
        string docPath;

        public Prescription(string Title, string DocPath, Professional Sender, List<Professional> Receivers, Patient Patient)
            : base(Sender, Receivers, Patient, Title)
        {
            this.DocPath = DocPath;
        }

        public string DocPath
        {
            get
            {
                return docPath;
            }

            set
            {
                docPath = value;
            }
        }
    }
}
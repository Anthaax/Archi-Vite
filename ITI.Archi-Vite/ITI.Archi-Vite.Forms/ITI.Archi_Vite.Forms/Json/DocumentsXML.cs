using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
    public abstract class DocumentsXML
    {
        public List<ProfessionalXML> Recievers { get; set; }
        public PatientXML Patient { get; set; }
        public UserXML Sender { get; set; }
        public DateTime Data { get; set; }
        public string Title { get; set; }
        public string SenderFullName { get; set; }
        public string PatientFullName { get; set; }
    }
}

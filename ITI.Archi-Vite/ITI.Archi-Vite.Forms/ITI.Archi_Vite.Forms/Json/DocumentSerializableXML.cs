using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
    public class DocumentSerializableXML
    {
        public List<MessageXML> Message { get; set; }
        public List<PrescriptionXML> Prescription { get; set; }
    }
}

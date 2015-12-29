using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
    public class DocumentSerializableJson
    {
        public List<MessageJson> Message { get; set; }
        public List<PrescriptionJson> Prescription { get; set; }
    }
}

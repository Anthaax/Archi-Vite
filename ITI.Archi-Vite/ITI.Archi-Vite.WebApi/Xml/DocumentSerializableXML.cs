using System;
using System.Collections.Generic;

namespace ITI.Archi_Vite.WebApi
{
    [Serializable]
    public class DocumentSerializableXml
    {
        public List<MessageXml> Message { get; set; }
        public List<PrescriptionXml> Prescription { get; set; }
    }
}

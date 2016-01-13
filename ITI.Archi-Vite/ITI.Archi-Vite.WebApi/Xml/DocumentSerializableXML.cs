using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ITI.Archi_Vite.WebApi
{
    [DataContract]
    [Serializable]
    public class DocumentSerializableXML
    {
        [DataMember]
        public List<MessageXML> Message { get; set; }
        [DataMember]
        public List<PrescriptionXML> Prescription { get; set; }
    }
}

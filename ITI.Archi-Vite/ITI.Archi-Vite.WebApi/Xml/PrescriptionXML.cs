using System;
using System.Runtime.Serialization;

namespace ITI.Archi_Vite.WebApi
{
    [Serializable]
    [DataContract]
    public class PrescriptionXML : DocumentsXML
    {
        [DataMember]
        public string DocPath { get; set; }
        [DataMember]
        public byte[] Doc { get; set; }
    }
}
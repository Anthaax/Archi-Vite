using System;
using System.Runtime.Serialization;

namespace ITI.Archi_Vite.WebApi
{
    [Serializable]
    [DataContract]
    public class MessageXML : DocumentsXML
    {
        [DataMember]
        public string Contents { get; set; }
    }
}

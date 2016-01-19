using System;
using System.Collections.Generic;
using ITI.Archi_Vite.DataBase;
using System.Runtime.Serialization;

namespace ITI.Archi_Vite.WebApi
{
    [Serializable]
    [DataContract]

    public abstract class DocumentsXML
    {
        [DataMember]
        public List<ProfessionalXML> Recievers { get; set; }
        [DataMember]
        public PatientXML Patient { get; set; }
        [DataMember]
        public UserXML Sender { get; set; }
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public string Title { get; set; }
    }
}

using System.Collections.Generic;
using ITI.Archi_Vite.DataBase;
using System;
using System.Runtime.Serialization;

namespace ITI.Archi_Vite.WebApi
{
    [DataContract]
    [Serializable]
    public class DataXML
    {
        [DataMember]
        public List<PatientXML> Patients { get; set; }
        [DataMember]
        public List<ProfessionalXML[]> Professionals { get; set; }
        [DataMember]
        public UserXML User { get; set; }
        [DataMember]
        public DocumentSerializableXML Documents { get; set; }
    }
}


using System.Collections.Generic;
using ITI.Archi_Vite.DataBase;
using System;

namespace ITI.Archi_Vite.WebApi
{
    [Serializable]
    public class DataXml
    {
        public List<Patient> Patients { get; set; }
        public List<Professional[]> Professionals { get; set; }
        public User User { get; set; }
        public DocumentSerializableXml Documents { get; set; }
    }
}


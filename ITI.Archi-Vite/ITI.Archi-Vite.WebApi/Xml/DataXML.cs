﻿using System.Collections.Generic;
using ITI.Archi_Vite.DataBase;
using System;

namespace ITI.Archi_Vite.WebApi
{
    [Serializable]
    public class DataXML
    {
        public List<PatientXML> Patients { get; set; }
        public List<ProfessionalXML[]> Professionals { get; set; }
        public UserXML User { get; set; }
        public DocumentSerializableXML Documents { get; set; }
    }
}

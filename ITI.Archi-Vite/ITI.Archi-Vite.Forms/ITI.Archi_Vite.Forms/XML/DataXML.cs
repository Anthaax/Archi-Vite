﻿using System.Collections.Generic;

namespace ITI.Archi_Vite.Forms
{
    public class DataXML
    {

        public List<PatientXML> Patients { get; set; }
        public List<ProfessionalXML[]> Professionals { get; set; }
        public UserXML User { get; set; }
        public DocumentSerializableXML Documents { get; set; }
        public DocumentSerializableXML DocumentAdded { get; set; }
        public bool NeedUpdate { get; set; }
    }
}


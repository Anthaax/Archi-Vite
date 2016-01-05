using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ITI.Archi_Vite.Forms
{
    public class DataJson
    {

        public Dictionary<PatientJson, ProfessionalJson[]> Follow { get; set; }
        public UserJson User { get; set; }
        public DocumentSerializableJson Documents { get; set; }
        public DocumentSerializableJson DocumentAdded { get; set; }
    }
}


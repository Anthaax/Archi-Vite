using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime.Serialization;

namespace ITI.Archi_Vite.WebApi
{
    [Serializable]
    [DataContract]
    public class PatientXML : UserXML
    {
        [DataMember]
        public int PatientId { get; set; }

    }
}

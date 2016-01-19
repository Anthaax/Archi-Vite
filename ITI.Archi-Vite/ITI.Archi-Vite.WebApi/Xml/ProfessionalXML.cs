using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.WebApi
{
    [Serializable]
    [DataContract]
	public class ProfessionalXML : UserXML
    {
        [DataMember]
        public int ProfessionalId { get; set; }
        [DataMember]
        public string Role { get; set; }
    }
}

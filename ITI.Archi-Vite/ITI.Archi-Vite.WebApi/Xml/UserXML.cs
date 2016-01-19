using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.WebApi
{
    [DataContract]
    [Serializable]
    public class UserXML
    {
        [DataMember]
        public int UserId { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public DateTime Birthdate { get; set; }
        [DataMember]
        public string Adress { get; set; }
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public int Postcode { get; set; }
        [DataMember]
        public string Pseudo { get; set; }
        [DataMember]
        public string Password { get; set; }
        [DataMember]
        public int PhoneNumber { get; set; }
        [DataMember]
        public string PhotoPath { get; set; }
        [DataMember]
        public byte[] Photo { get; set; }

    }
}

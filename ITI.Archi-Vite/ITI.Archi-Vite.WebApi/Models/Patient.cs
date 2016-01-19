using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace ITI.Archi_Vite.WebApi
{
    [Serializable]
    [DataContract]
    public class Patient
    {
        public Patient()
        {
            Followers = new List<Follower>();
        }

        [DataMember]
        public int PatientId { get; set; }

        [DataMember]
        [ForeignKey("PatientId")]
        public User User { get; set; }
        [NotMapped]
        [XmlIgnore]
        public ICollection<Follower> Followers { get; set; }
    }
}

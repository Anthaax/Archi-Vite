using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ITI.Archi_Vite.DataBase
{
    [Serializable]

    [DataContract]
    public class Professional
    {
        public Professional()
        {
            Followers = new List<Follower>();
        }
        [DataMember]
        public int ProfessionalId { get; set; }
        [Required]
        [DataMember]
        public string Role { get; set; }
        [ForeignKey("ProfessionalId")]
        [DataMember]
        public User User { get; set; }
        [NotMapped]
        [XmlIgnore]
        public ICollection<Follower> Followers { get; set; }
    }
}

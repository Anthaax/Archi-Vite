using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.DataBase
{
    [Serializable]

    [DataContract]
    public class Follower
    {
        [Key]
        [Column(Order = 1)]
        [DataMember]
        public int ProfessionnalId { get; set; }
        [ForeignKey("ProfessionnalId")]
        [DataMember]
        public Professional Professionnal { get; set; }
        [Key]
        [Column(Order = 0)]
        [DataMember]
        public int PatientId { get; set; }
        [ForeignKey("PatientId")]
        [DataMember]
        public Patient Patient { get; set; }
    }
}

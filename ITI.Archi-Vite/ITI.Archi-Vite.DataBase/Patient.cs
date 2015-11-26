using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ITI.Archi_Vite.DataBase
{
    [Serializable]
    public class Patient
    {
        public Patient()
        {
            Followers = new List<Follower>();
        }
        public int PatientId { get; set; }
        
        [ForeignKey("PatientId")]
        public User User { get; set; }
        [NotMapped]
        public ICollection<Follower> Followers { get; set; }
    }
}

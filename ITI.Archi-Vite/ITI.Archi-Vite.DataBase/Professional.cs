using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.DataBase
{
    [Serializable]
    public class Professional
    {
        public Professional()
        {
            Patients = new List<Patient>();
            Followers = new List<Follower>();
        }
        public int ProfessionalId { get; set; }
        [Required]
        public string Role { get; set; }
        [ForeignKey("ProfessionalId")]
        public User User { get; set; }
        public ICollection<Patient> Patients { get; set; }
        public ICollection<Follower> Followers { get; set; }
    }
}

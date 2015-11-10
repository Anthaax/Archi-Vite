using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.DataBase
{
    public class Patient
    {
        public Patient()
        {
            Followers = new List<Follower>();
        }
        public int PatientId { get; set; }
        [Required]
        public string PathFiles { get; set; }
        [Required]
        public Professional Referent { get; set; }
        
        public User User { get; set; }
        public ICollection<Follower> Followers { get; set; }
    }
}

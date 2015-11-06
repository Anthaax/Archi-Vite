using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.DataBase
{
    public class PatientFile
    {
        public PatientFile()
        {
            Follower = new List<Follower>();
        }
        public int PatientFileId { get; set; }
        [Required]
        public string PathFiles { get; set; }
        public int Referent { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public ICollection<Follower> Follower { get; set; }
    }
}

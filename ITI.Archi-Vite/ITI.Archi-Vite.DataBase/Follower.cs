using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.DataBase
{
    public class Follower
    {
        public int FollowerId { get; set; }
        [Required]
        public string FilePath { get; set; }
        [Required]
        public Professional Professionnal { get; set; }
        public Patient Patient { get; set; }
    }
}

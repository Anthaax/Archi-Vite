using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.DataBase
{
    public class Follower
    {
        public int FollowerId { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public PatientFile PatientFile { get; set; }
    }
}

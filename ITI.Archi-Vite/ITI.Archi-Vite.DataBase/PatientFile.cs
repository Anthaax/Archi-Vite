using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.DataBase
{
    public class PatientFile
    {
        public PatientFile()
        {

        }
        public int PatientFileId { get; set; }
        public string PathFiles { get; set; }
        public int Referent { get; set; }
        public User User { get; set; }
        public ICollection<User> Follower { get; set; }
    }
}

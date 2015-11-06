using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.DataBase
{
    public class ArchiViteContexts : DbContext
    {
        public ArchiViteContexts()
            :base("ArchiVite")
        {

        }
        public DbSet<User> User { get; set; }
        public DbSet<PatientFile> PatientFile { get; set; }
        public DbSet<Follower> Follower { get; set; }
    }
}

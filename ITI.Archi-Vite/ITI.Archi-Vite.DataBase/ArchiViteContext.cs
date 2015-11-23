using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.DataBase
{
    public class ArchiViteContext : DbContext
    {
        readonly AddRequest _ar;
        readonly UpdateRequest _up;
        readonly SuppressionRequest _sr;
        public ArchiViteContext()
            :base("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=ArchiVite;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        {
            _ar = new AddRequest(this);
            _up = new UpdateRequest(this);
            _sr = new SuppressionRequest(this);
        }
        public DbSet<User> User { get; set; }
        public DbSet<Patient> Patient { get; set; }
        public DbSet<Follower> Follower { get; set; }
        public DbSet<Professional> Professional { get; set; }

        public AddRequest Ar
        {
            get
            {
                return _ar;
            }
        }

        public UpdateRequest Up
        {
            get
            {
                return _up;
            }
        }

        public SuppressionRequest Sr
        {
            get
            {
                return _sr;
            }
        }
    }
}

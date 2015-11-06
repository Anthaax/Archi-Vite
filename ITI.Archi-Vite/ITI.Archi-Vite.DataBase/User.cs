using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.DataBase
{
    public class User
    {
        public User()
        {
            PatientFile = new List<PatientFile>();
            Follower = new List<Follower>();
        }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        [Column(TypeName = "ntext")]
        public string Adress { get; set; }
        public string City { get; set; }
        public int Postcode { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        [Column(TypeName = "ntext")]
        public string Photo { get; set; }
        public ICollection<PatientFile> PatientFile { get; set; }
        public ICollection<Follower> Follower { get; set; }
    }
}

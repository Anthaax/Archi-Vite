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
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        [Column(TypeName = "ntext")]
        [Required]
        public string Adress { get; set; }
        [Required]
        public string City { get; set; }
        public int Postcode { get; set; }
        public int PhoneNumber { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Role { get; set; }
        [Column(TypeName = "ntext")]
        [Required]
        public string Photo { get; set; }
        public ICollection<PatientFile> PatientFile { get; set; }
        public ICollection<Follower> Follower { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.WebApi
{
    [Serializable]

    [DataContract]
    public class User
    {
        [DataMember]
        public int UserId { get; set; }
        [Required]
        [DataMember]
        public string FirstName { get; set; }
        [Required]
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public DateTime Birthdate { get; set; }
        [Column(TypeName = "ntext")]
        [Required]
        [DataMember]
        public string Adress { get; set; }
        [Required]
        [DataMember]
        public string City { get; set; }
        [DataMember]
        public int Postcode { get; set; }
        [DataMember]
        public int PhoneNumber { get; set; }
        [Required]
        [DataMember]
        public string Pseudo { get; set; }
        [Required]
        [DataMember]
        public string Password { get; set; }
        [Column(TypeName = "ntext")]
        [Required]
        [DataMember]
        public string Photo { get; set; }
    }
}

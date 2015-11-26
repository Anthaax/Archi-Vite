﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.DataBase
{
    [Serializable]
    public class User
    {
        public User()
        {
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
        public string Pseudo { get; set; }
        [Required]
        public string Password { get; set; }
        [Column(TypeName = "ntext")]
        [Required]
        public string Photo { get; set; }
    }
}

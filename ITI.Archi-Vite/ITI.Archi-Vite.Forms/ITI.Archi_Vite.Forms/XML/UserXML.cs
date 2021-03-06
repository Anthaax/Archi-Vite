﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
    public class UserXML
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthdate { get; set; }
        public string Adress { get; set; }
        public string City { get; set; }
        public int Postcode { get; set; }
        public string Pseudo { get; set; }
        public string Password { get; set; }
        public int PhoneNumber { get; set; }
        public string PhotoPath { get; set; }
        public byte[] Photo { get; set; }

    }
}

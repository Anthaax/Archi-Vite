﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.WebApi
{
    [Serializable]
	public class ProfessionalXML : UserXML
    {
        public int ProfessionalId { get; set; }
        public string Role { get; set; }
    }
}

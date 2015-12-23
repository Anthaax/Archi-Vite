using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
	public class ProfessionalJson : UserJson
    {
        public ProfessionalJson()
        {

        }
        public ProfessionalJson(JsonToken jsonToken)
        {

        }
        public int ProfessionalId { get; set; }
        public string Role { get; set; }
    }
}

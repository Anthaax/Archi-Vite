using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ITI.Archi_Vite.Forms
{
    public class PatientJson : UserJson
    {
        public PatientJson()
        {

        }
        public PatientJson(JsonToken jsonToken)
        {

        }
        public int PatientId { get; set; }

        public override string ToString()
        {
            JToken j = JsonConvert.SerializeObject(this);
            PatientJson pa = this;
            JToken j1 = JToken.FromObject(new PatientJson
                        { PatientId = pa.PatientId, Adress = pa.Adress, Birthdate = pa.Birthdate, City = pa.City, FirstName = pa.FirstName, LastName = pa.LastName, Password = pa.Password, PhoneNumber = pa.PhoneNumber, Photo = pa.Photo, Postcode = pa.Postcode, Pseudo = pa.Pseudo, UserId = pa.UserId});
			string str = JsonConvert.SerializeObject(j1);
			var js = JToken.Parse(str).ToString();
			return js;
        }
    }
}

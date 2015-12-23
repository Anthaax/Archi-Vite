using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
    public abstract class DocumentsJson
    {
        public DocumentsJson()
        {

        }
        public DocumentsJson(JsonToken jsonToken)
        {

        }
        public List<ProfessionalJson> Recievers { get; set; }
        public PatientJson Patient { get; set; }
        public ProfessionalJson Sender { get; set; }
        public DateTime Data { get; set; }
        public string Title { get; set; }
        public string SenderFullName { get; set; }
        public string PatientFullName { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
    public class DataXml
    {
        public List<PatientJson> Patients { get; set; }
        public List<ProfessionalJson[]> Professionals { get; set; }
        public UserJson User { get; set; }
        public DocumentSerializableJson Documents { get; set; }
    }
}

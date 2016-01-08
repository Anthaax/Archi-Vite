using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
    public class FromXML
    {
        public DataJson FromXml(DataXml data)
        {
            DataJson d = new DataJson()
            {
                DocumentAdded = new DocumentSerializableJson(),
                Documents = data.Documents,
                Follow = CreateDictionary(data.Patients, data.Professionals),
                User = data.User
            };
            return d;
        }
        public DataXml FromXml(DataJson data)
        {
            DataXml d = new DataXml()
            {
                Documents = data.Documents,
                Patients = data.Follow.Keys.ToList(),
                Professionals = data.Follow.Values.ToList(),
                User = data.User
            };
            return d;
        }

        private Dictionary<PatientJson, ProfessionalJson[]> CreateDictionary(List<PatientJson> patients, List<ProfessionalJson[]> professionals)
        {
            Dictionary<PatientJson, ProfessionalJson[]> d = new Dictionary<PatientJson, ProfessionalJson[]>();
            for (int i = 0; i < patients.Count; i++)
            {
                d.Add(patients[i], professionals[i]);
            }
            return d;
        }

    }
}

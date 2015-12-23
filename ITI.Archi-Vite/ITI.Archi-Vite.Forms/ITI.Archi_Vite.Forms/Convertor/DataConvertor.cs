using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
    public class DataConvertor
    {
        public DataJson DataToDataJson(Data json)
        {
            UserJson curentUser = CreateUser(json.User);
            Dictionary<PatientJson, ProfessionalJson[]> follow = CreateDictionary(json.Follow);
            DocumentSerializableJson documents = CreateDocumentSerializable(json.Documents);
            DataJson d = new DataJson();
            d.Documents = documents;
            d.Follow = follow;
            d.User = curentUser;
            return d;
        }

        private UserJson CreateUser(User user)
        {
            UserJson u = new UserJson();
            u.Adress = user.Adress;
            u.Birthdate = user.Birthdate;
            u.City = user.City;
            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u.Password = user.Password;
            u.PhoneNumber = user.PhoneNumber;
            u.Photo = user.Photo;
            u.Postcode = user.Postcode;
            u.Pseudo = user.Pseudo;
            u.UserId = user.UserId;
            return u;
        }
        private UserJson CreateUser(int userId, string firstName, string lastName, DateTime birthdate, string adress, string city, int postcode, string pseudo, string password, int phoneNumber, string Photo)
        {
            UserJson u = new UserJson();
            u.Adress = adress;
            u.Birthdate = birthdate;
            u.City = city;
            u.FirstName = firstName;
            u.LastName = lastName;
            u.Password = password;
            u.PhoneNumber = phoneNumber;
            u.Photo = Photo;
            u.Postcode = postcode;
            u.Pseudo = pseudo;
            u.UserId = userId;
            return u;
        }
        private PatientJson CreatePatient(int userId, string firstName, string lastName, DateTime birthdate, string adress, string city, int postcode, string pseudo, string password, int phoneNumber, string Photo)
        {
            PatientJson p = new PatientJson();
            p.Adress = adress;
            p.Birthdate = birthdate;
            p.City = city;
            p.FirstName = firstName;
            p.LastName = lastName;
            p.Password = password;
            p.PhoneNumber = phoneNumber;
            p.Photo = Photo;
            p.Postcode = postcode;
            p.Pseudo = pseudo;
            p.PatientId = userId;
            p.UserId = userId;
            return p;
        }
        private ProfessionalJson CreatePro(int userId, string firstName, string lastName, DateTime birthdate, string adress, string city, int postcode, string pseudo, string password, int phoneNumber, string Photo, string role)
        {
            ProfessionalJson p = new ProfessionalJson();
            p.Adress = adress;
            p.Birthdate = birthdate;
            p.City = city;
            p.FirstName = firstName;
            p.LastName = lastName;
            p.Password = password;
            p.PhoneNumber = phoneNumber;
            p.Photo = Photo;
            p.Postcode = postcode;
            p.Pseudo = pseudo;
            p.UserId = userId;
            p.ProfessionalId = userId;
            p.Role = role;
            return p;
        }
        private Dictionary<PatientJson, ProfessionalJson[]> CreateDictionary(Dictionary<Patient, Professional[]> follow)
        {
            List<Patient> f = follow.Keys.ToList();
            List<Professional[]> p = follow.Values.ToList();
            List<PatientJson> fJson = CreatePatientList(f);
            List<ProfessionalJson[]> pJson = CreateArrayProList(p);
            return CreateDictionaryWithList(fJson, pJson);
        }
        private List<PatientJson> CreatePatientList(List<Patient> patientJson)
        {
            List<PatientJson> f = new List<PatientJson>();
            foreach (var pJson in patientJson)
            {
                PatientJson p = CreatePatient(pJson.UserId, pJson.FirstName, pJson.LastName, pJson.Birthdate, pJson.Adress, pJson.City, pJson.Postcode, pJson.Pseudo, pJson.Password, pJson.PhoneNumber, pJson.Photo);
                f.Add(p);
            }
            return f;
        }
        private List<ProfessionalJson[]> CreateArrayProList(List<Professional[]> proJson)
        {
            List<ProfessionalJson[]> f = new List<ProfessionalJson[]>();
            foreach (var pJson in proJson)
            {
                ProfessionalJson[] p = CreateProArray(pJson);
				f.Add (p);
            }
            return f;
        }
        private ProfessionalJson[] CreateProArray(Professional[] proJson)
        {
            ProfessionalJson[] p = new ProfessionalJson[10];
            for (int i = 0; i < proJson.Length; i++)
            {
                if (proJson[i] != null)
                {
                    ProfessionalJson pro = CreatePro(proJson[i].UserId, proJson[i].FirstName, proJson[i].LastName, proJson[i].Birthdate, proJson[i].Adress, proJson[i].City, proJson[i].Postcode, proJson[i].Pseudo, proJson[i].Password, proJson[i].PhoneNumber, proJson[i].Photo, proJson[i].Role);
                    p.SetValue(pro, i);
                }
                else
                {
                    p.SetValue(null, i);
                }
            }
            return p;
        }
        private Dictionary<PatientJson, ProfessionalJson[]> CreateDictionaryWithList(List<PatientJson> p, List<ProfessionalJson[]> pro)
        {
            Dictionary<PatientJson, ProfessionalJson[]> dico = new Dictionary<PatientJson, ProfessionalJson[]>();
            for (int i = 0; i < p.Count; i++)
            {
                dico.Add(p[i], pro[i]);
            }
            return dico;
        }
        private DocumentSerializableJson CreateDocumentSerializable(DocumentSerializable json)
        {
            List<MessageJson> m = CreateMessageList(json.Messages);
            List<PrescriptionJson> p = CreatePrescriptionList(json.Prescriptions);
            DocumentSerializableJson d = new DocumentSerializableJson();
            d.Message = m;
            d.Prescription = p;
            return d;
        }
        private List<MessageJson> CreateMessageList(List<Message> mJson)
        {
            List<MessageJson> m = new List<MessageJson>();
            foreach (var message in mJson)
            {
                m.Add(CreateMessage(message));
            }
            return m;
        }
        private List<PrescriptionJson> CreatePrescriptionList(List<Prescription> mJson)
        {
            List<PrescriptionJson> m = new List<PrescriptionJson>();
            foreach (var message in mJson)
            {
                m.Add(CreatePrescription(message));
            }
            return m;
        }

        private PrescriptionJson CreatePrescription(Prescription mJson)
        {
            ProfessionalJson p = CreatePro(mJson.Sender.UserId, mJson.Sender.FirstName, mJson.Sender.LastName, mJson.Sender.Birthdate, mJson.Sender.Adress, mJson.Sender.City, mJson.Sender.Postcode, mJson.Sender.Pseudo, mJson.Sender.Password, mJson.Sender.PhoneNumber, mJson.Sender.Photo, mJson.Sender.Role);
            PatientJson pa = CreatePatient(mJson.Patient.UserId, mJson.Patient.FirstName, mJson.Patient.LastName, mJson.Patient.Birthdate, mJson.Patient.Adress, mJson.Patient.City, mJson.Patient.Postcode, mJson.Patient.Pseudo, mJson.Patient.Password, mJson.Patient.PhoneNumber, mJson.Patient.Photo);
            List<ProfessionalJson> pro = CreateListPro(mJson.Recievers);
            PrescriptionJson m = new PrescriptionJson();
            m.DocPath = mJson.DocPath;
            m.Data = mJson.Date;
            m.Patient = pa;
            m.Recievers = pro;
            m.Sender = p;
            m.Title = mJson.Title;
            return m;
        }

        private MessageJson CreateMessage(Message mJson)
        {
            ProfessionalJson p = CreatePro(mJson.Sender.UserId, mJson.Sender.FirstName, mJson.Sender.LastName, mJson.Sender.Birthdate, mJson.Sender.Adress, mJson.Sender.City, mJson.Sender.Postcode, mJson.Sender.Pseudo, mJson.Sender.Password, mJson.Sender.PhoneNumber, mJson.Sender.Photo, mJson.Sender.Role);
            PatientJson pa = CreatePatient(mJson.Patient.UserId, mJson.Patient.FirstName, mJson.Patient.LastName, mJson.Patient.Birthdate, mJson.Patient.Adress, mJson.Patient.City, mJson.Patient.Postcode, mJson.Patient.Pseudo, mJson.Patient.Password, mJson.Patient.PhoneNumber, mJson.Patient.Photo);
            List<ProfessionalJson> pro = CreateListPro(mJson.Recievers);
            MessageJson m = new MessageJson();
            m.Contents = mJson.Contents;
            m.Data = mJson.Date;
            m.Patient = pa;
            m.Recievers = pro;
            m.Sender = p;
            m.Title = mJson.Title;
            return m;

        }
        private List<ProfessionalJson> CreateListPro(List<Professional> pJson)
        {
            List<ProfessionalJson> p = new List<ProfessionalJson>();
            foreach (var pro in pJson)
            {
                ProfessionalJson pr = CreatePro(pro.UserId, pro.FirstName, pro.LastName, pro.Birthdate, pro.Adress, pro.City, pro.Postcode, pro.Pseudo, pro.Password, pro.PhoneNumber, pro.Photo, pro.Role);
                p.Add(pr);
            }
            return p;
        }
    }
}

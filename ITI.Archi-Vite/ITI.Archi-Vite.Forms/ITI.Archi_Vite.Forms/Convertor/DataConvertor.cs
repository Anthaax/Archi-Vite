using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    public class DataConvertor
    {
        public DataXML DataToDataJson(Data userData)
        {
            DataXML d = new DataXML();
            d.Documents = CreateDocumentSerializable(userData.Documents); ;
            d.Patients = CreatePatientList(userData.Follow.Keys.ToList());
            d.Professionals = CreateArrayProList(userData.Follow.Values.ToList());
            d.User = CreateUser(userData.User);
            d.DocumentAdded = CreateDocumentSerializable(userData.DocumentsAdded);
            return d;
        }

        private UserXML CreateUser(User user)
        {
            UserXML u = new UserXML();
            u.Adress = user.Adress;
            u.Birthdate = user.Birthdate;
            u.City = user.City;
            u.FirstName = user.FirstName;
            u.LastName = user.LastName;
            u.Password = user.Password;
            u.PhoneNumber = user.PhoneNumber;
            u.PhotoPath = user.Photo;
            u.Photo = DependencyService.Get<IBytesSaveAndLoad>().LoadByteArray(user.Photo);
            u.Postcode = user.Postcode;
            u.Pseudo = user.Pseudo;
            u.UserId = user.UserId;
            return u;
        }
        private UserXML CreateUser(int userId, string firstName, string lastName, DateTime birthdate, string adress, string city, int postcode, string pseudo, string password, int phoneNumber, string Photo)
        {
            UserXML u = new UserXML();
            u.Adress = adress;
            u.Birthdate = birthdate;
            u.City = city;
            u.FirstName = firstName;
            u.LastName = lastName;
            u.Password = password;
            u.PhoneNumber = phoneNumber;
            u.PhotoPath = Photo;
            u.Photo = DependencyService.Get<IBytesSaveAndLoad>().LoadByteArray(Photo);
            u.Postcode = postcode;
            u.Pseudo = pseudo;
            u.UserId = userId;
            return u;
        }
        private PatientXML CreatePatient(int userId, string firstName, string lastName, DateTime birthdate, string adress, string city, int postcode, string pseudo, string password, int phoneNumber, string Photo)
        {
            PatientXML p = new PatientXML();
            p.Adress = adress;
            p.Birthdate = birthdate;
            p.City = city;
            p.FirstName = firstName;
            p.LastName = lastName;
            p.Password = password;
            p.PhoneNumber = phoneNumber;
            p.PhotoPath = Photo;
            p.Photo = DependencyService.Get<IBytesSaveAndLoad>().LoadByteArray(Photo);
            p.Postcode = postcode;
            p.Pseudo = pseudo;
            p.PatientId = userId;
            p.UserId = userId;
            return p;
        }
        private ProfessionalXML CreatePro(int userId, string firstName, string lastName, DateTime birthdate, string adress, string city, int postcode, string pseudo, string password, int phoneNumber, string Photo, string role)
        {
            ProfessionalXML p = new ProfessionalXML();
            p.Adress = adress;
            p.Birthdate = birthdate;
            p.City = city;
            p.FirstName = firstName;
            p.LastName = lastName;
            p.Password = password;
            p.PhoneNumber = phoneNumber;
            p.PhotoPath = Photo;
            p.Photo = DependencyService.Get<IBytesSaveAndLoad>().LoadByteArray(Photo);
            p.Postcode = postcode;
            p.Pseudo = pseudo;
            p.UserId = userId;
            p.ProfessionalId = userId;
            p.Role = role;
            return p;
        }
        private List<PatientXML> CreatePatientList(List<Patient> patientJson)
        {
            List<PatientXML> f = new List<PatientXML>();
            foreach (var pJson in patientJson)
            {
                PatientXML p = CreatePatient(pJson.UserId, pJson.FirstName, pJson.LastName, pJson.Birthdate, pJson.Adress, pJson.City, pJson.Postcode, pJson.Pseudo, pJson.Password, pJson.PhoneNumber, pJson.Photo);
                f.Add(p);
            }
            return f;
        }
        private List<ProfessionalXML[]> CreateArrayProList(List<Professional[]> proJson)
        {
            List<ProfessionalXML[]> f = new List<ProfessionalXML[]>();
            foreach (var pJson in proJson)
            {
                ProfessionalXML[] p = CreateProArray(pJson);
				f.Add (p);
            }
            return f;
        }
        private ProfessionalXML[] CreateProArray(Professional[] proJson)
        {
            ProfessionalXML[] p = new ProfessionalXML[10];
            for (int i = 0; i < proJson.Length; i++)
            {
                if (proJson[i] != null)
                {
                    ProfessionalXML pro = CreatePro(proJson[i].UserId, proJson[i].FirstName, proJson[i].LastName, proJson[i].Birthdate, proJson[i].Adress, proJson[i].City, proJson[i].Postcode, proJson[i].Pseudo, proJson[i].Password, proJson[i].PhoneNumber, proJson[i].Photo, proJson[i].Role);
                    p.SetValue(pro, i);
                }
                else
                {
                    p.SetValue(null, i);
                }
            }
            return p;
        }
        private Dictionary<PatientXML, ProfessionalXML[]> CreateDictionaryWithList(List<PatientXML> p, List<ProfessionalXML[]> pro)
        {
            Dictionary<PatientXML, ProfessionalXML[]> dico = new Dictionary<PatientXML, ProfessionalXML[]>();
            for (int i = 0; i < p.Count; i++)
            {
                dico.Add(p[i], pro[i]);
            }
            return dico;
        }
        private DocumentSerializableXML CreateDocumentSerializable(DocumentSerializable json)
        {
            List<MessageXML> m = CreateMessageList(json.Messages);
            List<PrescriptionXML> p = CreatePrescriptionList(json.Prescriptions);
            DocumentSerializableXML d = new DocumentSerializableXML();
            d.Message = m;
            d.Prescription = p;
            return d;
        }
        private List<MessageXML> CreateMessageList(List<Message> mJson)
        {
            List<MessageXML> m = new List<MessageXML>();
            foreach (var message in mJson)
            {
                m.Add(CreateMessage(message));
            }
            return m;
        }
        private List<PrescriptionXML> CreatePrescriptionList(List<Prescription> mJson)
        {
            List<PrescriptionXML> m = new List<PrescriptionXML>();
            foreach (var message in mJson)
            {
                m.Add(CreatePrescription(message));
            }
            return m;
        }

        private PrescriptionXML CreatePrescription(Prescription mJson)
        {
            UserXML p = CreateUser(mJson.Sender.UserId, mJson.Sender.FirstName, mJson.Sender.LastName, mJson.Sender.Birthdate, mJson.Sender.Adress, mJson.Sender.City, mJson.Sender.Postcode, mJson.Sender.Pseudo, mJson.Sender.Password, mJson.Sender.PhoneNumber, mJson.Sender.Photo);
            PatientXML pa = CreatePatient(mJson.Patient.UserId, mJson.Patient.FirstName, mJson.Patient.LastName, mJson.Patient.Birthdate, mJson.Patient.Adress, mJson.Patient.City, mJson.Patient.Postcode, mJson.Patient.Pseudo, mJson.Patient.Password, mJson.Patient.PhoneNumber, mJson.Patient.Photo);
            List<ProfessionalXML> pro = CreateListPro(mJson.Recievers);
            PrescriptionXML m = new PrescriptionXML();
            m.DocPath = mJson.DocPath;
            m.Doc = DependencyService.Get<IBytesSaveAndLoad>().LoadByteArray(mJson.DocPath);
            m.Data = mJson.Date;
            m.Patient = pa;
            m.Recievers = pro;
            m.Sender = p;
            m.Title = mJson.Title;
            return m;
        }

        private MessageXML CreateMessage(Message mJson)
        {
            UserXML p = CreateUser(mJson.Sender.UserId, mJson.Sender.FirstName, mJson.Sender.LastName, mJson.Sender.Birthdate, mJson.Sender.Adress, mJson.Sender.City, mJson.Sender.Postcode, mJson.Sender.Pseudo, mJson.Sender.Password, mJson.Sender.PhoneNumber, mJson.Sender.Photo);
            PatientXML pa = CreatePatient(mJson.Patient.UserId, mJson.Patient.FirstName, mJson.Patient.LastName, mJson.Patient.Birthdate, mJson.Patient.Adress, mJson.Patient.City, mJson.Patient.Postcode, mJson.Patient.Pseudo, mJson.Patient.Password, mJson.Patient.PhoneNumber, mJson.Patient.Photo);
            List<ProfessionalXML> pro = CreateListPro(mJson.Recievers);
            MessageXML m = new MessageXML();
            m.Contents = mJson.Contents;
            m.Data = mJson.Date;
            m.Patient = pa;
            m.Recievers = pro;
            m.Sender = p;
            m.Title = mJson.Title;
            return m;

        }
        private List<ProfessionalXML> CreateListPro(List<Professional> pJson)
        {
            List<ProfessionalXML> p = new List<ProfessionalXML>();
            foreach (var pro in pJson)
            {
                ProfessionalXML pr = CreatePro(pro.UserId, pro.FirstName, pro.LastName, pro.Birthdate, pro.Adress, pro.City, pro.Postcode, pro.Pseudo, pro.Password, pro.PhoneNumber, pro.Photo, pro.Role);
                p.Add(pr);
            }
            return p;
        }
        private void SaveBytesArray(byte[] bytesArray)
        {

        }
    }
}

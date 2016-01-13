using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    public class DataJsonConvertor
    {
        public Data DataJsonToData(DataXML userDataXML)
        {
            User curentUser = CreateUser(userDataXML.User);
            Dictionary<Patient, Professional[]> follow = CreateDictionary(userDataXML.Patients, userDataXML.Professionals);
            DocumentSerializable documents = CreateDocumentSerializable(userDataXML.Documents);
			DocumentSerializable documentsAdded = new DocumentSerializable (new List<Message> (), new List<Prescription> ());
            Data d = new Data(curentUser, follow, documents, documentsAdded);
            return d;
        }

        private User CreateUser(UserXML user)
        {
            User u = new User(user.UserId, user.FirstName, user.LastName, user.Birthdate, user.Adress, user.City, user.Postcode, user.Pseudo, user.Password, user.PhoneNumber, user.PhotoPath);
            DependencyService.Get<IBytesSaveAndLoad>().SaveByteArray(user.Photo, user.PhotoPath);
            return u;
        }
        private User CreateUser(int userId, string firstName, string lastName, DateTime birthdate, string adress, string city, int postcode, string pseudo, string password, int phoneNumber, string PhotoPath , byte[] Photo)
        {
            User u = new User(userId, firstName, lastName, birthdate, adress, city, postcode, pseudo, password, phoneNumber, PhotoPath);
            DependencyService.Get<IBytesSaveAndLoad>().SaveByteArray(Photo, PhotoPath);
            return u;
        }
        private Patient CreatePatient(int userId, string firstName, string lastName, DateTime birthdate, string adress, string city, int postcode, string pseudo, string password, int phoneNumber, string PhotoPath, byte[] Photo)
        {
            Patient p = new Patient(CreateUser(userId, firstName, lastName, birthdate, adress, city, postcode, pseudo, password, phoneNumber, PhotoPath ,Photo));
            return p;
        }
        private Professional CreatePro(int userId, string firstName, string lastName, DateTime birthdate, string adress, string city, int postcode, string pseudo, string password, int phoneNumber, string PhotoPath, byte[] Photo, string role)
        {
            Professional p = new Professional(CreateUser(userId, firstName, lastName, birthdate, adress, city, postcode, pseudo, password, phoneNumber, PhotoPath,Photo), role);
            return p;
        }
        private Dictionary<Patient, Professional[]> CreateDictionary(List<PatientXML> patientXML, List<ProfessionalXML[]> proXML)
        {
            List<Patient> f = CreatePatientList(patientXML);
            List<Professional[]> p = CreateArrayProList(proXML);
            return CreateDictionaryWithList(f, p);
        }
        private List<Patient> CreatePatientList(List<PatientXML> patientJson)
        {
            List<Patient> f = new List<Patient>();
            foreach (var pJson in patientJson)
            {
                Patient p = CreatePatient(pJson.UserId, pJson.FirstName, pJson.LastName, pJson.Birthdate, pJson.Adress, pJson.City, pJson.Postcode, pJson.Pseudo, pJson.Password, pJson.PhoneNumber, pJson.PhotoPath, pJson.Photo);
                f.Add(p);
            }
            return f;
        }
        private List<Professional[]> CreateArrayProList(List<ProfessionalXML[]> proJson)
        {
            List<Professional[]> f = new List<Professional[]>();
            foreach (var pJson in proJson)
            {
                Professional[] p = CreateProArray(pJson);
				f.Add (p);
            }
            return f;
        }
        private Professional[] CreateProArray(ProfessionalXML[] proJson)
        {
            Professional[] p = new Professional[10];
            for (int i = 0; i < proJson.Length; i++)
            {
                if (proJson[i] != null)
                {
                    Professional pro = CreatePro(proJson[i].UserId, proJson[i].FirstName, proJson[i].LastName, proJson[i].Birthdate, proJson[i].Adress, proJson[i].City, proJson[i].Postcode, proJson[i].Pseudo, proJson[i].Password, proJson[i].PhoneNumber, proJson[i].PhotoPath, proJson[i].Photo, proJson[i].Role);
                    p.SetValue(pro, i);
                }
                else
                {
                    p.SetValue(null, i);
                }
            }
            return p;
        }
        private Dictionary<Patient, Professional[]> CreateDictionaryWithList(List<Patient> p, List<Professional[]> pro)
        {
            Dictionary<Patient, Professional[]> dico = new Dictionary<Patient, Professional[]>();
            for (int i = 0; i < p.Count; i++)
            {
                dico.Add(p.ToArray()[i], pro.ToArray()[i]);
            }
            return dico;
        }
        private DocumentSerializable CreateDocumentSerializable(DocumentSerializableXML json)
        {
            List<Message> m = CreateMessageList(json.Message);
            List<Prescription> p = CreatePrescriptionList(json.Prescription);
            DocumentSerializable d = new DocumentSerializable(m, p);
            return d;
        }
        private List<Message> CreateMessageList(List<MessageXML> mJson)
        {
            List<Message> m = new List<Message>();
            foreach (var message in mJson)
            {
                m.Add(CreateMessage(message));
            }
            return m;
        }
        private List<Prescription> CreatePrescriptionList(List<PrescriptionXML> mJson)
        {
            List<Prescription> m = new List<Prescription>();
            foreach (var message in mJson)
            {
                m.Add(CreatePrescription(message));
            }
            return m;
        }

        private Prescription CreatePrescription(PrescriptionXML mJson)
        {
            User p = CreateUser(mJson.Sender.UserId, mJson.Sender.FirstName, mJson.Sender.LastName, mJson.Sender.Birthdate, mJson.Sender.Adress, mJson.Sender.City, mJson.Sender.Postcode, mJson.Sender.Pseudo, mJson.Sender.Password, mJson.Sender.PhoneNumber, mJson.Sender.PhotoPath, mJson.Sender.Photo);
            Patient pa = CreatePatient(mJson.Patient.UserId, mJson.Patient.FirstName, mJson.Patient.LastName, mJson.Patient.Birthdate, mJson.Patient.Adress, mJson.Patient.City, mJson.Patient.Postcode, mJson.Patient.Pseudo, mJson.Patient.Password, mJson.Patient.PhoneNumber, mJson.Patient.PhotoPath, mJson.Patient.Photo);
            List<Professional> pro = CreateListPro(mJson.Recievers);
            DependencyService.Get<IBytesSaveAndLoad>().SaveByteArray(mJson.Doc, mJson.DocPath);
            Prescription m = new Prescription(mJson.Title, mJson.DocPath, p, pro, pa);
            return m;
        }

        private Message CreateMessage(MessageXML mJson)
        {
            User p = CreateUser(mJson.Sender.UserId, mJson.Sender.FirstName, mJson.Sender.LastName, mJson.Sender.Birthdate, mJson.Sender.Adress, mJson.Sender.City, mJson.Sender.Postcode, mJson.Sender.Pseudo, mJson.Sender.Password, mJson.Sender.PhoneNumber, mJson.Sender.PhotoPath, mJson.Sender.Photo);
            Patient pa = CreatePatient(mJson.Patient.UserId, mJson.Patient.FirstName, mJson.Patient.LastName, mJson.Patient.Birthdate, mJson.Patient.Adress, mJson.Patient.City, mJson.Patient.Postcode, mJson.Patient.Pseudo, mJson.Patient.Password, mJson.Patient.PhoneNumber, mJson.Patient.PhotoPath, mJson.Patient.Photo);
            List<Professional> pro = CreateListPro(mJson.Recievers);
            Message m = new Message(mJson.Title, mJson.Contents, p, pro, pa);
            return m;

        }
        private List<Professional> CreateListPro(List<ProfessionalXML> pJson)
        {
            List<Professional> p = new List<Professional>();
            foreach (var pro in pJson)
            {
                Professional pr = CreatePro(pro.UserId, pro.FirstName, pro.LastName, pro.Birthdate, pro.Adress, pro.City, pro.Postcode, pro.Pseudo, pro.Password, pro.PhoneNumber, pro.PhotoPath, pro.Photo, pro.Role);
                p.Add(pr);
            }
            return p;
        }
    }
}

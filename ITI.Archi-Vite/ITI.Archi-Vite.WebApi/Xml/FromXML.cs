using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITI.Archi_Vite.DataBase;

namespace ITI.Archi_Vite.WebApi
{
    public class FromXml
    {
        ImageManager _img = new ImageManager();
        public Data FromXML(DataXML dataXML)
        {
            Data d = new Data(CreateDocument(dataXML.Documents), CreateDictionary(CreatePatientList(dataXML.Patients), CreateProList(dataXML.Professionals)), CreateUser(dataXML.User));
            return d;
        }

        public User CreateUser(UserXML user)
        {
            User u = new User()
            {
                Adress = user.Adress,
                Birthdate = user.Birthdate,
                City = user.City,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                Photo = user.PhotoPath,
                Postcode = user.Postcode,
                Pseudo = user.Pseudo,
                UserId = user.UserId
            };
            _img.SaveImage(_img.BytesArrayConverter(user.Photo), user.PhotoPath);
            return u;
        }

        private User CreateUser(int userId, string firstName, string lastName, DateTime birthdate, string adress, string city, int postcode, string pseudo, string password, int phoneNumber, string PhotoPath, byte[] Photo)
        {
            User u = new User()
            {
                Adress = adress,
                Birthdate = birthdate,
                City = city,
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                PhoneNumber = phoneNumber,
                Photo = PhotoPath,
                Postcode = postcode,
                Pseudo = pseudo,
                UserId = userId
            };
            _img.SaveImage(_img.BytesArrayConverter(Photo), PhotoPath);

            return u;
        }

        private List<Professional[]> CreateProList(List<ProfessionalXML[]> professionals)
        {
            List<Professional[]> p = new List<Professional[]>();
            foreach (var pro in professionals)
            {
                p.Add(CreateProArray(pro));
            }
            return p;
        }

        private Professional[] CreateProArray(ProfessionalXML[] pro)
        {
            Professional[] p = new Professional[pro.Length];
            for (int i = 0; i < p.Length; i++)
            {
                if (pro[i] != null)
                {
                    p.SetValue(CreatePro(pro[i]), i);
                }
                else
                {
                    p.SetValue(null, i);
                }
            }
            return p;
        }

        private Professional CreatePro(ProfessionalXML professionalXML)
        {
            Professional p = new Professional()
            {
                ProfessionalId = professionalXML.ProfessionalId,
                Role = professionalXML.Role,
                User = CreateUser(professionalXML.UserId, professionalXML.FirstName, professionalXML.LastName, professionalXML.Birthdate, professionalXML.Adress, professionalXML.City, professionalXML.Postcode, professionalXML.Pseudo, professionalXML.Password, professionalXML.PhoneNumber, professionalXML.PhotoPath, professionalXML.Photo)
            };
            return p;
        }

        private List<Patient> CreatePatientList(List<PatientXML> patients)
        {
            List<Patient> p = new List<Patient>();
            foreach (var patient in patients)
            {
                p.Add(CreatePatient(patient));
            }
            return p;
        }

        private Patient CreatePatient(PatientXML patient)
        {
            Patient p = new Patient()
            {
                PatientId = patient.UserId,
                User = CreateUser(patient.UserId, patient.FirstName, patient.LastName, patient.Birthdate, patient.Adress, patient.City, patient.Postcode, patient.Pseudo, patient.Password, patient.PhoneNumber, patient.PhotoPath, patient.Photo)
            };
            return p;
        }

        private Dictionary<Patient, Professional[]> CreateDictionary(List<Patient> patients, List<Professional[]> professionals)
        {
            Dictionary<Patient, Professional[]> d = new Dictionary<Patient, Professional[]>();
            for (int i = 0; i < patients.Count; i++)
            {
                d.Add(patients[i], professionals[i]);
            }
            return d;
        }

        public DocumentSerializable CreateDocument(DocumentSerializableXML documents)
        {
            DocumentSerializable d = 
                new DocumentSerializable(CreateMessageList(documents.Message), CreatePrescriptionList(documents.Prescription));
            return d;
        }

        private List<Message> CreateMessageList(List<MessageXML> messages)
        {
            List<Message> m = new List<Message>();
            foreach (var message in messages)
            {
                m.Add(CreateMessage(message));
            }
            return m;
        }

        public Message CreateMessage(MessageXML message)
        {
            Message m = new Message(message.Title, message.Contents, CreateUser(message.Sender), CreateProList(message.Recievers), CreatePatient(message.Patient));
            return m;
        }

        private List<Professional> CreateProList(List<ProfessionalXML> recievers)
        {
            List<Professional> p = new List<Professional>();
            foreach (var r in recievers)
            {
                p.Add(CreatePro(r));
            }
            return p;
        }

        private List<Prescription> CreatePrescriptionList(List<PrescriptionXML> prescriptions)
        {
            List<Prescription> m = new List<Prescription>();
            foreach (var prescription in prescriptions)
            {
                m.Add(CreatePrescription(prescription));
            }
            return m;
        }

        public Prescription CreatePrescription(PrescriptionXML prescription)
        {
            Prescription p = new Prescription(prescription.Title, prescription.DocPath, CreateUser(prescription.Sender), CreateProList(prescription.Recievers), CreatePatient(prescription.Patient));
            _img.SaveImage(_img.BytesArrayConverter(prescription.Doc), prescription.DocPath);
            return p;
        }
    }
}
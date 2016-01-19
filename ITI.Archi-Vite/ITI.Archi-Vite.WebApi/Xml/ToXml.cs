using ITI.Archi_Vite.Core;
using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.Archi_Vite.WebApi
{
    public class ToXml
    {
        ImageManager _img = new ImageManager();
        public DataXML ToXML(Data data)
        {
            DataXML d = new DataXML();
            d.User = CreateUser(data.User);
            d.Patients = CreatePatientList(data.Followers.Keys.ToList());
            d.Professionals = CreateProList(data.Followers.Values.ToList());
            d.Documents = CreateDocuments(data.Documents);
            return d;
        }

        private UserXML CreateUser(User user)
        {

            UserXML u = new UserXML()
            {
                Adress = user.Adress,
                Birthdate = user.Birthdate,
                City = user.City,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Password = user.Password,
                PhoneNumber = user.PhoneNumber,
                PhotoPath = user.Photo,
                Photo = _img.ImageCoverter(_img.LoadImage(user.Photo)),
                Postcode = user.Postcode,
                Pseudo = user.Pseudo,
                UserId = user.UserId
            };
            return u;
        }

        private UserXML CreateUser(int userId, string firstName, string lastName, DateTime birthdate, string adress, string city, int postcode, string pseudo, string password, int phoneNumber, string Photo)
        {
            UserXML u = new UserXML()
            {
                Adress = adress,
                Birthdate = birthdate,
                City = city,
                FirstName = firstName,
                LastName = lastName,
                Password = password,
                PhoneNumber = phoneNumber,
                PhotoPath = Photo,
                Photo = _img.ImageCoverter(_img.LoadImage(Photo)),
                Postcode = postcode,
                Pseudo = pseudo,
                UserId = userId
            };
            return u;
        }

        private PatientXML CreatePatient(Patient patient)
        {
            PatientXML p = new PatientXML();
            p.Adress = patient.User.Adress;
            p.Birthdate = patient.User.Birthdate;
            p.City = patient.User.City;
            p.FirstName = patient.User.FirstName;
            p.LastName = patient.User.LastName;
            p.Password = patient.User.Password;
            p.PhoneNumber = patient.User.PhoneNumber;
            p.PhotoPath = patient.User.Photo;
            p.Photo = _img.ImageCoverter(_img.LoadImage(patient.User.Photo));
            p.Postcode = patient.User.Postcode;
            p.Pseudo = patient.User.Pseudo;
            p.PatientId = patient.User.UserId;
            p.UserId = patient.User.UserId;
            return p;
        }

        private ProfessionalXML CreatePro(Professional pro)
        {
            ProfessionalXML p = new ProfessionalXML();
            p.Adress = pro.User.Adress;
            p.Birthdate = pro.User.Birthdate;
            p.City = pro.User.City;
            p.FirstName = pro.User.FirstName;
            p.LastName = pro.User.LastName;
            p.Password = pro.User.Password;
            p.PhoneNumber = pro.User.PhoneNumber;
            p.PhotoPath = pro.User.Photo;
            p.Photo = _img.ImageCoverter(_img.LoadImage(pro.User.Photo));
            p.Postcode = pro.User.Postcode;
            p.Pseudo = pro.User.Pseudo;
            p.ProfessionalId = pro.User.UserId;
            p.UserId = pro.User.UserId;
            p.Role = pro.Role;
            return p;
        }

        private List<ProfessionalXML[]> CreateProList(List<Professional[]> proList)
        {
            List<ProfessionalXML[]> p = new List<ProfessionalXML[]>();
            foreach (var pro in proList)
            {
                p.Add(CreateProArray(pro));
            }
            return p;
        }

        private ProfessionalXML[] CreateProArray(Professional[] pro)
        {
            ProfessionalXML[] p = new ProfessionalXML[pro.Length];
            for (int i = 0; i < pro.Length; i++)
            {
                if (pro[i] == null)
                    p.SetValue(null, i);
                else
                    p.SetValue(CreatePro(pro[i]), i);
            }
            return p;
        }

        private List<PatientXML> CreatePatientList(List<Patient> patientList)
        {
            List<PatientXML> p = new List<PatientXML>();
            foreach (var patient in patientList)
            {
                p.Add(CreatePatient(patient));
            }
            return p;
        }

        public DocumentSerializableXML CreateDocuments(DocumentSerializable documents)
        {
            DocumentSerializableXML d = new DocumentSerializableXML();
            d.Message = CreateMessagesList(documents.Messages);
            d.Prescription = CreatePrescriptionList(documents.Prescriptions);
            return d;
        }

        public List<PrescriptionXML> CreatePrescriptionList(List<Prescription> prescriptions)
        {
            List<PrescriptionXML> p = new List<PrescriptionXML>();
            foreach (var pres in prescriptions)
            {
                p.Add(CreatePrescription(pres));
            }
            return p;
        }

        private PrescriptionXML CreatePrescription(Prescription pres)
        {
            PrescriptionXML p = new PrescriptionXML()
            {
                Date = pres.Date,
                Doc = _img.ImageCoverter(_img.LoadImage(pres.DocPath)),
                DocPath = pres.DocPath,
                Patient = CreatePatient(pres.Patient),
                Recievers = CreateProList(pres.Receivers),
                Sender = CreateUser(pres.Sender),
                Title = pres.Title
            };
            return p;
        }

        private List<MessageXML> CreateMessagesList(List<Message> messages)
        {
            List<MessageXML> m = new List<MessageXML>();
            foreach (var message in messages)
            {
                m.Add(CreateMessage(message));
            }
            return m;
        }

        private MessageXML CreateMessage(Message message)
        {
            MessageXML m = new MessageXML()
            {
                Contents = message.Contents,
                Date = message.Date,
                Patient = CreatePatient(message.Patient),
                Recievers = CreateProList(message.Receivers),
                Sender = CreateUser(message.Sender),
                Title = message.Title
            };
            return m;
        }

        private List<ProfessionalXML> CreateProList(List<Professional> receivers)
        {
            List<ProfessionalXML> p = new List<ProfessionalXML>();
            foreach (var r in receivers)
            {
                p.Add(CreatePro(r));
            }
            return p;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITI.Archi_Vite.Core;
using ITI.Archi_Vite.DataBase;

namespace ITI.Archi_Vite.WebApi
{
    public class FromXml
    {
        public Data FromXML(DataXML dataXML)
        {
            Data d = new Data(CreateDocument(dataXML.Documents), CreateDictionary(dataXML.Patients, dataXML.Professionals), dataXML.User);
            return d;
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

        private DocumentSerializable CreateDocument(DocumentSerializableXML documents)
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

        private Message CreateMessage(MessageXML message)
        {
            Message m = new Message(message.Title, message.Contents, message.Sender, message.Recievers, message.Patient);
            return m;
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

        private Prescription CreatePrescription(PrescriptionXML prescription)
        {
            Prescription p = new Prescription(prescription.Title, prescription.DocPath, prescription.Sender, prescription.Recievers, prescription.Patient);
            return p;
        }
    }
}
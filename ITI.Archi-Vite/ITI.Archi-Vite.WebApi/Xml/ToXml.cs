using ITI.Archi_Vite.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.Archi_Vite.WebApi
{
    public class ToXml
    {
        public DataXML ToXML(Data data)
        {
            DataXML d = new DataXML();
            d.User = data.User;
            d.Patients = data.Followers.Keys.ToList();
            d.Professionals = data.Followers.Values.ToList();
            d.Documents = CreateDocuments(data.Documents);
            return d;
        }

        private DocumentSerializableXML CreateDocuments(DocumentSerializable documents)
        {
            DocumentSerializableXML d = new DocumentSerializableXML();
            d.Message = CreateMessagesList(documents.Messages);
            d.Prescription = CreatePrescriptionList(documents.Prescriptions);
            return d;
        }

        private List<PrescriptionXML> CreatePrescriptionList(List<Prescription> prescriptions)
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
                DocPath = pres.DocPath,
                Patient = pres.Patient,
                Recievers = pres.Receivers,
                Sender = pres.Sender,
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
                Patient = message.Patient,
                Recievers = message.Receivers,
                Sender = message.Sender,
                Title = message.Title
            };
            return m;
        }
    }
}
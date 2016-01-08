using ITI.Archi_Vite.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.Archi_Vite.WebApi
{
    public class ToXml
    {
        public DataXml ToXML(Data data)
        {
            DataXml d = new DataXml();
            d.User = data.User;
            d.Patients = data.Followers.Keys.ToList();
            d.Professionals = data.Followers.Values.ToList();
            d.Documents = CreateDocuments(data.Documents);
            return d;
        }

        private DocumentSerializableXml CreateDocuments(DocumentSerializable documents)
        {
            DocumentSerializableXml d = new DocumentSerializableXml();
            d.Message = CreateMessagesList(documents.Messages);
            d.Prescription = CreatePrescriptionList(documents.Prescriptions);
            return d;
        }

        private List<PrescriptionXml> CreatePrescriptionList(List<Prescription> prescriptions)
        {
            List<PrescriptionXml> p = new List<PrescriptionXml>();
            foreach (var pres in prescriptions)
            {
                p.Add(CreatePrescription(pres));
            }
            return p;
        }

        private PrescriptionXml CreatePrescription(Prescription pres)
        {
            PrescriptionXml p = new PrescriptionXml()
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

        private List<MessageXml> CreateMessagesList(List<Message> messages)
        {
            List<MessageXml> m = new List<MessageXml>();
            foreach (var message in messages)
            {
                m.Add(CreateMessage(message));
            }
            return m;
        }

        private MessageXml CreateMessage(Message message)
        {
            MessageXml m = new MessageXml()
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
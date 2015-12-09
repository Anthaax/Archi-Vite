using ITI.Archi_Vite.Core;
using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class DocumentService
    {
        private ArchiViteContext _db = new ArchiViteContext();
        DocumentManager _doc;

        public DocumentSerializable SeeDocument(int patientId, int proId)
        {
            _doc = new DocumentManager(_db);
            DocumentSerializable doc = _doc.SeeDocument(proId, patientId);
            return doc;
        }

        public DocumentSerializable SeeDocument(int patientId)
        {
            _doc = new DocumentManager(_db);
            DocumentSerializable doc = _doc.SeeDocument(patientId);
            return doc;
        }

        public  void putDoc(MessageCreator newMessage)
        {
            _doc = new DocumentManager(_db);
            List<Professional> pro = new List<Professional>();

            foreach (var proId in newMessage.ReceiverId)
            {
                pro.Add(_db.SelectRequest.SelectProfessional(proId));
            }
            _doc.CreateMessage(pro, _db.SelectRequest.SelectProfessional(newMessage.SenderId), newMessage.Title, newMessage.Content, _db.SelectRequest.SelectPatient(newMessage.PatientId));
        }

        public void putDoc(PrescriptionCreator newPrescription)
        {
            _doc = new DocumentManager(_db);
            List<Professional> pro = new List<Professional>();
            foreach (var proId in newPrescription.Receivers)
            {
                pro.Add(_db.SelectRequest.SelectProfessional(proId));
            }
            _doc.CreatePrescription(pro, _db.SelectRequest.SelectProfessional(newPrescription.Sender), _db.SelectRequest.SelectPatient(newPrescription.Patient), newPrescription.Title, newPrescription.DocPath);
        }

        public void postDocument(ReciverModification newDoc)
        {
            _doc = new DocumentManager(_db);
            DocumentSerializable patientDoc = _doc.SeeDocument(newDoc.PatientId);
            _doc.AddReciver(newDoc.RecieverId, newDoc.PatientId, newDoc.Date);
        }

        public void deleteDocument(ReciverModification doc)
        {
            _doc = new DocumentManager(_db);
            DocumentSerializable patientDoc = _doc.SeeDocument(doc.PatientId);

            _doc.DeleteReciever(doc.RecieverId, doc.PatientId, doc.Date);
        }
    }
    
}
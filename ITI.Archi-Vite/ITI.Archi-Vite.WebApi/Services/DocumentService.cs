using ITI.Archi_Vite.Core;
using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class DocumentService
    {
        private ArchiViteContext _db = new ArchiViteContext();
        ImageManager _img = new ImageManager();
        DocumentManager _doc;

        public DocumentService()
        {
            _doc = new DocumentManager(_db);
        }
        public DocumentSerializable SeeDocument(int patientId, int proId)
        {
            DocumentSerializable doc = _doc.SeeDocument(proId, patientId);
            return doc;
        }

        public DocumentSerializable SeeDocument(int patientId)
        {
            DocumentSerializable doc = _doc.SeeDocument(patientId);
            return doc;
        }

        public void postMessage(int patientId, int receiverId, DateTime date)
        {
            _doc.AddReciver(receiverId, patientId, date);

        }

        public void putMessage(List<Professional> Receivers, User Sender, string Title, string Contents, Patient Patient)
        {
            _doc.CreateMessage(Receivers, Sender, Title, Contents, Patient);
        }

        public void deleteMessage(int reciverId, int patientid, DateTime date)
        {
            _doc.DeleteReciever(reciverId, patientid, date);
        }

        public void putPrescription(Prescription pres)
        {
            _doc.CreatePrescription(pres.Receivers, pres.Sender, pres.Patient, pres.Title, pres.DocPath);
        }

        public void postPrescripton(int reciverId, int patientid, DateTime date)
        {
            _doc.AddReciver(reciverId, patientid, date);
        }

        public void deletePrescription(Prescription prescription, string FilePath)
        {
            _doc.DeleteDoc(prescription, FilePath);
        }
    }
    
}
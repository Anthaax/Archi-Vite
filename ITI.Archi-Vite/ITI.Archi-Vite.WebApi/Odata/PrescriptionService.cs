using ITI.Archi_Vite.Core;
using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class PrescriptionService
    {
        private ArchiViteContext _db = new ArchiViteContext();
        DocumentManager _doc;
        
        public DocumentSerializable getPrescription(int patientId, int proId)
        {
            _doc = new DocumentManager(_db);
            DocumentSerializable doc = _doc.SeeDocument(proId, patientId);
            return doc;
        }

        public void putPrescription(List<Professional> Receivers, User Sender, Patient Patient, string Title, string DocPath)
        {
            _doc.CreatePrescription(Receivers, Sender, Patient, Title, DocPath);
        }

        public void postFollower(int reciverId, int patientid, DateTime date)
        {
            _doc.AddReciver(reciverId, patientid, date);
        }

        public void deleteFollower(Prescription prescription, string FilePath)
        {
            _doc.DeleteDoc(prescription, FilePath);
        }


    }
}
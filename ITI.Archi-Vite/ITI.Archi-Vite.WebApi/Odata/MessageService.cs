using ITI.Archi_Vite.Core;
using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class MessageService
    {
        private ArchiViteContext _db = new ArchiViteContext();
        private DocumentManager _doc;

        public DocumentSerializable getMessage(int idPro, int idPatient )
        {
            _doc = new DocumentManager(_db);
            Professional pro = _db.SelectRequest.SelectProfessional(idPro);
            Patient patient = _db.SelectRequest.SelectPatient(idPatient);
            DocumentSerializable doc = _doc.SeeDocument(pro, patient);
            return doc;
        }

        public void postMessage(int patientId, int receiverId, DateTime date)
        {
            _doc.AddReciver(receiverId, patientId, date);

        }

        public void putMessage(List<Professional> Receivers, User Sender, string Title, string Contents, Patient Patient)
        {
            _doc = new DocumentManager(_db);
            _doc.CreateMessage(Receivers, Sender, Title, Contents, Patient);
        }

        public void deleteMessage(int reciverId, int patientid, DateTime date)
        {
            _doc.DeleteReciever(reciverId, patientid, date);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ITI.Archi_Vite.DataBase;
using ITI.Archi_Vite.Core;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class UserService
    {
        private ArchiViteContext _db = new ArchiViteContext();

        public Data getUser(int id)
        {
            DocumentManager _doc = new DocumentManager(_db);
            DocumentSerializable doc;
            Dictionary<Patient,Professional[]> follower = _db.SelectRequest.SelectAllFollow(id);
            User user = _db.SelectRequest.SelectUser(id);
            Professional test = _db.SelectRequest.SelectProfessional(id);
            if (test == null)
            {
                 doc = _doc.SeeDocument(id);
            }
            else
            {

                doc = new DocumentSerializable(new List<Message>(), new List<Prescription>());

//                follower.Select(p => follower.Keys).

                foreach (var patient in follower)
                {

                    foreach (var message in _doc.SeeDocument(patient.Key.PatientId, id).Messages)
                    {
                        doc.Messages.Add(message);
                    }
                    foreach (var prescription in _doc.SeeDocument(patient.Key.PatientId, id).Prescriptions)
                    {
                        doc.Prescriptions.Add(prescription);
                    }
                }
            }

            Data swag = new Data(doc, follower, user);
            return swag;
        }

        public User getUser(string pseudo, string password)
        {
            User user = _db.SelectRequest.SelectUser(pseudo, password);
            return user;
        }

        public void PostUser(User user)
        {
            _db.UpdateRequest.CheckUserInfo(user);
        }
    }
}
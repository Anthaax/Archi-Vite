using ITI.Archi_Vite.Core;
using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class PatientService
    {
        private ArchiViteContext _db = new ArchiViteContext();
        private DocumentManager _doc;

        public Patient getPatient(int id)
        {
            Patient patient = _db.SelectRequest.SelectPatient(id);
            return patient;
        }

        public void putPatient(PatientCreation newPatient)
        {
            _doc = new DocumentManager(_db);
            _db.AddRequest.AddPatient(newPatient.User);
            _doc.CreateEmptyFile(_db.SelectRequest.SelectPatient(newPatient.User.Pseudo, newPatient.User.Password).PatientId.ToString());
        }

        public  Patient deletePatientCheck(int id)
        {
            Patient patient = _db.SelectRequest.SelectPatient(id);
            if (patient != null) deletePatient(id, patient);
            return patient;
        }

        private void deletePatient(int id, Patient patient)
        {
            List<Follower> follow = _db.SelectRequest.SelectFollowForPatient(id);
            foreach (var f in follow)
            {
                _doc.DeleteFollowerFile(f.Professionnal.ProfessionalId, patient.PatientId);
            }

            _doc.DeletePatientFile(patient.PatientId);
            _db.SuppressionRequest.PatientSuppression(patient);
        }


    }
}
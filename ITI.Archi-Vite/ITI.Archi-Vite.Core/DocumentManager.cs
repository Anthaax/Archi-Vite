using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace ITI.Archi_Vite.Core
{
    public class DocumentManager
    {
        ArchiViteContext _context;
        public DocumentManager(ArchiViteContext context)
        {
            _context = context;
        }
        public void CreateMessage(List<Professional> Receivers, Professional Sender, string Title, string Contents, Patient Patient)
        {
            Message m = new Message(Title, Contents, Sender, Receivers, Patient);
            CreateDoc(m);
        }
        public void CreatePrescription(List<Professional> Receivers, Professional Sender, Patient Patient, string Title, string DocPath)
        {
            Prescription p = new Prescription(Title, DocPath, Sender, Receivers, Patient);
            CreateDoc(p);
        }
        public void CreateEmptyFile(string FileName)
        {
            List<Message> m = new List<Message>();
            List<Prescription> p = new List<Prescription>();
            DocumentSerializable d = new DocumentSerializable(m, p);
            string path = GetPathFile(FileName);
            SerializeListDoc(d, path);
        }
        public DocumentSerializable SeeDocument(Professional pro, Patient patient)
        {
            DocumentSerializable Documents = DeserializeListDoc(GetPathFile(patient.PatientId + "$" + pro.ProfessionalId));
            return Documents;
        }
        public DocumentSerializable SeeDocument(string path)
        {
            DocumentSerializable Documents = DeserializeListDoc(GetPathFile(path));
            return Documents;
        }
        public void DeleteFollow(Professional pro, Patient patient)
        {
            var senderFollow = _context.Follower
                                    .Include(c => c.Patient)
                                    .Include(c => c.Professionnal)
                                    .Include(c => c.Professionnal.User)
                                    .Include(c => c.Patient.User)
                                    .Include(c => c.Patient.Referent)
                                    .Where(t => t.Patient.PatientId.Equals(patient.PatientId) && t.ProfessionnalId.Equals(pro.ProfessionalId))
                                    .FirstOrDefault();
            if (senderFollow != null)
            {
                DeleteFile(GetPathFile(patient.PatientId + "$" + pro.ProfessionalId));
                _context.Follower.Remove(senderFollow);
                _context.SaveChanges();
            }
        }
        private void DeleteFile(string path)
        {
            if (File.Exists(GetPathFile(path)))
            {
                try
                {
                    File.Delete(GetPathFile(path));
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }
        }
        private void CreateDoc(Message message)
        {
            var senderFollow = _context.Follower
                                    .Include(c => c.Patient)
                                    .Include(c => c.Professionnal)
                                    .Include(c => c.Professionnal.User)
                                    .Include(c => c.Patient.User)
                                    .Include(c => c.Patient.Referent)
                                    .Where(t => t.Patient.PatientId.Equals(message.Patient.PatientId) && t.ProfessionnalId.Equals(message.Sender.ProfessionalId))
                                    .FirstOrDefault();
            if (senderFollow != null)
            {
                AddDoc(message, message.Patient.PatientId + "$" + message.Sender.ProfessionalId);
            }
            foreach (var receiver in message.Receivers)
            {
                var follow = _context.Follower
                                    .Include(c => c.Patient)
                                    .Include(c => c.Professionnal)
                                    .Include(c => c.Professionnal.User)
                                    .Include(c => c.Patient.User)
                                    .Include(c => c.Patient.Referent)
                                    .Where(t => t.Patient.PatientId.Equals(message.Patient.PatientId) && t.ProfessionnalId.Equals(receiver.ProfessionalId))
                                    .FirstOrDefault();
                if (follow != null && follow != senderFollow)
                {
                    AddDoc(message, message.Patient.PatientId + "$" + receiver.ProfessionalId);
                }
            }
        }
        private void CreateDoc(Prescription prescription)
        {
            var senderFollow = _context.Follower
                                    .Include(c => c.Patient)
                                    .Include(c => c.Professionnal)
                                    .Include(c => c.Professionnal.User)
                                    .Include(c => c.Patient.User)
                                    .Include(c => c.Patient.Referent)
                                    .Where(t => t.Patient.PatientId.Equals(prescription.Patient.PatientId) && t.ProfessionnalId.Equals(prescription.Sender.ProfessionalId))
                                    .FirstOrDefault();
            if (senderFollow != null)
            {
                AddDoc(prescription, senderFollow.FilePath);
            }
            foreach (var receiver in prescription.Receivers)
            {
                var follow = _context.Follower
                                    .Include(c => c.Patient)
                                    .Include(c => c.Professionnal)
                                    .Include(c => c.Professionnal.User)
                                    .Include(c => c.Patient.User)
                                    .Include(c => c.Patient.Referent)
                                    .Where(t => t.Patient.PatientId.Equals(prescription.Patient.PatientId) && t.ProfessionnalId.Equals(receiver.ProfessionalId))
                                    .FirstOrDefault();
                if (follow != null && follow != senderFollow)
                {
                    AddDoc(prescription, follow.FilePath);
                }
            }
        }
        private void AddDoc(Message message, string FilePath)
        {
            DocumentSerializable Documents = SeeDocument(FilePath);
            Documents.Messages.Add(message);
            SerializeListDoc(Documents, GetPathFile(FilePath));
        }
        private void AddDoc(Prescription prescription, string FilePath)
        {
            DocumentSerializable Documents = DeserializeListDoc(GetPathFile(FilePath));
            Documents.Prescriptions.Add(prescription);
            SerializeListDoc(Documents, GetPathFile(FilePath));
        }

        private void SerializeListDoc(DocumentSerializable Documents, string FilePath)
        {
            Stream fileStream = new FileStream(FilePath, FileMode.Create, FileAccess.Write, FileShare.None);
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fileStream, Documents);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fileStream.Close();
            }
        }

        private DocumentSerializable DeserializeListDoc(string FilePath)
        {
            List<Message> m = new List<Message>();
            List<Prescription> p = new List<Prescription>();
            DocumentSerializable d = new DocumentSerializable(m, p);

            Stream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                DocumentSerializable newdoc = (DocumentSerializable)formatter.Deserialize(fileStream);
                foreach(var message in newdoc.Messages)
                {
                    d.Messages.Add(message);
                }
                foreach (var prescription in newdoc.Prescriptions)
                {
                    d.Prescriptions.Add(prescription);
                }
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fileStream.Close();
            }
            return d;
        }
        
        private string GetPathFile(string fileName)
        {
            string folderName = @"C:\ArchiFile";
            string pathString = Path.Combine(folderName, fileName);
            return pathString;
        }
    }
}

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

namespace ITI.Archi_Vite.WebApi
{
    public class DocumentManager 
    {
        ArchiViteContext _context;
        SuppressionRequest _supp;
        public DocumentManager(ArchiViteContext context)
        {
            _context = context;
            _supp = new SuppressionRequest(context);
        }
        public void CreateMessage(List<Professional> Receivers, User Sender, string Title, string Contents, Patient Patient)
        {
            Message m = new Message(Title, Contents, Sender, Receivers, Patient);
            CreateDoc(m);
        }
        public void CreatePrescription(List<Professional> Receivers, User Sender, Patient Patient, string Title, string DocPath)
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
        public DocumentSerializable SeeDocument(int proId, int patientId)
        {
            DocumentSerializable Documents = DeserializeListDoc(GetPathFile(patientId + "$" + proId));
            return Documents;
        }
        public DocumentSerializable SeeDocument(int patient)
        {
            DocumentSerializable Documents = DeserializeListDoc(GetPathFile(patient.ToString()));
            return Documents;
        }
        public DocumentSerializable SeeDocument(string path)
        {
            DocumentSerializable Documents = DeserializeListDoc(GetPathFile(path));
            return Documents;
        }
        public void DeleteFollowerFile(int proId, int patientId)
        {
            var senderFollow = _context.Follower
                                    .Include(c => c.Patient)
                                    .Include(c => c.Professionnal)
                                    .Where(t => t.Patient.PatientId.Equals(patientId) && t.ProfessionnalId.Equals(proId))
                                    .FirstOrDefault();
            if (senderFollow != null)
            {
                DeleteFile(GetPathFile(patientId + "$" + proId));
            }
        }
        public void DeletePatientFile(int patientId)
        {
            DeleteFile(GetPathFile(patientId.ToString()));
        }
        public void AddReciver(int reciverId, int patientid, DateTime date)
        {
            DocumentSerializable doc = DeserializeListDoc(GetPathFile(patientid.ToString()));
            foreach (var message in doc.Messages)
            {
                if (message.Date == date)
                {
                    message.Receivers.Add(_context.SelectRequest.SelectProfessional(reciverId));
                    AddDoc(message, patientid + "$" + reciverId);
                    ModifyDoc(message, patientid.ToString());
                    foreach( var reciver in message.Receivers)
                    {
                        ModifyDoc(message, patientid + "$" + reciver.ProfessionalId);
                    }
                }
            }
            foreach (var prescription in doc.Prescriptions)
            {
                if (prescription.Date == date)
                {
                    prescription.Receivers.Add(_context.SelectRequest.SelectProfessional(reciverId));
                    AddDoc(prescription, patientid + "$" + reciverId);
                    ModifyDoc(prescription, patientid.ToString());
                    foreach (var reciver in prescription.Receivers)
                    {
                        ModifyDoc(prescription, patientid + "$" + reciver.ProfessionalId);
                    }
                }
            }
        }
        public void AddReciver(List<int> reciversId, int patientid, DateTime date)
        {
            DocumentSerializable doc = DeserializeListDoc(GetPathFile(patientid.ToString()));
            foreach (var message in doc.Messages)
            {
                if (message.Date == date)
                {
                    foreach(var receiverId in reciversId)
                    {
                        message.Receivers.Add(_context.SelectRequest.SelectProfessional(receiverId));
                    }
                    foreach(var receiverId in reciversId)
                    {
                        AddDoc(message, patientid + "$" + receiverId);
                    }
                    ModifyDoc(message, patientid.ToString());
                    foreach (var reciver in message.Receivers)
                    {
                        ModifyDoc(message, patientid + "$" + reciver.ProfessionalId);
                    }
                }
            }
            foreach (var prescription in doc.Prescriptions)
            {
                if (prescription.Date == date)
                {
                    foreach (var receiverId in reciversId)
                    {
                        prescription.Receivers.Add(_context.SelectRequest.SelectProfessional(receiverId));
                    }
                    foreach (var receiverId in reciversId)
                    {
                        AddDoc(prescription, patientid + "$" + receiverId);
                    }
                    ModifyDoc(prescription, patientid.ToString());
                    foreach (var reciver in prescription.Receivers)
                    {
                        ModifyDoc(prescription, patientid + "$" + reciver.ProfessionalId);
                    }
                }
            }
        }

        public void DeleteReciever(int reciverId, int patientid, DateTime date)
        {
            DocumentSerializable doc = DeserializeListDoc(GetPathFile(patientid.ToString()));
            foreach (var message in doc.Messages)
            {
                if (message.Date == date)
                {
                    message.Receivers.Remove(_context.SelectRequest.SelectProfessional(reciverId));
                    DeleteDoc(message, patientid + "$" + reciverId);
                    ModifyDoc(message, patientid.ToString());
                    foreach (var reciver in message.Receivers)
                    {
                        ModifyDoc(message, patientid + "$" + reciver.ProfessionalId);
                    }
                }
            }
            foreach (var prescription in doc.Prescriptions)
            {
                if (prescription.Date == date)
                {
                    prescription.Receivers.Add(_context.SelectRequest.SelectProfessional(reciverId));
                    DeleteDoc(prescription, patientid + "$" + reciverId);
                    ModifyDoc(prescription, patientid.ToString());
                    foreach (var reciver in prescription.Receivers)
                    {
                        ModifyDoc(prescription, patientid + "$" + reciver.ProfessionalId);
                    }
                }
            }
        }
        public void DeleteReciever(List<int> reciversId, int patientid, DateTime date)
        {
            DocumentSerializable doc = DeserializeListDoc(GetPathFile(patientid.ToString()));
            foreach (var message in doc.Messages)
            {
                if (message.Date == date)
                {
                    foreach (var receiverId in reciversId)
                    {
                        message.Receivers.Remove(_context.SelectRequest.SelectProfessional(receiverId));
                    }
                    foreach (var receiverId in reciversId)
                    {
                        DeleteDoc(message, patientid + "$" + receiverId);
                    }
                    ModifyDoc(message, patientid.ToString());
                    foreach (var reciver in message.Receivers)
                    {
                        ModifyDoc(message, patientid + "$" + reciver.ProfessionalId);
                    }
                }
            }
            foreach (var prescription in doc.Prescriptions)
            {
                if (prescription.Date == date)
                {
                    foreach (var receiverId in reciversId)
                    {
                        prescription.Receivers.Remove(_context.SelectRequest.SelectProfessional(receiverId));
                    }
                    foreach (var receiverId in reciversId)
                    {
                        DeleteDoc(prescription, patientid + "$" + receiverId);
                    }
                    ModifyDoc(prescription, patientid.ToString());
                    foreach (var reciver in prescription.Receivers)
                    {
                        ModifyDoc(prescription, patientid + "$" + reciver.ProfessionalId);
                    }
                }
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
            if (_context.SelectRequest.SelectProfessional(message.Sender.UserId) != null)
            {
                var senderFollow = _context.SelectRequest.SelectOneFollow(message.Patient.PatientId, message.Sender.UserId);
                if (senderFollow != null)
                {
                    AddDoc(message, message.Patient.PatientId + "$" + message.Sender.UserId);
                }
            }
            foreach (var receiver in message.Receivers)
            {
                var follow = _context.SelectRequest.SelectOneFollow(message.Patient.PatientId, receiver.ProfessionalId);
                if (follow != null)
                {
                    AddDoc(message, message.Patient.PatientId + "$" + receiver.ProfessionalId);
                }
            }
            AddDoc(message, GetPathFile(message.Patient.PatientId.ToString()));
        }

        private void CreateDoc(Prescription prescription)
        {
            var senderFollow = _context.SelectRequest.SelectOneFollow(prescription.Patient.PatientId, prescription.Sender.UserId);
            if (senderFollow != null)
            {
                AddDoc(prescription, prescription.Patient.PatientId + "$" + prescription.Sender.UserId);
            }
            foreach (var receiver in prescription.Receivers)
            {
                var follow = _context.SelectRequest.SelectOneFollow(prescription.Patient.PatientId, receiver.ProfessionalId);
                if (follow != null && follow != senderFollow)
                {
                    AddDoc(prescription, senderFollow.PatientId + "$" + senderFollow.ProfessionnalId);
                }
            }
            AddDoc(prescription, GetPathFile(prescription.Patient.PatientId.ToString()));
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

        private void ModifyDoc(Message message, string FilePath)
        {
            DocumentSerializable Documents = DeserializeListDoc(GetPathFile(FilePath));
            foreach(var mes in Documents.Messages)
            {
                if (mes.Date == message.Date && mes.Contents == message.Contents && mes.Patient.PatientId == message.Patient.PatientId)
                {
                    Documents.Messages.Remove(mes);
                    Documents.Messages.Add(message);
                }
            }
            SerializeListDoc(Documents, GetPathFile(FilePath));
        }

        private void ModifyDoc(Prescription prescription, string FilePath)
        {
            DocumentSerializable Documents = DeserializeListDoc(GetPathFile(FilePath));
            foreach (var pres in Documents.Prescriptions)
            {
                if (pres.Date == prescription.Date && pres.DocPath == prescription.DocPath && pres.Patient.PatientId == prescription.Patient.PatientId)
                {
                    Documents.Prescriptions.Remove(pres);
                    Documents.Prescriptions.Add(prescription);
                }
            }
            SerializeListDoc(Documents, GetPathFile(FilePath));
        }

        public void DeleteDoc(Message message, string FilePath)
        {
            DocumentSerializable Documents = DeserializeListDoc(GetPathFile(FilePath));
            List<Message> messages = new List<Message>();
            foreach (var mess in Documents.Messages)
            {
                messages.Add(mess);
            }
            foreach (var mes in messages)
            {
                if (mes.Date == message.Date && mes.Contents == message.Contents && mes.Patient.PatientId == message.Patient.PatientId)
                {
                    Documents.Messages.Remove(mes);
                }
            }
            SerializeListDoc(Documents, GetPathFile(FilePath));
        }

        public void DeleteDoc(Prescription prescription, string FilePath)
        {
            DocumentSerializable Documents = DeserializeListDoc(GetPathFile(FilePath));
            foreach (var pres in Documents.Prescriptions)
            {
                if (pres.Date == prescription.Date && pres.DocPath == prescription.DocPath && pres.Patient.PatientId == prescription.Patient.PatientId)
                {
                    Documents.Prescriptions.Remove(pres);
                }
            }
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
            string folderName;
            if (fileName.Contains("$")) folderName = @"C:\ArchiFile\Professional";
            else folderName = @"C:\ArchiFile\Patient";
            if (!Directory.Exists(folderName)) Directory.CreateDirectory(folderName);
            string pathString = Path.Combine(folderName, fileName);
            return pathString;
        }
    }
}

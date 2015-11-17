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
            List<Document> empty = new List<Document>();
            string path = GetPathFile(FileName);
            SerializeListDoc(empty, path);
        }
        public List<Document> SeeDocument(Professional pro, Patient patient)
        {
            List<Document> d = new List<Document>();
            d = DeserializeListDoc(patient.PatientId + "$" + pro.ProfessionalId);
            return d;
        }
        private void CreateDoc(Document d)
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var senderFollow = context.Follower
                                        .Include(c => c.Patient)
                                        .Include(c => c.Professionnal)
                                        .Include(c => c.Professionnal.User)
                                        .Include(c => c.Patient.User)
                                        .Include(c => c.Patient.Referent)
                                        .Where(t => t.Patient.PatientId.Equals(d.Patient.PatientId) && t.ProfessionnalId.Equals(d.Sender.ProfessionalId))
                                        .FirstOrDefault();
                if (senderFollow != null)
                {
                    AddDoc(d, senderFollow.FilePath);
                }
                foreach (var receiver in d.Receivers)
                {
                    var follow = context.Follower
                                        .Include(c => c.Patient)
                                        .Include(c => c.Professionnal)
                                        .Include(c => c.Professionnal.User)
                                        .Include(c => c.Patient.User)
                                        .Include(c => c.Patient.Referent)
                                        .Where(t => t.Patient.PatientId.Equals(d.Patient.PatientId) && t.ProfessionnalId.Equals(receiver.ProfessionalId))
                                        .FirstOrDefault();
                    if (follow != null && follow != senderFollow)
                    {
                        AddDoc(d, follow.FilePath);
                    }
                }
            }
        }
        private void AddDoc(Document Document, string FilePath)
        {
            List<Document> ProDocuments = new List<Document>();
            ProDocuments = DeserializeListDoc(GetPathFile(FilePath));
            ProDocuments.Add(Document);
            SerializeListDoc(ProDocuments, FilePath);
        }

        private void SerializeListDoc(List<Document> Documents, string FilePath)
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

        private List<Document> DeserializeListDoc(string FilePath)
        {
            List<Document> d = new List<Document>();

            Stream fileStream = new FileStream(FilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                d = (List<Document>)formatter.Deserialize(fileStream);
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

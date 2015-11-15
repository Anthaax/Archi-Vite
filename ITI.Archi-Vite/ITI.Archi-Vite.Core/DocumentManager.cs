using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

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
            string path = CreateFile(FileName);
            SerializeListDoc(empty, path);
        }
        private void CreateDoc(Document d)
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                foreach (var receiver in d.Receivers)
                {
                    var follow = context.Follower.Where(t => t.Patient.PatientId.Equals(d.Patient.PatientId)).Where(t => t.ProfessionnalId.Equals(receiver.ProfessionalId)).FirstOrDefault();
                    if (follow != null)
                    {
                        AddDocForOnePerson(follow.Professionnal, follow.Patient, d, follow.FilePath);
                    }
                }
                var senderFollow = context.Follower.Where(t => t.Patient.PatientId.Equals(d.Patient.PatientId)).Where(t => t.ProfessionnalId.Equals(d.Sender.ProfessionalId)).FirstOrDefault();
                if (senderFollow != null)
                {
                    AddDocForOnePerson(senderFollow.Professionnal, senderFollow.Patient, d, senderFollow.FilePath);
                }
            }
        }
        private void AddDocForOnePerson(Professional Pro, Patient Patient, Document Document, string FilePath)
        {
            if (Pro == null) throw new ArgumentNullException("Pro can't be null");
            List<Document> ProDocuments = new List<Document>();
            ProDocuments = DeserializeListDoc(FilePath);
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
        private string CreateFile(string fileName)
        {
            string folderName = @"C:\ArchiFile";
            string pathString = Path.Combine(folderName, fileName);
            Console.WriteLine("Path to my file: {0}\n", pathString);
            File.Create(pathString);
            return pathString;
        }
    }
}

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
            string path = GetPathFile(FileName);
            SerializeListDoc(empty, path);
        }
        private void CreateDoc(Document d)
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var senderFollow = context.Follower.Include("Patient").Include("Professionnal").Where(t => t.Patient.PatientId.Equals(d.Patient.PatientId)).Where(t => t.ProfessionnalId.Equals(d.Sender.ProfessionalId)).FirstOrDefault();
                if (senderFollow != null)
                {
                    var patient = context.Patient.Include("User").Include("Referent").Where(t => t.PatientId.Equals(senderFollow.PatientId)).FirstOrDefault();
                    var pro = context.Professional.Include("User").Where(t => t.ProfessionalId.Equals(senderFollow.ProfessionnalId)).FirstOrDefault();
                    AddDocForOnePerson(pro, patient, d, senderFollow.FilePath);
                }
                foreach (var receiver in d.Receivers)
                {
                    var follow = context.Follower.Include("Patient").Include("Professionnal").Where(t => t.Patient.PatientId.Equals(d.Patient.PatientId)).Where(t => t.ProfessionnalId.Equals(receiver.ProfessionalId)).FirstOrDefault();
                    if (follow != null && follow != senderFollow)
                    {
                        var patient = context.Patient.Include("User").Include("Referent").Where(t => t.PatientId.Equals(follow.PatientId)).FirstOrDefault();
                        var pro = context.Professional.Include("User").Where(t => t.ProfessionalId.Equals(follow.ProfessionnalId)).FirstOrDefault();
                        AddDocForOnePerson(pro, patient, d, follow.FilePath);
                    }
                }
            }
        }
        private void AddDocForOnePerson(Professional Pro, Patient Patient, Document Document, string FilePath)
        {
            if (Pro == null) throw new ArgumentNullException("Pro can't be null");
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

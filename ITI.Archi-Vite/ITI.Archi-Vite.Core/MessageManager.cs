using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Core
{
    public class MessageManager
    {
        readonly List<Message> messages;
        public MessageManager()
        {
            messages = new List<Message>();
        }
        public void CreateMessage(List<Professional> Receivers, Professional Sender, string Title, string Contents, Patient Patient)
        {
            Message m = new Message(Title, Contents, Sender, Receivers, Patient);
            messages.Add(m);
            AddRequest a = new AddRequest();
            using (ArchiViteContext context = new ArchiViteContext())
            {
                foreach (var receiver in Receivers)
                {
                    var follow = context.Follower.Where(t => t.Patient.PatientId.Equals(Patient.PatientId)).Where(t => t.ProfessionnalId.Equals(receiver.ProfessionalId)).FirstOrDefault();
                    if (follow != null)
                    {
                        ;
                    }
                }
                var senderFollow = context.Follower.Where(t => t.Patient.PatientId.Equals(Patient.PatientId)).Where(t => t.ProfessionnalId.Equals(Sender.ProfessionalId)).FirstOrDefault();
                if(senderFollow != null)
                {
                    ;
                }
            }   
        }
    }
}

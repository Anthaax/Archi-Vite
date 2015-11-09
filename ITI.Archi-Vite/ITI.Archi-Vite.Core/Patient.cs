using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Core
{
    public class Patient : Person
    {
        readonly Person referent;
        readonly List<Message> comment;
        readonly List<Person> follow;
        public Patient(string FirstName, string LastName, DateTime BirthDate, string Adress, string City, int PostCode, int PhoneNumber, string Email, string Photo, Person Referent)
            :base(FirstName, LastName, BirthDate, Adress, City, PostCode, PhoneNumber, Email, "Patient", Photo)
        {
            referent = Referent;
            comment = new List<Message>();
            follow = new List<Person>();
        }
        public List<Message> Comment
        {
            get
            {
                return comment;
            }
        }

        public List<Person> Follow
        {
            get
            {
                return follow;
            }
        }

        public Person Referent
        {
            get
            {
                return referent;
            }
        }

        public void InsertMessage(Message Message)
        {
            if (Message.Receiver.Count.Equals(0) || Message.Contents.Length.Equals(0) || Message.Title.Length.Equals(0) || Message.Sender == null) throw new ArgumentNullException("Veuillez remplir tout les champs");
            int count = 0;
            List<Person> r = Message.Receiver;
            foreach (var recepteur in r)
            {
                foreach (var suivi in Follow)
                {
                    if (recepteur.ID == suivi.ID) count++;
                }

            }
            if (count != Message.Receiver.Count) throw new ArgumentOutOfRangeException("Message.Recepeteurs", "Certain nom ne figures pas dans la liste des suivis");
            Comment.Add(Message);
        }

        public void AddSuivis(Person Follower, Person Referent)
        {
            if (Referent.ID == 0 || Referent.ID == this.Referent.ID) Follow.Add(Follower);

            else throw new ArgumentException("Action possible seulement si admin ou réferent", "Utilisateur");
        }

        public void SupprimerSuivi(Person Referent, Person Delete)
        {
            if (Referent.ID == 0 || Referent.ID == this.Referent.ID) Follow.Remove(Delete);

            else throw new ArgumentException("Action possible seulement si admin ou réferent", "Utilisateur");
        }
    }
}

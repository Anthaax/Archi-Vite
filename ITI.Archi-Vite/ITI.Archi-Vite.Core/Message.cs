using System.Collections.Generic;

namespace ITI.Archi_Vite.Core
{
    public class Message
    {
        readonly string title;
        readonly string contents;
        readonly List<Person> receiver;
        readonly Person sender;
        public Message(string Intitulé, string Contenu, List<Person> Recepteurs, Person Expediteur)
        {
            title = Intitulé;
            contents = Contenu;
            sender = Expediteur;
            receiver = Recepteurs;
        }
        public string Title
        {
            get
            {
                return title;
            }
        }

        public string Contents
        {
            get
            {
                return contents;
            }
        }

        public List<Person> Receiver
        {
            get
            {
                return receiver;
            }
        }

        public Person Sender
        {
            get
            {
                return sender;
            }
        }
    }
}
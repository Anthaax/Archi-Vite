using System;
using System.Collections.Generic;
using ITI.Archi_Vite.DataBase;

namespace ITI.Archi_Vite.WebApi
{
    [Serializable]

    public abstract class DocumentsXml
    {
        public List<Professional> Recievers { get; set; }
        public Patient Patient { get; set; }
        public User Sender { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
    }
}

using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class PrescriptionCreator
    {
        List<int> _receivers;
        int _sender;
        int _patient;
        string _title;
        string _docPath;

        public PrescriptionCreator(List<int> Receivers, int Sender, int Patient, string Title, string DocPath)
        {
            _receivers = Receivers;
            _sender = Sender;
            _patient = Patient;
            _title = Title;
            _docPath = DocPath;
        }
        public List<int> Receivers
        {
            get
            {
                return _receivers;
            }
        }
        public int Sender
        {
            get
            {
                return _sender;
            }
        }
        public int Patient
        {
            get
            {
                return _patient;
            }
        }
        public string Title
        {
            get
            {
                return _title;
            }
        }
        public string DocPath
        {
            get
            {
                return _docPath;
            }
        }
    }
}
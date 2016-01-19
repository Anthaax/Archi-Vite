using System;
using System.Collections.Generic;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class ReciverModification
    {
        readonly int _patientId;
        readonly int _senderId;
        readonly List<int> _recieverId;
        readonly DateTime _date;

        public ReciverModification(int patientId, int senderId, List<int> recieverId, DateTime date)
        {
            _patientId = patientId;
            _recieverId = recieverId;
            _senderId = senderId;
            _date = date;
        }

        public int PatientId
        {
            get
            {
                return _patientId;
            }
        }

        public int SenderId
        {
            get
            {
                return _senderId;
            }
        }

        public List<int> RecieverId
        {
            get
            {
                return _recieverId;
            }
        }

        public DateTime Date
        {
            get
            {
                return _date;
            }
        }
    }
}
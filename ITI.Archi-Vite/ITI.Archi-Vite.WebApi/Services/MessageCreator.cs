using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class MessageCreator
    {
        List<int> _receiverId;
        int _senderId;
        string _title;
        string _content;
        int _patientId;

        public MessageCreator(List<int> recieverId, int senderId, string title, string content, int patientId)
        {
            _receiverId = recieverId;
            _senderId = senderId;
            _title = title;
            _content = content;
            _patientId = patientId;
        }

        public List<int> ReceiverId
        {
            get
            {
               return  _receiverId;
            }
        }

        public int SenderId
        {
            get
            {
                return _senderId;
            }
        }

        public string Title
        {
            get
            {
                return _title;
            }
        }

        public string Content
        {
            get
            {
                return _content;
            }
        }

        public int PatientId
        {
            get
            {
                return _patientId;
            }
        }



    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
    public class Patient : User
    {
        readonly int _patientId;
		public Patient
		(User user, string photoPath)
			:base(user.UserId, user.FirstName, user.LastName, user.BirthDate, user.Adress, user.City, user.PostCode, user.Pseudo, user.Password, user.PhoneNumber, user.PhotoPath)
        {
			_patientId = UserId;
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

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
		(User user)
			:base(user.UserId, user.FirstName, user.LastName, user.Birthdate, user.Adress, user.City, user.Postcode, user.Pseudo, user.Password, user.PhoneNumber, user.Photo)
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

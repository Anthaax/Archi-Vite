using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
	public class Professional : User
    {
		readonly int _professionalId;
		string _role;
		public Professional(User user, string role)
			:base(user.UserId, user.FirstName, user.LastName, user.Birthdate, user.Adress, user.City, user.Postcode, user.Pseudo, user.Password, user.PhoneNumber, user.Photo)
		{
			_professionalId = UserId;
			_role = role;
		}
    }
}

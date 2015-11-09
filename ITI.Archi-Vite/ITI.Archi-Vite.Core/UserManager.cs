using ITI.Archi_Vite.DataBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Core
{
    public class UserManager 
    {
        public User UserCreator(Person p)
        {
            User u = new User()
            {
                FirstName = p.FirstName,
                LastName = p.LastName,
                Birthdate = p.BirthDate,
                Adress = p.Adress,
                City = p.City,
                Postcode = p.PostCode,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                Role = p.Role,
                Photo = p.Photo                
            };
            return u;
        }
    }
}

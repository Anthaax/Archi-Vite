using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.DataBase
{
    public class UserInfoUpdate
    {
        User user;

        public UserInfoUpdate(User user)
        {
            this.user = user;
        }

        public void CheckInfo(string FirstName, string LastName, string Adress, DateTime Birthdate, string City, string Email, int PostCode, int PhoneNumber)
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var selectQuery = context.User.Where(s => s.UserId.Equals(user.UserId)).FirstOrDefault();
                if (selectQuery != null)
                {
                    if (selectQuery.FirstName != FirstName) UpdateFirstName(FirstName, selectQuery);
                    if (selectQuery.LastName != LastName) UpdateLastName(LastName, selectQuery);
                    if (selectQuery.Adress != Adress) UpdateAdress(Adress, selectQuery);
                    if (selectQuery.Birthdate != Birthdate) UpdateBirthDate(Birthdate, selectQuery);
                    if (selectQuery.City != City) UpdateCity(City, selectQuery);
                    if (selectQuery.Email != Email) UpdateEmail(Email, selectQuery);
                    if (selectQuery.Postcode != PostCode) UpdatePostcode(PostCode, selectQuery);
                    if (selectQuery.PhoneNumber != PhoneNumber) UpdatePhoneNumber(PhoneNumber, selectQuery); 
                }
                context.Entry(selectQuery).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
            
        }

        private void UpdatePhoneNumber(int PhoneNumber, User User)
        {
            User.PhoneNumber = PhoneNumber;
        }

        private void UpdatePostcode(int PostCode, User User)
        {
            User.Postcode = PostCode;
        }

        private void UpdateEmail(string Email, User User)
        {
            User.Email = Email;
        }

        private void UpdateCity(string City, User User)
        {
            User.City = City;
        }

        private void UpdateBirthDate(DateTime Birthdate, User User)
        {
            User.Birthdate = Birthdate;
        }

        private void UpdateAdress(string Adress, User User)
        {
            User.Adress = Adress;
        }

        private void UpdateLastName(string LastName, User User)
        {
            User.LastName = LastName;
        }

        public void UpdateFirstName(string FirstName, User User)
        {
            User.FirstName = FirstName;
        }
    }
}

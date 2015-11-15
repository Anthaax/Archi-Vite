using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.DataBase
{
    public class UpdateRequest
    {
        /// <summary>
        /// Methode use for update an user if just one thing change one gonna change
        /// </summary>
        /// <param name="FirstName"> new FirstName </param>
        /// <param name="LastName"> new LastName </param>
        /// <param name="Adress"> new Adress </param>
        /// <param name="Birthdate"> new Birthdate </param>
        /// <param name="City"> new City </param>
        /// <param name="Email"> new Email </param>
        /// <param name="PostCode"> new PostCode </param>
        /// <param name="PhoneNumber"> new PhoneNumber </param>
        /// <param name="User"> User to modifie </param>
        public void CheckUserInfo(string FirstName, string LastName, string Adress, DateTime Birthdate, string City, string Email, int PostCode, int PhoneNumber, User User)
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var selectQuery = context.User.Where(s => s.UserId.Equals(User.UserId)).FirstOrDefault();
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
        /// <summary>
        /// Change Role of an professional
        /// </summary>
        /// <param name="Role"> New Role </param>
        /// <param name="Pro"> Professional to modifie </param>
        public void CheckProInfo(string Role, Professional Pro)
        {
            using (ArchiViteContext context = new ArchiViteContext())
            {
                var professional = context.Professional.Where(s => s.ProfessionalId.Equals(Pro.ProfessionalId)).FirstOrDefault();
                if (professional != null)
                {
                    if (professional.Role != Role) UpdateRole(Role, professional);   
                }
                context.Entry(professional).State = System.Data.Entity.EntityState.Modified;
                context.SaveChanges();
            }
        }
        /// <summary>
        /// Change Referent of a Patient
        /// </summary>
        /// <param name="Pro"> New Referent not null</param>
        /// <param name="Patient"> Patient to modifie not null</param>
        public void CheckPatientInfo(Professional Pro, Patient Patient)
        {
            if (Patient == null || Pro == null) throw new ArgumentNullException("All value need to be not null");
            if (Patient.Referent != Pro)
            {
                using (ArchiViteContext context = new ArchiViteContext())
                {
                    UpdateReferent(Pro, Patient);
                    context.Entry(Patient).State = System.Data.Entity.EntityState.Modified;
                    context.SaveChanges();
                }
            } 
        }

        private void UpdateReferent(Professional Pro, Patient Patient)
        {
            Patient.Referent = Pro;
        }

        private void UpdateRole(string Role, Professional Professional)
        {
            Professional.Role = Role;
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

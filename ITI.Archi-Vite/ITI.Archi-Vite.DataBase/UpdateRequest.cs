using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace ITI.Archi_Vite.DataBase
{
    public class UpdateRequest
    {
        ArchiViteContext _context;
        public UpdateRequest(ArchiViteContext context)
        {
            _context = context;
        }
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
        /// <param name="user"> User to modifie </param>
        public void CheckUserInfo(User user)
        {
            var selectQuery = _context.User.Where(s => s.UserId.Equals(user.UserId)).FirstOrDefault();
            if (selectQuery != null)
            {
                if (selectQuery.FirstName != user.FirstName) UpdateFirstName(user.FirstName, selectQuery);
                if (selectQuery.LastName != user.LastName) UpdateLastName(user.LastName, selectQuery);
                if (selectQuery.Adress != user.Adress) UpdateAdress(user.Adress, selectQuery);
                if (selectQuery.Birthdate != user.Birthdate) UpdateBirthDate(user.Birthdate, selectQuery);
                if (selectQuery.City != user.City) UpdateCity(user.City, selectQuery);
                if (selectQuery.Email != user.Email) UpdateEmail(user.Email, selectQuery);
                if (selectQuery.Postcode != user.Postcode) UpdatePostcode(user.Postcode, selectQuery);
                if (selectQuery.PhoneNumber != user.PhoneNumber) UpdatePhoneNumber(user.PhoneNumber, selectQuery);
                if (selectQuery.Photo != user.Photo) UpdatePhoto(user.Photo, selectQuery);
            _context.Entry(selectQuery).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
            }            
        }
        /// <summary>
        /// Change Role of an professional
        /// </summary>
        /// <param name="Role"> New Role </param>
        /// <param name="Pro"> Professional to modifie </param>
        public void UpdateProInfo(string Role, Professional Pro)
        {
            var professional = _context.Professional.Include("User").Where(s => s.ProfessionalId.Equals(Pro.ProfessionalId)).FirstOrDefault();
            if (professional != null)
            {
                if (professional.Role != Role) UpdateRole(Role, professional);   
            }
            _context.Entry(professional).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        /// <summary>
        /// Change Referent of a Patient
        /// </summary>
        /// <param name="Pro"> New Referent not null</param>
        /// <param name="Patient"> Patient to modifie not null</param>
        public void CheckPatientInfo(Professional Pro, Patient Patient)
        {
        if (Patient == null || Pro == null) throw new ArgumentNullException("All value need to be not null");
        var patient = _context.Patient
                        .Include(c => c.User)
                        .Include(c => c.Referent)
                        .Include(c => c.Referent.User)
                        .Where(t => t.PatientId.Equals(Patient.PatientId))
                        .FirstOrDefault();
        if (patient.Referent != Pro)
        {
            UpdateReferent(Pro, patient);
            _context.Entry(patient).State = System.Data.Entity.EntityState.Modified;
            _context.SaveChanges();
        }
        }

        private void UpdatePhoto(string Photo, User User)
        {
            User.Photo = Photo;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ITI.Archi_Vite.DataBase;

namespace ITI.Archi_Vite.Core
{
    public class Person
    {
        readonly int Id;
        readonly string firstName;
        readonly string lastName;
        readonly DateTime birthDate;
        string adress;
        string city;
        int postCode;
        int phoneNumber;
        string email;
        readonly string role;
        string photo;
        public Person
            (string FirstName, string LastName, DateTime BirthDate, string Adress, string City, int PostCode, int PhoneNumber, string Email, string Role, string Photo)
        {
            using (ArchiViteContexts ctx = new ArchiViteContexts())
            {
                Id = ctx.User.Count() + 1;
            }
                
            firstName = FirstName;
            lastName = LastName;
            birthDate = BirthDate;
            adress = Adress;
            city = City;
            postCode = PostCode;
            phoneNumber = PhoneNumber;
            email = Email;
            role = Role;
            photo = Photo;
        }
        public int ID
        {
            get { return Id; }
        }

        public string FirstName
        {
            get { return firstName; }
        }

        public string LastName
        {
            get { return lastName; }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
        }

        public string Adress
        {
            get { return adress; }

            set { adress = value; }
        }

        public string City
        {
            get { return city; }

            set
            { city = value; }
        }

        public int PostCode
        {
            get { return postCode; }

            set { postCode = value; }
        }

        public int PhoneNumber
        {
            get { return phoneNumber; }

            set { phoneNumber = value; }
        }

        public string Email
        {
            get { return email; }

            set { email = value; }
        }

        public string Role
        {
            get { return role; }
        }

        public string Photo
        {
            get { return photo; }

            set { photo = value; }
        }
    }
}

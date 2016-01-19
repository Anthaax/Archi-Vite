using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    public class User
    {
        readonly int _userId;
        string _firstName;
        string _lastName;
        DateTime _birthDate;
        string _adress;
        string _city;
        int _postCode;
        string _pseudo;
        string _password;
        int _phoneNumber;
        string _photoPath;

		public User
           (int userId, string firstName, string lastName, DateTime birthDate, string adress, string city, int postCode, string pseudo, string password, int phoneNumber, string photoPath)
        {
            _adress = adress;
            _birthDate = birthDate;
            _city = city;
            _firstName = firstName;
            _lastName = lastName;
            _password = password;
            _phoneNumber = phoneNumber;
            _photoPath = photoPath;
            _postCode = postCode;
            _pseudo = pseudo;
            _userId = userId;
        }

        public int UserId
        {
            get
            {
                return _userId;
            }
        }

        public string FirstName
        {
            get
            {
                return _firstName;
            }

            set
            {
                _firstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _lastName;
            }

            set
            {
                _lastName = value;
            }
        }

		public DateTime Birthdate
        {
            get
            {
                return _birthDate;
            }

            set
            {
                _birthDate = value;
            }
        }

        public string Adress
        {
            get
            {
                return _adress;
            }

            set
            {
                _adress = value;
            }
        }

        public string City
        {
            get
            {
                return _city;
            }

            set
            {
                _city = value;
            }
        }

        public int Postcode
        {
            get
            {
                return _postCode;
            }

            set
            {
                _postCode = value;
            }
        }

        public string Pseudo
        {
            get
            {
                return _pseudo;
            }

            set
            {
                _pseudo = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }

            set
            {
                _password = value;
            }
        }

        public int PhoneNumber
        {
            get
            {
                return _phoneNumber;
            }

            set
            {
                _phoneNumber = value;
            }
        }

        public string Photo
        {
            get
            {
                return _photoPath;
            }

            set
            {
                _photoPath = value;
            }
        }
    }
}

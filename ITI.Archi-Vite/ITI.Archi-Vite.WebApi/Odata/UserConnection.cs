namespace ITI.Archi_Vite.WebApi.Controllers
{
    public class UserConnection
    {
        string _pseudo;
        string _password;

        public UserConnection(string pseudo, string password)
        {
            _pseudo = pseudo;
            _password = password;
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
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Net.Http;

using Xamarin.Forms;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;

namespace ITI.Archi_Vite.Forms
{
    public class ConnectionPage : ContentPage
    {
		User _user;
		Data _dataForUser;
        public ConnectionPage()
        {
			//AutoConnection();
            Image logo = new Image
            {
                Source = "Logo.png",
				Scale = 0.75,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start
            };
            Entry pseudo = new Entry
            {
                Placeholder = "Pseudo",
                FontSize = 40,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
                TextColor = Color.Gray
            };
            pseudo.TextChanged += EntryTextChanged;
            Entry password = new Entry
            {
                Placeholder = "Password",
                IsPassword = true,
                FontSize = 40,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            password.TextChanged += EntryTextChanged;
            Button send = new Button
            {
                Text = "Se connecter",
                FontSize = 40,
                BackgroundColor = Color.FromHex("439DFE"),
                HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.End,
            };
            send.Clicked += async (sender, e) =>
            {
                if (pseudo.Text != null && password.Text != null)
                {
                    //User newUser = await ConnectionGestion(pseudo.Text, password.Text);

                    if (CanPutUserData(pseudo.Text, password.Text))
                    {
      //                  LoadUserData();
						//SaveUserData();
						await Navigation.PushAsync(new ProfilPage(_dataForUser, _dataForUser.User));
                    }
                    else await DisplayAlert("Error", "Les champ doivent etre valides", "Ok");
                }
                else await DisplayAlert ("Error", "Les champ doivent etre valides", "Ok");

            };
            Content = new StackLayout
            {

                Children = {
                    logo,
                    pseudo,
                    password,
                    send,
                }
            };
            this.BackgroundColor = Color.White;
        }

        private async void AutoConnection()
        {
            LoadUserData();
            if(_dataForUser != null)
            {
                await Navigation.PushAsync(new ProfilPage(_dataForUser, _dataForUser.User));
            }
        }

        private void EntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            if (entry != null)
            {
                entry.TextColor = Color.Gray;
            }
        }
        private async Task<User> ConnectionGestion(string pseudo, string password)
        {
            using (var client = new HttpClient())
            {
				string baseUrl = "http://10.0.2.2";
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				string url = "api/Users/?pseudo=GuillaumeF&password=GuillaumeF";
				string completeUrl = string.Format(url, pseudo, password);
				string content = await client.GetStringAsync (url);
				return null;
            }
                
        }
		private bool CanPutUserData(string pseudo, string password)
		{
			User[] users = new User[6];
			User u0 = new User (1, "Antoine", "Raquillet", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12345, "AntoineR", "AntoineR",0616066606, "http://new.intechinfo.fr/wp-content/uploads/2015/10/RAQUILLET-Antoine.jpg");
			User u1 = new User(2, "Simon", "Favraud", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12345, "SimonF", "SimonF", 0626066606, "http://i.imgur.com/9cSffeM.png");
			User u2 = new User (3, "Clement", "Rousseau", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12345, "ClementR", "ClementR",0636066606, "http://i.imgur.com/silO1AR.png");
			User u3 = new User (4, "Olivier", "Spinelli", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12345, "OlivierS", "OlivierS",0646066606, "http://new.intechinfo.fr/wp-content/uploads/2015/10/olivier-spinelli_portrait.png");
			User u4 = new User (5, "Guillaume", "Fimes", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12345, "GuillaumeF", "GuillaumeF", 0656066606, "http://i.imgur.com/GWji92h.png");
			User u5 = new User (6, "Maxime", "De Vogelas", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12345, "MaximeD", "MaximeD", 0666066606, "http://i.imgur.com/3yZF0Lz.png");
			users[0] = u0;
			users[1] = u1;
			users[2] = u2;
			users[3] = u3;
			users[4] = u4;
			users[5] = u5;

			for (int i=0; i < users.Length; i++)
			{
				if (users[i].Password == password && users[i].Pseudo == pseudo)
					_user = users[i];
			}
			if (_user == null)
				return false;
            List<Message> m = new List<Message>();
            List<Prescription> p = new List<Prescription>();
            DocumentSerializable doc = new DocumentSerializable(m,p);
            if(_user != u5)
            {
                foreach (var message in CreateSerializableDocument(CreateFollowerDictionnary(users, _user), u4).Messages)
                {
                    doc.Messages.Add(message);
                }
                foreach (var prescription in CreateSerializableDocument(CreateFollowerDictionnary(users, _user), u4).Prescriptions)
                {
                    doc.Prescriptions.Add(prescription);
                }
            }
            if (_user != u4 && _user != u3)
            {
                foreach (var message in CreateSerializableDocument(CreateFollowerDictionnary(users, _user), u5).Messages)
                {
                    doc.Messages.Add(message);
                }
                foreach (var prescription in CreateSerializableDocument(CreateFollowerDictionnary(users, _user), u5).Prescriptions)
                {
                    doc.Prescriptions.Add(prescription);
                }
            }
            _dataForUser = new Data(_user, CreateFollowerDictionnary(users, _user), doc);
            return true;
		}
		private Dictionary<Patient, Professional[]> CreateFollowerDictionnary(User[] users, User curentUser)
		{
            Dictionary<Patient, Professional[]> follows = new Dictionary<Patient, Professional[]>();
            Professional[] proForGuillaume = new Professional[10];
            Professional[] proForMaxime = new Professional[10];
            Patient guillaume = new Patient(users[4]);
            Patient maxime = new Patient(users[5]); 
            for( int i = 0; i<4; i++)
            {
                Professional pro = new Professional(users[i], "Infirmier");
				if(pro != null)
				proForGuillaume.SetValue(pro, i);
            }
            for (int i = 0; i < 2; i++)
            {
                Professional pro = new Professional(users[i], "Infirmier");
                proForMaxime.SetValue(pro, i);
            }
            if(curentUser != users[5] ) follows.Add(guillaume, proForGuillaume);
            if (curentUser != users[4] && curentUser != users[3]) follows.Add(maxime, proForMaxime);
            return follows;
		}
		private DocumentSerializable CreateSerializableDocument(Dictionary<Patient, Professional[]> userData, User Patient)
        {
            List<Message> messages = new List<Message>();
            List<Prescription> prescriptions = new List<Prescription>();
            List<Professional> professional = new List<Professional>();
            Patient p = new Patient(Patient);
            Professional[] proForPatient = ProfessionalArray(userData, p);
            for (int i = 0; i < 2; i++)
            {
				professional.Add(proForPatient[i]);
            }
			messages.Add(new Message("Coucou", "Il va bien", proForPatient[0], professional, p));
			if (p.UserId == 5) 
			{
				messages.Add (new Message ("Hey", "Il va bien", proForPatient [1], professional, p));
				prescriptions.Add (new Prescription ("Hey", "http://i.imgur.com/GWji92h.png", proForPatient [1], professional, p));
			}
            DocumentSerializable doc = new DocumentSerializable(messages, prescriptions);
            return doc;
        }

		private Professional[] ProfessionalArray(Dictionary<Patient, Professional[]> userData, Patient user)
        {
            Professional[] pro = new Professional[10];
			Patient p = user;
            foreach (var patient in userData.Keys)
            {
                if (user.UserId == patient.UserId)
                    p = patient;
            }
            userData.TryGetValue(p, out pro);
            int count = 0;
            foreach (var professional in pro)
            {
                pro.SetValue(professional, count);
                count++;
            }
            return pro;
        }

        public void SaveUserData()
        {
            var obj = _dataForUser;
        }

        public bool LoadUserData()
        {
            try
            {
                string text = DependencyService.Get<ISaveAndLoad>().LoadText("user.txt");
                string serializer = JsonConvert.SerializeObject(_dataForUser);
				var userdata = JsonConvert.DeserializeObject(serializer);
				_dataForUser = (Data)userdata;
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }
    }
}

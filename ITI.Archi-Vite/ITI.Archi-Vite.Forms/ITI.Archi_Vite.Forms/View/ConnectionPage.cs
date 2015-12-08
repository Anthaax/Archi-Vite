﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Net.Http;


using Xamarin.Forms;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
    public class ConnectionPage : ContentPage
    {
		User _user;
		Data _dataForUser;
        public ConnectionPage()
        {
            Image logo = new Image
            {
                Source = "Logo.png",
				Scale = 0.8,
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
				VerticalOptions = LayoutOptions.Center,
            };
            send.Clicked += async (sender, e) =>
            {
                if (pseudo.Text != null && password.Text != null)
                {
                    //User newUser = await ConnectionGestion(pseudo.Text, password.Text);

                    if (CanPutUserData(pseudo.Text, password.Text))
                    {
                        Patient patient = new Patient(_user);
                        await Navigation.PushAsync(new FollowPatientPage(_dataForUser, patient));
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
			User u0 = new User (1, "Antoine", "Raquillet", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12345, "AntoineR", "AntoineR",0606066606, "http://new.intechinfo.fr/wp-content/uploads/2015/10/RAQUILLET-Antoine.jpg");
			User u1 = new User(2, "Simon", "Favraud", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12345, "SimonF", "SimonF", 0606066606, "http://www.go-e-lan.info/vue/images/event/simon.PNG");
			User u2 = new User (3, "Clement", "Rousseau", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12345, "ClementR", "ClementR",0606066606, "http://www.go-e-lan.info/vue/images/event/clem.PNG");
			User u3 = new User (4, "Olivier", "Spinelli", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12345, "OlivierS", "OlivierS",0606066606, "http://new.intechinfo.fr/wp-content/uploads/2015/10/olivier-spinelli_portrait.png");
			User u4 = new User (5, "Guillaume", "Fimes", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12345, "GuillaumeF", "GuillaumeF", 0606066606, "http://www.go-e-lan.info/vue/images/event/fimes.PNG");
			User u5 = new User (6, "Maxime", "De Vogelas", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12345, "MaximeD", "MaximeD", 0606066606, "http://www.go-e-lan.info/vue/images/event/max.PNG");
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
            _dataForUser = new Data(_user, CreateFollowerDictionnary(users, _user));
            return true;
		}
		private Dictionary<Patient, List<Professional>> CreateFollowerDictionnary(User[] users, User curentUser)
		{
			Dictionary<Patient, List<Professional>> follows = new Dictionary<Patient, List<Professional>> ();
            List<Professional> proForGuillaume = new List<Professional>();
            List<Professional> proForMaxime = new List<Professional>();
            Patient guillaume = new Patient(users[4]);
            Patient maxime = new Patient(users[5]); 
            for( int i = 0; i<4; i++)
            {
                Professional pro = new Professional(users[i], "Infirmier");
                proForGuillaume.Add(pro);
            }
            for (int i = 0; i < 2; i++)
            {
                Professional pro = new Professional(users[i], "Infirmier");
                proForMaxime.Add(pro);
            }
            if(curentUser != users[5] ) follows.Add(guillaume, proForGuillaume);
            if (curentUser != users[4] && curentUser != users[3]) follows.Add(maxime, proForMaxime);
            return follows;
		}
	}
}

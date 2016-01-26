using System;

using Xamarin.Forms;
using System.IO;
using Newtonsoft.Json;

namespace ITI.Archi_Vite.Forms
{
    public class ConnectionPage : ContentPage
    {
		Data _dataForUser;
        DataJsonConvertor _jsonCovertor = new DataJsonConvertor();
        DataConvertor _convertor = new DataConvertor();
        public ConnectionPage()
        {
			AutoConnection();
            Image logo = new Image
            {
                Source = "Logo.png",
				Scale = 0.7,
                VerticalOptions = LayoutOptions.Start
            };
            Entry pseudo = new Entry
            {
                Placeholder = "Pseudo",
                FontSize = 40,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Gray
            };
            pseudo.TextChanged += EntryTextChanged;
            Entry password = new Entry
            {
                Placeholder = "Password",
                IsPassword = true,
                FontSize = 40,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Start,
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
                    ConnectionGestion(pseudo.Text, password.Text);
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

        private Data XmlDeseriliaze(string s)
        {
            DataXML data = new DataXML();
            data = JsonConvert.DeserializeObject<DataXML>(s);
            return _jsonCovertor.DataJsonToDataFromRequest(data);
        }

        private void EntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            if (entry != null)
            {
                entry.TextColor = Color.Gray;
            }
        }
        private async void ConnectionGestion(string pseudo, string password)
        {
            var response = await HttpRequest.HttpRequestGetUserData(pseudo, password);
			if (response != null || response.IsSuccessStatusCode)
            {
                string information = await response.Content.ReadAsStringAsync();
                Data u = XmlDeseriliaze(information);
                _dataForUser = u;
                SaveUserData();
               	await Navigation.PushAsync(new ProfilPage(_dataForUser, _dataForUser.User));
            }
			else if(response == null)
            {
                await DisplayAlert("Error", "Problème avec le serveur", "Ok");
            }
			else {
				await DisplayAlert("Error", "Identifiants incorrects", "Ok");
					
			}
        }

        public void SaveUserData()
        {
            DataXML json = _convertor.DataToDataJsonForSave(_dataForUser);
			DependencyService.Get<ISaveLoadAndDelete>().SaveData("user.txt", json);
        }

        public bool LoadUserData()
        {
            try
            {
				DataXML json = DependencyService.Get<ISaveLoadAndDelete>().LoadData("user.txt");
				if(json != null)
                	_dataForUser = _jsonCovertor.DataJsonToDataFromSave(json);
				else return false;
            }
            catch (IOException)
            {
                return false;
            }
			catch(Exception e) {
				return false;
			}
            return true;
        }
    }
}

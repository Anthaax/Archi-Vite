using System;
using System.Collections.Generic;
using System.Net.Http;

using Xamarin.Forms;
using System.IO;
using ModernHttpClient;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace ITI.Archi_Vite.Forms
{
    public class ConnectionPage : ContentPage
    {
		User _user;
		Data _dataForUser;
        DataJsonConvertor _jsonCovertor = new DataJsonConvertor();
        DataConvertor _convertor = new DataConvertor();
        public ConnectionPage()
        {
			AutoConnection();
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
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Start,
            };
            password.TextChanged += EntryTextChanged;
            Button send = new Button
            {
                Text = "Se connecter",
                FontSize = 40,
                BackgroundColor = Color.FromHex("439DFE"),
                HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.FillAndExpand,
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
            var client = new HttpClient(new NativeMessageHandler());
            client.BaseAddress = new Uri("http://10.8.110.152:8080/");
            client.Timeout = new TimeSpan(0, 0, 50);
            client.MaxResponseContentBufferSize = Int64.MaxValue;
			string s = "api/Users/?pseudo=" + pseudo + "&password=" + password;
			var response = await client.GetAsync(s);
            if (response.IsSuccessStatusCode)
            {
                s = await response.Content.ReadAsStringAsync();
                Data u = XmlDeseriliaze(s);
                _dataForUser = u;
                SaveUserData();
                await Navigation.PushAsync(new ProfilPage(_dataForUser, _dataForUser.User));
            }
            else
            {
                await DisplayAlert("Error", "Les champ doivent etre valides", "Ok");
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

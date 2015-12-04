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
                    User newUser = await ConnectionGestion(pseudo.Text, password.Text);
                    Data _dataForUser = new Data(newUser);
                    await DisplayAlert("Reussite", "Les champ sont valides", "Ok");
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
    }
}

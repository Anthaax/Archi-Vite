using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    public class ConnectionPage : ContentPage
    {
        public ConnectionPage()
        {
            Image logo = new Image
            {
                Source = "Archi'Vite Logo.png",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand
            };
            Entry pseudo = new Entry
            {
                Placeholder = "Pseudo",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            Entry password = new Entry
            {
                Placeholder = "Password",
                IsPassword = true,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            Button send = new Button
            {
                Text = "Se connecter",
                TextColor = Color.Blue,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                VerticalOptions = LayoutOptions.End
            };
            send.Clicked += OnButtonClicked;
            Content = new StackLayout
            {

                Children = {
                    logo,
                    pseudo,
                    password,
                    send
                }
            };
        }

        private void OnButtonClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

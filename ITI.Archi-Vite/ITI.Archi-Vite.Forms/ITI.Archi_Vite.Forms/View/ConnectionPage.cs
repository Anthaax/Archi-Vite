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
                Source = "Logo.png",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.StartAndExpand
            };
            Entry pseudo = new Entry
            {
                Placeholder = "Pseudo",
				FontSize = 40,
				HorizontalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            Entry password = new Entry
            {
                Placeholder = "Password",
                IsPassword = true,
				FontSize = 40,
				HorizontalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            Button send = new Button
            {
                Text = "Se connecter",
				FontSize = 40,
				BackgroundColor = Color.FromHex("439DFE"),
				HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End
            };
			send.Clicked += (sender, e) => 
			{
				throw new NotImplementedException();
					
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
		private Entry Password
		{
			get{ return Content.R;}
		}
        private void OnButtonClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}

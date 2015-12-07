using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    public class ProfilPage : ContentPage
    {
		Data _userData;
		public ProfilPage(Data userData)
        {
			_userData = userData;
			Button profilButton = new Button {
				Text = "Mon Profil",
				BackgroundColor = Color.Gray,
				BorderColor = Color.Black,
				FontSize = 30,
				FontAttributes = FontAttributes.Bold,
			};

			Button followButton = new Button {
				Text = "Mes Suivis",
				BackgroundColor = Color.White,
				BorderColor = Color.Black,
				FontSize = 30,
				TextColor = Color.Black
			};

			Button documentsButton = new Button {
				Text = "Mes Documents",
				BackgroundColor = Color.White,
				BorderColor = Color.Black,
				FontSize = 30,
				TextColor = Color.Black
			};

			StackLayout buttonStack = new StackLayout {

				Children = {
					profilButton,
					followButton,
					documentsButton
				},
				Orientation = StackOrientation.Horizontal,					
				HorizontalOptions = LayoutOptions.Start

			};
			Label title = new Label {
				Text = "Profil",
				FontSize = 40,
				HorizontalOptions = LayoutOptions.Center
			};
			Label name = new Label {
				Text = _userData.User.FirstName + "  " + _userData.User.LastName,
				FontSize = 30,
				HorizontalOptions = LayoutOptions.Center
				
			};
			Label phonenumber = new Label
			{
				Text = "Numero : " + _userData.User.PhoneNumber ,
				FontSize = 30,
				HorizontalOptions = LayoutOptions.Center

            };
			Label adresse = new Label
			{
				Text = "Adresse : "+ _userData.User.Adress,
                FontSize = 30,
                HorizontalOptions = LayoutOptions.Center
			};
			Label postCode = new Label
			{
				Text = _userData.User.Postcode.ToString(),
				FontSize = 30,
				HorizontalOptions = LayoutOptions.Center
			};
			Label city = new Label
			{
				Text = _userData.User.City,
				FontSize = 30,
				HorizontalOptions = LayoutOptions.Center
			};
			Image logo = new Image
			{
				Source = _userData.User.Photo,
				HorizontalOptions = LayoutOptions.FillAndExpand,
			};

            Button modify = new Button
            {
                Text = "Modifier mes infos",
                FontSize = 40,
                BackgroundColor = Color.FromHex("439DFE"),
				VerticalOptions = LayoutOptions.EndAndExpand
            };
			modify.Clicked += async (sender, e) => 
			{
				await Navigation.PushAsync(new ModifyProfil(_userData));
			};
            Content = new StackLayout
            {

                Children = {
					buttonStack,
					title,
					name,
					phonenumber,
					adresse,
					postCode,
					city,
					logo,
					modify
                },
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

        private void OnLabelClicked()
        {
            throw new NotImplementedException();
        }
    }
}

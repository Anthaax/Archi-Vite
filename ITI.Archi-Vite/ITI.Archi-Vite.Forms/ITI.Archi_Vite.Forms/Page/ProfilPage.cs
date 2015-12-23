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
        User _user;
		public ProfilPage(Data userData, User user)
        {
			_userData = userData;
            _user = user;
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
            if (PageForPatient()) followButton.Text = "Mon Suivis";

            followButton.Clicked += FollowButtonClicked;

            Button documentsButton = new Button {
				Text = "Mes Documents",
				BackgroundColor = Color.White,
				BorderColor = Color.Black,
				FontSize = 30,
				TextColor = Color.Black
			};
            documentsButton.Clicked += Document_Clicked;

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
				Text = _user.FirstName + "  " + _user.LastName,
				FontSize = 30,
				HorizontalOptions = LayoutOptions.Center
				
			};
			Label phonenumber = new Label
			{
				Text = "Numero : " + _user.PhoneNumber ,
				FontSize = 30,
				HorizontalOptions = LayoutOptions.Center

            };
			Label adresse = new Label
			{
				Text = "Adresse : "+ _user.Adress,
                FontSize = 30,
                HorizontalOptions = LayoutOptions.Center
			};
			Label postCode = new Label
			{
				Text = _user.Postcode.ToString(),
				FontSize = 30,
				HorizontalOptions = LayoutOptions.Center
			};
			Label city = new Label
			{
				Text = _user.City,
				FontSize = 30,
				HorizontalOptions = LayoutOptions.Center
			};
			Image logo = new Image
			{
				Source = _user.Photo,
				HorizontalOptions = LayoutOptions.FillAndExpand,
			};

            Button Suivis = new Button
            {
                Text = "Voir mes Suivis",
                FontSize = 40,
                BackgroundColor = Color.FromHex("439DFE"),
                VerticalOptions = LayoutOptions.End
            };
            Suivis.Clicked += FollowButtonClicked;
            if (PageForPatient()) Suivis.Text = "Voir mon Suivi";
            else if (!UserAccount()) Suivis.Text = "Retour à mon suivi";

            Button document = new Button
			{
				Text = "Voir mes documents",
				FontSize = 40,
				BackgroundColor = Color.FromHex("439DFE"),
				VerticalOptions = LayoutOptions.End
			};
            document.Clicked += Document_Clicked;
            if (!UserAccount()) document.IsVisible = false;

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

            if (!UserAccount()) modify.IsVisible = false;

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
                    Suivis,
					document,
                    modify
                },
            };
            this.BackgroundColor = Color.White;
        }

        private async void Document_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DocumentsPage(_userData));
        }

        private async void FollowButtonClicked (object sender, EventArgs e)
		{
			if (PageForPatient())
			{
				Patient patient = new Patient(_userData.User);
				await Navigation.PushAsync(new FollowPatientPage(_userData, patient));
			}
			else await Navigation.PushAsync(new PatientListPage(_userData));
		}

        private bool UserAccount()
        {
            if(_userData.User.PhoneNumber == _user.PhoneNumber)
            {
                return true;
            }
			return false;
        }

        private bool PageForPatient()
        {
            foreach(var patient in _userData.Follow.Keys )
            {
                if (patient.UserId == _userData.User.UserId) return true;
            }
            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
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

            MultibleButtonView button = new MultibleButtonView(_userData);

			button.ProfilIsDisable ();
            button.FollowButton.Clicked += FollowButtonClicked;
            button.DocumentButton.Clicked += Document_Clicked;   

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
            Stream s = new MemoryStream(DependencyService.Get<IBytesSaveAndLoad>().LoadByteArray(_user.Photo));
			Image logo = new Image
			{
				Source = ImageSource.FromStream(() => s),
                HorizontalOptions = LayoutOptions.FillAndExpand,
			};

            Button Suivis = new Button
            {
                Text = "Voir mes Suivis",
                FontSize = 40,
                BackgroundColor = Color.FromHex("439DFE"),
                VerticalOptions = LayoutOptions.EndAndExpand
            };

            Suivis.Clicked += FollowButtonClicked;

            if (!UserAccount())
                Suivis.Text = "Retour à mon suivi";
            else if(PageForPatient())
                Suivis.Text = "Voir mon Suivi";
            

            Button document = new Button
			{
				Text = "Voir mes documents",
				FontSize = 40,
				BackgroundColor = Color.FromHex("439DFE"),
				VerticalOptions = LayoutOptions.EndAndExpand
			};
            document.Clicked += Document_Clicked;

            if (!UserAccount())
                document.IsVisible = false;

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
            Button deconnection = new Button
            {
                Text = "Se déconnecter",
                FontSize = 40,
                BackgroundColor = Color.FromHex("439DFE"),
                VerticalOptions = LayoutOptions.EndAndExpand
            };
            deconnection.Clicked += Deconnection_Clicked;

            Content = new StackLayout
            {

                Children = {
					button.Content,
					title,
					name,
					phonenumber,
					adresse,
					postCode,
					city,
					logo,
                    Suivis,
					document,
                    modify,
                    deconnection
                },
            };
            this.BackgroundColor = Color.White;
        }

        private async void Deconnection_Clicked(object sender, EventArgs e)
        {
			DependencyService.Get<ISaveLoadAndDelete>().DeleteData("user.txt");
            await Navigation.PushAsync(new ConnectionPage());
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
            if(_userData.User.UserId == _user.UserId)
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
        private async void UpdateUserInformation()
        {
            if (_userData.NeedUpdate)
            {

            }
        }
    }
}

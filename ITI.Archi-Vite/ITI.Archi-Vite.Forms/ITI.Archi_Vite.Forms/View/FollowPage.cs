using System;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
	public class FollowPage : ContentPage
	{
		Data _userData;
        Patient _patient;
		public FollowPage (Data userData, Patient patient)
		{
			_userData = userData;
			_userData = userData;
			Button profilButton = new Button {
				Text = "Mon Profil",
				BackgroundColor = Color.White,
				BorderColor = Color.Black,
				FontSize = 30,
			};

			Button followButton = new Button {
				Text = "Mes Suivis",
				BackgroundColor = Color.Gray,
				BorderColor = Color.Black,
				FontSize = 30,
				FontAttributes = FontAttributes.Bold,
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

            Image patientImage = new Image
            {
                Source = patient.Photo
            };



		}


	}
}



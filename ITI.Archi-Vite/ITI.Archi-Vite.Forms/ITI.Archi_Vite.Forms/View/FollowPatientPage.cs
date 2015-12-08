using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
	public class FollowPatientPage : ContentPage
	{
		Data _userData;
		Patient _patient;
        Professional[] professionals;
        public FollowPatientPage (Data userData, Patient patient)
		{
			_userData = userData;
			_patient = patient;
            var tappedGesture = new TapGestureRecognizer();
            tappedGesture.Tapped += TappedGesture_Tapped;
            AbsoluteLayout photoLayout = new AbsoluteLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            };

			Button profilButton = new Button {
				Text = "Mon Profil",
				BackgroundColor = Color.White,
				BorderColor = Color.Black,
				TextColor = Color.Black,
				FontSize = 30,
			};
			profilButton.Clicked += async (sender, e) =>
			{
				await Navigation.PushAsync(new ProfilPage(_userData,_userData.User));
			};
			Button followButton = new Button {
				Text = "Mes Suivis",
				BackgroundColor = Color.Gray,
				BorderColor = Color.Black,
				FontSize = 30,
				FontAttributes = FontAttributes.Bold,
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
                Source = patient.Photo,
				Scale = 0.75
            };
            AbsoluteLayout.SetLayoutFlags(patientImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(patientImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            photoLayout.Children.Add(patientImage);

            Image[] proImage = new Image[10];
            professionals = ProfessionalArray();
            double X = 0.5;
            double Y = 0.0;
            for (int i = 0; i < 3; i++)
            {
				proImage[i] = new Image();
				if (professionals [i] != null)
					proImage [i].Source = professionals [i].Photo;
				else
					proImage [i].Source = patient.Photo;
				proImage [i].Scale = 0.75;
				AbsoluteLayout.SetLayoutFlags(proImage[i], AbsoluteLayoutFlags.PositionProportional);
				AbsoluteLayout.SetLayoutBounds(proImage[i], new Rectangle(X, Y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
				photoLayout.Children.Add(proImage[i]);
                proImage[i].GestureRecognizers.Add(tappedGesture);
				X = X - 0.2;
				Y = Y + 0.2;

            }
            for (int i = 3; i < 6; i++)
            {
				X = X + 0.2;
				proImage[i] = new Image();
				if (professionals [i] != null)
					proImage [i].Source = professionals [i].Photo;
				else
					proImage [i].Source = patient.Photo;
				proImage [i].Scale = 0.75;
                AbsoluteLayout.SetLayoutFlags(proImage[i], AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(proImage[i], new Rectangle(X, Y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
                photoLayout.Children.Add(proImage[i]);
                proImage[i].GestureRecognizers.Add(tappedGesture);
				if (i != 5) Y = Y + 0.2;
            }
            for (int i = 6; i < 8; i++)
            {
				X = X + 0.2;
				Y = Y - 0.2;
				proImage[i] = new Image();
				if (professionals [i] != null)
					proImage [i].Source = professionals [i].Photo;
				else
					proImage [i].Source = "http://www.go-e-lan.info/vue/images/event/simon.PNG";
				proImage [i].Scale = 0.75;
                AbsoluteLayout.SetLayoutFlags(proImage[i], AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(proImage[i], new Rectangle(X, Y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
                proImage[i].GestureRecognizers.Add(tappedGesture);
                photoLayout.Children.Add(proImage[i]);
            }
            for (int i = 8; i < 10; i++)
            {
				Y = Y - 0.2;
				proImage[i] = new Image();
				if (professionals [i] != null)
					proImage [i].Source = professionals [i].Photo;
				else
					proImage [i].Source = patient.Photo;
				proImage [i].Scale = 0.75;
                AbsoluteLayout.SetLayoutFlags(proImage[i], AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(proImage[i], new Rectangle(X, Y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
                proImage[i].GestureRecognizers.Add(tappedGesture);
                photoLayout.Children.Add(proImage[i]);
                X = X - 0.2;

            }
            Content = new StackLayout
            {

                Children = {
                    buttonStack,
                    photoLayout
                },
				VerticalOptions = LayoutOptions.FillAndExpand
            };
            this.BackgroundColor = Color.White;

        }

        private void TappedGesture_Tapped(object sender, EventArgs e)
        {
            var image = sender as Image;
            if(image != null )
            {
				var source = image.Source as UriImageSource;
				if (source != null) 
				{
					for (int i = 0; i < professionals.Length; i++)
					{
						if (professionals[i] != null && source.Uri.ToString() == professionals[i].Photo) Navigation.PushAsync(new ProfilPage(_userData, professionals[i]));
					}
				}
                
            }
        }

        private Professional[] ProfessionalArray()
        {
            Professional[] pro = new Professional[10];
            List<Professional> professionals = new List<Professional>();
			Patient p = new Patient(_patient);
			foreach (var patient in _userData.Follow.Keys) 
			{
				if (_patient.UserId == patient.UserId)
					p = patient;
			}
			_userData.Follow.TryGetValue(p, out professionals);
            int count = 0;
            foreach (var professional in professionals)
            {
                pro.SetValue(professional, count);
                count++;
            }
            return pro;
        }
    }
}



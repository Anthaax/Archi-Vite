using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
	public class FollowPatientPage : ContentPage
	{
		Data _userData;
		Patient _patient;
		public FollowPatientPage (Data userData, Patient patient)
		{
			_userData = userData;
			_patient = patient;
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
            Professional[] professional = ProfessionalArray();
            double X = 0.6;
            double Y = 0.0;
            for (int i = 0; i < 3; i++)
            {
				if( professional[i] != null)
				{
					
					proImage[i] = new Image();
					proImage[i].Source = professional[i].Photo;
					proImage [i].Scale = 0.75;
					AbsoluteLayout.SetLayoutFlags(proImage[i], AbsoluteLayoutFlags.PositionProportional);
					AbsoluteLayout.SetLayoutBounds(proImage[i], new Rectangle(X, Y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
					photoLayout.Children.Add(proImage[i]);
					X = X - 0.2;
					Y = Y + 0.2;
				}
            }
	            for (int i = 3; i < 6; i++)
	            {
					if( professional[i] != null)
					{
						X = X + 0.2;
						proImage[i] = new Image();
		                proImage[i].Source = professional[i].Photo;
						proImage [i].Scale = 0.75;
		                AbsoluteLayout.SetLayoutFlags(proImage[i], AbsoluteLayoutFlags.PositionProportional);
		                AbsoluteLayout.SetLayoutBounds(proImage[i], new Rectangle(X, Y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
		                photoLayout.Children.Add(proImage[i]);
						Y = Y + 0.2;
	            	}
				}
	            for (int i = 6; i < 8; i++)
	            {
					if( professional[i] != null)
					{
		                
						proImage[i] = new Image();
		                proImage[i].Source = professional[i].Photo;
						proImage [i].Scale = 0.75;
		                AbsoluteLayout.SetLayoutFlags(proImage[i], AbsoluteLayoutFlags.PositionProportional);
		                AbsoluteLayout.SetLayoutBounds(proImage[i], new Rectangle(X, Y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
		                photoLayout.Children.Add(proImage[i]);
						X = X + 0.2;
						Y = Y - 0.2;
					}
	            }
	            for (int i = 8; i < 10; i++)
	            {
					if( professional[i] != null)
					{
		                
		                
						proImage[i] = new Image();
		                proImage[i].Source = professional[i].Photo;
						proImage [i].Scale = 0.5;
		                AbsoluteLayout.SetLayoutFlags(proImage[i], AbsoluteLayoutFlags.PositionProportional);
		                AbsoluteLayout.SetLayoutBounds(proImage[i], new Rectangle(X, Y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
		                photoLayout.Children.Add(proImage[i]);
						X = X - 0.2;
						Y = Y - 0.2;
					}
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



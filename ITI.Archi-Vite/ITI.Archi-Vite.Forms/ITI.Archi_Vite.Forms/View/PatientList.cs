using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    public class PatientList : ContentPage
    {
        List<Patient> _myPatient;
        Data _userData;
        public PatientList(Data userData)
        {
            _userData = userData;
			Button profilButton = new Button
			{
				Text = "Mon Profil",
				BackgroundColor = Color.White,
				BorderColor = Color.Black,
				TextColor = Color.Black,
				FontSize = 30,
			};
			profilButton.Clicked += async (sender, e) =>
			{
				await Navigation.PushAsync(new ProfilPage(_userData, _userData.User));
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
			Label myFollow = new Label {
				Text = "Mes Suivis",
				FontSize = 50,
				HorizontalOptions = LayoutOptions.Center
			};
            CreateMyPatient();
            ListView patientListView = new ListView
            {
                ItemsSource = _myPatient,
				SeparatorColor = Color.Black,
				RowHeight = 150,
                ItemTemplate = new DataTemplate(() =>
                {
                    Label firstName = new Label();
                    firstName.SetBinding(Label.TextProperty, "FirstName");
					firstName.FontSize = 40;

                    Label spaceLabel = new Label();
                    spaceLabel.Text = "  ";

                    Label lastName = new Label();
                    lastName.SetBinding(Label.TextProperty, "LastName");
                    lastName.FontSize = 40;

                    Image patientImage = new Image();
					patientImage.SetBinding(Image.SourceProperty, "Photo");
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
							Orientation = StackOrientation.Horizontal,
							HorizontalOptions = LayoutOptions.Center,
							VerticalOptions = LayoutOptions.Center,
                            Children =
                            {
                                
                                new StackLayout
                                {
                                    Spacing = 0,
                                    Children =
                                    {
										patientImage,
                                        firstName,
                                        spaceLabel,
                                        lastName
                                    },
									Orientation = StackOrientation.Horizontal,
									VerticalOptions = LayoutOptions.Center,
									HorizontalOptions = LayoutOptions.Center,
                                }
								
                            }
                        }
                    };
                })
                
            };
			this.BackgroundColor = Color.White;
			this.Content = new StackLayout
			{
				Children = 
				{
					buttonStack,
					myFollow,
					patientListView
				}
			};
            patientListView.ItemTapped += PatientListView_ItemTapped;
        }

        private async void PatientListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var user = e.Item as User;
            if(user != null)
            {
                Patient patient = new Patient(user);
                await Navigation.PushAsync(new FollowPatientPage(_userData, patient));
            }
        }

        private void CreateMyPatient()
        {
            _myPatient = new List<Patient>();
            foreach(var dictionaryPatient in _userData.Follow.Keys)
            {
                _myPatient.Add(dictionaryPatient);
            }
        }
    }
}

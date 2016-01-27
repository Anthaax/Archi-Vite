using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    public class PatientListPage : ContentPage
    {
        List<Patient> _myPatient;
        Data _userData;
        public PatientListPage(Data userData)
        {
            _userData = userData;
            MultibleButtonView button = new MultibleButtonView(_userData);

            button.FollowIsDisable();
            button.DocumentButton.Clicked += DocumentsButton_Clicked;
            button.ProfilButton.Clicked += ProfilButtonClicked;

            Label myFollow = new Label {
				Text = "Mes Suivis",
				FontSize = 25,
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
					firstName.FontSize = 25;

                    Label spaceLabel = new Label();
                    spaceLabel.Text = "  ";

                    Label lastName = new Label();
                    lastName.SetBinding(Label.TextProperty, "LastName");
                    lastName.FontSize = 25;

                    Image patientImage = new Image();
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
					button.Content,
					myFollow,
					patientListView
				}
			};
            patientListView.ItemTapped += PatientListView_ItemTapped;
        }

        private async void DocumentsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DocumentsPage(_userData));
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
		private bool PageForPatient()
		{
			foreach(var patient in _userData.Follow.Keys )
			{
				if (patient.UserId == _userData.User.UserId) return true;
			}
			return false;
		}
        private async void ProfilButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilPage(_userData, _userData.User));
        }
    }
}

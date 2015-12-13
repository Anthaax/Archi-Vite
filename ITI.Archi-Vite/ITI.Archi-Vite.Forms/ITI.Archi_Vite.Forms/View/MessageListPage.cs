using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    public class MessageListPage : ContentPage
    {
        Data _userData;
        List<Message> _message;
        public MessageListPage(Data userData)
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
            Button followButton = new Button
            {
                Text = "Mes Suivis",
                BackgroundColor = Color.Gray,
                BorderColor = Color.Black,
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
            };

            followButton.Clicked += FollowButtonClicked;

            Button documentsButton = new Button
            {
                Text = "Mes Documents",
                BackgroundColor = Color.White,
                BorderColor = Color.Black,
                FontSize = 30,
                TextColor = Color.Black
            };
            StackLayout buttonStack = new StackLayout
            {

                Children = {
                    profilButton,
                    followButton,
                    documentsButton
                },
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Start

            };
            Label myFollow = new Label
            {
                Text = "Mes Suivis",
                FontSize = 50,
                HorizontalOptions = LayoutOptions.Center
            };
            CreateMyMessage();
            ListView messageListView = new ListView
            {
                ItemsSource = _message,
                SeparatorColor = Color.Black,
                RowHeight = 150,
                ItemTemplate = new DataTemplate(() =>
                {
                    Label title = new Label();
                    title.SetBinding(Label.TextProperty, "Title");
					title.FontSize = 25;
					title.TextColor = Color.Black;
					
                    Label senderName = new Label();
                    senderName.SetBinding(Label.TextProperty, "SenderFullName");
					senderName.FontSize = 25;
					senderName.TextColor = Color.Black;

                    Label patientName = new Label();
                    senderName.SetBinding(Label.TextProperty, "PatientFullName");
					senderName.FontSize = 25;
					senderName.TextColor = Color.Black;

					Label spaceLabel = new Label();
					spaceLabel.Text = "  ";

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
                                        senderName,
										spaceLabel,
                                    	title,
										spaceLabel,
                                        patientName
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
                    messageListView
                }
            };
        }

        private void CreateMyMessage()
        {
            _message = _userData.Documents.Messages;
        }

        private async void FollowButtonClicked(object sender, EventArgs e)
        {
            if (PageForPatient())
            {
                Patient patient = new Patient(_userData.User);
                await Navigation.PushAsync(new FollowPatientPage(_userData, patient));
            }
            else await Navigation.PushAsync(new PatientList(_userData));
        }

        private bool PageForPatient()
        {
            foreach (var patient in _userData.Follow.Keys)
            {
                if (patient.UserId == _userData.User.UserId) return true;
            }
            return false;
        }
    }
}

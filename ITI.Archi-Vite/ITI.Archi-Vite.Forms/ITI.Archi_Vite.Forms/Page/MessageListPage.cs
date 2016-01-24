using System;
using System.Collections.Generic;

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

            MultibleButtonView button = new MultibleButtonView(_userData);

            button.DocumentsIsDisable();
            button.FollowButton.Clicked += FollowButtonClicked;
            button.ProfilButton.Clicked += ProfilButtonClicked;

            Label myFollow = new Label
            {
                Text = "Mes Messages",
                FontSize = 50,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Gray
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
					title.FontSize = 20;
					title.TextColor = Color.Gray;

					
                    Label senderName = new Label();
                    senderName.SetBinding(Label.TextProperty, "SenderName");
					senderName.FontSize = 20;
					senderName.TextColor = Color.Gray;


                    Label patientName = new Label();
                    patientName.SetBinding(Label.TextProperty, "PatientFullName");
                    patientName.FontSize = 20;
					patientName.TextColor = Color.Gray;


					Label patient = new Label();
					patient.Text = "Patient : "; 
					patient.FontSize = 20;
					patient.TextColor = Color.Gray;

					Label spaceLabel = new Label();
					spaceLabel.Text = "  ";
					spaceLabel.FontSize = 20;
					spaceLabel.TextColor = Color.Gray;

					Label pro = new Label();
					pro.Text = " De : ";
					pro.FontSize = 20;
					pro.TextColor = Color.Gray;


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
										patient,
										patientName,
										spaceLabel,
                                    	title,
										pro,
										senderName
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
			Button document = new Button
			{
				Text = "Voir mes documents",
				FontSize = 40,
				BackgroundColor = Color.FromHex("439DFE"),
				VerticalOptions = LayoutOptions.End
			};
			document.Clicked += Document_Clicked;
            this.BackgroundColor = Color.White;
            this.Content = new StackLayout
            {
                Children =
                {
                    button.Content,
                    myFollow,
                    messageListView
                }
            };
            messageListView.ItemTapped += MessageListView_ItemTapped;
        }

        private async void Document_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DocumentsPage(_userData));
        }

        private async void MessageListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var message = e.Item as Message;
            if(message != null)
            {
                await Navigation.PushAsync(new MessagePage(_userData, message));
            }
        }

        private void CreateMyMessage()
        {
            _message = _userData.Documents.Messages;
            _message.Reverse();
        }

        private async void FollowButtonClicked(object sender, EventArgs e)
        {
            if (PageForPatient())
            {
                Patient patient = new Patient(_userData.User);
                await Navigation.PushAsync(new FollowPatientPage(_userData, patient));
            }
            else await Navigation.PushAsync(new PatientListPage(_userData));
        }

        private bool PageForPatient()
        {
            foreach (var patient in _userData.Follow.Keys)
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

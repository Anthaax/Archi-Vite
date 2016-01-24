using System;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    public class MessagePage : ContentPage
    {
        Data _userData;
        Message _message;
        public MessagePage(Data userData, Message message)
        {
            _userData = userData;
            _message = message;

            MultibleButtonView button = new MultibleButtonView(_userData);

            button.DocumentsIsDisable();
            button.FollowButton.Clicked += FollowButtonClicked;
            button.ProfilButton.Clicked += ProfilButtonClicked;

            Label messageLabel = new Label()
            {
                Text = "Message",
                FontSize = 40,
                TextColor = Color.Gray,
				HorizontalOptions = LayoutOptions.Center,
            };

            Label titleLabel = new Label()
            {
                Text = "Titre : " + _message.Title,
                FontSize = 40,
                TextColor = Color.Gray,
				HorizontalOptions = LayoutOptions.StartAndExpand,
            };

            Label senderLabel = new Label()
            {
                Text = "De : " + _message.SenderName,
                FontSize = 40,
                TextColor = Color.Gray,
				HorizontalOptions = LayoutOptions.StartAndExpand,
            };

            Label ContentLabel = new Label()
            {
                Text = _message.Contents,
                FontSize = 30,
                TextColor = Color.Gray,
                VerticalOptions = LayoutOptions.CenterAndExpand,
				HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            Label recieversLabel = new Label()
            {
                Text = "À : " + _message.Recievers.Count + " personne(s)",
                FontSize = 40,
                TextColor = Color.Gray,
				HorizontalOptions = LayoutOptions.StartAndExpand,
            };

			Label patientLabel = new Label () {
				Text = "Patient : " + _message.PatientFullName,
				FontSize = 40,
				TextColor = Color.Gray,
				HorizontalOptions = LayoutOptions.StartAndExpand,
			};

            Label dateLabel = new Label()
            {
                Text = "Date : " + _message.Date.ToString(),
                FontSize = 40,
                TextColor = Color.Gray,
				HorizontalOptions = LayoutOptions.StartAndExpand,
            };

            Button returnButton = new Button()
            {
                Text = "Retour à mes messages",
                FontSize = 40,
                BackgroundColor = Color.FromHex("439DFE"),
                VerticalOptions = LayoutOptions.End
            };
            returnButton.Clicked += ReturnButton_Clicked;

            Content = new StackLayout
            {
                Children =
                {
                    button.Content,
                    messageLabel,
                    titleLabel,
                    senderLabel,
                    recieversLabel,
					patientLabel,
                    dateLabel,
                    ContentLabel,
                    returnButton
                }
            };
            this.BackgroundColor = Color.White;
        }

        private async void ReturnButton_Clicked(object sender, EventArgs e)
        {
           await Navigation.PushAsync(new MessageListPage(_userData));
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

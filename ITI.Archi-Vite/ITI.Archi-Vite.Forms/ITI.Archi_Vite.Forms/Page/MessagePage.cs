using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

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
                BackgroundColor = Color.White,
                BorderColor = Color.Black,
                FontSize = 30,
                TextColor = Color.Black
            };
            if (PageForPatient()) followButton.Text = "Mon Suivis";

            followButton.Clicked += FollowButtonClicked;
            Button documentsButton = new Button
            {
                Text = "Mes Documents",
                BackgroundColor = Color.Gray,
                BorderColor = Color.Black,
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
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
				Text = "À propos de : " + _message.PatientFullName,
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
                    buttonStack,
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
            await Navigation.PushAsync(new MessageListPage(_userData));
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

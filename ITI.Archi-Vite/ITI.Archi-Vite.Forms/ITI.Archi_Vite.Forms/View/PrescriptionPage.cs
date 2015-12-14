using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    public class PrescriptionPage : ContentPage
    {
        Data _userData;
        Prescription _prescription;
        public PrescriptionPage(Data userData, Prescription prescription)
        {
            _userData = userData;
            _prescription = prescription;

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
                Text = "Titre : " + _prescription.Title,
                FontSize = 40,
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };

            Label senderLabel = new Label()
            {
                Text = "De : " + _prescription.SenderName,
                FontSize = 40,
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };

            Image ContentLabel = new Image()
            {
                Source = _prescription.DocPath,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            Label recieversLabel = new Label()
            {
                Text = "À : " + _prescription.Receivers.Count + " personne(s)",
                FontSize = 40,
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };

            Label patientLabel = new Label()
            {
                Text = "À propos de : " + _prescription.PatientFullName,
                FontSize = 40,
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };

            Label dateLabel = new Label()
            {
                Text = "Date : " + _prescription.Date.ToString(),
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
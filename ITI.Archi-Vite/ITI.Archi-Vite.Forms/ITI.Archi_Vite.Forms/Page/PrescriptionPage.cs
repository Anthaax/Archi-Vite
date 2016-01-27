using ModernHttpClient;
using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection.Emit;
using System.Text;
using Newtonsoft.Json;

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

            MultibleButtonView button = new MultibleButtonView(_userData);

            button.DocumentsIsDisable();
            button.FollowButton.Clicked += FollowButtonClicked;
            button.ProfilButton.Clicked += ProfilButtonClicked;

            Label messageLabel = new Label()
            {
                Text = "Prescription",
                FontSize = 20,
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.Center,
            };

            Label titleLabel = new Label()
            {
                Text = "Titre : " + _prescription.Title,
				FontSize = 20,
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };

            Label senderLabel = new Label()
            {
                Text = "De : " + _prescription.SenderName,
				FontSize = 20,
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };
            Stream s = new MemoryStream(DependencyService.Get<IBytesSaveAndLoad>().LoadByteArray(_prescription.DocPath));

            Image ContentLabel = new Image()
            {
                Source = ImageSource.FromStream(() => s),
                HeightRequest = 200,
                WidthRequest = 200,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            Label recieversLabel = new Label()
            {
                Text = "À : " + _prescription.Recievers.Count + " personne(s)",
				FontSize = 20,
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };

            Label patientLabel = new Label()
            {
                Text = "Patient : " + _prescription.PatientFullName,
				FontSize = 20,
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };

            Label dateLabel = new Label()
            {
                Text = "Date : " + _prescription.Date.ToString(),
				FontSize = 20,
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };

            Button returnButton = new Button()
            {
                Text = "Retour à mes prescriptions",
				FontSize = 20,
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
            await Navigation.PushAsync(new PrescriptionListPage(_userData));
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
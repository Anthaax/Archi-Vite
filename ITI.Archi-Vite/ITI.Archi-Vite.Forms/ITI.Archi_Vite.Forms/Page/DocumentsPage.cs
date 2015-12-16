using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms.View
{
    public class DocumentsPage : ContentPage
    {
        Data _userData;
        public DocumentsPage(Data userData)
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
                BorderColor = Color.White,
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
            };
            if (PageForPatient()) followButton.Text = "Mon Suivis";

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
                Text = "Mes Documents",
                FontSize = 50,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Gray
            };

            Button messages = new Button
            {
                Text = _userData.Documents.Messages.Count.ToString(),
                FontSize = 40,
                BackgroundColor = Color.FromHex("439DFE"),
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            messages.Clicked += Messages_Clicked;

            Button prescription = new Button
            {
                Text = _userData.Documents.Prescriptions.Count.ToString(),
                FontSize = 40,
                BackgroundColor = Color.FromHex("439DFE"),
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            prescription.Clicked += Prescription_Clicked;
        }
        private async void Prescription_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PrescriptionListPage(_userData));
        }

        private async void Messages_Clicked(object sender, EventArgs e)
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

using System;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    public class DocumentsPage : ContentPage
    {
        Data _userData;
        public DocumentsPage(Data userData)
        {
            _userData = userData;
            MultibleButtonView button = new MultibleButtonView(_userData);

            button.DocumentsIsDisable();
            button.FollowButton.Clicked += FollowButtonClicked;
            button.ProfilButton.Clicked += ProfilButtonClicked;

            Label myFollow = new Label
            {
                Text = "Mes Documents",
                FontSize = 50,
				VerticalOptions = LayoutOptions.CenterAndExpand,
                TextColor = Color.Gray
            };

            Button messages = new Button
            {
				Text = "Nombre de messages : " + _userData.Documents.Messages.Count.ToString(),
                FontSize = 40,
                BackgroundColor = Color.FromHex("439DFE"),
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            messages.Clicked += Messages_Clicked;

            Button prescription = new Button
            {
				Text = "Nombre de prescriptions : " + _userData.Documents.Prescriptions.Count.ToString(),
                FontSize = 40,
                BackgroundColor = Color.FromHex("439DFE"),
                VerticalOptions = LayoutOptions.CenterAndExpand
            };
            prescription.Clicked += Prescription_Clicked;
            Content = new StackLayout
            {

                Children = {
                    button.Content,
                    messages,
                    prescription
                },
            };
            this.BackgroundColor = Color.White;
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

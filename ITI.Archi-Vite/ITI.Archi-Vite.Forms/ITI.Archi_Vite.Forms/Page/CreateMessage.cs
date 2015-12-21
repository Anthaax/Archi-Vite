using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    class CreateMessage : ContentPage
    {
        Data _userData;
        Patient _patient;
		List<Professional> _recievers;

		public CreateMessage(Data userData, Patient patient, List<Professional> recievers)
        {
            _userData = userData;
            _patient = patient;
            if (recievers != null)
                _recievers = recievers;
			else recievers = new List<Professional>();
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
                TextColor = Color.Black,
                FontSize = 30,
            };
            if (PageForPatient()) followButton.Text = "Mon Suivi";

            followButton.Clicked += FollowButtonClicked;

            Button documentsButton = new Button
            {
                Text = "Mes Documents",
                BackgroundColor = Color.Gray,
                BorderColor = Color.White,
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
            Label message = new Label
            {
                Text = "Creation de message",
                FontSize = 50,
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Gray
            };
            Label recivers = new Label
            {
                Text = "À : " + recievers.Count.ToString() + " personnes",
                FontSize = 40,
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Gray
            };
            Button add = new Button
            {
                Text = "+",
                FontSize = 40,
                BackgroundColor = Color.FromHex("439DFE"),
            };
            add.Clicked += Add_Clicked;
            StackLayout addRecieverStack = new StackLayout
            {

                Children = {
                    recivers,
                    add,
                },
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Start

            };
            Entry title = new Entry
            {
                Placeholder = "Titre du message",
                FontSize = 40,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            title.TextChanged += EntryTextChanged;

            Editor content = new Editor
            {
                FontSize = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            content.TextChanged += Content_TextChanged;

            Button create = new Button
            {
                Text = "Envoyer",
                FontSize = 40,
                BackgroundColor = Color.FromHex("439DFE"),
                VerticalOptions = LayoutOptions.End
            };
            create.Clicked += Create_Clicked;

            Button back = new Button
            {
                Text = "retour au message",
                FontSize = 40,
                BackgroundColor = Color.FromHex("439DFE"),
                VerticalOptions = LayoutOptions.End
            };
            back.Clicked += Back_Clicked;

            Content = new StackLayout
            {

                Children = {
                    buttonStack,
                    message,
                    title,
                    addRecieverStack,
                    content,
                    create,
                    back
                },
            };
            this.BackgroundColor = Color.White;
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AddReciverPage(_userData, _patient, null));
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MessageListPage(_userData));
        }

        private async void Create_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Envoi", "Le message à été envoyé", "OK");
            await Navigation.PushAsync(new MessageListPage(_userData));
        }

        private void Content_TextChanged(object sender, TextChangedEventArgs e)
        {
            Editor entry = sender as Editor;
            if (entry != null)
            {
                entry.TextColor = Color.Gray;
            }
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
        private void EntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            if (entry != null)
            {
                entry.TextColor = Color.Gray;
            }
        }
    }
}

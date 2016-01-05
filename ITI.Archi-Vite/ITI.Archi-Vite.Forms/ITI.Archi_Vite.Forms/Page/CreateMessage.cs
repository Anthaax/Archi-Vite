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
        Entry _title;
        Editor _content;
        DataConvertor _convertor = new DataConvertor();
        public CreateMessage(Data userData, Patient patient, List<Professional> recievers, string title, string content)
        {
            _userData = userData;
            _patient = patient;

            MultibleButtonView button = new MultibleButtonView(_userData);

            button.DocumentsIsDisable();
            button.FollowButton.Clicked += FollowButtonClicked;
            button.ProfilButton.Clicked += ProfilButtonClicked;

            Label message = new Label
            {
                Text = "Creation de message",
                FontSize = 50,
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Gray
            };
            
            _title = new Entry
            {
                Placeholder = "Titre du message",
                FontSize = 40,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            _title.TextChanged += EntryTextChanged;

            _content = new Editor
            {
                FontSize = 20,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            _content.TextChanged += Content_TextChanged;
			Initiliaze(title, content, recievers);
			Label recivers = new Label
			{
				Text = "À : " + _recievers.Count.ToString() + " personnes",
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
                    button.Content,
                    message,
                    _title,
                    addRecieverStack,
                    _content,
                    create,
                    back
                },
            };
            this.BackgroundColor = Color.White;
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
			await Navigation.PushAsync(new AddReciverPage(_userData, _patient, null, _title.Text, _content.Text, true));
        }

        private void Initiliaze(string title, string content, List<Professional> recievers)
        {
            _title.Text = title;
            _content.Text = content;
            if (recievers != null)
                _recievers = recievers;
            else
                _recievers = new List<Professional>();
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MessageListPage(_userData));
        }

        private async void Create_Clicked(object sender, EventArgs e)
        {
            Message message = GetMessage();
            MessageAdd(message);
            SaveUserData();
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
        private void EntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            if (entry != null)
            {
                entry.TextColor = Color.Gray;
            }
        }
        private void MessageAdd( Message m )
        {
            _userData.Documents.Messages.Add(m);
			_userData.DocumentsAdded.Messages.Add (m);
        }
        private Message GetMessage()
        {
            return new Message(_title.Text, _content.Text, _userData.User, _recievers, _patient);
        }
        public void SaveUserData()
        {
            DataJson json = _convertor.DataToDataJson(_userData);
			DependencyService.Get<ISaveLoadAndDelete>().SaveData("user.txt", json);
        }
        private async void ProfilButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilPage(_userData, _userData.User));
        }
    }
}

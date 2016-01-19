using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    public class CreatePrescription : ContentPage
    {
        Data _userData;
        Patient _patient;
        List<Professional> _recievers;
        Entry _title;
        Button _takePhoto;
        Button _choosePhoto;
        CameraViewModel _cameraview;
        Image _photo;
        DataConvertor _convertor = new DataConvertor();
        string _docpath;
        public CreatePrescription(Data userData, Patient patient, List<Professional> recievers, string title, string docPath)
        {
            _userData = userData;
            _patient = patient;
			if (recievers != null)
				_recievers = recievers;
			else
				_recievers = new List<Professional>();
            _docpath = docPath;
            _cameraview = new CameraViewModel();
            MultibleButtonView button = new MultibleButtonView(_userData);

            button.DocumentsIsDisable();
            button.FollowButton.Clicked += FollowButtonClicked;
            button.ProfilButton.Clicked += ProfilButtonClicked;

            Label prescription = new Label
            {
                Text = "Creation de Prescription",
                FontSize = 50,
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Gray
            };
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
            _title = new Entry
            {
                Placeholder = "Titre de la prescription",
                FontSize = 40,
                HorizontalTextAlignment = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.Center,
            };
            _title.TextChanged += EntryTextChanged;

            _takePhoto = new Button
            {
                Text = "Prendre une photo",
				BackgroundColor = Color.FromHex("439DFE"),
                FontSize = 25,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            _takePhoto.Clicked += Content_Clicked;
            _choosePhoto = new Button
            {
                Text = "Choisir une image éxistante",
				BackgroundColor = Color.FromHex("439DFE"),
                FontSize = 25,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            _choosePhoto.Clicked += _choosePhoto_Clicked;
            StackLayout takePhotoStack = new StackLayout
            {

                Children = {
                    _takePhoto,
                    _choosePhoto,
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
                Text = "Retour aux prescriptions",
                FontSize = 40,
                BackgroundColor = Color.FromHex("439DFE"),
                VerticalOptions = LayoutOptions.End
            };
            back.Clicked += Back_Clicked;
			Initiliaze(title, docPath);

            Content = new StackLayout
            {

                Children = {
                    button.Content,
                    prescription,
                    _title,
                    addRecieverStack,
                    takePhotoStack,
                    _photo,
                    create,
                    back
                },
            };
            this.BackgroundColor = Color.White;
        }

        private void Initiliaze (string title, string docPath)
        {
			if (title != null)
				_title.Text = title;
			else
				_title.Text = "";
            _photo = new Image
            {
                Source = docPath
            };
        }

        private async void _choosePhoto_Clicked(object sender, EventArgs e)
        {
            await _cameraview.SelectPicture();
            _photo.Source = _cameraview.ImageSource;
        }

        private async void Content_Clicked(object sender, EventArgs e)
        {
            await _cameraview.TakePicture();
            _photo.Source = _cameraview.ImageSource;
            _docpath =TostringSource(_photo);
        }

        private async void Add_Clicked(object sender, EventArgs e)
        {
            if(_docpath == null)
			    await Navigation.PushAsync(new AddReciverPage(_userData, _patient, null, _title.Text, _docpath, false));
            else
                await Navigation.PushAsync(new AddReciverPage(_userData, _patient, null, _title.Text, TostringSource(_photo), false));
        }

        private async void Back_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MessageListPage(_userData));
        }

        private async void Create_Clicked(object sender, EventArgs e)
        {
			Prescription p = GetPrescription();
			PrescriptionAdd (p);
			SaveUserData ();
            await DisplayAlert("Envoi", "Le message à été envoyé", "OK");
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

		private void PrescriptionAdd( Prescription p )
		{
			_userData.Documents.Prescriptions.Add(p);
			_userData.DocumentsAdded.Prescriptions.Add (p);
		}

		private void MessageAdd( Message m )
		{
			_userData.Documents.Messages.Add(m);
			_userData.DocumentsAdded.Messages.Add (m);
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
        private async void MessageAdd(Prescription p)
        {
            if(TostringSource(_photo)!= null)
                _userData.Documents.Prescriptions.Add(p);
            else
                await DisplayAlert("Envoi", "Le message à été envoyé", "OK");

        }
		private Prescription GetPrescription()
        {
            return new Prescription(_title.Text, TostringSource(_photo), _userData.User, _recievers, _patient);
        }
        private string TostringSource(Image i)
        {
            var source = i.Source as UriImageSource;
            if (source != null)
            {
                return source.Uri.ToString();
            }
            var s = i.Source as FileImageSource;
            if (s != null)
            {
                return s.File.ToString();
            }
            return null;
        }
        public void SaveUserData()
        {
            DataXML json = _convertor.DataToDataJsonForSave(_userData);
			DependencyService.Get<ISaveLoadAndDelete>().SaveData("user.txt", json);
        }
        private async void ProfilButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilPage(_userData, _userData.User));
        }
    }
}

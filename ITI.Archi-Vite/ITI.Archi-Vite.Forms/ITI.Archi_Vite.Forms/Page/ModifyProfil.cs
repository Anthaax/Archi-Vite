using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using Xamarin.Forms;
using XLabs.Forms.Mvvm;
using XLabs.Ioc;
using XLabs.Platform.Device;
using XLabs.Platform.Services.Media;

namespace ITI.Archi_Vite.Forms
{
	public class ModifyProfil : ContentPage
	{
		Image _profilPhoto;
		Data _userData;
        CameraViewModel _cameraview;
		public ModifyProfil (Data userData)
		{
			_userData = userData;
            _cameraview = new CameraViewModel();
            var tappedGesture = new TapGestureRecognizer();
            tappedGesture.Tapped += TappedGesture_Tapped;

            MultibleButtonView button = new MultibleButtonView(_userData);

            button.ProfilIsDisable();
            button.FollowButton.Clicked += FollowButtonClicked;
            button.DocumentButton.Clicked += DocumentButton_Clicked;

            Entry firstName = new Entry
			{
				Text = _userData.User.FirstName,
				FontSize = 40,
				HorizontalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				TextColor = Color.Gray
			};
			firstName.TextChanged += EntryTextChanged;
			Entry lastName = new Entry
			{
				Text = _userData.User.LastName,
				FontSize = 40,
				HorizontalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				TextColor = Color.Gray
			};
			Entry adress = new Entry
			{
				Text = _userData.User.Adress,
				FontSize = 40,
				HorizontalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				TextColor = Color.Gray
			};
			Entry postCode = new Entry
			{
				Text = _userData.User.Postcode.ToString(),
				FontSize = 40,
				Keyboard = Keyboard.Numeric,
				HorizontalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				TextColor = Color.Gray
			};

			Entry city = new Entry
			{
				Text = _userData.User.City,
				FontSize = 40,
				HorizontalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				TextColor = Color.Gray
			};
			Entry phoneNumber = new Entry
			{
				Text = _userData.User.PhoneNumber.ToString(),
				FontSize = 40,
				Keyboard = Keyboard.Telephone,
				HorizontalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				TextColor = Color.Gray
			};
            Stream s = new MemoryStream(DependencyService.Get<IBytesSaveAndLoad>().LoadByteArray(_userData.User.Photo));
            _profilPhoto = new Image {
                Source = ImageSource.FromStream(() => s),
            };
			_profilPhoto.GestureRecognizers.Add (tappedGesture);

			Button modify = new Button
			{
				Text = "Sauvegarder",
				FontSize = 40,
				BackgroundColor = Color.FromHex("439DFE"),
				VerticalOptions = LayoutOptions.EndAndExpand,
                
			};
            Button photo = new Button
            {
                Text = "Prendre une photo",
                FontSize = 40,
                BackgroundColor = Color.FromHex("439DFE"),
                VerticalOptions = LayoutOptions.EndAndExpand,
        	};
			photo.Clicked += Photo_Clicked;
            modify.Clicked += async (sender, e) => 
			{
				_userData.User.Adress = adress.Text;
				_userData.User.City = city.Text;
				_userData.User.FirstName = firstName.Text;
				_userData.User.LastName = lastName.Text;
				_userData.User.PhoneNumber = Int32.Parse(phoneNumber.Text);
				_userData.User.Postcode = Int32.Parse(postCode.Text);
                UpdateAll();
				await Navigation.PushAsync(new ProfilPage(_userData, _userData.User));
			};
			Content = new StackLayout
			{

				Children = {
					button,
					firstName,
					lastName,
					adress,
					postCode,
					city,
					phoneNumber,
					_profilPhoto,
                    photo,
					modify
				},
			};
			this.BackgroundColor = Color.White;
		}

        private async void DocumentButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DocumentsPage(_userData));
        }

        async void Photo_Clicked (object sender, EventArgs e)
		{
			await _cameraview.TakePicture();
			_profilPhoto.Source = _cameraview.ImageSource;
		}

        private async void TappedGesture_Tapped(object sender, EventArgs e)
        {
            await _cameraview.SelectPicture();
            _profilPhoto.Source = _cameraview.ImageSource;
        }

        public Image ProfilPhoto
		{
			get { return _profilPhoto; }
		}
		public 
		void EntryTextChanged (object sender, TextChangedEventArgs e)
		{
			Entry entry = sender as Entry;
			if (entry != null)
			{
				entry.TextColor = Color.Gray;
			}
		}
		private bool PageForPatient()
		{
			foreach(var patient in _userData.Follow.Keys )
			{
				if (patient.UserId == _userData.User.UserId) return true;
			}
			return false;
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
        private bool CheckResponse(HttpResponseMessage response)
        {
            return response.IsSuccessStatusCode;
        }
        private async void UpdateAll()
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                if (_userData.NeedUpdate && _userData.DocumentsAdded.Messages.Count == 0 && _userData.DocumentsAdded.Prescriptions.Count == 0 )
                {
                    DataConvertor d = new DataConvertor();
                    DocumentSerializableXML doc = await HttpRequest.HttpRequestSetDocument(d.CreateDocumentSerializable(_userData.DocumentsAdded));
                    _userData.NeedUpdate = false;
                    _userData.DocumentsAdded.Messages = new List<Message>();
                    _userData.DocumentsAdded.Prescriptions = new List<Prescription>();
                }
                var response = await HttpRequest.HttpRequestSetUserData(_userData);
                if(!response.IsSuccessStatusCode)
                {
                    _userData.NeedUpdate = true;
                }
            }

        }
    }
}



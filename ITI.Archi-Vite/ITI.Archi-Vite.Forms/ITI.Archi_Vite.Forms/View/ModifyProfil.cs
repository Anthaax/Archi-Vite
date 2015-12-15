using System;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
	public class ModifyProfil : ContentPage
	{
		Image _profilPhoto;
		Data _userData;
		public ModifyProfil (Data userData)
		{
			_userData = userData;
			Button profilButton = new Button {
				Text = "Mon Profil",
				BackgroundColor = Color.Gray,
				BorderColor = Color.Black,
				FontSize = 30,
				FontAttributes = FontAttributes.Bold,
			};

			Button followButton = new Button {
				Text = "Mes Suivis",
				BackgroundColor = Color.White,
				BorderColor = Color.Black,
				FontSize = 30,
				TextColor = Color.Black
			};
			if (PageForPatient()) followButton.Text = "Mon Suivis";

			followButton.Clicked += FollowButtonClicked;

			Button documentsButton = new Button {
				Text = "Mes Documents",
				BackgroundColor = Color.White,
				BorderColor = Color.Black,
				FontSize = 30,
				TextColor = Color.Black
			};

			StackLayout buttonStack = new StackLayout {

				Children = {
					profilButton,
					followButton,
					documentsButton
				},
				Orientation = StackOrientation.Horizontal,					
				HorizontalOptions = LayoutOptions.Start

			};
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
			_profilPhoto = new Image {
				Source = _userData.User.Photo
			};
			Button modify = new Button
			{
				Text = "Sauvegarder",
				FontSize = 40,
				BackgroundColor = Color.FromHex("439DFE"),
				VerticalOptions = LayoutOptions.EndAndExpand
			};
			modify.Clicked += async (sender, e) => 
			{
				_userData.User.Adress = adress.Text;
				_userData.User.City = city.Text;
				_userData.User.FirstName = firstName.Text;
				_userData.User.LastName = lastName.Text;
				_userData.User.PhoneNumber = Int32.Parse(phoneNumber.Text);
				_userData.User.Postcode = Int32.Parse(postCode.Text);
				await Navigation.PushAsync(new ProfilPage(_userData, _userData.User));
			};
			Content = new StackLayout
			{

				Children = {
					buttonStack,
					firstName,
					lastName,
					adress,
					postCode,
					city,
					phoneNumber,
					_profilPhoto,
					modify
				},
			};
			this.BackgroundColor = Color.White;
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
			await Navigation.PushAsync(new MessageListPage(_userData));
		}
	}
}



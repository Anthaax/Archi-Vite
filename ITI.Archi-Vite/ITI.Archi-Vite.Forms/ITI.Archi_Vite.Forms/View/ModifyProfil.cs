using System;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
	public class ModifyProfil : ContentPage
	{
		Image _profilPhoto;
		public ModifyProfil ()
		{
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

			Button documentsButton = new Button {
				Text = "Mes Documents",
				BackgroundColor = Color.White,
				BorderColor = Color.Black,
				FontSize = 30,
				TextColor = Color.Black
			};

			StackLayout topStack = new StackLayout {

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
				Text = "Guillaume",
				FontSize = 40,
				HorizontalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				TextColor = Color.Gray
			};
			firstName.TextChanged += EntryTextChanged;
			Entry lastName = new Entry
			{
				Placeholder = "Fimes",
				FontSize = 40,
				HorizontalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				TextColor = Color.Gray
			};
			Entry adress = new Entry
			{
				Placeholder = "74 avenue morice thorez",
				FontSize = 40,
				HorizontalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				TextColor = Color.Gray
			};
			Entry postCode = new Entry
			{
				Placeholder = "94200",
				FontSize = 40,
				Keyboard = Keyboard.Numeric,
				HorizontalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				TextColor = Color.Gray
			};
			Entry city = new Entry
			{
				Placeholder = "Ivry",
				FontSize = 40,
				HorizontalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				TextColor = Color.Gray
			};
			Entry phoneNumber = new Entry
			{
				Placeholder = "0662147351",
				FontSize = 40,
				Keyboard = Keyboard.Telephone,
				HorizontalTextAlignment = TextAlignment.Center,
				HorizontalOptions = LayoutOptions.FillAndExpand,
				VerticalOptions = LayoutOptions.CenterAndExpand,
				TextColor = Color.Gray
			};
			_profilPhoto = new Image {
				Source = "http://www.go-e-lan.info/vue/images/event/fimes.PNG"
			};
			var tapGestureRecognizer = new TapGestureRecognizer();
			tapGestureRecognizer.Tapped += async (s, e) => {
				await Navigation.PushAsync(new ConnectionPage());
			};
			_profilPhoto.GestureRecognizers.Add(tapGestureRecognizer);
			Button modify = new Button
			{
				Text = "Sauvegarder",
				FontSize = 40,
				BackgroundColor = Color.FromHex("439DFE"),
				VerticalOptions = LayoutOptions.EndAndExpand
			};
			modify.Clicked += async (sender, e) => 
			{
				await Navigation.InsertPageBefore();
			};
			Content = new StackLayout
			{

				Children = {
					topStack,
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
	}
}



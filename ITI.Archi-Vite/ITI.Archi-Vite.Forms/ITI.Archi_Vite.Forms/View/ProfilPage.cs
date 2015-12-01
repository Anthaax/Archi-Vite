using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    public class ProfilPage : ContentPage
    {
		public ProfilPage()
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
			Label title = new Label {
				Text = "Profil",
				FontSize = 40,
				HorizontalOptions = LayoutOptions.Center
			};
			Label name = new Label {
				Text = "Guillaume Fimes",
				FontSize = 30,
				HorizontalOptions = LayoutOptions.Center
				
			};
			Label phonenumber = new Label
			{
				Text = "Numero : 0662147351",
				FontSize = 30,
				HorizontalOptions = LayoutOptions.Center

            };
			Label adresse = new Label
			{
				Text = "Adresse : 74 avenue morice thorez",
                FontSize = 30,
                HorizontalOptions = LayoutOptions.Center
			};
			Label postCode = new Label
			{
				Text = "94200",
				FontSize = 30,
				HorizontalOptions = LayoutOptions.Center
			};
			Label city = new Label
			{
				Text = "Ivry-Sur-Seine",
				FontSize = 30,
				HorizontalOptions = LayoutOptions.Center
			};
			Image logo = new Image
			{
				Source = "http://www.go-e-lan.info/vue/images/event/fimes.PNG",
				HorizontalOptions = LayoutOptions.FillAndExpand,
			};

            Button modify = new Button
            {
                Text = "Modifier mes infos",
                FontSize = 40,
                BackgroundColor = Color.FromHex("439DFE"),
				VerticalOptions = LayoutOptions.EndAndExpand
            };
			modify.Clicked += async (sender, e) => 
			{
				await Navigation.PushModalAsync(new ModifyProfil());
				Navigation.RemovePage(this);
			};
            Content = new StackLayout
            {

                Children = {
					topStack,
					title,
					name,
					phonenumber,
					adresse,
					postCode,
					city,
					logo,
					modify
                },
            };
            this.BackgroundColor = Color.White;
        }

        private void EntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            if (entry != null)
            {
                entry.TextColor = Color.Gray;
            }
        }

        private void OnLabelClicked()
        {
            throw new NotImplementedException();
        }
    }
}

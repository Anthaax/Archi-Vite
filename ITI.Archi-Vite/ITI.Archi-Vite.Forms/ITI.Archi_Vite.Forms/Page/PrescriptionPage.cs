﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

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
                FontSize = 40,
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.Center,
            };

            Label titleLabel = new Label()
            {
                Text = "Titre : " + _prescription.Title,
                FontSize = 40,
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };

            Label senderLabel = new Label()
            {
                Text = "De : " + _prescription.SenderName,
                FontSize = 40,
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };

            Image ContentLabel = new Image()
            {
                Source = _prescription.DocPath,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            Label recieversLabel = new Label()
            {
                Text = "À : " + _prescription.Recievers.Count + " personne(s)",
                FontSize = 40,
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };

            Label patientLabel = new Label()
            {
                Text = "À propos de : " + _prescription.PatientFullName,
                FontSize = 40,
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };

            Label dateLabel = new Label()
            {
                Text = "Date : " + _prescription.Date.ToString(),
                FontSize = 40,
                TextColor = Color.Gray,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };

            Button returnButton = new Button()
            {
                Text = "Retour à mes prescriptions",
                FontSize = 40,
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
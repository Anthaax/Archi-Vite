﻿using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    public class PrescriptionListPage : ContentPage
    {
        Data _userData;
        List<Prescription> _prescriptions;
        public PrescriptionListPage(Data userData)
        {
            _userData = userData;

            MultibleButtonView button = new MultibleButtonView(_userData);

            button.DocumentsIsDisable();
            button.FollowButton.Clicked += FollowButtonClicked;
            button.ProfilButton.Clicked += ProfilButtonClicked;

            Label myFollow = new Label
            {
                Text = "Mes Prescriptions",
                FontSize = 25,
                HorizontalOptions = LayoutOptions.Center,
                TextColor = Color.Gray
            };
            CreateMyPrescriptions();
            ListView prescriptionListView = new ListView
            {
                ItemsSource = _prescriptions,
                SeparatorColor = Color.Black,
                RowHeight = 150,
                ItemTemplate = new DataTemplate(() =>
                {
                    Label title = new Label();
                    title.SetBinding(Label.TextProperty, "Title");
                    title.FontSize = 13;
                    title.TextColor = Color.Gray;


                    Label senderName = new Label();
                    senderName.SetBinding(Label.TextProperty, "SenderName");
                    senderName.FontSize = 13;
                    senderName.TextColor = Color.Gray;


                    Label patientName = new Label();
                    patientName.SetBinding(Label.TextProperty, "PatientFullName");
                    patientName.FontSize = 13;
                    patientName.TextColor = Color.Gray;


                    Label patient = new Label();
                    patient.Text = "Patient : ";
                    patient.FontSize = 13;
                    patient.TextColor = Color.Gray;

                    Label spaceLabel = new Label();
					spaceLabel.Text = " '' ";
                    spaceLabel.FontSize = 13;
                    spaceLabel.TextColor = Color.Gray;
					
					Label spaceLabel2 = new Label();
					spaceLabel2.Text = " '' ";
					spaceLabel2.FontSize = 13;
					spaceLabel2.TextColor = Color.Gray;
					
                    Label pro = new Label();
                    pro.Text = " De : ";
                    pro.FontSize = 13;
                    pro.TextColor = Color.Gray;


                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Orientation = StackOrientation.Horizontal,
                            HorizontalOptions = LayoutOptions.Center,
                            VerticalOptions = LayoutOptions.Center,
                            Children =
                            {

                                new StackLayout
                                {
                                    Spacing = 0,
                                    Children =
                                    {
                                        patient,
                                        patientName,
                                        spaceLabel,
                                        title,
										spaceLabel2,
                                        pro,
                                        senderName
                                    },
                                    Orientation = StackOrientation.Horizontal,
                                    VerticalOptions = LayoutOptions.Center,
                                    HorizontalOptions = LayoutOptions.Center,
                                }

                            }
                        }
                    };
                })

            };
            Button document = new Button
            {
                Text = "Voir mes documents",
                FontSize = 40,
                BackgroundColor = Color.FromHex("439DFE"),
                VerticalOptions = LayoutOptions.End
            };
            document.Clicked += Document_Clicked;
            this.BackgroundColor = Color.White;
            this.Content = new StackLayout
            {
                Children =
                {
                    button.Content,
                    myFollow,
                    prescriptionListView
                }
            };
            prescriptionListView.ItemTapped += MessageListView_ItemTapped;
        }

        private async void MessageListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var prescription = e.Item as Prescription;
            if (prescription != null)
            {
                await Navigation.PushAsync(new PrescriptionPage(_userData, prescription));
            }
        }
        private async void Document_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DocumentsPage(_userData));
        }

        private void CreateMyPrescriptions()
        {
            _prescriptions = _userData.Documents.Prescriptions;
            _prescriptions.Reverse();
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

using System;
using System.Collections.Generic;
using System.IO;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    public class AddReciverPage : ContentPage
    {
        Data _userData;
        Patient _patient;
        List<Professional> _recievers;
        Professional[] professionals;
        string _title;
        string _content;
        Guid[] _imageId = new Guid[11];
        bool _message;
        TapGestureRecognizer imageTapped = new TapGestureRecognizer();

        public AddReciverPage(Data userData, Patient patient, List<Professional> recievers, string title, string content, bool message)
        {
            _userData = userData;
            _patient = patient;
            _message = message;
            Initiliaze(title, content, recievers);
            imageTapped.Tapped += Image_Taped;
            AbsoluteLayout photoLayout = new AbsoluteLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            MultibleButtonView button = new MultibleButtonView(_userData);

            button.DocumentsIsDisable();
            button.FollowButton.Clicked += FollowButtonClicked;
            button.ProfilButton.Clicked += ProfilButtonClicked;

            Image patientImage = new Image
            {
                Source = patient.Photo,
                Scale = 0.75
            };
            AbsoluteLayout.SetLayoutFlags(patientImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(patientImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            photoLayout.Children.Add(patientImage);

            Button backMessage = new Button
            {
                Text = "Retour aux messages",
                FontSize = 23,
                BackgroundColor = Color.FromHex("439DFE"),
            };
            if (!_message)
            {
                backMessage.Text = "Retour à la prescription";
            }
            backMessage.Clicked += BackMessage_Clicked;

            AbsoluteLayout.SetLayoutFlags(backMessage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(backMessage, new Rectangle(0, 1, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            photoLayout.Children.Add(backMessage);

            Image[] proImage = new Image[10];
            professionals = ProfessionalArray();
            double X = 0.5;
            double Y = 0.0;
            for (int i = 0; i < 3; i++)
            {
                proImage[i] = new Image();
                if (professionals[i] != null)
                {
                    Stream s = new MemoryStream(DependencyService.Get<IBytesSaveAndLoad>().LoadByteArray(professionals[i].Photo), true);
                    proImage[i].Scale = 0.75;
                    proImage[i] = InitializePhoto(proImage[i], i, professionals);
                    proImage[i].Source = ImageSource.FromStream(() => s);
                    _imageId.SetValue(proImage[i].Id, i);
                }
                else
                {
                    proImage[i].Source = "http://3.bp.blogspot.com/_9Q_36sq8aPo/S0D4__i1w1I/AAAAAAAAACo/cgLl5IYQtjA/s400/croix.png";
                    proImage[i].Scale = 0.75;
                }
                AbsoluteLayout.SetLayoutFlags(proImage[i], AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(proImage[i], new Rectangle(X, Y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
                photoLayout.Children.Add(proImage[i]);
                X = X - 0.2;
                Y = Y + 0.2;

            }
            for (int i = 3; i < 6; i++)
            {
                X = X + 0.2;
                proImage[i] = new Image();
                if (professionals[i] != null)
                {
                    Stream s = new MemoryStream(DependencyService.Get<IBytesSaveAndLoad>().LoadByteArray(professionals[i].Photo), true);
                    proImage[i].Scale = 0.75;
                    proImage[i] = InitializePhoto(proImage[i], i, professionals);
                    proImage[i].Source = ImageSource.FromStream(() => s);
                    _imageId.SetValue(proImage[i].Id, i);
                }
                else
                {
                    proImage[i].Source = "http://3.bp.blogspot.com/_9Q_36sq8aPo/S0D4__i1w1I/AAAAAAAAACo/cgLl5IYQtjA/s400/croix.png";
                    proImage[i].Scale = 0.75;
                }
                AbsoluteLayout.SetLayoutFlags(proImage[i], AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(proImage[i], new Rectangle(X, Y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
                photoLayout.Children.Add(proImage[i]);
                if (i != 5) Y = Y + 0.2;
            }
            for (int i = 6; i < 8; i++)
            {
                X = X + 0.2;
                Y = Y - 0.2;
                proImage[i] = new Image();
                if (professionals[i] != null)
                {
                    Stream s = new MemoryStream(DependencyService.Get<IBytesSaveAndLoad>().LoadByteArray(professionals[i].Photo), true);
                    proImage[i].Scale = 0.75;
                    proImage[i] = InitializePhoto(proImage[i], i, professionals);
                    proImage[i].Source = ImageSource.FromStream(() => s);
                    _imageId.SetValue(proImage[i].Id, i);
                }
                else
                {
                    proImage[i].Source = "http://3.bp.blogspot.com/_9Q_36sq8aPo/S0D4__i1w1I/AAAAAAAAACo/cgLl5IYQtjA/s400/croix.png";
                    proImage[i].Scale = 0.75;
                }
                AbsoluteLayout.SetLayoutFlags(proImage[i], AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(proImage[i], new Rectangle(X, Y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
                photoLayout.Children.Add(proImage[i]);
            }
            for (int i = 8; i < 10; i++)
            {
                Y = Y - 0.2;
                proImage[i] = new Image();
                if (professionals[i] != null)
                {
                    Stream s = new MemoryStream(DependencyService.Get<IBytesSaveAndLoad>().LoadByteArray(professionals[i].Photo),true);
                    proImage[i].Scale = 0.75;
					proImage[i] = InitializePhoto(proImage[i], i, professionals);
                    proImage[i].Source = ImageSource.FromStream(() => s);
                    _imageId.SetValue(proImage[i].Id, i);
                }
                else
                {
                    proImage[i].Source = "http://3.bp.blogspot.com/_9Q_36sq8aPo/S0D4__i1w1I/AAAAAAAAACo/cgLl5IYQtjA/s400/croix.png";
                    proImage[i].Scale = 0.75;
                }
                AbsoluteLayout.SetLayoutFlags(proImage[i], AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(proImage[i], new Rectangle(X, Y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
                photoLayout.Children.Add(proImage[i]);
                X = X - 0.2;
            }
            Content = new StackLayout
            {

                Children = {
                    button.Content,
                    photoLayout
                },
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            this.BackgroundColor = Color.White;
        }

        private void Initiliaze(string title, string content, List<Professional> recievers)
        {
            _title = title;
            _content = content;
            if (recievers != null)
                _recievers = recievers;
            else
                _recievers = new List<Professional>();
        }
        private async void BackMessage_Clicked(object sender, EventArgs e)
        {
            if (_message)
                await Navigation.PushAsync(new CreateMessage(_userData, _patient, _recievers, _title, _content));
            else if (!_message)
                await Navigation.PushAsync(new CreatePrescription(_userData, _patient, _recievers, _title, _content));
        }
        private void Image_Taped(object sender, EventArgs e)
        {
            var image = sender as Image;
            if (image != null)
            {
                if (image.Scale == 0.75)
                {
                    image.Scale = 0.6;
                    for (int i = 0; i < professionals.Length; i++)
                    {
						if (_imageId[i] == image.Id && !_recievers.Contains(professionals[i]))
                        {
                            _recievers.Add(professionals[i]);
                        }
                    }
                }
                else if (true)
                {
                    image.Scale = 0.75;
                    Professional[] p = ChangeListToArray(_recievers);
                    for (int i = 0; i < p.Length; i++)
                    {
                        if (_imageId[i] == image.Id)
                        {
                            p[i] = null;
                        }
                    } 
                    _recievers = ChangeArrayToList(p);
                }
            }
        }

        private Professional[] ProfessionalArray()
        {
            Professional[] pro = new Professional[10];
            Patient p = new Patient(_patient);
            foreach (var patient in _userData.Follow.Keys)
            {
                if (_patient.UserId == patient.UserId)
                    p = patient;
            }
            _userData.Follow.TryGetValue(p, out pro);
            return pro;
        }
        private bool PageForPatient()
        {
            foreach (var patient in _userData.Follow.Keys)
            {
                if (patient.UserId == _userData.User.UserId) return true;
            }
            return false;
        }
        private List<Professional> ChangeArrayToList(Professional[] pro)
        {
            List<Professional> p = new List<Professional>();
            for (int i = 0; i < pro.Length; i++)
            {
                if (pro[i] != null) p.Add(pro[i]);
            }
            return p;
        }
        private Professional[] ChangeListToArray(List<Professional> pro)
        {
            Professional[] p = new Professional[10];
            int count = 0;
            foreach (var pr in pro)
            {
                p.SetValue(pr, count);
                count++;
            }
            return p;
        }
        private Image InitializePhoto(Image image, int count, Professional[] professionals)
        {
			if (_recievers.Count == 0)
            {
                image.GestureRecognizers.Add(imageTapped);
				image.Scale = 0.75;
            }
            else
            {
                foreach (var pro in _recievers)
                {
                    if(pro.UserId == professionals[count].UserId)
                    {
                        image.Scale = 0.6;
                        return image;
                    }
                    else
                    {
                        image.Scale = 0.75;
                    }
                }
            }
            return image;
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
        private async void ProfilButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilPage(_userData, _userData.User));
        }
    }
}

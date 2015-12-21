using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    public class AddReciverPage : ContentPage
    {
        Data _userData;
        Patient _patient;
        List<Professional> _recievers;
        Professional[] professionals;
        TapGestureRecognizer imageTapped = new TapGestureRecognizer();

        public AddReciverPage(Data userData, Patient patient, List<Professional> recievers)
        {
            _userData = userData;
            _patient = patient;
            if (recievers != null)
                _recievers = recievers;
            else
                _recievers = new List<Professional>();
            
            imageTapped.Tapped += Image_Taped;
            AbsoluteLayout photoLayout = new AbsoluteLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            };

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
                BackgroundColor = Color.Gray,
                BorderColor = Color.Black,
                FontSize = 30,
                FontAttributes = FontAttributes.Bold,
            };
            if (PageForPatient()) followButton.Text = "Mon Suivis";

            Button documentsButton = new Button
            {
                Text = "Mes Documents",
                BackgroundColor = Color.White,
                BorderColor = Color.Black,
                FontSize = 30,
                TextColor = Color.Black
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
					proImage[i] = InitializePhoto(proImage[i], i, professionals);
                    proImage[i].Source = professionals[i].Photo;
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
					proImage[i].Scale = 0.75;
					proImage[i] = InitializePhoto(proImage[i], i, professionals);
                    proImage[i].Source = professionals[i].Photo;
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
					proImage [i].Scale = 0.75;
					proImage[i] = InitializePhoto(proImage[i], i, professionals);
                    proImage[i].Source = professionals[i].Photo;
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
					proImage [i].Scale = 0.75;
					proImage[i] = InitializePhoto(proImage[i], i, professionals);
                    proImage[i].Source = professionals[i].Photo;

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
                    buttonStack,
                    photoLayout
                },
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            this.BackgroundColor = Color.White;
        }

        private async void BackMessage_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateMessage(_userData, _patient, _recievers));
        }
        private void Image_Taped(object sender, EventArgs e)
        {
            var image = sender as Image;
            if (image != null)
            {
                if (image.Scale == 0.75)
                {
                    image.Scale = 0.6;
                    var source = image.Source as UriImageSource;
                    if (source != null)
                    {
                        for (int i = 0; i < professionals.Length; i++)
                        {
                            if (professionals[i] != null && source.Uri.ToString() == professionals[i].Photo) _recievers.Add(professionals[i]);
                        }
                    }
                }
                else if (true)
                {
                    image.Scale = 0.75;
                    var source = image.Source as UriImageSource;
                    if (source != null)
                    {
                        Professional[] p = ChangeListToArray(_recievers);
                        for (int i = 0; i < p.Length; i++)
                        {
							if (p[i] != null && source.Uri.ToString() == p[i].Photo) p[i] = null;
                        } 
                        _recievers = ChangeArrayToList(p);
                    }
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
    }
}

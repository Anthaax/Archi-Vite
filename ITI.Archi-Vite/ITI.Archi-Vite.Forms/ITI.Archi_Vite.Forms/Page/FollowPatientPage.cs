using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    public class FollowPatientPage : ContentPage
	{
		Data _userData;
		Patient _patient;
        Professional[] _professionals;
        Guid[] _imageId = new Guid[11];
        public FollowPatientPage (Data userData, Patient patient)
		{
			_userData = userData;
			_patient = patient;
            var tappedGesture = new TapGestureRecognizer();
            tappedGesture.Tapped += TappedGesture_Tapped;
            AbsoluteLayout photoLayout = new AbsoluteLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            MultibleButtonView button = new MultibleButtonView(_userData);

            button.FollowIsDisable();
            button.DocumentButton.Clicked += DocumentsButton_Clicked;
            button.ProfilButton.Clicked += ProfilButtonClicked;

            Button messageButton = new Button
            {
                Text = "Messages",
                FontSize = 30,
                BackgroundColor = Color.FromHex("439DFE"),
            };

            AbsoluteLayout.SetLayoutFlags(messageButton, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(messageButton, new Rectangle(0, 1, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            photoLayout.Children.Add(messageButton);
            messageButton.Clicked += MessageButton_Clicked;

            Button createMessageButtton = new Button
            {
                Text = "Ajouter un message",
                FontSize = 20,
                BackgroundColor = Color.FromHex("439DFE"),
            };

            AbsoluteLayout.SetLayoutFlags(createMessageButtton, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(createMessageButtton, new Rectangle(0, 0.9, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            photoLayout.Children.Add(createMessageButtton);
            createMessageButtton.Clicked += CreateMessageButtton_Clicked;

            Button createPrescriptionButtton = new Button
            {
                Text = "Ajouter une prescription",
                FontSize = 20,
                BackgroundColor = Color.FromHex("439DFE"),
            };

            AbsoluteLayout.SetLayoutFlags(createPrescriptionButtton, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(createPrescriptionButtton, new Rectangle(1, 0.9, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            photoLayout.Children.Add(createPrescriptionButtton);
            createPrescriptionButtton.Clicked += CreatePrescriptionButtton_Clicked;

            Button prescriptionButton = new Button
            {
                Text = "Prescriptions",
                FontSize = 30,
                BackgroundColor = Color.FromHex("439DFE"),
            };

            AbsoluteLayout.SetLayoutFlags(prescriptionButton, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(prescriptionButton, new Rectangle(1, 1, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            photoLayout.Children.Add(prescriptionButton);
            prescriptionButton.Clicked += PrescriptionButton_Clicked;
            Stream str = new MemoryStream(DependencyService.Get<IBytesSaveAndLoad>().LoadByteArray(patient.Photo), true);
            Image patientImage = new Image
            {
                Source = ImageSource.FromStream(() => str),
				Scale = 0.75
            };
            AbsoluteLayout.SetLayoutFlags(patientImage, AbsoluteLayoutFlags.PositionProportional);
            AbsoluteLayout.SetLayoutBounds(patientImage, new Rectangle(0.5, 0.5, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            photoLayout.Children.Add(patientImage);

            Image[] proImage = new Image[10];
            _professionals = ProfessionalArray();
            double X = 0.5;
            double Y = 0.0;
            for (int i = 0; i < 3; i++)
            {
				proImage[i] = new Image();
				if (_professionals [i] != null) 
				{
                    Stream s = new MemoryStream(DependencyService.Get<IBytesSaveAndLoad>().LoadByteArray(_professionals[i].Photo), true);
                    proImage [i].GestureRecognizers.Add (tappedGesture);
                    proImage[i].Source = ImageSource.FromStream(() => s);
                    _imageId.SetValue(proImage[i].Id, i);
                }
                else
                {
                    proImage[i].Source = "http://3.bp.blogspot.com/_9Q_36sq8aPo/S0D4__i1w1I/AAAAAAAAACo/cgLl5IYQtjA/s400/croix.png";
                }
                proImage [i].Scale = 0.75;
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
				if (_professionals [i] != null) 
				{
                    Stream s = new MemoryStream(DependencyService.Get<IBytesSaveAndLoad>().LoadByteArray(_professionals[i].Photo),true);
					proImage [i].GestureRecognizers.Add (tappedGesture);
                    proImage[i].Source = ImageSource.FromStream(() => s);
                    _imageId.SetValue(proImage[i].Id, i);
                }
                else
                {
                    proImage[i].Source = "http://3.bp.blogspot.com/_9Q_36sq8aPo/S0D4__i1w1I/AAAAAAAAACo/cgLl5IYQtjA/s400/croix.png";
                }
                proImage [i].Scale = 0.75;
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
				if (_professionals [i] != null) 
				{
                    Stream s = new MemoryStream(DependencyService.Get<IBytesSaveAndLoad>().LoadByteArray(_professionals[i].Photo), true);
                    proImage[i].GestureRecognizers.Add (tappedGesture);
                    _imageId.SetValue(proImage[i].Id, i);
                    proImage[i].Source = ImageSource.FromStream(() => s);
                }
                else
                {
                    proImage[i].Source = "http://3.bp.blogspot.com/_9Q_36sq8aPo/S0D4__i1w1I/AAAAAAAAACo/cgLl5IYQtjA/s400/croix.png";
                }
                proImage [i].Scale = 0.75;
                AbsoluteLayout.SetLayoutFlags(proImage[i], AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(proImage[i], new Rectangle(X, Y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
                photoLayout.Children.Add(proImage[i]);
            }
            for (int i = 8; i < 10; i++)
            {
				Y = Y - 0.2;
				proImage[i] = new Image();
				if (_professionals [i] != null) 
				{
                    Stream s = new MemoryStream(DependencyService.Get<IBytesSaveAndLoad>().LoadByteArray(_professionals[i].Photo), true);
                    proImage[i].GestureRecognizers.Add (tappedGesture);
                    _imageId.SetValue(proImage[i].Id, i);
                    proImage[i].Source = ImageSource.FromStream(() => s);
                }
                else
                {
                    proImage[i].Source = "http://3.bp.blogspot.com/_9Q_36sq8aPo/S0D4__i1w1I/AAAAAAAAACo/cgLl5IYQtjA/s400/croix.png";
                }
                proImage [i].Scale = 0.75;
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

        private async void CreatePrescriptionButtton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreatePrescription(_userData, _patient, null, null, null));
        }

        private async void DocumentsButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DocumentsPage(_userData));
        }

        private async void CreateMessageButtton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreateMessage(_userData, _patient, null, null, null));
        }

        private async void PrescriptionButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new PrescriptionListPage(_userData));
        }

        private async void MessageButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MessageListPage(_userData));
        }

        private async void TappedGesture_Tapped(object sender, EventArgs e)
        {
            var image = sender as Image;
            if(image != null )
            {
                for (int i = 0; i < _professionals.Length; i++)
                {
                    if (_imageId[i] == image.Id)
                    {
                        if(_professionals[i] != null)
                            await Navigation.PushAsync(new ProfilPage(_userData, _professionals[i]));
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
			foreach(var patient in _userData.Follow.Keys )
			{
				if (patient.UserId == _userData.User.UserId) return true;
			}
			return false;
		}
        private async void ProfilButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProfilPage(_userData, _userData.User));
        }
        private async Task<Stream> GetStreamFromImageSourceAsync(StreamImageSource imageSource, CancellationToken canellationToken = default(CancellationToken))
        {
            if (imageSource.Stream != null)
            {
                return await imageSource.Stream(canellationToken);
            }
            return null;
        }
        public static byte[] ReadFully(Stream input)
        {
            StreamReader str = new StreamReader(input);
            byte[] buffer = new byte[16 * 1024];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = input.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
    }
}



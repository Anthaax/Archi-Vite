using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms.View
{
    public class DispositionPhotoView : ContentView
    {
        readonly Data _userData;
        readonly Patient _patient;
        readonly Professional[] _professionals;
        readonly double _scale;

        public DispositionPhotoView(Data userData, Patient patient, double scale)
        {
            _scale = scale;
            AbsoluteLayout photoLayout = new AbsoluteLayout
            {
                VerticalOptions = LayoutOptions.FillAndExpand
            };
            Image patientImage = new Image
            {
                Source = patient.Photo,
                Scale = _scale
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
                if (_professionals[i] != null)
                {
                    proImage[i].Source = _professionals[i].Photo;
                }
                else
                    proImage[i].Source = "http://3.bp.blogspot.com/_9Q_36sq8aPo/S0D4__i1w1I/AAAAAAAAACo/cgLl5IYQtjA/s400/croix.png";
                proImage[i].Scale = _scale;
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
                if (_professionals[i] != null)
                {
                    proImage[i].Source = _professionals[i].Photo;
                }
                else
                    proImage[i].Source = "http://3.bp.blogspot.com/_9Q_36sq8aPo/S0D4__i1w1I/AAAAAAAAACo/cgLl5IYQtjA/s400/croix.png";
                proImage[i].Scale = _scale;
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
                if (_professionals[i] != null)
                {
                    proImage[i].Source = _professionals[i].Photo;
                }
                else
                    proImage[i].Source = "http://3.bp.blogspot.com/_9Q_36sq8aPo/S0D4__i1w1I/AAAAAAAAACo/cgLl5IYQtjA/s400/croix.png";
                proImage[i].Scale = _scale;
                AbsoluteLayout.SetLayoutFlags(proImage[i], AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(proImage[i], new Rectangle(X, Y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
                photoLayout.Children.Add(proImage[i]);
            }
            for (int i = 8; i < 10; i++)
            {
                Y = Y - 0.2;
                proImage[i] = new Image();
                if (_professionals[i] != null)
                {
                    proImage[i].Source = _professionals[i].Photo;
                }
                else
                    proImage[i].Source = "http://3.bp.blogspot.com/_9Q_36sq8aPo/S0D4__i1w1I/AAAAAAAAACo/cgLl5IYQtjA/s400/croix.png";
                proImage[i].Scale = _scale;
                AbsoluteLayout.SetLayoutFlags(proImage[i], AbsoluteLayoutFlags.PositionProportional);
                AbsoluteLayout.SetLayoutBounds(proImage[i], new Rectangle(X, Y, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
                photoLayout.Children.Add(proImage[i]);
                X = X - 0.2;

            }
            Content = new Label { Text = "Hello ContentView" };
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
        public Professional[] Professionals
        {
            get
            {
                return _professionals;
            }
        }

        public double PhotoScale
        {
            get
            {
                return _scale;
            }
        }
    }
}

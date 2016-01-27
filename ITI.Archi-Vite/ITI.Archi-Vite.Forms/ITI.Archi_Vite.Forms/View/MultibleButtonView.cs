using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    public class MultibleButtonView : ContentView
    {
        readonly Button _profilButton;
        readonly Button _followButton;
        readonly Button _documentButton;
        readonly Data _userData;

        public MultibleButtonView( Data userData)
        {
			_userData = userData;
            _profilButton = new Button
            {
                Text = "Mon Profil",
                BackgroundColor = Color.White,
                BorderColor = Color.Black,
                TextColor = Color.Black,
                FontSize = 16,
            };
            _followButton = new Button
            {
                Text = "Mes Suivis",
                BackgroundColor = Color.White,
                BorderColor = Color.Black,
                TextColor = Color.Black,
                FontSize = 16,
            };
            _documentButton = new Button
            {
                Text = "Mes Documents",
                BackgroundColor = Color.White,
                BorderColor = Color.Black,
                TextColor = Color.Black,
                FontSize = 16,
            };
            if (PageForPatient())
                _followButton.Text = "Mon Suivi";
            Content = new StackLayout
            {

                Children = {
                    _profilButton,
                    _followButton,
                    _documentButton
                },
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Start

            };
        }
        public void ProfilIsDisable()
        {
            _profilButton.BackgroundColor = Color.Gray;
            _profilButton.TextColor = Color.White;
            _profilButton.FontAttributes = FontAttributes.Bold;
        }

        public void FollowIsDisable()
        {
            _followButton.BackgroundColor = Color.Gray;
            _followButton.TextColor = Color.White;
            _followButton.FontAttributes = FontAttributes.Bold;
        }
        public void DocumentsIsDisable()
        {
            _documentButton.BackgroundColor = Color.Gray;
            _documentButton.TextColor = Color.White;
            _documentButton.FontAttributes = FontAttributes.Bold;
        }

        public Button ProfilButton
        {
            get
            {
                return _profilButton;
            }
        }

        public Button FollowButton
        {
            get
            {
                return _followButton;
            }
        }

        public Button DocumentButton
        {
            get
            {
                return _documentButton;
            }
        }
        private bool PageForPatient()
        {
            foreach (var patient in _userData.Follow.Keys)
            {
                if (patient.UserId == _userData.User.UserId) return true;
            }
            return false;
        }

    }
}

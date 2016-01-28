﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Net.Http;

using Xamarin.Forms;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using Polenter.Serialization;
using ModernHttpClient;
using System.Xml.Serialization;

namespace ITI.Archi_Vite.Forms
{
    public class ConnectionPage : ContentPage
    {
		User _user;
		Data _dataForUser;
        DataXMLConvertor _xmlCovertor = new DataXMLConvertor();
        DataConvertor _convertor = new DataConvertor();
        public ConnectionPage()
        {
			AutoConnection();
            Image logo = new Image
            {
                Source = "Logo.png",
                HeightRequest = 250,
                WidthRequest = 250,
            };
            Entry pseudo = new Entry
            {
                Placeholder = "Pseudo",
                FontSize = 35,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Start,
                TextColor = Color.Gray,
                PlaceholderColor = Color.Gray
            };
            pseudo.TextChanged += EntryTextChanged;
            Entry password = new Entry
            {
                Placeholder = "Password",
                IsPassword = true,
                FontSize = 35,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalOptions = LayoutOptions.Start,
                PlaceholderColor = Color.Gray
            };
            password.TextChanged += EntryTextChanged;
            Button send = new Button
            {
                Text = "Se connecter",
                FontSize = 35,
                BackgroundColor = Color.FromHex("439DFE"),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.End,
            };
            send.Clicked += async (sender, e) =>
            {
                if (pseudo.Text != null && password.Text != null)
                {
                    _dataForUser = PutUserData(pseudo.Text, password.Text);
					SaveUserData();
					await Navigation.PushAsync(new ProfilPage(_dataForUser, _dataForUser.User));
                }
                else await DisplayAlert ("Error", "Les champ doivent etre valides", "Ok");

            };
            Content = new StackLayout
            {

                Children = {
                    logo,
                    pseudo,
                    password,
                    send,
                }
            };
            this.BackgroundColor = Color.White;
        }

        private async void AutoConnection()
        {
            LoadUserData();
            if(_dataForUser != null)
            {
                await Navigation.PushAsync(new ProfilPage(_dataForUser, _dataForUser.User));
            }
        }

        private Data XmlDeseriliaze(string s)
        {
            DataXML data = new DataXML();
            XmlSerializer ser = new XmlSerializer(data.GetType());
            TextReader text = new StringReader(s);
            data = (DataXML)ser.Deserialize(text);
            return _xmlCovertor.DataXMLToData(data);
        }

        private void EntryTextChanged(object sender, TextChangedEventArgs e)
        {
            Entry entry = sender as Entry;
            if (entry != null)
            {
                entry.TextColor = Color.Gray;
            }
        }
		private Data PutUserData(string pseudo, string password)
		{
            User[] users = AllUsers();

			for (int i=0; i < users.Length; i++)
			{
				if (users[i].Password == password && users[i].Pseudo == pseudo)
					_user = users[i];
			}
			if (_user == null)
				return null;
            List<Message> m = new List<Message>();
            List<Prescription> p = new List<Prescription>();
            DocumentSerializable doc = new DocumentSerializable(m,p);
            if(_user != users[5])
            {
                foreach (var message in CreateSerializableDocument(CreateFollowerDictionnary(users, _user), users[4]).Messages)
                {
                    doc.Messages.Add(message);
                }
                foreach (var prescription in CreateSerializableDocument(CreateFollowerDictionnary(users, _user), users[4]).Prescriptions)
                {
                    doc.Prescriptions.Add(prescription);
                }
            }
            if (_user != users[4] && _user != users[3])
            {
                foreach (var message in CreateSerializableDocument(CreateFollowerDictionnary(users, _user), users[5]).Messages)
                {
                    doc.Messages.Add(message);
                }
                foreach (var prescription in CreateSerializableDocument(CreateFollowerDictionnary(users, _user), users[5]).Prescriptions)
                {
                    doc.Prescriptions.Add(prescription);
                }
            }
            return new Data(_user, CreateFollowerDictionnary(users, _user), doc);
		}
		private Dictionary<Patient, Professional[]> CreateFollowerDictionnary(User[] users, User curentUser)
		{
            Dictionary<Patient, Professional[]> follows = new Dictionary<Patient, Professional[]>();
            Professional[] proForGuillaume = new Professional[10];
            Professional[] proForMaxime = new Professional[10];
            Patient guillaume = new Patient(users[4]);
            Patient maxime = new Patient(users[5]); 
            for( int i = 0; i<4; i++)
            {
                Professional pro = new Professional(users[i], "Infirmier");
				if(pro != null)
				proForGuillaume.SetValue(pro, i);
            }
            for (int i = 0; i < 2; i++)
            {
                Professional pro = new Professional(users[i], "Infirmier");
                proForMaxime.SetValue(pro, i);
            }
            if(curentUser != users[5] ) follows.Add(guillaume, proForGuillaume);
            if (curentUser != users[4] && curentUser != users[3]) follows.Add(maxime, proForMaxime);
            return follows;
		}
		private DocumentSerializable CreateSerializableDocument(Dictionary<Patient, Professional[]> userData, User Patient)
        {
            List<Message> messages = new List<Message>();
            List<Prescription> prescriptions = new List<Prescription>();
            List<Professional> professional = new List<Professional>();
            Patient p = new Patient(Patient);
            Professional[] proForPatient = ProfessionalArray(userData, p);
            for (int i = 0; i < 2; i++)
            {
				professional.Add(proForPatient[i]);
            }
			messages.Add(new Message("Coucou", "Il va bien", proForPatient[0], professional, p));
			if (p.UserId == 5) 
			{
				messages.Add (new Message ("Hey", "Il va bien", proForPatient [1], professional, p));
				prescriptions.Add (new Prescription ("Hey", "http://i.imgur.com/GWji92h.png", proForPatient [1], professional, p));
			}
            DocumentSerializable doc = new DocumentSerializable(messages, prescriptions);
            return doc;
        }

		private Professional[] ProfessionalArray(Dictionary<Patient, Professional[]> userData, Patient user)
        {
            Professional[] pro = new Professional[10];
			Patient p = user;
            foreach (var patient in userData.Keys)
            {
                if (user.UserId == patient.UserId)
                    p = patient;
            }
            userData.TryGetValue(p, out pro);
            int count = 0;
            foreach (var professional in pro)
            {
                pro.SetValue(professional, count);
                count++;
            }
            return pro;
        }

        public void SaveUserData()
        {
            DataXML xml = _convertor.DataToDataJson(_dataForUser);
            DependencyService.Get<ISaveLoadAndDelete>().SaveData("user.txt", xml);
            User[] u = AllUsers();
            foreach (var user in u)
            {
                SaveDataOneUser(user.Pseudo, user.Password);
            }
        }

        private User[] AllUsers()
        {
            User[] users = new User[6];
            User u0 = new User(1, "Antoine", "Raquillet", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12345, "AntoineR", "AntoineR", 0616066606, "http://new.intechinfo.fr/wp-content/uploads/2015/10/RAQUILLET-Antoine.jpg");
            User u1 = new User(2, "Simon", "Favraud", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12345, "SimonF", "SimonF", 0626066606, "http://i.imgur.com/9cSffeM.png");
            User u2 = new User(3, "Clement", "Rousseau", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12345, "ClementR", "ClementR", 0636066606, "http://i.imgur.com/silO1AR.png");
            User u3 = new User(4, "Olivier", "Spinelli", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12345, "OlivierS", "OlivierS", 0646066606, "http://new.intechinfo.fr/wp-content/uploads/2015/10/olivier-spinelli_portrait.png");
            User u4 = new User(5, "Guillaume", "Fimes", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12345, "GuillaumeF", "GuillaumeF", 0656066606, "http://i.imgur.com/GWji92h.png");
            User u5 = new User(6, "Maxime", "De Vogelas", DateTime.Now, "72 avenue maurice thorez", "Ivry-sur-Seine", 12345, "MaximeD", "MaximeD", 0666066606, "http://i.imgur.com/3yZF0Lz.png");
            users[0] = u0;
            users[1] = u1;
            users[2] = u2;
            users[3] = u3;
            users[4] = u4;
            users[5] = u5;
            return users;
        }

        private void SaveDataOneUser(string pseudo, string password)
        {
            Data d = PutUserData(pseudo, password);
            string s = d.User.FirstName + "$" + d.User.LastName;
            DataXML xml = _convertor.DataToDataJson(d);
            DependencyService.Get<ISaveLoadAndDelete>().SaveData(s + ".txt", xml);

        }

        public bool LoadUserData()
        {
            try
            {
                DataXML xml = DependencyService.Get<ISaveLoadAndDelete>().LoadData("user.txt");
				if(xml != null)
                	_dataForUser = _xmlCovertor.DataXMLToData(xml);
				else return false;
            }
            catch (IOException)
            {
                return false;
            }
			catch(Exception e) {
				return false;
			}
            return true;
        }
    }
}

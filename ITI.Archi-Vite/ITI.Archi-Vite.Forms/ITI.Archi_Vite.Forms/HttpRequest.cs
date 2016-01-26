using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ITI.Archi_Vite.Forms
{
    public class HttpRequest
    {
		static string _serveur = "192.168.10.24";
        public static async Task<HttpResponseMessage> HttpRequestGetUserData(string pseudo, string password )
        {
			try {
				var client = new HttpClient(new NativeMessageHandler());
				client.BaseAddress = new Uri("http://"+ _serveur +":8080/");
				client.Timeout = new TimeSpan(0, 0, 50);
				client.MaxResponseContentBufferSize = long.MaxValue;
				string s = "api/Users/?pseudo=" + pseudo + "&password=" + password;
				var response = await client.GetAsync(s);
				return response;
			} catch (Exception ex) {
				return null;
			}
        }

        public static async Task<DocumentSerializableXML> HttpRequestSetDocument(DocumentSerializableXML documents)
        {
			bool ok = false;
			var client = new HttpClient(new NativeMessageHandler());
			try {
	            client.BaseAddress = new Uri("http://"+ _serveur +":8080/");
	            client.Timeout = new TimeSpan(0, 0, 50);
	            client.MaxResponseContentBufferSize = long.MaxValue;
			} catch (Exception ex) {
				return null;
			}
            if (documents.Message.Any())
            {
				string s = "api/Message";
                foreach (var message in documents.Message)
                {
                    string json = JsonConvert.SerializeObject(message);
					StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = await client.PutAsync(s, content);
                    if (response.IsSuccessStatusCode)
                    {
                        ok = true;
                    }
                }
            }
            if (documents.Prescription.Any())
            {
				string s = "api/Prescription";
                string xml = JsonConvert.SerializeObject(documents.Prescription);
                StringContent content = new StringContent(xml);
				var response = await client.PutAsync(s, content);
                if (response.IsSuccessStatusCode)
                {
                    ok = true;
                }
            }
            if(ok)
            {
                return documents;
            }
            return null;
        }
        public static async Task<HttpResponseMessage> HttpRequestSetUserData(UserXML user)
        {
            var client = new HttpClient(new NativeMessageHandler());
			try {
				client.BaseAddress = new Uri("http://"+ _serveur +":8080/");
				client.Timeout = new TimeSpan(0, 0, 50);
				client.MaxResponseContentBufferSize = long.MaxValue;
			} catch (Exception ex) {
				return null;
			}
            string s = "api/Users";
            
            string json = JsonConvert.SerializeObject(user);
			StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(s, content);
            return response;
        }
    }
}

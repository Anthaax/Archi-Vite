using ModernHttpClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ITI.Archi_Vite.Forms
{
    public class HttpRequest
    {
        public static async Task<HttpResponseMessage> HttpRequestGetUserData(string pseudo, string password )
        {
            var client = new HttpClient(new NativeMessageHandler());
            client.BaseAddress = new Uri("http://10.8.110.152:8080/");
            client.Timeout = new TimeSpan(0, 0, 50);
            client.MaxResponseContentBufferSize = long.MaxValue;
            string s = "api/Users/?pseudo=" + pseudo + "&password=" + password;
            var response = await client.GetAsync(s);
            return response;
        }

        public static async void HttpRequestSetDocument(DocumentSerializableXML documents)
        {
            var client = new HttpClient(new NativeMessageHandler());
            client.BaseAddress = new Uri("http://10.8.110.152:8080/");
            client.Timeout = new TimeSpan(0, 0, 50);
            client.MaxResponseContentBufferSize = long.MaxValue;
            if (documents.Message.Any())
            {
                string s = "api/Prescription";
                string xml = JsonConvert.SerializeObject(documents.Message);
                StringContent content = new StringContent(xml);
                var response = await client.PostAsync(s, content);

            }
            if (documents.Prescription.Any())
            {
                string s = "api/Message";
                string xml = JsonConvert.SerializeObject(documents.Prescription);
                StringContent content = new StringContent(xml);
                var response = await client.PostAsync(s, content);
            } 
        }
        public static async Task<HttpResponseMessage> HttpRequestSetUserData(Data user)
        {
            var client = new HttpClient(new NativeMessageHandler());
            client.BaseAddress = new Uri("http://10.8.110.152:8080/");
            client.Timeout = new TimeSpan(0, 0, 50);
            client.MaxResponseContentBufferSize = long.MaxValue;
            string s = "api/Users";
            string xml = JsonConvert.SerializeObject(user.User);
            StringContent content = new StringContent(xml);
            var response = await client.PostAsync(s, content);
            return response;
        }
    }
}

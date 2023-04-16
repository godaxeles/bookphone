using Azure.Core;
using System.Net.Http;
using System.Net.Http.Headers;

namespace BookPhone.Api
{
    public class ContactAPI
    {
        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("http://localhost:34091/");
            return Client;
        }

        public HttpClient Initial(HttpContext context)
        {
            var Client = new HttpClient();
            if (context.Request.Cookies["Cookie"] != null)
            {
                string token = context.Request.Cookies["Cookie"];
                Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            }
            Client.BaseAddress = new Uri("http://localhost:34091/");
            return Client;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using BookPhone.Models;

namespace BookPhone.WPF.DataProvider
{
    public class ContactWebApiDataProvider : IContactDataProvider
    {
        private static readonly HttpClient _client = new HttpClient();
        public async Task<IEnumerable<Contact>> LoadContacts()
        {
            string json;
            try
            {
                json = await _client.GetStringAsync("http://localhost:34091/api/Contacts");
            }
            catch (Exception)
            {
                json = null;
            }

            var inMemoryContacts = new[]
                {
                new Contact{ FirstName = "Ivan", Address = "Test adres", Description = "Descr", LastName = "Ivanov", MiddleName = "Alexandrovich", PhoneNumbe = "+7(777)777-77-77"},
                new Contact{ FirstName = "Ivan", Address = "Test adres", Description = "Descr", LastName = "Ivanov", MiddleName = "Alexandrovich", PhoneNumbe = "+7(777)777-77-77"},
                new Contact{ FirstName = "Ivan", Address = "Test adres", Description = "Descr", LastName = "Ivanov", MiddleName = "Alexandrovich", PhoneNumbe = "+7(777)777-77-77"},
                new Contact{ FirstName = "Ivan", Address = "Test adres", Description = "Descr", LastName = "Ivanov", MiddleName = "Alexandrovich", PhoneNumbe = "+7(777)777-77-77"}
                };

            return string.IsNullOrEmpty(json) ? inMemoryContacts
                : JsonConvert.DeserializeObject<IEnumerable<Contact>>(json) ?? inMemoryContacts;
        }
    }
}

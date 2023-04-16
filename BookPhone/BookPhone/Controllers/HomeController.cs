using BookPhone.Api;
using BookPhone.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Elfie.Model.Tree;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace BookPhone.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        ContactAPI _api = new ContactAPI();

        
        public async Task<IActionResult> Index()
        {
            List<Contact> contacts = new List<Contact>();
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.GetAsync("api/Contacts");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                contacts = JsonConvert.DeserializeObject<List<Contact>>(result);
            }
            return View(contacts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (!ModelState.IsValid)
                return View("Create");

            HttpClient client = _api.Initial();
            var json = JsonConvert.SerializeObject(contact);
            HttpContent contentPost = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PostAsync("api/Contacts", contentPost);
            if (!res.IsSuccessStatusCode)
                return View("Create");

            return View("CreateContact", contact);
        }

        public IActionResult Edit(int id) 
        {
            Contact contact = null;

            var client = _api.Initial();
            var responseTask = client.GetAsync("api/Contacts/" + id.ToString());

            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync().Result;

                contact = JsonConvert.DeserializeObject<Contact>(readTask);
            }
            return View(contact); 
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Contact contact)
        {
            HttpClient client= _api.Initial();
            var json = JsonConvert.SerializeObject(contact);
            HttpContent content = new StringContent(json,Encoding.UTF8, "application/json");
            HttpResponseMessage res = await client.PutAsync($"api/Contacts/{contact.Id}", content);
            if (!res.IsSuccessStatusCode)
                return View("Edit");

            return View("EditContact", contact);
        }

        public IActionResult Details(int id)
        {
            Contact contact = null;

            var client = _api.Initial();
            var responseTask = client.GetAsync("api/Contacts/" + id.ToString());

            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync().Result;

                contact = JsonConvert.DeserializeObject<Contact>(readTask);
            }
            return View(contact);
        }

        public IActionResult Delete(int id)
        {
            Contact contact = null;

            var client = _api.Initial();
            var responseTask = client.GetAsync("api/Contacts/" + id.ToString());

            var result = responseTask.Result;

            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync().Result;

                contact = JsonConvert.DeserializeObject<Contact>(readTask);
            }
            return View(contact);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Contact contact)
        {
            HttpClient client = _api.Initial();
            HttpResponseMessage res = await client.DeleteAsync($"api/Contacts/{contact.Id}");
            if (!res.IsSuccessStatusCode)
                return View("Delete");

            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
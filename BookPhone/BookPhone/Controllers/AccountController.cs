using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Azure;
using BookPhone.Api;
using BookPhone.Models;
using BookPhone.Models.BookPhone.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;

namespace BookPhone.Controllers
{
    
    public class AccountController : Controller
    {
        ContactAPI _api = new ContactAPI();

        [HttpGet]
        public IActionResult Login(string returnUrl)
        {
            return View(new UserLogin()
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLogin model)
        {
            if (ModelState.IsValid)
            {
                var context = HttpContext;
                HttpClient client = _api.Initial(context);
                var json = JsonConvert.SerializeObject(model);
                HttpContent contentPost = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage res = await client.PostAsync("api/Users/Login", contentPost);

                var token = await res.Content.ReadAsStringAsync();

                Response.Cookies.Append("Cookie", token);
                
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);
                var claims = jwtToken.Claims;

                var claimIdentity = new ClaimsIdentity(claims, "Cookie");
                var claimPrincipal = new ClaimsPrincipal(claimIdentity);
                await HttpContext.SignInAsync("Cookie", claimPrincipal);

                if (Url.IsLocalUrl(model.ReturnUrl))
                    return RedirectToAction("Index", "Home");
            }
            else
                ModelState.AddModelError("", "Пользователь не найден");

            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserRegistration());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserRegistration model)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = _api.Initial();
                var json = JsonConvert.SerializeObject(model);
                HttpContent contentPost = new StringContent(json, Encoding.UTF8, "application/json");
                HttpResponseMessage res = await client.PostAsync("api/Users", contentPost);

                if (!res.IsSuccessStatusCode)
                {
                    return View("Register");

                }
            }
            return RedirectToAction("Login");
        }


        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
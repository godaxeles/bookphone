using BookPhone;
using IdentityServer3.AccessTokenValidation;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvc();
builder.Services.AddRazorPages();

builder.Services.AddAuthentication("Cookie")
        .AddCookie("Cookie", options =>
        {
            options.LoginPath = "/Account/Login";
        });

builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("Administrator", builder =>
        {
            builder.RequireClaim(ClaimTypes.Role, "Administrator");
        });

        options.AddPolicy("User", builder =>
        {
            builder.RequireClaim(ClaimTypes.Role, "User");
        });

        options.AddPolicy("Manager", builder =>
        {
            builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "Manager")
            || x.User.HasClaim(ClaimTypes.Role, "Administrator"));
        });

    });



builder.Services.AddControllersWithViews();


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseMiddleware<TokenMiddleware>();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCookiePolicy();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

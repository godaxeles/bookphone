using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.Data;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly JWTSettings _options;
        public UsersController(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<JWTSettings> options)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _options = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegistration model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
         
            var user = new User { UserName = model.LoginProp};
            
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
                return BadRequest();
               
            await _signInManager.SignInAsync(user, isPersistent: false);

            List<Claim> claims = new List<Claim>();
            //claims.Add(new Claim(ClaimTypes.Name, model.LoginProp));
            claims.Add(new Claim(ClaimTypes.Role, "User"));

            await _userManager.AddClaimsAsync(user, claims);
            return Ok();
        }

        private string GetToken(IdentityUser user, IEnumerable<Claim> prinicpal) 
        {
            var claims = prinicpal.ToList();
            //claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SeсretKey));

            var jwt = new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                notBefore: DateTime.UtcNow,
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLogin model)
        {
            if (!ModelState.IsValid)
                return NoContent();

            var user = await _userManager.FindByNameAsync(model.LoginProp);


            var result = await _signInManager.PasswordSignInAsync(model.LoginProp,
                    model.Password,
                    false,
                    lockoutOnFailure: false);

            if (result.Succeeded)
            {
                IEnumerable<Claim> claims = await _userManager.GetClaimsAsync(user);
                var token = GetToken(user, claims);

                return Ok(token);
            }

            return BadRequest();
        }
    }
}

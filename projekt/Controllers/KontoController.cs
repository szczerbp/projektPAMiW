using Azure.Core;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using projekt.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace projekt.Controllers
{
    public class KontoController : Controller
    {
        private readonly DatabaseDbContext _db;
        private IConfiguration _config;

        public KontoController(DatabaseDbContext db, IConfiguration config)
        {
            _db = db;
            _config = config;
        }

        //GET 
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Konto k)
        {
            if (ModelState.IsValid)
            {
                k.Haslo = BCrypt.Net.BCrypt.HashPassword(k.Haslo);
                k.TypKonta = "user";
                _db.Konta.Add(k);
                _db.Uzytkownicy.Add(k.Uzytkownik);
                _db.SaveChanges();
                return RedirectToAction("Zaloguj");
            }
            return View(k);
        }

        //GET
        public IActionResult Zaloguj()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Zaloguj(Konto k)
        {
            Konto? konto = _db.Konta.SingleOrDefault(ko => ko.Login.Equals(k.Login));

            if (konto != null)
            {
                if (BCrypt.Net.BCrypt.Verify(k.Haslo, konto.Haslo))
                {
                    var tokenString = GenerateJSONWebToken(k);
                    SetJWTCookie(tokenString);

                    return View();
                }
            }
            return View();
        }

        private string GenerateJSONWebToken(Konto k)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, k.Login.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private void SetJWTCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddMinutes(120),
            };
            Response.Cookies.Append("jwtCookie", token, cookieOptions);
        }

        [Authorize]
        public string AuthLogin()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            if (identity != null)
            {
                IEnumerable<Claim> claims = identity.Claims;
                //return identity.Claims.ElementAt(0).Value;
                return claims.ElementAt(0).Value;
            }
            return null;
        }


    }
}

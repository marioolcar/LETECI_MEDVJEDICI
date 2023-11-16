using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SpotPicker.Model;
using SpotPicker.Service.Interface;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SpotPicker.Controllers
{
    public class KorisnikController : BaseController
    {
        private readonly IKorisnikService _korisnikService;

        public KorisnikController(IKorisnikService korisnikService)
        {
            _korisnikService = korisnikService;
        }

        [Authorize(Policy = "AccessLevel3")]
        [HttpGet]
        public async Task<IActionResult> GetAllKorisnik()
        {
            return Ok( await _korisnikService.GetAllKorisnik() );
        }

        [Authorize(Policy = "AccessLevel3")]
        [HttpGet]
        public async Task<IActionResult> GetKorisnik(int korisnikId)
        {
            var ret = await _korisnikService.GetKorisnik(korisnikId);
            return Ok( ret );
        }

        [HttpPost]
        public async Task<IActionResult> Register(Korisnik korisnik)
        {
            var registracijaUspjesna = await _korisnikService.Register(korisnik);

            return Ok( registracijaUspjesna );
            
        }

        [Authorize(Policy = "AccessLevel3")]
        [HttpPost]
        public async Task<IActionResult> ChangeAccountEnabled(int korisnikId)
        {
            Korisnik k = await _korisnikService.ChangeAccountEnabled(korisnikId);
            if(k.AccountEnabled == true) {
                return Ok("AccountEnabled promijenjeno sa false na true.");
            }
            else
            {
                return Ok("AccountEnabled promijenjeno sa true na false.");
            }

        }
        private string GenerateToken(string username, int accessLevel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = "4a56f0a7c38b9d8e2a4f6d8c2b9e1d5adsfzesfdsfzjdsfdas";
            var issuer = "SpotPicker";
            var audience = "audience";

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim("username", username),
                new Claim("accessLevel", accessLevel.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    SecurityAlgorithms.HmacSha256Signature
                ),
                Issuer = issuer,
                Audience = audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return tokenString;
        }


        [HttpPost]
        public async Task<IActionResult> Login(string? user, string? password, string? confirmpassword)
        {
            var kor = await _korisnikService.Login(user, password, confirmpassword);
            if (kor == null) return Ok();
            var token = GenerateToken(kor.Username, (int) kor.RazinaPristupa);
            return Ok(token);
        }

        [Authorize(Policy = "AccessLevel3")]
        [HttpPost]
        public async Task<IActionResult> UpdateKorisnik(Korisnik korisnik)
        {
            return Ok(await _korisnikService.UpdateKorisnik(korisnik));
        }

        [Authorize(Policy = "AccessLevel3")]
        [HttpGet]
        public async Task<IActionResult> GetAllKorisnikForApproval()
        {
            return Ok(await _korisnikService.GetAllKorisnikForApproval());
        }

        [HttpPost]
        public async Task<IActionResult> ConfirmKorisnikEmail(string email) 
        {
            return Ok(await _korisnikService.ConfirmKorisnikEmail(email));
        }
    }
}

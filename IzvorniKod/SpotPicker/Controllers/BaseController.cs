using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SpotPicker.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    [EnableCors("AllRequestPolicy")]
    public class BaseController : ControllerBase
    {
       public int KorisnikID { get
            {
                if (!string.IsNullOrEmpty(HttpContext.Request.Headers["username"]))
                {
                    return Convert.ToInt32(HttpContext.Request.Headers["username"]);
                }
                else return 0;
            } 
        }

        public int AccessLevel { get
            {
                if (!string.IsNullOrEmpty(HttpContext.Request.Headers["accessLevel"]))
                {
                    return Convert.ToInt32(HttpContext.Request.Headers["accessLevel"]);
                }
                else return 0;
            } 
        }
        [HttpGet]
        public IActionResult GenerateToken(string username, int accessLevel)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = "segvsrhbrzbzjetjneenbwwrrwhrwv";
            var issuer = "SpotPicker";
            var audience = "audience";

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, accessLevel.ToString())
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

            return Ok(new { Token = tokenString });
        }
    }
}

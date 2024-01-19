using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SpotPicker.Model;
using SpotPicker.Model.Dtos;
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
        public async Task<IActionResult> Register(KorisnikDto korisnik)
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
        public async Task<IActionResult> Login(string username, string password)
        {
            var kor = await _korisnikService.Login(username, password, password);
            if (kor == null) return Ok();
            kor.token = GenerateToken(kor.Username, (int) kor.RazinaPristupa);
            return Ok(kor);
        }

        [Authorize(Policy = "AccessLevel3")]
        [HttpPost]
        public async Task<IActionResult> UpdateKorisnik(KorisnikDto korisnik)
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

        [HttpPost]
        public async Task<IActionResult> SendConfirmationEmail(string userEmail, string confirmationCode)
        {
            await _korisnikService.SendConfirmationEmail(userEmail, confirmationCode);
            return Ok();
        }
        [Authorize(Policy = "AccessLevel2")]
        [HttpGet]
        public async Task<IActionResult> GetParkingsForVlasnik(int korisnikId)
        {
            return Ok(await _korisnikService.GetParkingsForVlasnik(korisnikId));
        }
        [HttpGet]
        public async Task<IActionResult> GetParkingSpotsForParking(int parkingId)
        {
            return Ok(await _korisnikService.GetParkingSpotsForParking(parkingId));
        }
        [Authorize(Policy = "AccessLevel2")]
        [HttpPost]
        public async Task<IActionResult> CreateParking(Parking parking)
        {
            var result = await _korisnikService.CreateParking(parking);
            if (result == null) return BadRequest("Failed to create item.");
            return Ok(result);
        }
        [Authorize(Policy = "AccessLevel2")]
        [HttpPost]
        public async Task<IActionResult> CreateParkingSpot(ParkingSpot parkingSpot)
        {
            var result = await _korisnikService.CreateParkingSpot(parkingSpot);
            if (result == null) return BadRequest("Failed to create item.");
            return Ok(result);
        }
        [Authorize(Policy = "AccessLevel2")]
        [HttpPost]
        public async Task<IActionResult> UpdateParking(Parking parking)
        {
            var result = await _korisnikService.UpdateParking(parking);
            if (result == null) return BadRequest("Failed to update item.");
            return Ok(result);
        }
        [Authorize(Policy = "AccessLevel2")]
        [HttpPost]
        public async Task<IActionResult> UpdateParkingSpot(ParkingSpot spot)
        {
            var result = await _korisnikService.UpdateParkingSpot(spot);
            if (result == null) return BadRequest("Failed to update item.");
            return Ok(result);
        }
        [Authorize(Policy = "AccessLevel2")]
        [HttpDelete]
        public async Task<IActionResult> DeleteParkingSpot(int parkingSpotId)
        {
            var result = await _korisnikService.DeleteParkingSpot(parkingSpotId);
            if (result != null) return BadRequest("Failed to delete item.");
            return Ok(result);
        }
        [Authorize(Policy = "AccessLevel2")]
        [HttpDelete]
        public async Task<IActionResult> DeleteParking(int parkingId)
        {
            var result = await _korisnikService.DeleteParking(parkingId);
            if (result != null) return BadRequest("Failed to delete item.");
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetParkingsForKorisnik()
        {
            return Ok(await _korisnikService.GetParkingsForKorisnik());
        }

        [Authorize(Policy = "AccessLevel1")]
        [HttpGet]
        public async Task<IActionResult> GetAvailableParkingSpotsForParking(int parkingId, DateTime start, DateTime end)
        {
            return Ok(await _korisnikService.GetAvailableParkingSpotsForParking(parkingId, start, end));
        }
        [Authorize(Policy = "AccessLevel1")]
        [HttpPost]
        public async Task<IActionResult> ChangeBalance(int korisnikId, double amount)
        {
            return Ok(await _korisnikService.ChangeBalance(korisnikId, amount));
        }
        [Authorize(Policy = "AccessLevel1")]
        [HttpPost]
        public async Task<IActionResult> MakeReservation(Reservation reservation)
        {
            if (reservation.DateTimeEnd <= reservation.DateTimeStart) return BadRequest("End time must be after start time.");
            if (reservation.DateTimeStart.Date <= DateTime.Today) return BadRequest("Reservations must be tommorow or later.");
            try
            {
                var result = await _korisnikService.MakeReservation(reservation);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}

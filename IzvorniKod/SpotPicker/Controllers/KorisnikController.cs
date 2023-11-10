using Microsoft.AspNetCore.Mvc;
using SpotPicker.Model;
using SpotPicker.Service.Interface;

namespace SpotPicker.Controllers
{
    public class KorisnikController : BaseController
    {
        private readonly IKorisnikService _korisnikService;

        public KorisnikController(IKorisnikService korisnikService)
        {
            _korisnikService = korisnikService;
        }

        [HttpGet("GetAllKorisnik")]
        public async Task<IActionResult> GetAllKorisnik()
        {
            return Ok( await _korisnikService.GetAllKorisnik() );
        }
        [HttpGet("GetKorisnik")]
        public async Task<IActionResult> GetKorisnik(int korisnikId)
        {
            var ret = await _korisnikService.GetKorisnik(korisnikId);
            return Ok( ret );
        }

        [HttpPost("Registracija")]
        public async Task<IActionResult> Registracija(Korisnik korisnik)
        {
            var registracijaUspjesna = await _korisnikService.Registracija(korisnik);

            return Ok( registracijaUspjesna );
            
        }

        [HttpPost("ChangeAccountEnabled")]
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

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string? username, string? password)
        {
            var kor = await _korisnikService.Login(username, password);
            if (kor.Username == null) return BadRequest("Molimo unesite vaš username.");
            if (kor.Password == null) return BadRequest("Molimo unesite vaš password.");

            if (kor == null)
            {
                return BadRequest("Pogrešan username ili password.");
            }
            else if (kor.AccountEnabled == false)
            {
                return BadRequest("Račun nije omogućen.");
            }
            else
            {
                return Ok(" ");
            }
        }


    }
}

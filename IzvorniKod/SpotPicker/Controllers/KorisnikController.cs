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
        public async Task<IActionResult> Registracija(string? username, string? password, int? razinaPristupa, string? name, string? surname, string? bankAccountNumber, string? email)
        {
            var registracijaUspjesna = await _korisnikService.Registracija(username, password, razinaPristupa, name, surname, bankAccountNumber, email);

            if (registracijaUspjesna != null)
            {
                return Ok($"Uspješno ste se registrirali! Korisnik ID: {registracijaUspjesna.KorisnikID}");
            }
            else
            {
                return BadRequest("Registracija nije uspjela. Provjerite unesene podatke.");
            }
        }
    }
}

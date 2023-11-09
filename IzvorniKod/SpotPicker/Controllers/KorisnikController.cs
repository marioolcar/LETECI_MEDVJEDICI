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
            Korisnik kor = new Korisnik();
            kor.Username = username;
            kor.Password = password;
            kor.Name = name;
            kor.Surname = surname;
            kor.RazinaPristupa = razinaPristupa;
            kor.BankAccountNumber = bankAccountNumber;
            kor.Email = email;
            var registracijaUspjesna = await _korisnikService.Registracija(kor);

            if (registracijaUspjesna != null)
            {
                return Ok($"Uspješno ste se registrirali! Korisnik ID: {registracijaUspjesna.KorisnikID}");
            }
            else
            {
                return BadRequest("Registracija nije uspjela. Provjerite unesene podatke.");
            }
        }

        [HttpPost("Enable")]
        public async Task<IActionResult> Enable(int korisnikId)
        {
            Korisnik k = await _korisnikService.Enable(korisnikId);
            if(k.AccountEnabled == true) {
                return Ok("AccountEnabled promijenjeno sa false na true.");
            }
            else
            {
                return Ok("AccountEnabled promijenjeno sa true na false.");
            }

        }
    }
}

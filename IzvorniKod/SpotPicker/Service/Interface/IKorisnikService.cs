using SpotPicker.Model;
using SpotPicker.Model.Dtos;

namespace SpotPicker.Service.Interface
{
    public interface IKorisnikService
    {
        public Task<List<Korisnik>> GetAllKorisnik();
        public Task<List<Korisnik>> GetAllKorisnikForApproval();
        public Task<Korisnik?> GetKorisnik(int korisnikId);
        public Task<Korisnik?> Register(KorisnikDto k);
        public Task<Korisnik?> ChangeAccountEnabled(int korisnikId);
        public Task<Korisnik?> Login(string? username, string? password, string? confirmpassword);
        public Task<Korisnik?> UpdateKorisnik(KorisnikDto korisnik);
        public Task<bool> ConfirmKorisnikEmail(string email);
    }
}

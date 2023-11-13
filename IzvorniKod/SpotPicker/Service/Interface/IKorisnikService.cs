using SpotPicker.Model;

namespace SpotPicker.Service.Interface
{
    public interface IKorisnikService
    {
        public Task<List<Korisnik>> GetAllKorisnik();
        public Task<Korisnik?> GetKorisnik(int korisnikId);
        public Task<Korisnik?> Register(Korisnik k);
        public Task<Korisnik?> ChangeAccountEnabled(int korisnikId);
        public Task<Korisnik?> Login(string? username, string? password, string? confirmpassword);
    }
}

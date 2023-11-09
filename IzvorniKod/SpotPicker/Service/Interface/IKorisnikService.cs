using SpotPicker.Model;

namespace SpotPicker.Service.Interface
{
    public interface IKorisnikService
    {
        public Task<List<Korisnik>> GetAllKorisnik();
        public Task<Korisnik?> GetKorisnik(int korisnikId);
        public Task<Korisnik?> Registracija(Korisnik k);
        public Task<Korisnik?> Enable(int korisnikId);
    }
}

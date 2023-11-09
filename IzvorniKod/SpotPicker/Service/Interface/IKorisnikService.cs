using SpotPicker.Model;

namespace SpotPicker.Service.Interface
{
    public interface IKorisnikService
    {
        public Task<List<Korisnik>> GetAllKorisnik();
        public Task<Korisnik?> GetKorisnik(int korisnikId);
        public Task<Korisnik?> Registracija(string? username, string? password, int? razinaPristupa, string? name, string? surname, string? bankAccountNumber, string? email);
    }
}

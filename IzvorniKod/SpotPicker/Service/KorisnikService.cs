using Microsoft.EntityFrameworkCore;
using SpotPicker.Model;
using SpotPicker.Service.Interface;

namespace SpotPicker.Service
{
    public class KorisnikService : IKorisnikService
    {
        private readonly SpotPickerContext _context;

        public KorisnikService(SpotPickerContext context)
        {
            _context = context;
        }

        public async Task<List<Korisnik>> GetAllKorisnik()
        {
            var ret = await _context.Korisnik.ToListAsync();
            return ret;
        }

        public async Task<Korisnik?> GetKorisnik(int korisnikId)
        {
            var ret = await _context.Korisnik.Where(x => x.KorisnikID == korisnikId).FirstOrDefaultAsync();
            return ret;
        }

        public async Task<Korisnik?> Registracija(string? username, string? password, int? razinaPristupa, string? name, string? surname, string? bankAccountNumber, string? email)
        {
            var ki = await _context.Korisnik.Where(x => x.Username == username).FirstOrDefaultAsync();
            if (ki != null) { return null; }
            else
            {
                Korisnik? em = await _context.Korisnik.Where(x => x.Email == email).FirstOrDefaultAsync();
                if (em != null) { return null; }
                else
                {
                    Korisnik novi = new Korisnik();
                    int NajveciId;
                    var KorisnikSaNajvecim = await _context.Korisnik.OrderByDescending(x => x.KorisnikID).FirstOrDefaultAsync();
                    if(KorisnikSaNajvecim == null)
                    {
                        NajveciId = 0;
                    }
                    else
                    {
                        NajveciId = KorisnikSaNajvecim.KorisnikID;
                    }
                    novi.Username = username;
                    novi.RazinaPristupa = razinaPristupa;
                    novi.Password = password;
                    novi.Email = email;
                    novi.Name = name;
                    novi.Surname = surname;
                    novi.BankAccountNumber = bankAccountNumber;
                    novi.KorisnikID = NajveciId+1;
                 
                    await _context.AddAsync(novi);
                    await _context.SaveChangesAsync();
                    return novi;
                }
            }
        }
    }
}

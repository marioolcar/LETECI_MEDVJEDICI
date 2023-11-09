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

        public async Task<Korisnik?> Registracija(Korisnik k)
        {
            var ki = await _context.Korisnik.Where(x => x.Username == k.Username).FirstOrDefaultAsync();
            if (ki != null) { return null; }
            else
            {
                Korisnik? em = await _context.Korisnik.Where(x => x.Email == k.Email).FirstOrDefaultAsync();
                if (em != null) { return null; }
                else
                {
                    Korisnik novi = new Korisnik();
                    int NajveciId;
                    var KorisnikSaNajvecim = await _context.Korisnik.OrderByDescending(x => x.KorisnikID).FirstOrDefaultAsync();
                    if (KorisnikSaNajvecim == null)
                    {
                        NajveciId = 0;
                    }
                    else
                    {
                        NajveciId = KorisnikSaNajvecim.KorisnikID;
                    }
                    novi.Username = k.Username;
                    novi.RazinaPristupa = k.RazinaPristupa;
                    novi.Password = k.Password;
                    novi.Email = k.Email;
                    novi.Name = k.Name;
                    novi.Surname = k.Surname;
                    novi.BankAccountNumber = k.BankAccountNumber;
                    novi.KorisnikID = NajveciId + 1;

                    await _context.AddAsync(novi);
                    await _context.SaveChangesAsync();
                    return novi;
                }
            }
        }


        public async Task<Korisnik?> Enable(int korisnikId)
        {

            Korisnik korisn = await _context.Korisnik.Where(x => x.KorisnikID == korisnikId).FirstOrDefaultAsync();

            if (korisn.AccountEnabled == false)
            {
                _context.Update(korisn);
                korisn.AccountEnabled = true;
            }
            else
            {
                _context.Update(korisn);
                korisn.AccountEnabled = false;
            }
            _context.SaveChanges();

            return korisn;

        }

        public async Task<Korisnik?> Login(string? user, string? password)
        {        
            var k = await _context.Korisnik.Where(x => (x.Username == user || x.Email == user) && x.Password == password).FirstOrDefaultAsync();
            if (k == null) { return null; }
            else
            {
                if (k.AccountEnabled == false) { return new Korisnik { AccountEnabled = false }; }
                return k;
            }
        }
    }
}

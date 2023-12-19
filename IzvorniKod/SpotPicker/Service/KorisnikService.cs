using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SpotPicker.Model;
using SpotPicker.Model.Dtos;
using SpotPicker.Service.Interface;

namespace SpotPicker.Service
{
    public class KorisnikService : IKorisnikService
    {
        private readonly SpotPickerContext _context;
        private readonly IMapper _mapper;

        public KorisnikService(SpotPickerContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;  
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

        public async Task<Korisnik?> Register(KorisnikDto k)
        {
            byte[]? imageData = null;
            var file = k.PictureData;
            using (var stream = new MemoryStream())

            {

                await file.CopyToAsync(stream);

                imageData = stream.ToArray();

            }
            var korisnikModel = _mapper.Map<Korisnik>(k);
            korisnikModel.PictureData = imageData;
            var ki = await _context.Korisnik.Where(x => x.Username == k.Username || x.Email == k.Email).FirstOrDefaultAsync();
            if (ki != null) return null;
            else
            {
                await _context.Korisnik.AddAsync(korisnikModel);
                await _context.SaveChangesAsync();
                return korisnikModel;
            }
            
        }

        public async Task<Korisnik?> ChangeAccountEnabled(int korisnikId){
            
            Korisnik korisn = await _context.Korisnik.Where(x => x.KorisnikID == korisnikId).FirstOrDefaultAsync();
            if(korisn.AccountEnabled == false)
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



        public async Task<Korisnik?> Login(string? user, string? password, string? confirmpassword)
        {
            if (user == null) throw new ArgumentException("Molimo unesite vaš username ili e-mail.");
            if (password == null) throw new ArgumentException("Molimo unesite vaš password.");
            if (confirmpassword == null) throw new ArgumentException("Molimo potvrdite vaš password.");
            if (confirmpassword != password) throw new ArgumentException("Vaši passwordi se ne podudaraju.");

            var k = await _context.Korisnik.Where(x => (x.Username == user || x.Email == user) && x.Password == password).FirstOrDefaultAsync();
            if (k == null) { return null; }
            return k;
        }

        public async Task<Korisnik?> UpdateKorisnik(KorisnikDto korisnik)
        {
            byte[]? imageData = null;
            var file = korisnik.PictureData;
            using (var stream = new MemoryStream())

            {

                await file.CopyToAsync(stream);

                imageData = stream.ToArray();

            }
            var korisnikModel = _mapper.Map<Korisnik>(korisnik);
            korisnikModel.PictureData = imageData;
            try
            {
                _context.Korisnik.Update(korisnikModel);
                await _context.SaveChangesAsync();
                return korisnikModel;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<List<Korisnik>> GetAllKorisnikForApproval()
        {
            return await _context.Korisnik.Where(k => k.EmailVerified == true && k.AccountEnabled == false).ToListAsync();
        }

        public async Task<bool> ConfirmKorisnikEmail(string email)
        {
            var korisnik = await _context.Korisnik.Where(k => k.Email.Equals(email)).FirstOrDefaultAsync();
            if (korisnik == null) return false;
            korisnik.AccountEnabled = true;
            _context.Update(korisnik);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}

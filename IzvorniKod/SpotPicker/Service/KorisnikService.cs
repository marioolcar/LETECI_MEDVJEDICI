﻿using Microsoft.EntityFrameworkCore;
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
                
                    await _context.AddAsync(k);
                    await _context.SaveChangesAsync();
                    return k;
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

        public async Task<Korisnik?> Login(string? username, string? password)
        {
            var k = await _context.Korisnik.Where(x => x.Username == username && x.Password == password).FirstOrDefaultAsync();
            if (k == null) { return null; }
            else
            {
                if (k.AccountEnabled == false) { return new Korisnik { AccountEnabled = false }; }
                return k;
            }
        }
    }
}

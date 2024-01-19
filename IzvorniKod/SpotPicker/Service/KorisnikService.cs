using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SpotPicker.Model;
using SpotPicker.Model.Dtos;
using SpotPicker.Service.Interface;
using System.Net.Mail;
using System.Net;
using System.Text.RegularExpressions;
using IbanNet;



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
            if (IsValidIBAN(k.BankAccountNumber) == false) throw new ArgumentException("Molimo provjerite vaš IBAN.");
            if (IsValidEmail(k.Email) == false) throw new ArgumentException("Molimo provjerite vašu e-mail adresu.");


            var korisnikModel = _mapper.Map<Korisnik>(k);
            try
            {
                byte[]? imageData = new byte[] { };
                IFormFile file = k.PictureData;
                if (file != null)
                {
                    using (var stream = new MemoryStream())

                    {
                        await file.CopyToAsync(stream);
                        imageData = stream.ToArray();
                    }
                }

                korisnikModel.PictureData = imageData;
            }
            catch (Exception ex) { korisnikModel.PictureData = null; }
            korisnikModel.ConfirmationCode = GenerateConfirmationCode();
            var ki = await _context.Korisnik.Where(x => x.Username == k.Username || x.Email == k.Email).FirstOrDefaultAsync();
            if (ki != null) return null;
            else
            {
                await _context.Korisnik.AddAsync(korisnikModel);
                await _context.SaveChangesAsync();
                await SendConfirmationEmail(korisnikModel.Email, korisnikModel.ConfirmationCode);
                return korisnikModel;
            }
        }

        public async Task<Korisnik?> ChangeAccountEnabled(int korisnikId)
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
            if (IsValidIBAN(korisnik.BankAccountNumber) == false) throw new ArgumentException("Molimo provjerite vaš IBAN.");
            if (IsValidEmail(korisnik.Email) == false) throw new ArgumentException("Molimo provjerite vašu e-mail adresu.");

            byte[]? imageData = new byte[] { };
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
            korisnik.EmailVerified = true;
            _context.Update(korisnik);
            await _context.SaveChangesAsync();
            return true;
        }

        // Generiranje i slanje e-pošte s linkom za potvrdu
        public async Task SendConfirmationEmail(string userEmail, string confirmationCode)
        {
            try
            {
                // Kreiranje e-poruke
                var message = new MailMessage();
                message.From = new MailAddress("letecimedvjediciprogi@gmail.com");
                message.To.Add(new MailAddress(userEmail));
                message.Subject = "Potvrdite registraciju";
                message.Body = $"Molimo potvrdite registraciju klikom na ovaj link: http://localhost:3001/potvrda?code={confirmationCode}";
                message.IsBodyHtml = true;

                // Slanje e-poruke putem SMTP-a (primjer)
                using (var smtpClient = new SmtpClient("smtp.gmail.com", 587))
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.Credentials = new NetworkCredential("letecimedvjediciprogi@gmail.com", "daqdvhcdexdqplhc");
                    smtpClient.Send(message);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Došlo je do greške: {ex.Message}");
            }
        }

        private static bool IsValidEmail(string email)
        {
            string Check_mail = @"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
            return Regex.IsMatch(email, Check_mail);
        }

        private static bool IsValidIBAN(string iban)
        {
            var ibanValidator = new IbanValidator();
            var validationResult = ibanValidator.Validate(iban);
            return validationResult.IsValid;
        }

        // Proces potvrde registracije
        public async Task<bool> ConfirmRegistration(string userEmail, string confirmationCode)
        {
            // Ovdje provjerite podudara li se kod/token s onim koji ste spremili prilikom registracije korisnika.
            var ki = await _context.Korisnik.Where(k => k.Email == userEmail && k.ConfirmationCode == confirmationCode).FirstOrDefaultAsync();
            if (ki == null) return false;
            // Ako se podudara, označite korisnički račun kao potvrđen.
            await ConfirmKorisnikEmail(userEmail);
            // Primjerice, koristite Entity Framework za ažuriranje statusa potvrde.
            // Nakon uspješne potvrde, možete vratiti true
            return true;
        }
        static string GenerateConfirmationCode()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789"; // Dozvoljeni znakovi za potvrdni kod
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, 8)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
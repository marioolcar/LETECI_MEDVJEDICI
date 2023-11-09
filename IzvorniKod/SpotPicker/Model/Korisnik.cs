using System.ComponentModel.DataAnnotations;

namespace SpotPicker.Model
{
    public class Korisnik
    {
        public Korisnik()
        {
        }

        public Korisnik( string? username, string? password, int? razinaPristupa, string? name, string? surname, byte[]? pictureData, string? bankAccountNumber, string? email)
        {
            KorisnikID = 0;
            Username = username;
            Password = password;
            RazinaPristupa = razinaPristupa;
            Name = name;
            Surname = surname;
            PictureData = null;
            BankAccountNumber = bankAccountNumber;
            Email = email;
            AccountEnabled = false;
        }

        [Key]
        public int KorisnikID { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }  
        public int? RazinaPristupa { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public byte[]? PictureData { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? Email { get; set; }
        public bool AccountEnabled { get; set; }
    }
}

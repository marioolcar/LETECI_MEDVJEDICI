using System.ComponentModel.DataAnnotations;

namespace SpotPicker.Model
{
    public class Korisnik
    {
        public Korisnik()
        {
        }

        public Korisnik(string? username, string? password, int? razinaPristupa)
        {
            KorisnikID = 0;
            Username = username;
            Password = password;
            RazinaPristupa = razinaPristupa;
        }

        [Key]
        public int KorisnikID { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }  
        public int? RazinaPristupa { get; set; }

    }
}

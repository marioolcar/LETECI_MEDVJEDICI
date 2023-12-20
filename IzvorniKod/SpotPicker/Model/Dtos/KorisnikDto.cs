namespace SpotPicker.Model.Dtos
{
    public class KorisnikDto
    {
        public KorisnikDto()
        {
        }

        public int KorisnikID { get; set; }
        public string? Username { get; set; }
        public string? Password { get; set; }
        public int? RazinaPristupa { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public IFormFile? PictureData { get; set; }
        public string? BankAccountNumber { get; set; }
        public string? Email { get; set; }
        public bool AccountEnabled { get; set; }
        public bool? EmailVerified { get; set; }
    }
}

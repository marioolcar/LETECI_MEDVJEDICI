namespace SpotPicker.Model
{
    public class Wallet
    {
        public Wallet(int korisnikId)
        {
            WalletId = 0;
            KorisnikId = korisnikId;
            Balance = 0;
        }

        public int WalletId { get; set; }
        public int KorisnikId { get; set; }
        public double Balance { get; set; }
    }
}

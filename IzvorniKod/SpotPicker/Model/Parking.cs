namespace SpotPicker.Model
{
    public class Parking
    {
        public Parking()
        {
        }

        public int ParkingID { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        //public byte[]? Photo { get; set; }
        public int PricePerHour { get; set; }
        public int KorisnikId { get; set; }
    }
}
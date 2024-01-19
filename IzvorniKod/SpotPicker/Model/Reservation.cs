namespace SpotPicker.Model
{
    public class Reservation
    {
        public int ReservationId { get; set; }
        public int KorisnikId { get; set; }
        public int ParkingId { get; set; }
        public DateTime DateTimeStart { get; set; }
        public DateTime DateTimeEnd { get; set; }
        public int ParkingPlaceId { get; set; }
    }

    

}

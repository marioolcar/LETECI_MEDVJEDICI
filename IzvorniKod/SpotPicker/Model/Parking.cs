namespace SpotPicker.Model
{
    public class Parking
    {
        public Parking()
        {
        }

        public Parking(int parkingID, string? name, string? description, byte[]? photo, byte[]? pricePerHour, int? userId, double? coordinateX, double? coordinateY)
        {
            ParkingID = parkingID;
            Name = name;
            Description = description;
            Photo = photo;
            PricePerHour = pricePerHour;
            UserId = userId;
        }

        public int ParkingID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public byte[]? Photo { get; set; }
        public byte[]? PricePerHour { get; set; }
        public int? UserId { get; set; }
    }
}
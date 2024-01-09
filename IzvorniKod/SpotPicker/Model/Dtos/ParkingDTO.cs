namespace SpotPicker.Model.Dtos
{
    public class ParkingDto
    {
        public ParkingDto()
        {
        }

        public int ParkingID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public IFormFile? Photo { get; set; }
        public IFormFile? PricePerHour { get; set; }
        public int? UserId { get; set; }

    }
}

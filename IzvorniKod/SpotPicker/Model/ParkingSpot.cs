using Microsoft.SqlServer.Types;

namespace SpotPicker.Model
{
    public class ParkingSpot
    {
        public ParkingSpot()
        {
        }
        public int ParkingSpotId { get; set; }
        public int ParkingID { get; set; }
        
        public string? spotType { get; set; }
        public double coordinate1 { get; set; }
        public double coordinate2 { get; set; }
        public double coordinate3 { get; set; }
        public double coordinate4 { get; set; }
        public bool? isEnabled { get; set; }
    }
}

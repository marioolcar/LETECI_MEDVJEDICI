using Microsoft.SqlServer.Types;

namespace SpotPicker.Model.Dtos
{
    public class ParkingSpotDto 
    {
        public ParkingSpotDto()
        {
        }
        public int ParkingID { get; set; }
        public int? SpotId { get; set; }
        public string? spotType { get; set; }
        public SqlGeography coordinate1 { get; set; }
        public SqlGeography coordinate2 { get; set; }
        public SqlGeography coordinate3 { get; set; }
        public SqlGeography coordinate4 { get; set; }
        public bool? isEnabled { get; set; }
    }
}

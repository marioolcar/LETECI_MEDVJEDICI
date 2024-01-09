using Microsoft.SqlServer.Types;

namespace SpotPicker.Model
{
    public class ParkingSpot
    {
        public ParkingSpot()
        {
        }

        public ParkingSpot(int parkingID, int? spotId, string? spotType, SqlGeography coordinate1, SqlGeography coordinate2, SqlGeography coordinate3, SqlGeography coordinate4, bool? isEnabled)
        {
            ParkingID = parkingID;
            SpotId = spotId;
            this.spotType = spotType;
            this.coordinate1 = coordinate1;
            this.coordinate2 = coordinate2;
            this.coordinate3 = coordinate3;
            this.coordinate4 = coordinate4;
            this.isEnabled = isEnabled;
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

using SpotPicker.Model;
using SpotPicker.Model.Dtos;

namespace SpotPicker.Service.Interface
{
    public interface IKorisnikService
    {
        public Task<List<Korisnik>> GetAllKorisnik();
        public Task<List<Korisnik>> GetAllKorisnikForApproval();
        public Task<Korisnik?> GetKorisnik(int korisnikId);
        public Task<Korisnik?> Register(KorisnikDto k);
        public Task<Korisnik?> ChangeAccountEnabled(int korisnikId);
        public Task<Korisnik?> Login(string? username, string? password, string? confirmpassword);
        public Task<Korisnik?> UpdateKorisnik(KorisnikDto korisnik);
        public Task<bool> ConfirmKorisnikEmail(string email);
        public Task SendConfirmationEmail(string userEmail, string confirmationCode);
        public Task<bool> ConfirmRegistration(string userEmail, string confirmationCode);
        public Task<List<Parking>> GetParkingsForVlasnik(int korisnikId);
        public Task<List<Parking>> GetParkingsForKorisnik();
        public Task<List<ParkingSpot>> GetParkingSpotsForParking(int parkingId);
        public Task<List<ParkingSpot>> GetAvailableParkingSpotsForParking(int parkingId, DateTime start, DateTime end);
        public Task<Parking?> CreateParking(Parking parking);
        public Task<ParkingSpot?> CreateParkingSpot(ParkingSpot parkingSpot);
        public Task<Parking?> UpdateParking(Parking parking);
        public Task<ParkingSpot?> UpdateParkingSpot(ParkingSpot spot);
        public Task<ParkingSpot?> DeleteParkingSpot(int parkingSpotId);
        public Task<Parking?> DeleteParking(int parkingId);
        public Task<double> ChangeBalance(int korisnikId, double amount);
        public Task<Reservation?> MakeReservation(Reservation reservation);
    }
}

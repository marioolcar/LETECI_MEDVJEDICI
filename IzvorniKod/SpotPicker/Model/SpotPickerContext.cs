using Microsoft.EntityFrameworkCore;

namespace SpotPicker.Model;
public class SpotPickerContext : DbContext
{
    public SpotPickerContext(DbContextOptions<SpotPickerContext> options) : base(options)
    {
    }

    public DbSet<Korisnik> Korisnik { get; set; }
    public DbSet<Parking> Parking { get; set; }
    public DbSet<ParkingSpot> ParkingSpot { get; set; }
    public DbSet<Reservation> Reservation { get; set; }
    public DbSet<Wallet> Wallet { get; set; }
}

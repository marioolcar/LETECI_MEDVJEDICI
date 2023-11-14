using Microsoft.EntityFrameworkCore;

namespace SpotPicker.Model;
public class SpotPickerContext : DbContext
{
    public SpotPickerContext(DbContextOptions<SpotPickerContext> options) : base(options)
    {
    }

    public DbSet<Korisnik> Korisnik { get; set; }
}

using Microsoft.EntityFrameworkCore;
using projekt.Data.Models;
using System.Reflection.Emit;



public class DatabaseDbContext : DbContext
{
    public DatabaseDbContext(DbContextOptions<DatabaseDbContext> options) : base(options)
    {
    }

    public DbSet<Obserwacja> Obserwacje { get; set; }
    public DbSet<Uzytkownik> Uzytkownicy { get; set; }
    public DbSet<Ogloszenie> Ogloszenia { get; set; }
    public DbSet<Konto> Konta { get; set; }
    public DbSet<Czat> Czaty { get; set; }
    public DbSet<Wiadomosc> Wiadomosci{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Wiadomosc>()
             .HasOne(w => w.Czat)
             .WithMany(s => s.Wiadomosci)
             .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Obserwacja>()
            .HasOne(ob => ob.Ogloszenie)
            .WithMany(og => og.Obserwacje)
            .OnDelete(DeleteBehavior.Restrict);
    }

}
    


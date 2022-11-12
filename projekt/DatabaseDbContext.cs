using Microsoft.EntityFrameworkCore;
using projekt.Data.Models;
using System.Reflection.Emit;



public class DatabaseDbContext : DbContext
{

    public DbSet<Obserwacja> Obserwacje { get; set; }
    public DbSet<Uzytkownik> Uzytkownicy { get; set; }
    public DbSet<Ogloszenie> Ogloszenia { get; set; }
    public DbSet<Konto> Konta { get; set; }
    public DbSet<Skrzynka> Skrzynki { get; set; }
    public DbSet<Wiadomosc> Wiadomosci{ get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        /*
        modelBuilder.Entity<Obserwacja>()
        .HasOne(og => og.Ogloszenie)
        .WithMany(ob => ob.Obserwacje)
        .HasForeignKey(oi => oi.OgloszenieId);

        modelBuilder.Entity<Obserwacja>()
        .HasOne(uz => uz.Uzytkownik)
        .WithMany(ob => ob.Obserwacje)
        .HasForeignKey(ui => ui.UzytkownikId);

        */
        /*
        //modelBuilder.Entity<Uzytkownik>()
        //        .HasOne(u => u.Konto)
        //        .WithOne(k => k.Uzytkownik);
        
        modelBuilder.Entity<Konto>()
            .HasOne(k => k.Uzytkownik)
            .WithOne(u => u.Konto);

        modelBuilder.Entity<Uzytkownik>()
                .HasOne(u => u.Skrzynka)
                .WithOne(s => s.Wlasciciel);

        
        //modelBuilder.Entity<Uzytkownik>()
        //        .HasMany(u => u.Wiadomosci)
        //        .WithOne(w => w.Autor);
        

        modelBuilder.Entity<Wiadomosc>()
                .HasOne(w => w.Autor)
                .WithMany(a => a.Wiadomosci)
                .HasForeignKey(w => w.AutorId);

        modelBuilder.Entity<Wiadomosc>()
                .HasOne(w => w.Skrzynka)
                .WithMany(s => s.Wiadomosci)
                .HasForeignKey(w => w.SkrzynkaId);

        
        //modelBuilder.Entity<Skrzynka>()
        //        .HasOne(s => s.Wlasciciel)
        //        .WithOne(w => w.Skrzynka);
        */

        modelBuilder.Entity<Wiadomosc>()
             .HasOne(w => w.Skrzynka)
             .WithMany(s => s.Wiadomosci)
             .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Obserwacja>()
            .HasOne(ob => ob.Ogloszenie)
            .WithMany(og => og.Obserwacje)
            .OnDelete(DeleteBehavior.Restrict);
        /*
        modelBuilder.Entity<Wiadomosc>()
            .HasOne(w => w.Autor)
            .WithMany(a => a.Wiadomosci)
            .OnDelete(DeleteBehavior.Restrict);
        */
        /*
        modelBuilder.Entity<Uzytkownik>()
            .HasMany(u => u.Wiadomosci)
            .WithOne(w => w.Autor)
            .OnDelete(DeleteBehavior.Restrict);*/
    }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        //@"Data Source=.\sqlexpress;Initial Catalog=TestDB1;Integrated Security=True"
        string connString = @"Data Source=DESKTOP-H0P2GOI\SQLEXPRESS;Initial Catalog=ProjDB;Trusted_Connection=True;TrustServerCertificate=True;";
        optionsBuilder.UseSqlServer(connString);
    }


}
    


using System.ComponentModel.DataAnnotations;

namespace projekt.Data.Models
{
    public class Uzytkownik
    {
        [Key] 
        public long Id { get; set; }
        [Required]
        public string? Imie { get; set; }
        [Required]
        public string? Nazwisko { get; set; }
        [Required]
        public string? Email { get; set; }
        public Konto? Konto { get; set; }
        public List<Ogloszenie>? Ogloszenia { get; set; }
        public List<Obserwacja>? Obserwacje { get; set; }
        public List<Wiadomosc>? Wiadomosci { get; set; }
        public List<Czat>? Czat { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace projekt.Data.Models
{
    public class Uzytkownik
    {
        [Key] 
        public long Id { get; set; }
        public string? Imie { get; set; }
        public string? Nazwisko { get; set; }
        public string? Email { get; set; }

        //Navigation Properties
        public Konto? Konto { get; set; }
        //public List<Ogloszenie>? Obserwowane { get; set; }
        public List<Ogloszenie>? Ogloszenia { get; set; }

        public List<Obserwacja>? Obserwacje { get; set; }
        public List<Wiadomosc>? Wiadomosci { get; set; }
        public Skrzynka? Skrzynka { get; set; }
    }
}

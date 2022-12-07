using System.ComponentModel.DataAnnotations;

namespace projekt.Data.Models
{
    public class Obserwacja
    {
        [Key]
        public long Id { get; set; }
        public long UzytkownikId { get; set; }
        public Uzytkownik? Uzytkownik { get; set; }
        public long OgloszenieId { get; set; }
        public Ogloszenie? Ogloszenie { get; set; }

    }
}

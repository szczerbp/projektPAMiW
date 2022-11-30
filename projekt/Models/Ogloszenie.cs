using System.ComponentModel.DataAnnotations;

namespace projekt.Data.Models
{
    public class Ogloszenie
    {
        [Key]
        public long Id { get; set; }
        [Required]
        public string? Tytul { get; set; }
        [Required]
        public string? Opis { get; set; }
        public DateTime DataStworzenia { get; set; }
        public DateTime DataWygasniecia { get; set; }


        //Navigation Properties
        //public List<Uzytkownik>? Obserwujacy { get; set; }
        
        public List<Obserwacja>? Obserwacje { get; set; }
        public Uzytkownik? Autor { get; set; }
        public long AutorId { get; set; }
        
    }
}

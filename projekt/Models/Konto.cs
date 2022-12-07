using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekt.Data.Models
{
    public class Konto
    {
        [Key] 
        public long Id { get; set; }
        [Required]
        public string? Login { get; set; }
        [Required]
        public string? Haslo { get; set; }
        public string? TypKonta { get; set; }
        public Uzytkownik? Uzytkownik { get; set; }
        public long UzytkownikId { get; set; }
    }
}

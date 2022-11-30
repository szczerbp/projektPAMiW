using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekt.Data.Models
{
    public class Wiadomosc
    {
        [Key]
        public long Id { get; set; }
        public string? Tekst { get; set; }
        public DateTime Data { get; set; }

        //Navigation Properties
        //[ForeignKey("AutorId")]
        public Uzytkownik? Autor { get; set; }
        public long? AutorId { get; set; }

        //[ForeignKey("SkrzynkaId")]
        public Czat? Czat { get; set; }
        public long CzatId { get; set; }
        
    }
}

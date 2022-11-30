using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekt.Data.Models
{
    public class Czat
    {
        [Key]
        public long Id { get; set; }

        //Navigation Properties
        //[ForeignKey("WlascicielId")]
        public List<Wiadomosc>? Wiadomosci { get; set; }
        public Uzytkownik? Uzytkownik { get; set; }
        public long UzytkownikId { get; set; }

    }
}

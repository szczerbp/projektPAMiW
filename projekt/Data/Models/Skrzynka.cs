using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace projekt.Data.Models
{
    public class Skrzynka
    {
        [Key]
        public long Id { get; set; }

        //Navigation Properties
        //[ForeignKey("WlascicielId")]
        public List<Wiadomosc>? Wiadomosci { get; set; }
        public Uzytkownik? Wlasciciel { get; set; }
        public long WlascicielId { get; set; }
        

    }
}

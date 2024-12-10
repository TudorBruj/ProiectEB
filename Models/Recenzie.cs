using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProiectEB.Models
{
    public class Recenzie
    {
        public int ID { get; set; }

        [ForeignKey("Client")]
        public int IdClient { get; set; }

        [ForeignKey("Produs")]
        public int IdProdus { get; set; }

        [Range(1, 5, ErrorMessage = "Evaluarea trebuie să fie între 1 și 5.")]
        public int Evaluare { get; set; }

        [StringLength(500)]
        public string? Comentariu { get; set; }

        // Relații many-to-one
        public Client? Client { get; set; }
        public Produs? Produs { get; set; }
    }
}
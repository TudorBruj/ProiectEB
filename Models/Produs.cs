using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProiectEB.Models
{
    public class Produs
    {
        public int Id { get; set; }
        public string Nume { get; set; }

        public string Descriere { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        [Range(0.01, 10000)]
        public decimal Pret { get; set; }

        public int Cantitate { get; set; }

        public ICollection<Recenzie>? Recenzii { get; set; }

        public Stoc? Stoc { get; set; }
    }
}

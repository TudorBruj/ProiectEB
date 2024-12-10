using System.ComponentModel.DataAnnotations.Schema;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProiectEB.Models
{
    public class Stoc
    {
        public int ID { get; set; }
        [ForeignKey("Produs")]
        public int IdProdus { get; set; }

        public string? LocatieDepozit { get; set; }

        public int Cantitate { get; set; }

        public Produs? Produs { get; set; }
    }
}

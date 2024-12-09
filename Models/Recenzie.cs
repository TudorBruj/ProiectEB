using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ProiectEB.Models
{
    public class Recenzie
    {
        public int ID { get; set; }

        public int IdClient { get; set; }

        public int IdProdus { get; set; }

        public int Evaluare { get; set; }

        public string? Comentariu { get; set; }

        public Client? Client { get; set; }
        public Produs? Produs { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ProiectEB.Models
{
    public class Comanda
    {
        public int Id { get; set; }
        public int ClientId { get; set; }

        public Client? Client { get; set; }

        [DataType(DataType.Date)]
        public DateTime DataComenzii { get; set; }

        public decimal TotalPlatit { get; set; }
    }
}
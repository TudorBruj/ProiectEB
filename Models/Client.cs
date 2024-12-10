using System.ComponentModel.DataAnnotations;

namespace ProiectEB.Models
{
    public class Client
    {
        public int Id { get; set; }
        [RegularExpression(@"^[A-Z]+[a-zA-Z\s-]*$", ErrorMessage = "Prenumele trebuie sa inceapa cu majuscula (ex. Andrei sau Andrei Tudor sau AndreiTudor")]
        [StringLength(30, MinimumLength = 3)]
        public string? Nume { get; set; }
        [RegularExpression(@"^[A-Z]+[a-z\s]*$")]
        [StringLength(30, MinimumLength = 3)]
        public string? Prenume { get; set; }
        [StringLength(70)]
        public string? Adresa { get; set; }
        public string? Email { get; set; }
        [RegularExpression(@"^\(?([0-9]{4})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{3})$", ErrorMessage = "Telefonul trebuie sa fie de forma '0722-123-123' sau '0722.123.123' sau '0722 123 123'")]
        public string? NumarTelefon { get; set; }

        public ICollection<Comanda>? Comenzi { get; set; }
    }
}

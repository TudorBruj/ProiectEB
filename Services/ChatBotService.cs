namespace ProiectEB.Services
{
    public class ChatBotService
    {
        private readonly Dictionary<string, string> _faq;

        public ChatBotService()
        {
            _faq = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "Care sunt metodele de plată?", "Acceptăm plata cu cardul, PayPal și ramburs." },
                { "Care este programul de livrare?", "Livrăm de luni până vineri, între orele 9:00 și 18:00." },
                { "Cum pot returna un produs?", "Poți returna un produs în termen de 30 de zile. Detalii pe pagina Politica de retur." },
                { "Aveți reduceri?", "Da, avem reduceri săptămânale! Verifică pagina noastră de oferte." }
            };
        }

        public string GetResponse(string question)
        {
            if (_faq.TryGetValue(question.ToLower(), out var response))
            {
                return response;
            }
            return "Îmi pare rău, nu am găsit un răspuns la întrebarea ta.";
        }
    }
}

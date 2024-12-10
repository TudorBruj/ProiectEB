using Microsoft.AspNetCore.SignalR;
using ProiectEB.Models;
using ProiectEB.Services;

namespace ProiectEB.Hubs
{
    public class ChatHub : Hub
    {
        private readonly ChatBotService _chatBotService;

        public ChatHub(ChatBotService chatBotService)
        {
            _chatBotService = chatBotService;
        }

        public async Task SendMessage(string user, string message)
        {
            var response = _chatBotService.GetResponse(message);

            if (!string.IsNullOrEmpty(response) && response != "Îmi pare rău, nu am găsit un răspuns la întrebarea ta.")
            {
                await Clients.All.SendAsync("ReceiveMessage", "ChatBot", response);
            }
            else
            {
                await Clients.All.SendAsync("ReceiveMessage", user, message);
            }
        }
    }
}

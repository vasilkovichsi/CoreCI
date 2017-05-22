using System.Threading.Tasks;

namespace CoreCI.Modules.WebSockets.Interfaces
{
    public interface IWebSocketMessageHandler
    {
        Task SendMessage(string socketId, string message);
    }
}
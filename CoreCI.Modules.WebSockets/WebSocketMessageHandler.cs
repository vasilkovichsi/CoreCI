using System.Net.WebSockets;
using System.Threading.Tasks;
using CoreCI.Common.Communication;
using CoreCI.Common.Serialization.Interfaces;
using CoreCI.Modules.WebSockets.Interfaces;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using WebSocketManager;

namespace CoreCI.Modules.WebSockets
{
    internal class WebSocketMessageHandler : WebSocketHandler, IWebSocketMessageHandler
    {
        private readonly ISerializer _serializer;
        public WebSocketMessageHandler(WebSocketConnectionManager webSocketConnectionManager, ISerializer serializer) : base(webSocketConnectionManager)
        {
            _serializer = serializer;
        }

        public override async Task OnConnected(WebSocket socket)
        {
            await base.OnConnected(socket);

            var socketId = WebSocketConnectionManager.GetId(socket);

            //var message = new BuildItem()
            //{
            //    MessageType = MessageType.Text,
            //    Data = $"{socketId} is now connected"
            //};

            //await SendMessageToAllAsync(message);
        }
        public async Task SendMessage(string socketId, string message)
        {
            await InvokeClientMethodToAllAsync("receiveMessage", socketId, message);
        }

        public override async Task OnDisconnected(WebSocket socket)
        {
            var socketId = WebSocketConnectionManager.GetId(socket);

            await base.OnDisconnected(socket);

            //var message = new Message()
            //{
            //    MessageType = MessageType.Text,
            //    Data = $"{socketId} disconnected"
            //};
            //await SendMessageToAllAsync(message);
        }

        public async Task<bool> SendMessage<TType>(TType obj)
        {
            await SendMessage("", _serializer.Serialize(obj));
            return true;
        }

        public TType RecieveMessage<TType>()
        {
            throw new System.NotImplementedException();
        }

        public void StopMessenger()
        {
            throw new System.NotImplementedException();
        }
    }
}

using SmartUnitApi.WebSocketServices;
using System;
using System.Net.WebSockets;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using SmartUnitApi.Repositories;

namespace SmartUnitApi.WebSocketChannels
{
    public class ChatMessageHandler : WebSocketHandler
    {
        public ChatMessageHandler(WebSocketConnectionManager webSocketConnectionManager, IUserRepository repo) 
            : base(webSocketConnectionManager, repo)
        {
        }
        public override async Task OnConnected(WebSocket socket, int userId)
        {
            await base.OnConnected(socket, userId);

            var socketId = WebSocketConnectionManager.GetId(socket);
            if (WebSocketConnectionManager.FirstTimeConnected(userId))
            {
                await SendMessageToAllAsync($"{socketId} is now connected"); 
            }
        }
        public override async Task OnDisconnected(WebSocket socket, int userid)
        {

            var socketId = WebSocketConnectionManager.GetId(socket);

            await base.OnDisconnected(socket, userid);
            var isUserOnline = WebSocketConnectionManager.IsUserOnline(userid);
            if (!isUserOnline)
            {
                await SendMessageToAllAsync($"{socketId} disconnected");
            }
        }

        public override async Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            var socketId = WebSocketConnectionManager.GetId(socket); 
            var message = $"{socketId} said: {Encoding.UTF8.GetString(buffer, 0, result.Count)}";

            await SendMessageToAllAsync(message);
        }
    }
}

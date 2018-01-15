using SmartUnitApi.Repositories;
using SmartUnitApi.WebSocketServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.WebSockets;

namespace SmartUnitApi.WebSocketChannels
{
    public class AlarmSocketHandler : WebSocketHandler
    {
        public AlarmSocketHandler(WebSocketConnectionManager webSocketConnectionManager, IUserRepository repo) 
            : base(webSocketConnectionManager, repo)
        {
        }

        public override Task ReceiveAsync(WebSocket socket, WebSocketReceiveResult result, byte[] buffer)
        {
            throw new NotImplementedException();
        }
    }
}

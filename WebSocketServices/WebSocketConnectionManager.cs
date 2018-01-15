using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using SmartUnitApi.Models;
using SmartUnitApi.Repositories;
using System.Collections.Generic;
using SmartUnitApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace SmartUnitApi.WebSocketServices
{
    public class WebSocketConnectionManager
    {
        private ConcurrentDictionary<string, WebSocket> sockets = new ConcurrentDictionary<string, WebSocket>();
        private List<SocketUser> users = new List<SocketUser>();
        public IUserRepository UserRepo { get; set; }



        public WebSocket GetSocketById(string id)
        {
            return sockets.FirstOrDefault(p => p.Key == id).Value;
        }

        public ConcurrentDictionary<string, WebSocket> GetAll()
        {
            return sockets;
        }


        public string GetId(WebSocket socket)
        {
            return sockets.FirstOrDefault(p => p.Value == socket).Key;
        }
        private void TryAddUser(int userId, string socketId)
        {
            var user = users.Where(usr => usr.UserId == userId).FirstOrDefault();

            if (user == null)
            {
                var repoUser = UserRepo.Get(userId);
                user = new SocketUser
                {
                    UserId = repoUser.UserId, 
                    SocketIds = new List<string>()
                };
                users.Add(user);
            } 

        }
        private void AddSocketToUser(int userId, string socketId)
        {
            users.Where(usr => usr.UserId == userId).FirstOrDefault().SocketIds.Add(socketId);  
        } 
        public void AddSocket(WebSocket socket, int userId)
        {
            var socketId = CreateConnectionId();
            TryAddUser(userId, socketId);
            AddSocketToUser(userId, socketId);
            sockets.TryAdd(socketId, socket); 
        } 
        public async Task RemoveSocket(string id)
        {
            WebSocket socket;
            sockets.TryRemove(id, out socket);
            RemoveSocketFromUser(id);
            await socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                                    statusDescription: "Closed by the WebSocketManager",
                                    cancellationToken: CancellationToken.None);
        }
        public bool FirstTimeConnected(int userId)
        {
            var user = users.Where(usr => usr.UserId == userId).FirstOrDefault();
            if(user.SocketIds.Count == 1)
            {
                return true;
            }
            return false;
        }
        public bool IsUserOnline(int userId)
        {
            var user = users.Where(usr => usr.UserId == userId).FirstOrDefault();
            if(user == null)
            {
                return false;
            } 
            return true; 
        }
        private void RemoveSocketFromUser(string id)
        {
            var user = users
                .FirstOrDefault(usr => 
                    usr.SocketIds.Any(sid => sid == id));

            users.Where(usr => usr.UserId == user.UserId)
                .FirstOrDefault()
                .SocketIds.Remove(id);

            user.SocketIds.Remove(id);
            if (!user.SocketIds.Any())
            {
                users.Remove(user);
            }
        }

        private string CreateConnectionId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}

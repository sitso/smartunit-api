using System.Collections.Generic;
using System.Net.WebSockets;

namespace SmartUnitApi.Models
{
    public class SocketUser
    {
        public int UserId { get; set; } 
        public string SubjectId { get; set; }
        public List<string> SocketIds { get; set; }
    }
}

using System.Collections.Concurrent;
using ASP_Project.Models;

namespace ASP_Project.Data
{
    public class DataSignalR
    {
        private readonly ConcurrentDictionary<string, int> _connections = new();
        public ConcurrentDictionary<string, int> connections => _connections;
    }
}
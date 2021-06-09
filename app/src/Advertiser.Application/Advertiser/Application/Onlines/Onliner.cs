using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Advertiser.Application.Onlines
{
    public class Onliner : IOnliner
    {
        private readonly ILogger<Onliner> _logger;
        private readonly ConcurrentDictionary<long, string> _users = new();

        public Onliner(ILogger<Onliner> logger)
        {
            _logger = logger;
        }

        public int Count => _users.Count;

        public ConcurrentDictionary<long, string> Users => _users;

        public Task OnConnectedAsync(string connectionId, long userId)
        {
            _logger.LogInformation("{userId} Connected: {connectionId}", connectionId, userId);
            string value = _users.AddOrUpdate(userId, connectionId, (userId, connectionId) =>
            {
                _logger.LogInformation("{userId} Updated: {connectionId}", userId, connectionId);
                return connectionId;
            });
            return Task.CompletedTask;
        }

        public Task OnDisconnectedAsync(string id)
        {
            var pair = _users.Where(predicate => predicate.Value == id).FirstOrDefault();
            if (_users.TryRemove(pair))
            {
                _logger.LogInformation("{0} Disconnected: {1}", pair.Key, pair.Value);
            }
            return Task.CompletedTask;
        }
    }
}

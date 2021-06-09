using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Advertiser.Application.Onlines
{
    public interface IOnliner
    {
        int Count { get; }

        ConcurrentDictionary<long, string> Users { get; }

        Task OnConnectedAsync(string connectionId, long userId);

        Task OnDisconnectedAsync(string connectionId);
    }
}

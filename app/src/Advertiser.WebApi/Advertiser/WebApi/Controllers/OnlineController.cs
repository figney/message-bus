using Advertiser.Application.Messages;
using Advertiser.Application.Onlines;
using Advertiser.WebApi.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace Advertiser.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OnlineController : ControllerBase
    {
        private readonly IOnliner _onliner;
        private readonly IHubContext<MessageHuber, IMessage> _hubContext;

        public OnlineController(IOnliner onliner, IHubContext<MessageHuber, IMessage> hubContext)
        {
            _onliner = onliner;
            _hubContext = hubContext;
        }

        [HttpGet]
        [Route("count")]
        public int GetCount()
        {
            return _onliner.Count;
        }

        [HttpGet]
        [Route("users")]
        public ConcurrentDictionary<long, string> GetOnlineUsersAsync()
        {
            return _onliner.Users;
        }

        [HttpPut]
        [Route("notify")]
        public void NotifyAsync([FromBody] object message)
        {
            _hubContext.Clients.All.NotifyAsync(message.ToString()!);
        }
    }
}

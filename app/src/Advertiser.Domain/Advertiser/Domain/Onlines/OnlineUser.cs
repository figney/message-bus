namespace Advertiser.Domain.Onlines
{
    public class OnlineUser
    {
        public string ConnectionId { get; set; }

        public long UserId { get; set; }

        public OnlineUser(string connectionId)
        {
            ConnectionId = connectionId;
        }
    }
}

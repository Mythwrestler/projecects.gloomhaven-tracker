using System;

namespace GloomhavenTracker.Service.Models.Hub;

public class HubClient
{
    public HubClient(Guid id, string clientId, string groupId, User user, DateTime lastSeen){
        Id = id;
        ClientId = clientId;
        GroupId = groupId;
        User = user;
        this.lastSeen = lastSeen;
    }
    public Guid Id { get; }
    public string ClientId { get; }
    public string GroupId { get; }
    public User User { get; }
    private DateTime lastSeen;
    public DateTime LastSeen => lastSeen;
    public void UpdateLastSeen()
    {
        lastSeen = DateTime.UtcNow;
    }
}
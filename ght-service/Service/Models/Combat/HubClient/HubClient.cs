using System;
using GloomhavenTracker.Service.Models.Combat.Combatant;

namespace GloomhavenTracker.Service.Models.Combat.Hub;

public class HubClient
{
    public HubClient(Guid id, string clientId, string groupId, User user, DateTime lastSeen, Character? character = null){
        Id = id;
        ClientId = clientId;
        GroupId = groupId;
        User = user;
        this.lastSeen = lastSeen;
        Character = character;
    }
    public Guid Id { get; }
    public string ClientId { get; }
    public string GroupId { get; }
    public User User { get; }
    public Character? Character { get; }
    private DateTime lastSeen;
    public DateTime LastSeen => lastSeen;
    public void UpdateLastSeen()
    {
        lastSeen = DateTime.UtcNow;
    }
}
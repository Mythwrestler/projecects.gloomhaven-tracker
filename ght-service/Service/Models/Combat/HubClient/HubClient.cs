using System;
using System.Collections.Generic;
using GloomhavenTracker.Service.Models.Combat.Combatant;

namespace GloomhavenTracker.Service.Models.Combat.Hub;

public class HubClient
{
    public HubClient(Guid id, string clientId, string groupId, User user, DateTime lastSeen, List<Character>? characters, bool? isObserver){
        Id = id;
        ClientId = clientId;
        GroupId = groupId;
        User = user;
        this.lastSeen = lastSeen;
        Characters = characters ?? new List<Character>();
        IsObserver = isObserver ?? false;
    }
    public Guid Id { get; }
    public string ClientId { get; }
    public string GroupId { get; }
    public User User { get; }
    public List<Character> Characters { get; }
    public bool? IsObserver { get; set; }
    private DateTime lastSeen;
    public DateTime LastSeen => lastSeen;
    public void UpdateLastSeen()
    {
        lastSeen = DateTime.UtcNow;
    }
}
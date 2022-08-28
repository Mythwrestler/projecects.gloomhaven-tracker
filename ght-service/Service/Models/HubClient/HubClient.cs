using System;

namespace GloomhavenTracker.Service.Models.Hub;

public class HubClient
{
    public HubClient(Guid id, string connectionId, User user){
        Id = id;
        ClientId = connectionId;
        User = user;
    }
    public Guid Id { get; }
    public string ClientId { get; }
    public User User { get; }
}
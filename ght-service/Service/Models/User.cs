using System;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models;

public class User
{
    public User(Guid userId, string userName, string firstName, string lastName)
    {
        UserId = userId;
        UserName = userName;
        FirstName = firstName;
        LastName = lastName;
    }
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}

[Serializable]
public class UserDTO
{
    [JsonPropertyName("id")]
    public Guid UserId { get; }
    
    [JsonPropertyName("userName")]
    public string UserName { get; } = string.Empty;
    
    [JsonPropertyName("name")]
    public string Name { get; } = string.Empty;
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Campaign;

public class Character
{
    public Guid Id {get;}
    public string Name {get;}
    public Guid CharacterContentId {get;}
    public int Experience {get;}
    public int Gold {get;}
    public List<Guid> Items {get;}
    public List<Guid> AppliedPerks {get;}
    public int PerkPoints {get;}

    public Character(CharacterDO character)
    {
        this.Id = new Guid(character.Id);
        this.Name = character.Name;
        this.CharacterContentId = new Guid(character.CharacterContentId);
        this.Experience = character.Experience;
        this.Gold = character.Gold;
        this.Items = character.Items.Select(itemId => new Guid(itemId)).ToList();
        this.AppliedPerks = character.AppliedPerks.Select(perkId => new Guid(perkId)).ToList();
        this.PerkPoints = character.PerkPoints;
    }

    public CharacterDO ToDO() 
    {
        return new CharacterDO()
        {
            Id = this.Id.ToString(),
            Name = this.Name,
            CharacterContentId = this.CharacterContentId.ToString(),
            Experience = this.Experience,
            Gold = this.Gold,
            Items = this.Items.Select(itemId => itemId.ToString()).ToList(), 
            AppliedPerks = this.Items.Select(perkId => perkId.ToString()).ToList(), 
            PerkPoints = this.PerkPoints
        };
    }
}

[Serializable]
public struct CharacterDO
{
    [JsonPropertyName("id")]
    public string Id {get; set;}

    [JsonPropertyName("name")]
    public string Name {get; set;}

    [JsonPropertyName("characterContentId")]
    public string CharacterContentId {get; set;}

    [JsonPropertyName("experience")]
    public int Experience {get; set;}

    [JsonPropertyName("gold")]
    public int Gold {get; set;}

    [JsonPropertyName("items")]
    public List<string> Items {get; set;}

    [JsonPropertyName("appliedPerks")]
    public List<string> AppliedPerks {get; set;}

    [JsonPropertyName("perkPoints")]
    public int PerkPoints {get; set;}
}
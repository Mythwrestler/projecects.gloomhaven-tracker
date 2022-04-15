using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Campaign;

public class Character
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Models.Content.Character CharacterContent { get; set; } = new Content.Character();
    public int Experience { get; set; }
    public int Gold { get; set; }
    public int PerkPoints {get; set; }
    // public List<string> Items {get;}
    // public List<string> AppliedPerks {get;}

    // public Character(CharacterDO character)
    // {
    //     this.Id = new Guid(character.Id);
    //     this.Name = character.Name;
    //     this.CharacterContentCode = character.CharacterContentCode;
    //     this.Experience = character.Experience;
    //     this.Gold = character.Gold;
    //     this.Items = character.Items;
    //     this.AppliedPerks = character.AppliedPerks;
    //     this.PerkPoints = character.PerkPoints;
    // }
    // public Character(CharacterDTO character)
    // {
    //     this.Id = new Guid(character.Id);
    //     this.Name = character.Name;
    //     this.CharacterContentCode = character.CharacterContentCode;
    //     this.Experience = character.Experience;
    //     this.Gold = character.Gold;
    //     this.Items = character.Items;
    //     this.AppliedPerks = character.AppliedPerks;
    //     this.PerkPoints = character.PerkPoints;
    // }

    public CharacterSummary Summary 
    {
        get {
            return new CharacterSummary(){
                Name = this.Name,
                CharacterContentCode = this.CharacterContent.ContentCode
            };
        }
    }

    // public CharacterDO ToDO() 
    // {
    //     return new CharacterDO()
    //     {
    //         Id = this.Id.ToString(),
    //         Name = this.Name,
    //         CharacterContentCode = this.CharacterContentCode,
    //         Experience = this.Experience,
    //         Gold = this.Gold,
    //         Items = this.Items, 
    //         AppliedPerks = this.Items, 
    //         PerkPoints = this.PerkPoints
    //     };
    // }

    // public CharacterDTO ToDTO() 
    // {
    //     return new CharacterDTO()
    //     {
    //         Id = this.Id.ToString(),
    //         Name = this.Name,
    //         CharacterContentCode = this.CharacterContentCode,
    //         Experience = this.Experience,
    //         Gold = this.Gold,
    //         Items = this.Items, 
    //         AppliedPerks = this.Items, 
    //         PerkPoints = this.PerkPoints
    //     };
    // }
}

// [Serializable]
// public struct CharacterDO
// {
//     [JsonPropertyName("id")]
//     public string Id {get; set;}

//     [JsonPropertyName("name")]
//     public string Name {get; set;}

//     [JsonPropertyName("characterContentCode")]
//     public string CharacterContentCode {get; set;}

//     [JsonPropertyName("experience")]
//     public int Experience {get; set;}

//     [JsonPropertyName("gold")]
//     public int Gold {get; set;}

//     [JsonPropertyName("items")]
//     public List<string> Items {get; set;}

//     [JsonPropertyName("appliedPerks")]
//     public List<string> AppliedPerks {get; set;}

//     [JsonPropertyName("perkPoints")]
//     public int PerkPoints {get; set;}
// }


[Serializable]
public struct CharacterDTO
{
    [JsonPropertyName("id")]
    public string Id {get; set;}

    [JsonPropertyName("name")]
    public string Name {get; set;}

    [JsonPropertyName("characterContentCode")]
    public string CharacterContentCode {get; set;}

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


[Serializable]
public struct CharacterSummary
{
    [JsonPropertyName("name")]
    public string Name {get; set;}

    [JsonPropertyName("characterContentCode")]
    public string CharacterContentCode {get; set;}
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;

namespace GloomhavenTracker.Service.Models.Campaign;

public class Party
{
    public Dictionary<string, Character> Characters {get;}
    public void AddCharacter(CharacterDO newCharacter) 
    {
        if(!Characters.TryAdd(newCharacter.CharacterContentCode, new Character(newCharacter)))
        {
            throw new ArgumentException("Character was not added to party");
        }
    }

    public Party(PartyDO party)
    {
        this.Characters = party.Characters.ToDictionary(character =>character.CharacterContentCode, characterDO => new Character(characterDO));
    }
    public PartyDO ToDO()
    {
        return new PartyDO()
        {
            Characters = this.Characters.Select(kvp => kvp.Value.ToDO()).ToList()
        };
    }
}

[Serializable]
public struct PartyDO
{

    [JsonPropertyName("characters")]
    public List<CharacterDO> Characters {get; set;}
}

using System;
using System.Collections.Generic;
using System.Linq;
namespace GloomhavenTracker.Service.Models.Campaign;

// public class Party
// {
//     public Dictionary<string, Character> Characters {get;}
//     public void AddCharacter(CharacterDO newCharacter) 
//     {
//         if(!Characters.TryAdd(newCharacter.CharacterContentCode, new Character(newCharacter)))
//         {
//             throw new ArgumentException("Character was not added to party");
//         }
//     }

//     public void UpdateCharacter(CharacterDO updateToApply)
//     {
//         Characters.Remove(updateToApply.CharacterContentCode);
        
//         if(!Characters.TryAdd(updateToApply.CharacterContentCode, new Character(updateToApply)))
//         {
//             throw new ArgumentException("Character was not updated in party");
//         }    
//     }

//     public Party(List<CharacterDO> party)
//     {
//         this.Characters = party.ToDictionary(character =>character.CharacterContentCode, characterDO => new Character(characterDO));
//     }
//     public Party(List<CharacterDTO> party)
//     {
//         this.Characters = party.ToDictionary(character => character.CharacterContentCode, characterDTO => new Character(characterDTO));
//     }

//     public List<CharacterDO> ToDO()
//     {
//         return this.Characters.Select(kvp => kvp.Value.ToDO()).ToList();
//     }
//     public List<CharacterDTO> ToDTO()
//     {
//         return this.Characters.Select(kvp => kvp.Value.ToDTO()).ToList();
//     }
// }

using System;
using System.Collections.Generic;
using System.Linq;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Models.Combat;
using GloomhavenTracker.Service.Models.Combat.Combatant;
using GloomhavenTracker.Service.Models.Combat.Hub;
using Microsoft.AspNetCore.Connections.Features;
using Microsoft.AspNetCore.SignalR;

namespace GloomhavenTracker.Service.Services;

public partial interface CombatService : HubClientService {
    public ParticipantsDTO GetCombatParticipants(Guid combatId);
    public List<string> GetGroupIds();
    // public ParticipantsDTO RegisterCharacterSelection(HubClient client, List<string> characterContentCodes);
}

public partial class CombatServiceImplantation : CombatService
{
    public void RegisterClient(HubCallerContext context, string groupId)
    {
        if(!CombatExists(new Guid(groupId)))
            throw new ArgumentException("Combat Not Found");

        if(String.IsNullOrWhiteSpace(context.UserIdentifier))
            throw new ArgumentException("Could not get user from context");

        Guid userId = Guid.Parse(context.UserIdentifier ?? string.Empty);
        User user = userService.GetUserById(userId);

        string clientId = context.ConnectionId;

        var heartbeat = context.Features.Get<IConnectionHeartbeatFeature>();
        if (heartbeat is not null)
        {
            heartbeat.OnHeartbeat((state) =>
            {
                var clientIdForHeartbeat = (state as string);
                if (string.IsNullOrEmpty(clientIdForHeartbeat)) return;
                clientTracker.UpdateLastSeen(clientIdForHeartbeat as string);
            }, clientId);
        }
        
        HubClient client = clientTracker.RegisterClient(groupId, clientId, user);
        combatRepo.AddClient(client);

    }

    public HubClient GetRegisteredClient(HubCallerContext context)
    {
        var client = clientTracker.AllClients.FirstOrDefault(client => client.ClientId == context.ConnectionId);
        if(client is not null)
            return client;
        else
            throw new ArgumentException("Client Not Connected");
    }

    public void SyncClients(int ageOutInSeconds)
    {

        // Update All Tracker Clients In DB.
        combatRepo.UpdateClients(clientTracker.AllClients);

        // Delete Clients In DB Older than n Seconds.
        combatRepo.DeleteOldClients(ageOutInSeconds);

        // Update Tracker W/ Clients From DB.
        List<HubClient> syncedClients = combatRepo.GetClients();
        List<string> syncedIds = syncedClients.Select(client => client.ClientId).ToList();

        List<HubClient> currentClients = clientTracker.AllClients;
        List<string> currentIds = currentClients.Select(client => client.ClientId).ToList();

        // Add Clients To Tracker That Only exist in DB.
        syncedClients.Where(client => !currentIds.Contains(client.ClientId)).ToList().ForEach(client => {
            clientTracker.RegisterClient(client.GroupId, client.ClientId, client.User);
        });

        // Update Clients In Tracker That Appeared In DB
        currentClients.Where(client => syncedIds.Contains(client.ClientId)).ToList().ForEach(client => {
            client = syncedClients.First(sc => sc.ClientId == client.ClientId);
            clientTracker.SyncClient(client);
        });

        // Remove Clients From Tracker That Did not exist in DB
        currentIds.Where(clientId => !syncedIds.Contains(clientId)).ToList().ForEach(clientId => {
            clientTracker.UnregisterClient(clientId);
        });
    }

    public void UnregisterClient(string clientId)
    {
        clientTracker.UnregisterClient(clientId);
        combatRepo.DeleteClient(clientId);
    }

    public List<HubClient> GetRegisteredClientsForGroup(string groupId)
    {
        return clientTracker.GetClientsForGroup(groupId);
    }

    // public List<HubClient> RegisterCharacterSelections(HubClient client, List<string> characterContentCodes)
    // {
    //     Guid combatId = new Guid(client.GroupId);
    //     Combat combat = GetCombat(combatId);

    //     List<string> validCodes = combat.Characters.Select(chr => chr.CampaignCharacter.CharacterContent.ContentCode).ToList();

    //     characterContentCodes.ForEach(chrCode => {
    //         if(!validCodes.Contains(chrCode))
    //             throw new ArgumentException("Character Code is not valid for game.");
    //     });

    //     client.Characters.AddRange(
    //         characterContentCodes.Select(chrCode => {
    //         var combatCharacter = combat.Characters.FirstOrDefault(chr => chr.CampaignCharacter.CharacterContent.ContentCode == chrCode);
    //         if(combatCharacter is null)
    //             throw new ArgumentException("Character Code is not valid for game.");
    //         })
    //     )

    // }


    public ParticipantsDTO GetCombatParticipants(Guid combatId)
    {
        List<ParticipantDTO> participants = new List<ParticipantDTO>();

        List<HubClient> combatHubs = clientTracker.GetClientsForGroup(combatId.ToString());

        participants.AddRange(
            combatHubs.Select(hub =>
            {
                return new ParticipantDTO()
                {
                    Username = hub.User.UserName,
                    IsObserver = hub.IsObserver ?? false,
                    CharacterCodes = hub.Characters.Select(chr => chr.CampaignCharacter.CharacterContent.ContentCode).ToList()
                };
            })
        );

        return new ParticipantsDTO()
        {
            CombatId = combatId,
            Participants = participants,
        };
    }

    public List<string> GetGroupIds()
    {
        return clientTracker.HubGroups;
    }
    
}
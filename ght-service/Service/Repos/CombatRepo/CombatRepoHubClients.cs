
using System;
using System.Collections.Generic;
using System.Linq;
using GloomhavenTracker.Database.Models;
using GloomhavenTracker.Database.Models.Combat;
using GloomhavenTracker.Service.Models.Combat.Hub;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Service.Repos;

public partial interface CombatRepo : HubClientRepo
{
    public List<HubClient> GetClientsForCombat(Guid combatId);
}

public partial class CombatRepoImplementation : CombatRepo
{
    public void AddClient(HubClient client)
    {
        CombatHubClientDAO? clientDAO = GetClientByUserId(client.User.UserId);
        if (clientDAO is not null)
        {
            context.HubCombatClient.Remove(clientDAO);
        }

        clientDAO = mapper.Map<CombatHubClientDAO>(client);
        context.HubCombatClient.Add(clientDAO);

        context.SaveChanges();

    }

    public void UpdateClients(List<HubClient> clients)
    {
        clients.ForEach(client =>
        {
            CombatHubClientDAO clientSaving = mapper.Map<CombatHubClientDAO>(client);
            List<CharacterDAO> charactersFromDB = clientSaving.Characters.Select(savChar =>
            {
                return context.CombatCharacters.First(chr => chr.Id == savChar.CharacterId);
            }).ToList();

            clientSaving.Characters.ToList().ForEach(savChr =>
            {
                savChr.Character = charactersFromDB.First(chr => chr.Id == savChr.CharacterId);
            });


            CombatHubClientDAO? clientUpdating = GetClientByClientId(client.ClientId);
            if (clientUpdating is null)
            {
                clientUpdating = mapper.Map<CombatHubClientDAO>(client);
                context.HubCombatClient.Add(clientUpdating);
            }
            else
            {

                clientUpdating.LastSeen = client.LastSeen;
                clientUpdating.IsObserver = client.IsObserver ?? false;

                var clientCharsToDelete = clientUpdating.Characters.Where(updChar =>
                    !clientSaving.Characters.Select(saveChar => saveChar.CharacterId).Contains(updChar.CharacterId)
                ).ToList();

                if (clientCharsToDelete.Count > 0)
                {
                    clientCharsToDelete.ForEach(delChar =>
                    {
                        clientUpdating.Characters.Remove(delChar);
                        context.CombatCharacterCombatHubClients.Remove(delChar);
                    });
                }

                var existingCharacterIDs = clientUpdating.Characters.Select(chr => chr.CharacterId).ToList();

                var test = context.CombatCharacterCombatHubClients.ToList();
                var test2 = context.CombatCharacters.ToList();

                clientSaving.Characters
                    .Where(savChar => !existingCharacterIDs.Contains(savChar.CharacterId))
                    .ToList().ForEach(savChr =>
                    {
                        savChr.Character = null;
                        savChr.CombatHubClient = null;
                        context.CombatCharacterCombatHubClients.Add(savChr);
                        clientUpdating.Characters.Add(savChr);
                    });

            }
        });

        if (clients.Count > 0)
            context.SaveChanges();
    }

    public void DeleteOldClients(int ageOutInSeconds)
    {
        var clientsToDelete = context.HubCombatClient.Where(client => client.LastSeen < DateTime.UtcNow.AddSeconds(-ageOutInSeconds));
        var clientIds = clientsToDelete.Select(client => client.Id).ToList();
        var combatClientCharactersToDelete = context.CombatCharacterCombatHubClients.Where(client => clientIds.Contains(client.CombatHubClientId));
        context.RemoveRange(combatClientCharactersToDelete);
        context.RemoveRange(clientsToDelete);
        context.SaveChanges();
    }

    public void DeleteClient(string clientId)
    {
        var clientToDelete = context.HubCombatClient.FirstOrDefault(client => client.ClientId == clientId);
        if (clientToDelete is not null)
        {
            context.HubCombatClient.Remove(clientToDelete);
            context.SaveChanges();
        }
    }

    public List<HubClient> GetClients() => mapper.Map<List<HubClient>>(
        context.HubCombatClient
            .Include(client => client.User)
            .Include(client => client.Characters).ThenInclude(chr => chr.Character).ThenInclude(chr => chr.CampaignCharacter).ThenInclude(chr => chr.CharacterContent)
            .ToList()
    );

    private CombatHubClientDAO? GetClientByClientId(string clientId)
    {
        return context.HubCombatClient
        .Include(client => client.Characters)
            .ThenInclude(characterHub => characterHub.Character)
        .Include(client => client.Combat)
        .Include(client => client.User)
        .FirstOrDefault(client => client.ClientId == clientId);
    }

    private CombatHubClientDAO? GetClientByUserId(Guid userId) => context.HubCombatClient.FirstOrDefault(hc => hc.UserId == userId);

    public List<HubClient> GetClientsForCombat(Guid combatId)
    {
        return mapper.Map<List<HubClient>>(
            context.HubCombatClient
            .Where(hub => hub.CombatId == combatId)
            .Include(hub => hub.Characters).ThenInclude(chr => chr.Character)
            .ToList()
        );
    }

}
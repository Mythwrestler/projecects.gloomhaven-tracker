
using System;
using System.Collections.Generic;
using System.Linq;
using GloomhavenTracker.Database.Models;
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
        if(clientDAO is not null)
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
            CombatHubClientDAO? clientDAO = GetClientByClientId(client.ClientId);
            if (clientDAO is not null)
            {
                clientDAO.LastSeen = client.LastSeen;
            }
            else
            {
                context.HubCombatClient.Add(mapper.Map<CombatHubClientDAO>(client));
            }
        });
        context.SaveChanges();
    }

    public void DeleteOldClients(int ageOutInSeconds)
    {
        var clientsToDelete = context.HubCombatClient.Where(client => client.LastSeen < DateTime.UtcNow.AddSeconds(-ageOutInSeconds));
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
            .ToList()
    );

    private CombatHubClientDAO? GetClientByClientId(string clientId) => context.HubCombatClient.FirstOrDefault(client => client.ClientId == clientId);

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

using System;
using System.Collections.Generic;
using System.Linq;
using GloomhavenTracker.Database.Models;
using GloomhavenTracker.Service.Models.Hub;

namespace GloomhavenTracker.Service.Repos;

public partial interface CombatRepo : HubClientRepo {}

public partial class CombatRepoImplementation : CombatRepo
{
    public void UpdateClients(List<HubClient> clients)
    {
        clients.ForEach(client => {
            CombatHubClientDAO? clientDAO = GetClientByClientId(client.ClientId);
            if(clientDAO is not null) 
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

    public void DeleteOldClients()
    {
       var clientsToDelete = context.HubCombatClient.Where(client => client.LastSeen < DateTime.UtcNow.AddSeconds(-30));
       context.RemoveRange(clientsToDelete);
       context.SaveChanges();
    }

    public List<HubClient> GetClients() => mapper.Map<List<HubClient>>(context.HubCombatClient.ToList());

    private CombatHubClientDAO? GetClientByClientId(string clientId) => context.HubCombatClient.FirstOrDefault(client => client.ClientId == clientId);
}
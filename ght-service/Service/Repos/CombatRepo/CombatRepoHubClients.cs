
using System;
using System.Collections.Generic;
using System.Linq;
using GloomhavenTracker.Database.Models;
using GloomhavenTracker.Database.Models.Combat;
using GloomhavenTracker.Service.Models.Combat;
using GloomhavenTracker.Service.Models.Hub;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Service.Repos;

public partial interface CombatRepo
{
    public List<HubClient> RegisterClient (Guid combatId, HubClient hubClient);
    public List<HubClient> RemoveClient (Guid combatId, Guid hubId);
}

public partial class CombatRepoImplementation : CombatRepo
{
    public List<HubClient> RegisterClient (Guid combatId, HubClient hubClient)
    {
        CombatDAO combat = GetCombatDAOById(combatId);

        if(!combat.HubClients.Select(client => client.ClientId).Contains(hubClient.ClientId))
        {
            var newClient = mapper.Map<CombatHubClientDAO>(hubClient);
            newClient.CombatId = combatId;
            context.HubCombatClient.Add(newClient);
            combat.HubClients.Add(newClient);
            context.SaveChanges();
        }

        return mapper.Map<List<HubClient>>(combat.HubClients);
    }
    
    public List<HubClient> RemoveClient (Guid combatId, Guid hubId)
    {
        CombatDAO combat = GetCombatDAOById(combatId);
        var hubClientToRemove = combat.HubClients.FirstOrDefault(client => client.Id == hubId);
        if(hubClientToRemove is not null)
        {
            combat.HubClients.Remove(hubClientToRemove);
            context.HubCombatClient.Remove(hubClientToRemove);
            context.SaveChanges();
        }

        return mapper.Map<List<HubClient>>(combat.HubClients);
    }
}
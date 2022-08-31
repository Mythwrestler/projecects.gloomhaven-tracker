using System;
using System.Collections.Generic;
using System.Linq;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Models.Hub;

namespace GloomhavenTracker.Service.Services;

public partial interface CombatService : HubClientService {}

public partial class CombatServiceImplantation : CombatService
{
    public void SyncClients()
    {
        // Update All Tracker Clients In DB.
        combatRepo.UpdateClients(clientTracker.AllClients);

        // Delete Clients In DB Older than n Seconds.
        combatRepo.DeleteOldClients();

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
}
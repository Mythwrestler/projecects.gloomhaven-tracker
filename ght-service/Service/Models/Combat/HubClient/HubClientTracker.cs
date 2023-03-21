using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace GloomhavenTracker.Service.Models.Combat.Hub;

public class HubClientTracker
{
    private ConcurrentDictionary<string, HubClient> hubClients = new ConcurrentDictionary<string, HubClient>();
    private ConcurrentDictionary<string, List<string>> groupClients = new ConcurrentDictionary<string, List<string>>();
    public List<HubClient> AllClients => hubClients.Select(kvp => kvp.Value).ToList();
    public List<string> HubGroups => groupClients.Select(kvp => kvp.Key).ToList();

    public HubClient RegisterClient(string groupId, string clientId, User user, List<Combatant.Character>? characters = null, Boolean? isObserver = false)
    {
        HubClient client = new HubClient(
            Guid.NewGuid(),
            clientId,
            groupId,
            user,
            DateTime.UtcNow,
            characters,
            isObserver
        );

        RemoveExistingClientForUser(groupId, user.UserId);

        hubClients.AddOrUpdate(
            clientId,
            client,
            (keyValue, OldValue) => client
        );

        groupClients.AddOrUpdate(
            groupId,
            new List<string>() { client.ClientId },
            (keyValue, oldValue) =>
            {
                oldValue.Add(clientId);
                return oldValue;
            }
        );

        return client;
    }

    private void RemoveExistingClientForUser(string groupId, Guid userId)
    {
        string? existingClientId = hubClients
                    .Where(kvp => kvp.Value.User.UserId == userId)
                    .Select(kvp => kvp.Value.ClientId)
                    .FirstOrDefault();

        if (existingClientId is not null)
        {
            HubClient? outHubClient = null;
            List<string>? outGroupClients = null;

            hubClients.TryRemove(existingClientId, out outHubClient);

            groupClients.TryGetValue(groupId, out outGroupClients);
            if (outGroupClients is not null)
            {
                outGroupClients.Remove(existingClientId);
            }
        }
    }


    public void UpdateLastSeen(string clientId)
    {
        HubClient? client;
        hubClients.TryGetValue(clientId, out client);
        if (client is null) return;
        client.UpdateLastSeen();
    }

    public void UnregisterClient(string clientId)
    {
        HubClient? client;
        hubClients.Remove(clientId, out client);
        if (client is null) return;

        List<string>? groupClientList;
        groupClients.TryGetValue(client.GroupId, out groupClientList);
        if (groupClientList is null) return;

        groupClientList.Remove(clientId);

    }

    public List<HubClient> GetClientsForGroup(string groupId)
    {
        List<HubClient>? groupClientHubList = new List<HubClient>();
        List<string>? groupClientIdList;
        if (!groupClients.TryGetValue(groupId, out groupClientIdList))
            return groupClientHubList;

        groupClientIdList.ForEach(clientId =>
        {
            HubClient? client;
            if (hubClients.TryGetValue(clientId, out client))
                groupClientHubList.Add(client);
        });

        return groupClientHubList;
    }

    public void SyncClient(HubClient client)
    {
        hubClients.AddOrUpdate(
            client.ClientId,
            client,
            (keyValue, oldValue) =>
            {
                if (oldValue.GroupId != client.GroupId)
                {
                    List<string>? groupClientIds;
                    groupClients.TryGetValue(oldValue.GroupId, out groupClientIds);
                    if (groupClientIds is not null) groupClientIds.Remove(oldValue.ClientId);
                    groupClients.AddOrUpdate(
                        client.GroupId,
                        new List<string>() { client.ClientId },
                        (keyValue, oldValue) =>
                        {
                            oldValue.Add(client.ClientId);
                            return oldValue;
                        }
                    );
                }
                return client;
            }
        );
    }

}

public class CombatHubClientTracker : HubClientTracker { }
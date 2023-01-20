using System;
using System.Collections.Generic;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Models.Combat.Hub;
using Microsoft.AspNetCore.SignalR;

namespace GloomhavenTracker.Service.Services;

public interface HubClientService
{
    public void RegisterClient(HubCallerContext context, string groupId);
    public void SyncClients(int ageOutInSeconds);
    public void UnregisterClient(string clientId);
    public List<HubClient> GetRegisteredClientsForGroup(string groupId);

}
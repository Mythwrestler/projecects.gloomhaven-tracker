using System;
using System.Collections.Generic;
using GloomhavenTracker.Database;
using GloomhavenTracker.Service.Models.Combat.Hub;

namespace GloomhavenTracker.Service.Repos;

public interface HubClientRepo
{
    public void UpdateClients(List<HubClient> clients);
    public void DeleteOldClients(int ageOutInSeconds);
    public void DeleteClient(string clientId);
    public List<HubClient> GetClients();
}

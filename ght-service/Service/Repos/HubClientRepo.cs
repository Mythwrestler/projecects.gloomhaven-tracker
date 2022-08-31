using System;
using System.Collections.Generic;
using GloomhavenTracker.Database;
using GloomhavenTracker.Service.Models.Hub;

namespace GloomhavenTracker.Service.Repos;

public interface HubClientRepo
{
    public void UpdateClients(List<HubClient> clients);
    public void DeleteOldClients();
    public List<HubClient> GetClients();
}

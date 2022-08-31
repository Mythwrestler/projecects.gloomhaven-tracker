using System.Collections.Generic;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Models.Hub;

public interface HubClientService
{
    public void SyncClients(int ageOutInSeconds);
}
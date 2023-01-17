using System.Collections.Generic;
using GloomhavenTracker.Service.Models;

namespace GloomhavenTracker.Service.Services;

public interface HubClientService
{
    public void SyncClients(int ageOutInSeconds);
    public void UnregisterClient(string clientId);
}
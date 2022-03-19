using System;

namespace GloomhavenTracker.Service.Models;

[Serializable]
public class HubRequestResult
{
    public String? errorMessage { get; set; }
    public Object? data {get; set;}
}   
using System;
using System.Collections.Generic;
using System.Text.Json;
using GloomhavenTracker.Service.Models.Campaign;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace GloomhavenTracker.Service.Repos;

public interface CampaignRepo
{
    public List<CampaignSummary> GetCampaignList();
    public CampaignDO? GetCampaign(Guid campaignId);
    public void NewCampaign(CampaignDO campaign);
    public void UpdateCampaign(CampaignDO campaign);
}

public class CampaignRepoImplementation : CampaignRepo
{
    private readonly string connectionString;
    private readonly ILogger<CampaignRepoImplementation> logger;

    public CampaignRepoImplementation(string connectionString, ILogger<CampaignRepoImplementation> logger)
    {
        this.connectionString = connectionString;
        this.logger = logger;
    }

    public List<CampaignSummary> GetCampaignList()
    {
        using var connection = new NpgsqlConnection(connectionString);
        var sqlString = $"SELECT json_build_object('description', c.campaignjson->'description','id', c.campaignjson->'id', 'game', c.campaignjson->'game') from \"Campaign\" c";
        List<CampaignSummary> campaigns = new List<CampaignSummary>();
        try
        {
            connection.Open();
            using var command = new NpgsqlCommand(sqlString, connection);
            string? jsonString;
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                jsonString = reader[0].ToString();
                if (jsonString != null)
                {
                    var campaign = JsonSerializer.Deserialize<CampaignSummary>(jsonString);
                    campaigns.Add(campaign);
                }
            }

            connection.Close();
        }
        catch (Exception ex)
        {
            connection.Close();
            logger.LogError(new EventId(), ex, "Failed to get campaign");
            throw new Exception("Failed to get campaign");
        }
        finally
        {
            connection.Close();
        }
        return campaigns;
    }

    public CampaignDO? GetCampaign(Guid campaignId)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var sqlString = $"SELECT c.campaignjson from \"Campaign\" c where c.campaignid='{campaignId.ToString()}'";
        try
        {
            connection.Open();
            using var command = new NpgsqlCommand(sqlString, connection);
            CampaignDO? campaign = null;
            string? jsonString;
            NpgsqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                jsonString = reader[0].ToString();
                if (jsonString != null)
                {
                    campaign = JsonSerializer.Deserialize<CampaignDO>(jsonString);
                    if (campaign != null) return campaign;
                }
            }

            connection.Close();
        }
        catch (Exception ex)
        {
            connection.Close();
            logger.LogError(new EventId(), ex, "Failed to get campaign");
            throw new Exception("Failed to get campaign");
        }
        finally
        {
            connection.Close();
        }
        return null;
    }

    public void NewCampaign(CampaignDO campaign)
    {
        using var connection = new NpgsqlConnection(connectionString);

        var campaignJsonString = JsonSerializer.Serialize<CampaignDO>(campaign);
        var sqlString = $"INSERT INTO \"Campaign\" (campaignId, game, description, campaignJson) VALUES('{campaign.Id}','{campaign.Game}','{campaign.Description}','{campaignJsonString}')";

        try
        {
            connection.Open();
            using var command = new NpgsqlCommand(sqlString, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception ex)
        {
            connection.Close();
            logger.LogError(new EventId(), ex, "Failed to save campaign.");
            throw new Exception("Failed to save campaign", ex);
        }
    }

    public void UpdateCampaign(CampaignDO campaign)
    {
        using var connection = new NpgsqlConnection(connectionString);

        var campaignJsonString = JsonSerializer.Serialize<CampaignDO>(campaign);
        var sqlString = $"UPDATE \"Campaign\" SET campaignJson = '{campaignJsonString}' where campaignId = '{campaign.Id}'";

        try
        {
            connection.Open();
            using var command = new NpgsqlCommand(sqlString, connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
        catch (Exception ex)
        {
            connection.Close();
            logger.LogError(new EventId(), ex, "Failed to save campaign.");
            throw new Exception("Failed to save campaign", ex);
        }

    }

}
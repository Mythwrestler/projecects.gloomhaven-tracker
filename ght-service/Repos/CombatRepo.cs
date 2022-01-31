using System;
using System.Collections.Generic;
using System.Text.Json;
using Dapper;
using GloomhavenTracker.Service.Models.Combat;
using Microsoft.Extensions.Logging;
using Npgsql;

namespace GloomhavenTracker.Service.Repos;

public interface CombatRepo
{
    public bool CombatExists(Guid campaignId, string scenarioContentCode);
    public CombatTrackerDO GetCombat(Guid combatId);
    public void NewCombat(CombatTrackerDO combat);
}

public class CombatRepoImplementation : CombatRepo
{
    private readonly string connectionString;
    private readonly ILogger<CombatRepoImplementation> logger;

    public CombatRepoImplementation(string connectionString, ILogger<CombatRepoImplementation> logger)
    {
        this.connectionString = connectionString;
        this.logger = logger;
    }

    public bool CombatExists(Guid campaignId, string scenarioContentCode)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var sqlString = $"SELECT EXISTS (SELECT 1 FROM \"Combat\" c WHERE c.combatjson ->> 'campaign' = '{campaignId.ToString()}' AND c.combatjson ->> 'scenarioContentCode' = '{scenarioContentCode}')";
        try
        {
            connection.Open();
            return connection.QuerySingle<bool>(sqlString);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Failed to verify campaign exists", ex);
        }
        finally
        {
            connection.Close();
        }
    }

   public CombatTrackerDO GetCombat(Guid combatId)
    {
        using var connection = new NpgsqlConnection(connectionString);
        var sqlString = $"SELECT c.combatjson from \"Combat\" c WHERE c.combatId = '{combatId.ToString()}'";
        try
        {
            connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, connection))
            {
                string? jsonString;
                CombatTrackerDO? combat;
                NpgsqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        combat = JsonSerializer.Deserialize<CombatTrackerDO>(jsonString);
                        if(combat != null) return combat;
                    }
                }
            }
            throw new ArgumentException("Could Not Find Combat");
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Could Not Pull Combat", ex);
        }
        finally
        {
            connection.Close();
        }
    }

    public void NewCombat(CombatTrackerDO combat)
    {
        
        using var connection = new NpgsqlConnection(connectionString);

        var combatJsonString = JsonSerializer.Serialize<CombatTrackerDO>(combat);
        var sqlString = $"INSERT INTO \"Combat\" (combatId, game, description, combatJson) VALUES('{combat.Id}','{combat.GameCode}','{combat.Description}','{combatJsonString}')";

        try
        {
            connection.Open();
            using var command = new NpgsqlCommand(sqlString, connection);
            command.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            logger.LogError(new EventId(), ex, "Failed to save new combat.");
            throw new Exception("Failed to save new combat", ex);
        }
        finally
        {
            connection.Close();
        }
    }

} 
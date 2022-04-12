using System;
using System.Collections.Generic;
using System.Text.Json;
using Dapper;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Models.Content;
using Npgsql;

namespace GloomhavenTracker.Service.Repos;

public interface ContentRepo
{
    public List<ContentSummary> GetContentSummary(CONTENT_TYPE contentType, GAME_TYPE? gameCode);
    public List<ScenarioSummary> GetScenarios(GAME_TYPE gameCode);
    public Game GetGameDefaults(GAME_TYPE gameCode);
    public Character GetCharacterDefaults(GAME_TYPE gameCode, string contentCode);
    public Monster GetMonsterDefaults(GAME_TYPE gameCode, string contentCode);
    public List<AttackModifier> GetBaseModifierDeck (GAME_TYPE gameCode);
    public Scenario GetScenarioDefaults(GAME_TYPE gameCode, string contentCode);
    public bool IsValidCode(string kind, string contentCode, string? gameCode);
}

public class ContentRepoImplementation : ContentRepo
{
    private readonly string connectionString;
    
    public ContentRepoImplementation(string connectionString) => this.connectionString = connectionString;

    public List<ContentSummary> GetContentSummary(CONTENT_TYPE contentType, GAME_TYPE? gameCode)
    {   
        using var connection = new NpgsqlConnection(connectionString);

        string contentTypeString = GameUtils.ContentTypeString(contentType);
        string sqlString = $"SELECT json_build_object('name', gc.contentjson->'name','contentCode', gc.contentjson->'contentCode', 'description', gc.description) from \"Game Content\" gc where gc.contentJson->>'kind' = '{contentTypeString}'";
        if(gameCode != null)
        {
            string gameString = GameUtils.GameTypeString(gameCode);
            sqlString += $" and gc.game='{gameString}'";
        }
        
        try
        {
            List<ContentSummary> summaries = new List<ContentSummary>();
            connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, connection))
            {
                string? jsonString;
                NpgsqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        var summary = JsonSerializer.Deserialize<ContentSummary>(jsonString);
                        if(summary != null) summaries.Add(summary);
                    }
                }
            }
            return summaries;
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Could Not Pull Player Defaults", ex);
        }
        finally
        {
            connection.Close();
        }
    }

    public List<ScenarioSummary> GetScenarios(GAME_TYPE gameCode) {
        using var connection = new NpgsqlConnection(connectionString);
        string gameString = GameUtils.GameTypeString(gameCode);
        string sqlString = $"SELECT json_build_object('name', gc.contentjson->'name','contentCode', gc.contentjson->'contentCode', 'description', gc.description, 'scenarioNumber', gc.contentjson -> 'scenarioNumber') from \"Game Content\" gc where gc.contentJson->>'kind' = 'scenario' and gc.game='{gameString}'";
        try
        {
            List<ScenarioSummary> summaries = new List<ScenarioSummary>();
            connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, connection))
            {
                string? jsonString;
                NpgsqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        var summary = JsonSerializer.Deserialize<ScenarioSummary>(jsonString);
                        if(summary != null) summaries.Add(summary);
                    }
                }
            }
            return summaries;
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Could Not Pull Scenario Summaries", ex);
        }
        finally
        {
            connection.Close();
        }

    }

    public Game GetGameDefaults(GAME_TYPE gameCode)
    {
        
        using var connection = new NpgsqlConnection(connectionString);

        string gameString = GameUtils.GameTypeString(gameCode);
        string sqlString = $"SELECT json_build_object('description', gc.description)::jsonb || gc.contentjson from \"Game Content\" gc where gc.contentJson->>'kind' = 'game' and gc.game='{gameString}'";
        try
        {
            connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, connection))
            {
                string? jsonString;
                Game? game;
                NpgsqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        game = JsonSerializer.Deserialize<Game>(jsonString);
                        if(game != null) return game;
                    }
                }
            }
            throw new ArgumentException("Could Not Find Game");
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Could Not Pull Game Defaults", ex);
        }
        finally
        {
            connection.Close();
        }
    }
    
    public Character GetCharacterDefaults(GAME_TYPE gameCode, string contentCode)
    {
        using var connection = new NpgsqlConnection(connectionString);
        string gameString = GameUtils.GameTypeString(gameCode);
        string sqlString = $"SELECT json_build_object('description', gc.description)::jsonb || gc.contentjson from \"Game Content\" gc where gc.contentJson->>'kind' = 'character' and gc.contentJson->>'contentCode'='{contentCode}' and gc.game='{gameString}'";
        try
        {
            connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, connection))
            {
                string? jsonString;
                Character? player;
                NpgsqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        player = JsonSerializer.Deserialize<Character>(jsonString);
                        if(player != null) return player;
                    }
                }
            }
            throw new ArgumentException("Could Not Find Game");
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Could Not Pull Game Defaults", ex);
        }
        finally
        {
            connection.Close();
        }
    }

    public Monster GetMonsterDefaults(GAME_TYPE gameCode, string contentCode)
    {
        using var connection = new NpgsqlConnection(connectionString);
        string gameString = GameUtils.GameTypeString(gameCode);
        string sqlString = $"SELECT json_build_object('description', gc.description)::jsonb || gc.contentjson from \"Game Content\" gc where gc.contentJson->>'kind' = 'monster' and gc.contentJson->>'contentCode'='{contentCode}' and gc.game='{gameString}'";
        try
        {
            connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, connection))
            {
                string? jsonString;
                Monster? monster;
                NpgsqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        monster = JsonSerializer.Deserialize<Monster>(jsonString);
                        if(monster != null) return monster;
                    }
                }
            }
            throw new ArgumentException("Could Not Find Monster");
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Could Not Pull Game Defaults", ex);
        }
        finally
        {
            connection.Close();
        }
    }

    public Scenario GetScenarioDefaults(GAME_TYPE gameCode, string contentCode)
    {
        using var connection = new NpgsqlConnection(connectionString);
        Scenario? scenario = null;
        string gameString = GameUtils.GameTypeString(gameCode);
        string sqlStringScenario = $"SELECT json_build_object('description', gc.description)::jsonb || gc.contentjson - 'monsters' from \"Game Content\" gc where gc.contentJson->>'kind' = 'scenario' and gc.contentJson->>'contentCode'='{contentCode}' and gc.game='{gameString}'";
        string sqlStringMonsters = $"SELECT json_build_object('name', gc.contentjson->'name','contentCode', gc.contentjson->'contentCode', 'description', gc.description) from \"Game Content\" gc where gc.contentjson->>'contentCode'::text = ANY(select jsonb_array_elements_text(gc2.contentjson->'monsters') from \"Game Content\" gc2 where gc2.game='{gameString}' and gc2.contentjson->>'contentCode'='{contentCode}')";
        try
        {
            connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlStringScenario, connection))
            {
                string? jsonString;
                NpgsqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        scenario = JsonSerializer.Deserialize<Scenario>(jsonString);
                    }
                }
            }
            connection.Close();
            
            if(scenario == null) throw new ArgumentException("Could Not Find Scenario");

            connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlStringMonsters, connection))
            {
                string? jsonString;
                NpgsqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    Monster? monster = null;
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        monster = JsonSerializer.Deserialize<Monster>(jsonString);
                        if(monster != null) scenario.Monsters.Add(monster);
                    }
                }
            }

            return scenario;
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Could Not Pull Game Defaults", ex);
        }
        finally
        {
            connection.Close();
        }
    }

    public bool IsValidCode(string kind, string contentCode, string? gameCode)
    {
        using var connection = new NpgsqlConnection(connectionString);
        string sqlString = $"select exists(select 1 from \"Game Content\" gc where gc.contentJson->>'kind' = '{kind}' and gc.contentjson->>'contentCode' = '{contentCode}'";
        if(gameCode != null) sqlString += $" and gc.game='{gameCode}'";
        sqlString += ")";

        try
        {
            connection.Open();
            return connection.QuerySingle<bool>(sqlString);
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Failed to validate code", ex);
        }
        finally
        {
            connection.Close();
        }
    }

    
    public List<AttackModifier> GetBaseModifierDeck (GAME_TYPE gameCode)
    {
        using var connection = new NpgsqlConnection(connectionString);
        string gameString = GameUtils.GameTypeString(gameCode);
        string sqlString = $"select json_build_object('description', gc2.description)::jsonb || gc2.contentjson from (select jsonb_array_elements_text(gc.contentjson->'deck') as cardId from \"Game Content\" gc where gc.game = '{gameString}' and gc.contentjson ->> 'kind' = 'deck' and gc.contentjson->>'contentCode' = 'mod_base_deck') cards join \"Game Content\" gc2 on gc2.contentid::text = cards.cardId";
        try
        {
            List<AttackModifier> modDeck = new List<AttackModifier>();
            connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, connection))
            {
                string? jsonString;
                NpgsqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        var modCard = JsonSerializer.Deserialize<AttackModifier>(jsonString);
                        if(modCard != null) modDeck.Add(modCard);
                    }
                }
            }
            return modDeck;
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Could Not Pull Scenario Summaries", ex);
        }
        finally
        {
            connection.Close();
        }

    }


}
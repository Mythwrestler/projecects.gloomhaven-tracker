using System;
using System.Collections.Generic;
using System.Text.Json;
using GloomhavenTracker.Service.Models;
using GloomhavenTracker.Service.Models.Content;
using Npgsql;

namespace GloomhavenTracker.Service.Repos;

public interface ContentRepo
{
    public List<ContentSummary> GetContentSummary(CONTENT_TYPE kind, GAME_TYPE? gameCode);
    public Game GetGameDefaults(GAME_TYPE gameCode);
    public Character GetPlayerDefaults(GAME_TYPE gameCode, string contentCode);
    public Monster GetMonsterDefaults(GAME_TYPE gameCode, string contentCode);
    public ScenarioContent GetScenarioDefaults(GAME_TYPE gameCode, string contentCode);
}

public class ContentRepoImplementation : ContentRepo
{
    private readonly NpgsqlConnection _connection;
    
    public ContentRepoImplementation(string connectionString) => _connection = new NpgsqlConnection(connectionString);

    public List<ContentSummary> GetContentSummary(CONTENT_TYPE kind, GAME_TYPE? gameCode)
    {
        
        string contentTypeString = GameUtils.contentTypeString(kind);
        string sqlString = $"SELECT json_build_object('name', gc.contentjson->'name','code', gc.contentjson->'code') from \"Game Content\" gc where gc.contentJson->>'kind' = '{contentTypeString}'";
        if(gameCode != null)
        {
            string gameString = GameUtils.gameTypeString(gameCode);
            sqlString += $" and gc.game='{gameString}'";
        }
        
        try
        {
            List<ContentSummary> summaries = new List<ContentSummary>();
            _connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, _connection))
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
            _connection.Close();
        }
    }

    public Game GetGameDefaults(GAME_TYPE gameCode)
    {
        string gameString = GameUtils.gameTypeString(gameCode);
        string sqlString = $"SELECT gc.contentjson from \"Game Content\" gc where gc.contentJson->>'kind' = 'game' and gc.game='{gameString}'";
        try
        {
            _connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, _connection))
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
            _connection.Close();
        }
    }
    
    public Character GetPlayerDefaults(GAME_TYPE gameCode, string contentCode)
    {
        string gameString = GameUtils.gameTypeString(gameCode);
        string sqlString = $"SELECT gc.contentjson from \"Game Content\" gc where gc.contentJson->>'kind' = 'player' and gc.contentJson->>'code'='{contentCode}' and gc.game='{gameString}'";
        try
        {
            _connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, _connection))
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
            _connection.Close();
        }
    }

    public Monster GetMonsterDefaults(GAME_TYPE gameCode, string contentCode)
    {
        string gameString = GameUtils.gameTypeString(gameCode);
        string sqlString = $"SELECT gc.contentjson from \"Game Content\" gc where gc.contentJson->>'kind' = 'monster' and gc.contentJson->>'code'='{contentCode}' and gc.game='{gameString}'";
        try
        {
            _connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, _connection))
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
            _connection.Close();
        }
    }

    public ScenarioContent GetScenarioDefaults(GAME_TYPE gameCode, string contentCode)
    {
        ScenarioContent? scenario = null;
        string gameString = GameUtils.gameTypeString(gameCode);
        string sqlStringScenario = $"SELECT json_build_object('name', gc.contentjson->'name','code', gc.contentjson->'code') from \"Game Content\" gc where gc.contentJson->>'kind' = 'scenario' and gc.contentJson->>'code'='{contentCode}' and gc.game='{gameString}'";
        string sqlStringMonsters = $"SELECT gc.contentJson from \"Game Content\" gc where gc.contentjson->>'code'::text = ANY(select jsonb_array_elements_text(gc2.contentjson->'monsters') from \"Game Content\" gc2 where gc2.game='{gameString}' and gc2.contentjson->>'code'='{contentCode}')";
        try
        {
            _connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlStringScenario, _connection))
            {
                string? jsonString;
                NpgsqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        scenario = JsonSerializer.Deserialize<ScenarioContent>(jsonString);
                    }
                }
            }
            _connection.Close();
            
            if(scenario == null) throw new ArgumentException("Could Not Find Scenario");

            _connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlStringMonsters, _connection))
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
            _connection.Close();
        }
    }
}
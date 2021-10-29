using System;
using System.Collections.Generic;
using System.Text.Json;
using GloomhavenTracker.Service.Models;
using Npgsql;

namespace GloomhavenTracker.Service.Repos;

public interface IContentRepo
{
    public List<ContentItemSummary> GetContentSummary(CONTENT_TYPE kind, GAME_CODES? gameCode);
    public GameContent GetGameDefaults(GAME_CODES gameCode);
    public PlayerContent GetPlayerDefaults(GAME_CODES gameCode, string contentCode);
    public MonsterContent GetMonsterDefaults(GAME_CODES gameCode, string contentCode);
    public ScenarioContent GetScenarioDefaults(GAME_CODES gameCode, string contentCode);
}

public class ContentRepo : IContentRepo
{
    private readonly NpgsqlConnection _connection;
    
    public ContentRepo(string connectionString) => _connection = new NpgsqlConnection(connectionString);

    public List<ContentItemSummary> GetContentSummary(CONTENT_TYPE kind, GAME_CODES? gameCode)
    {
        
        string kindString = GameUtils.kindString(kind);
        string sqlString = $"SELECT json_build_object('name', gc.contentjson->'name','code', gc.contentjson->'code') from \"Game Content\" gc where gc.contentJson->>'kind' = '{kindString}'";
        if(gameCode != null)
        {
            string gameString = GameUtils.codeString(gameCode);
            sqlString += $" and gc.game='{gameString}'";
        }
        
        try
        {
            List<ContentItemSummary> summaries = new List<ContentItemSummary>();
            _connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, _connection))
            {
                string? jsonString;
                NpgsqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        var summary = JsonSerializer.Deserialize<ContentItemSummary>(jsonString);
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

    public GameContent GetGameDefaults(GAME_CODES gameCode)
    {
        string gameString = GameUtils.codeString(gameCode);
        string sqlString = $"SELECT gc.contentjson from \"Game Content\" gc where gc.contentJson->>'kind' = 'game' and gc.game='{gameString}'";
        try
        {
            _connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, _connection))
            {
                string? jsonString;
                GameContent? game;
                NpgsqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        game = JsonSerializer.Deserialize<GameContent>(jsonString);
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
    
    public PlayerContent GetPlayerDefaults(GAME_CODES gameCode, string contentCode)
    {
        string gameString = GameUtils.codeString(gameCode);
        string sqlString = $"SELECT gc.contentjson from \"Game Content\" gc where gc.contentJson->>'kind' = 'player' and gc.contentJson->>'code'='{contentCode}' and gc.game='{gameString}'";
        try
        {
            _connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, _connection))
            {
                string? jsonString;
                PlayerContent? player;
                NpgsqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        player = JsonSerializer.Deserialize<PlayerContent>(jsonString);
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

    public MonsterContent GetMonsterDefaults(GAME_CODES gameCode, string contentCode)
    {
        string gameString = GameUtils.codeString(gameCode);
        string sqlString = $"SELECT gc.contentjson from \"Game Content\" gc where gc.contentJson->>'kind' = 'monster' and gc.contentJson->>'code'='{contentCode}' and gc.game='{gameString}'";
        try
        {
            _connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, _connection))
            {
                string? jsonString;
                MonsterContent? monster;
                NpgsqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        monster = JsonSerializer.Deserialize<MonsterContent>(jsonString);
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

    public ScenarioContent GetScenarioDefaults(GAME_CODES gameCode, string contentCode)
    {
        ScenarioContent? scenario = null;
        string gameString = GameUtils.codeString(gameCode);
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
                    MonsterContent? monster = null;
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        monster = JsonSerializer.Deserialize<MonsterContent>(jsonString);
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
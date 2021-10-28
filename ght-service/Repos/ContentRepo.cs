using System;
using System.Collections.Generic;
using System.Text.Json;
using GloomhavenTracker.Service.Models;
using Npgsql;

namespace GloomhavenTracker.Service.Repos;

public interface IContentRepo
{
    public List<ContentItemSummary> GetContentSummary(CONTENT_TYPE kind, GAME_CODES? gameCode);
    public GameDefaults GetGameDefaults(GAME_CODES gameCode);
    public PlayerDefaults GetPlayerDefaults(GAME_CODES gameCode, string contentCode);
    public MonsterDefaults GetMonsterDefaults(GAME_CODES gameCode, string contentCode);
}

public class ContentRepo : IContentRepo
{
    private readonly NpgsqlConnection _connection;
    
    public ContentRepo(string connectionString) => _connection = new NpgsqlConnection(connectionString);

    public List<ContentItemSummary> GetContentSummary(CONTENT_TYPE kind, GAME_CODES? gameCode)
    {
        
        string kindString = GameUtils.kindString(kind);
        string sqlString = $"SELECT json_build_object('contentId', gc.contentId,'name', gc.contentjson->'name','code', gc.contentjson->'code') from \"Game Content\" gc where gc.contentJson->>'kind' = '{kindString}'";
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

    public GameDefaults GetGameDefaults(GAME_CODES gameCode)
    {
        string gameString = GameUtils.codeString(gameCode);
        string sqlString = $"SELECT gc.contentjson from \"Game Content\" gc where gc.contentJson->>'kind' = 'game' and gc.game='{gameString}'";
        try
        {
            _connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, _connection))
            {
                string? jsonString;
                GameDefaults? game;
                NpgsqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        game = JsonSerializer.Deserialize<GameDefaults>(jsonString);
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
    
    public PlayerDefaults GetPlayerDefaults(GAME_CODES gameCode, string contentCode)
    {
        string gameString = GameUtils.codeString(gameCode);
        string sqlString = $"SELECT gc.contentjson from \"Game Content\" gc where gc.contentJson->>'kind' = 'player' and gc.contentJson->>'code'='{contentCode}' and gc.game='{gameString}'";
        try
        {
            _connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, _connection))
            {
                string? jsonString;
                PlayerDefaults? player;
                NpgsqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        player = JsonSerializer.Deserialize<PlayerDefaults>(jsonString);
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

    public MonsterDefaults GetMonsterDefaults(GAME_CODES gameCode, string contentCode)
    {
        string gameString = GameUtils.codeString(gameCode);
        string sqlString = $"SELECT gc.contentjson from \"Game Content\" gc where gc.contentJson->>'kind' = 'monster' and gc.contentJson->>'code'='{contentCode}' and gc.game='{gameString}'";
        try
        {
            _connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, _connection))
            {
                string? jsonString;
                MonsterDefaults? monster;
                NpgsqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        monster = JsonSerializer.Deserialize<MonsterDefaults>(jsonString);
                        if(monster != null) return monster;
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
}
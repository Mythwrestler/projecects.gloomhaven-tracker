using System;
using System.Collections.Generic;
using System.Text.Json;
using GloomhavenTracker.Service.Models;
using Npgsql;

namespace GloomhavenTracker.Service.Repos;

public interface IContentRepo
{
    public List<ContentItemSummary> GetContentSummary(CONTENT_TYPE kind, GAME_CODES? gameCode);
    public ContentItemSummary GetDefaultsForGame(GAME_CODES gameCode);
    public List<ContentItemSummary> GetPlayerDefaultsForGame(GAME_CODES gameCode);
    public List<ContentItemSummary> GeMonsterDefaultsForGame(GAME_CODES gameCode);
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

    public ContentItemSummary GetDefaultsForGame(GAME_CODES gameCode)
    {
        string gameString = GameUtils.codeString(gameCode);
        string sqlString = $"SELECT json_build_object('contentId', gc.contentId,'name', gc.contentjson->'name','code', gc.contentjson->'code') from \"Game Content\" gc where gc.contentJson->>'kind' = 'game' and gc.contentJson->>'code'='{gameString}'";
        try
        {
            _connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, _connection))
            {
                string? jsonString;
                ContentItemSummary? game;
                NpgsqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        game = JsonSerializer.Deserialize<ContentItemSummary>(jsonString);
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
    public List<ContentItemSummary> GeMonsterDefaultsForGame(GAME_CODES gameCode)
    {
        string gameString = GameUtils.codeString(gameCode);
        string sqlString = $"SELECT json_build_object('contentId', gc.contentId,'name', gc.contentjson->'name','code', gc.contentjson->'code') from \"Game Content\" gc where gc.contentJson->>'kind' = 'monster' and gc.game='{gameString}'";
        try
        {
            List<ContentItemSummary> monsters = new List<ContentItemSummary>();
            _connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, _connection))
            {
                string? jsonString;
                NpgsqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        var monster = JsonSerializer.Deserialize<ContentItemSummary>(jsonString);
                        if(monster != null) monsters.Add(monster);
                    }
                }
            }
            return monsters;
        }
        catch (Exception ex)
        {
            throw new ArgumentException("Could Not Pull Monster Defaults", ex);
        }
        finally
        {
            _connection.Close();
        }
    }

    public List<ContentItemSummary> GetPlayerDefaultsForGame(GAME_CODES gameCode)
    {
        string gameString = GameUtils.codeString(gameCode);
        string sqlString = $"SELECT json_build_object('contentId', gc.contentId,'name', gc.contentjson->'name','code', gc.contentjson->'code') from \"Game Content\" gc where gc.contentJson->>'kind' = 'player' and gc.game='{gameString}'";
        try
        {
            List<ContentItemSummary> players = new List<ContentItemSummary>();
            _connection.Open();
            using(NpgsqlCommand command = new NpgsqlCommand(sqlString, _connection))
            {
                string? jsonString;
                NpgsqlDataReader reader = command.ExecuteReader();
                while(reader.Read())
                {
                    jsonString = reader[0].ToString();
                    if(jsonString != null){
                        var player = JsonSerializer.Deserialize<ContentItemSummary>(jsonString);
                        if(player != null) players.Add(player);
                    }
                }
            }
            return players;
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
}
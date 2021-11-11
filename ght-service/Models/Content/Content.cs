using System;

namespace GloomhavenTracker.Service.Models.Content;

public enum GAME_TYPE {
    original,
    jawsOfTheLion
}

public enum CONTENT_TYPE {
    game,
    scenario,
    monster,
    character,
    objective,
    attackModifier
}

public abstract class ContentItem{
    public Guid Id { get; set; } = new Guid();
    public string ContentCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}

public class ContentSummary {
    public string ContentCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty; 
    public string Description { get; set; } = string.Empty;
}


public static class GameUtils
{
    public static string gameTypeString(GAME_TYPE? game)
    {
        switch (game)
        {
            case GAME_TYPE.jawsOfTheLion:
                return "jawsOfTheLion";
            case GAME_TYPE.original:
                return "original";
            default:
                return "";
        }
    }

    public static string contentTypeString(CONTENT_TYPE? type)
    {
        switch (type)
        {
            case CONTENT_TYPE.game:
                return "game";
            case CONTENT_TYPE.monster:
                return "monster";
            case CONTENT_TYPE.objective:
                return "objective";
            case CONTENT_TYPE.character:
                return "player";
            case CONTENT_TYPE.scenario:
                return "scenario";
            case CONTENT_TYPE.attackModifier:
                return "attackModifier";
            default:
                return "";
        }
    }
}
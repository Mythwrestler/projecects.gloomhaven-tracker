using GloomhavenTracker.Database.Models.Content;

namespace GloomhavenTracker.Database.DataSeed;

public static partial class ContentSeedData
{
    private static void SeedGame(ContentContext context)
    {
        if(!GameExists(context, "original")) context.Game.Add(new Game(){ContentCode="original", Name="Original", Description="Game: Original"});
        if(!GameExists(context, "jawsOfTheLion")) context.Game.Add(new Game(){ContentCode="jawsOfTheLion", Name="Jaws of The Lion", Description="Game: Jaws of The Lion"});
    }

    private static bool GameExists(ContentContext context, string contentCode)
    {
        var check = context.Game.Local.FirstOrDefault(am => am.ContentCode == contentCode);
        if(check is null) check = context.Game.FirstOrDefault(am => am.ContentCode == contentCode);
        if(check is null) return false;
        return true;
    }

    private static Game GetGame(ContentContext context, string contentCode)
    {
        var game = context.Game.Local.FirstOrDefault(g => g.ContentCode == contentCode);
        if(game is null) game = context.Game.FirstOrDefault(g => g.ContentCode == contentCode);
        return game;
    }
}
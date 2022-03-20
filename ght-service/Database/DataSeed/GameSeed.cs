using GloomhavenTracker.Database.Models.Content;

namespace GloomhavenTracker.Database.DataSeed;

public static partial class ContentSeedData
{
    public static void SeedGame(ContentContextImplementation context)
    {
        context.Game.Add(new Game(){ContentCode="original", Name="Original", Description="Game: Original"});
        context.Game.Add(new Game(){ContentCode="jawsOfTheLion", Name="Jaws of The Lion", Description="Game: Jaws of The Lion"});
    }

    public static Game GetGame(ContentContextImplementation context, string contentCode)
    {
        var game = context.Game.Local.FirstOrDefault(g => g.ContentCode == contentCode);
        if(game is null) game = context.Game.FirstOrDefault(g => g.ContentCode == contentCode);
        return game;
    }
}
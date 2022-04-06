using GloomhavenTracker.Database.Models.Content;

namespace GloomhavenTracker.Database.DataSeed;

public static partial class ContentSeedData
{
    private static void SeedGame(ContentContext context)
    {
        if(!GameExists(context, "original")) context.Game.Add(new Game(){ContentCode="original", Name="Original", Description="Game: Original"});
        if(!GameExists(context, "jawsOfTheLion")) context.Game.Add(new Game(){ContentCode="jawsOfTheLion", Name="Jaws of The Lion", Description="Game: Jaws of The Lion"});
    }

    private static void SeedGameBaseDeck(ContentContext context)
    {
        var games = context.Game.Local.ToList();
        games.AddRange(context.Game.ToList());

        games.ForEach(game => {
            game.BaseModifierDeck.Clear();
            BuildGameBaseAttackModifiers(context, game, "mod_*2", 1).ForEach(am => game.BaseModifierDeck.Add(am));
            BuildGameBaseAttackModifiers(context, game, "mod_+2", 2).ForEach(am => game.BaseModifierDeck.Add(am));
            BuildGameBaseAttackModifiers(context, game, "mod_+1", 4).ForEach(am => game.BaseModifierDeck.Add(am));
            BuildGameBaseAttackModifiers(context, game, "mod_+0", 4).ForEach(am => game.BaseModifierDeck.Add(am));
            BuildGameBaseAttackModifiers(context, game, "mod_-1", 4).ForEach(am => game.BaseModifierDeck.Add(am));
            BuildGameBaseAttackModifiers(context, game, "mod_-2", 2).ForEach(am => game.BaseModifierDeck.Add(am));
            BuildGameBaseAttackModifiers(context, game, "mod_Cancel", 1).ForEach(am => game.BaseModifierDeck.Add(am));
        });
    }

    private static List<GameBaseAttackModifier> BuildGameBaseAttackModifiers(ContentContext context, Game game, string contentCode, int count = 1)
    {
        List<GameBaseAttackModifier> modifiers = new List<GameBaseAttackModifier>();
        var modifier = context.AttackModifier.FirstOrDefault(am => am.ContentCode == contentCode && am.GameId == game.Id);
        if(modifier is null) return modifiers;
        for (int i = 0; i < count; i++)
        {
            modifiers.Add(new GameBaseAttackModifier(){GameId=game.Id, Game=game, AttackModifierId=modifier.Id, AttackModifier=modifier});
        }
        return modifiers;
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
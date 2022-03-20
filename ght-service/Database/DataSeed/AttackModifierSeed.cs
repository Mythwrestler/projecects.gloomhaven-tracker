using GloomhavenTracker.Database.Models.Content;

namespace GloomhavenTracker.Database.DataSeed;

public static partial class ContentSeedData
{

    public static void SeedAttackModifiers(ContentContext context)
    {

    }

    private static bool AttackModifierExists(ContentContext context, string contentCode, Game game)
    {
        var check = context.AttackModifier.Local.FirstOrDefault(am => am.ContentCode == contentCode && am.Game.Id == game.Id);
        if(check is null) check = context.AttackModifier.FirstOrDefault(am => am.ContentCode == contentCode && am.Game.Id == am.Id);
        if(check is null) return false;
        return true;
    }

    private static List<AttackModifierEffect> BuildAttackModifierEffects(AttackModifier modifier, List<Effect> effects) => effects.Select(effect => new AttackModifierEffect(){EffectId=effect.Id, Effect=effect, AttackModifierId=modifier.Id, AttackModifier=modifier}).ToList();

    public static void game_attackmodAdd2(ContentContext context)
    {
        var game = GetGame(context, "game_content_code");
        //var modifier = new AttackModifier(){ContentCode="mod_+2", Name="Attack + 2", Description="Attack Modifier: +2", Value="A + 1", IsBlessing=false, IsCurse=false, TriggerShuffle=false, GameId=game.Id, Game=game, Effects = BuildAttackModifierEffects()};
    }

    // {"name":"+2 Attack Modifier","contentCode":"mod_+2","kind":"modCard","modifier":"A + 2","Text":"+2","isCurse":false,"isBlessing":false,"triggerShuffle":false, "effects": []}



}
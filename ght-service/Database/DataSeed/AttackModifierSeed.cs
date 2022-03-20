using GloomhavenTracker.Database.Models.Content;

namespace GloomhavenTracker.Database.DataSeed;

public static partial class ContentSeedData
{

    public static void SeedAttackModifiers(ContentContext context)
    {
        jaws_attackmod_Multiply2(context);
        jaws_attackmod_Add2(context);
        jaws_attackmod_Add1(context);
        jaws_attackmod_Add0(context);
        jaws_attackmod_Minus1(context);
        jaws_attackmod_Minus2(context);
        jaws_attackmod_Cancel(context);

        jaws_attackmod_Blessing(context);
        jaws_attackmod_Curse(context);

        jaws_attackmod_Add1_Muddle(context);
    }

    private static bool AttackModifierExists(ContentContext context, string contentCode, Game game)
    {
        var check = context.AttackModifier.Local.FirstOrDefault(am => am.ContentCode == contentCode && am.GameId == game.Id);
        if(check is null) check = context.AttackModifier.FirstOrDefault(am => am.ContentCode == contentCode && am.GameId == game.Id);
        if(check is null) return false;
        return true;
    }

    private static AttackModifier BuildAttackModifier(string contentCode, string name, string description, string value, bool isBlessing, bool isCurse, bool triggerShuffle, Game game, List<Effect> effects)
    {
        var modifier = BuildBaseAttackModifier(contentCode, name, description, value, isBlessing, isCurse, triggerShuffle, game);
        if(effects.Count() > 0) modifier.Effects = BuildAttackModifierEffects(modifier, effects);
        return modifier;
    }

    private static AttackModifier BuildBaseAttackModifier(string contentCode, string name, string description, string value, bool isBlessing, bool isCurse, bool triggerShuffle, Game game)
    {
        return new AttackModifier(){ContentCode=contentCode, Name=name, Description=description, Value=value, IsBlessing=isBlessing, IsCurse=isCurse, TriggerShuffle=triggerShuffle, GameId=game.Id, Game=game};
    }
    private static List<AttackModifierEffect> BuildAttackModifierEffects(AttackModifier modifier, List<Effect> effects) => effects.Select(effect => new AttackModifierEffect(){EffectId=effect.Id, Effect=effect, AttackModifierId=modifier.Id, AttackModifier=modifier}).ToList();


    #region Base Game Modifiers
    public static void jaws_attackmod_Multiply2(ContentContext context)
    {
        var game = GetGame(context, "jawsOfTheLion");
        if(AttackModifierExists(context, "mod_*2", game)) return;

        context.AttackModifier.Add(
            BuildAttackModifier("mod_*2", "Attack * 2", "Attack Modifier: *2", "A * 2", false, false, true, game, new List<Effect>())
        );
    }

    public static void jaws_attackmod_Add2(ContentContext context)
    {
        var game = GetGame(context, "jawsOfTheLion");
        if(AttackModifierExists(context, "mod_+2", game)) return;

        context.AttackModifier.Add(
            BuildAttackModifier("mod_+2", "Attack + 2", "Attack Modifier: +2", "A + 2", false, false, false, game, new List<Effect>())
        );
    }

    public static void jaws_attackmod_Add1(ContentContext context)
    {
        var game = GetGame(context, "jawsOfTheLion");
        if(AttackModifierExists(context, "mod_+1", game)) return;

        context.AttackModifier.Add(
            BuildAttackModifier("mod_+1", "Attack + 1", "Attack Modifier: +1", "A + 1", false, false, false, game, new List<Effect>())
        );
    }

    public static void jaws_attackmod_Add0(ContentContext context)
    {
        var game = GetGame(context, "jawsOfTheLion");
        if(AttackModifierExists(context, "mod_+0", game)) return;

        context.AttackModifier.Add(
            BuildAttackModifier("mod_+0", "Attack + 0", "Attack Modifier: +0", "A + 0", false, false, false, game, new List<Effect>())
        );
    }

    public static void jaws_attackmod_Minus1(ContentContext context)
    {
        var game = GetGame(context, "jawsOfTheLion");
        if(AttackModifierExists(context, "mod_-1", game)) return;

        context.AttackModifier.Add(
            BuildAttackModifier("mod_-1", "Attack - 1", "Attack Modifier: -1", "A - 1", false, false, false, game, new List<Effect>())
        );
    }

    public static void jaws_attackmod_Minus2(ContentContext context)
    {
        var game = GetGame(context, "jawsOfTheLion");
        if(AttackModifierExists(context, "mod_-2", game)) return;

        context.AttackModifier.Add(
            BuildAttackModifier("mod_-2", "Attack - 2", "Attack Modifier: -2", "A - 2", false, false, false, game, new List<Effect>())
        );
    }

    public static void jaws_attackmod_Cancel(ContentContext context)
    {
        var game = GetGame(context, "jawsOfTheLion");
        if(AttackModifierExists(context, "mod_cancel", game)) return;

        context.AttackModifier.Add(
            BuildAttackModifier("mod_cancel", "Attack Cancel", "Attack Modifier: Cancel", "A * 0", false, false, true, game, new List<Effect>())
        );
    }


    public static void jaws_attackmod_Blessing(ContentContext context)
    {
        var game = GetGame(context, "jawsOfTheLion");
        if(AttackModifierExists(context, "mod_blessing", game)) return;

        context.AttackModifier.Add(
            BuildAttackModifier("mod_blessing", "Attack * 2 (Blessing)", "Attack Modifier: *2 (Blessing)", "A * 2", true, false, true, game, new List<Effect>())
        );
    }

    public static void jaws_attackmod_Curse(ContentContext context)
    {
        var game = GetGame(context, "jawsOfTheLion");
        if(AttackModifierExists(context, "mod_curse", game)) return;

        context.AttackModifier.Add(
            BuildAttackModifier("mod_curse", "Attack Cancel (Curse)", "Attack Modifier: Cancel (Curse)", "A * 0", false, true, true, game, new List<Effect>())
        );
    }

    #endregion

    #region Specialty Modifier Cards

    public static async void jaws_attackmod_Add1_Muddle(ContentContext context)
    {
        var game = GetGame(context, "jawsOfTheLion");
        if(AttackModifierExists(context, "mod_+1_muddle", game)) return;

        context.AttackModifier.Add(
            BuildAttackModifier("mod_+1_muddle", "Attack + 1 | Muddle", "Attack Modifier: + 1 | Muddle", "A + 1", false, false, false, game, new List<Effect>(){CheckAddEffect(context, EFFECT_TYPE.muddle, -1, 1)})
        );
    }

    #endregion

}
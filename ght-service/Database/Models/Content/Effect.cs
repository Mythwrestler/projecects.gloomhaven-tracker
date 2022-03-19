namespace GloomhavenTracker.Database.Models.Content;

public enum EFFECT_TYPE
{
    strength,
    poison,
    wound,
    stun,
    shield,
    disarm,
    muddle,
    immobilize,
    curse,
}

public class Effect
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public EFFECT_TYPE Type { get; set; }
    public int Value { get; set; } = -1;
    public int Duration { get; set; } = -1;
}

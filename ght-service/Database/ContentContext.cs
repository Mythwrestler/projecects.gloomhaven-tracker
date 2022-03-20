using GloomhavenTracker.Database.Models.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GloomhavenTracker.Database;

public interface ContentContext
{
    public DbSet<Game> Game { get; }
    public DbSet<Effect> Effect { get; }
    public DbSet<AttackModifier> AttackModifier { get; }
    public DbSet<AttackModifierEffect> AttackModifierEffect { get; }
    public DbSet<Monster> Monster { get; }
    public DbSet<MonsterStatSet> MonsterStatSet { get; }
    public DbSet<MonsterDefenseEffect> MonsterDefenseEffect { get; }
    public DbSet<MonsterAttackEffect> MonsterAttackEffect { get; }
}

public class ContentContextImplementation : DbContext, ContentContext
{
    public ContentContextImplementation(DbContextOptions options): base(options) {}
    public DbSet<Game> Game => Set<Game>();
    public DbSet<Effect> Effect => Set<Effect>();
    public DbSet<AttackModifier> AttackModifier => Set<AttackModifier>();
    public DbSet<AttackModifierEffect> AttackModifierEffect => Set<AttackModifierEffect>();
    public DbSet<Monster> Monster => Set<Monster>();
    public DbSet<MonsterStatSet> MonsterStatSet => Set<MonsterStatSet>();
    public DbSet<MonsterDefenseEffect> MonsterDefenseEffect => Set<MonsterDefenseEffect>();
    public DbSet<MonsterAttackEffect> MonsterAttackEffect => Set<MonsterAttackEffect>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        var effectType = new EnumToStringConverter<EFFECT_TYPE>();
        var attackModifierType = new EnumToStringConverter<ATTACK_MODIFIER_TYPE>();

        builder.Entity<Effect>(entity => {
            entity.HasIndex(effect => new { effect.Type, effect.Value, effect.Duration }).IsUnique();
            entity.Property(effect => effect.Type).HasConversion(effectType);
        });

        builder.Entity<AttackModifier>(entity => {
            entity.Property(am => am.Type).HasConversion(attackModifierType);
        });

        builder.Entity<MonsterStatSet>(entity => {
            entity.HasMany(ae => ae.AttackEffects).WithOne(me => me.MonsterStatSet);
            entity.HasMany(ae => ae.DefenseEffects).WithOne(me => me.MonsterStatSet);
        });
        
        builder.Entity<AttackModifierEffect>(entity => {
            entity.HasKey(me => new {me.EffectId, me.AttackModifierId});
        });

        builder.Entity<MonsterAttackEffect>(entity => {
            entity.HasKey(me => new {me.EffectId, me.MonsterStatSetId});
        });

        builder.Entity<MonsterDefenseEffect>(entity => {
            entity.HasKey(me => new {me.EffectId, me.MonsterStatSetId});
        });
    }

}
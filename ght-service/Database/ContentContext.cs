using GloomhavenTracker.Database.Models.Content;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database;

public interface ContentContext
{
    public DbSet<Monster> Monster { get; }
    public DbSet<MonsterStatSet> MonsterStatSet { get; }
    public DbSet<MonsterDefenseEffect> MonsterDefenseEffect { get; }
    public DbSet<MonsterAttackEffect> MonsterAttackEffect { get; }
    public DbSet<Effect> Effect { get; }
}

public class ContentContextImplementation : DbContext, ContentContext
{
    public ContentContextImplementation(DbContextOptions options): base(options) {}
    public DbSet<Monster> Monster => Set<Monster>();
    public DbSet<MonsterStatSet> MonsterStatSet => Set<MonsterStatSet>();
    public DbSet<MonsterDefenseEffect> MonsterDefenseEffect => Set<MonsterDefenseEffect>();
    public DbSet<MonsterAttackEffect> MonsterAttackEffect => Set<MonsterAttackEffect>();
    public DbSet<Effect> Effect => Set<Effect>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Effect>()
            .HasIndex(effect => new { effect.Type, effect.Value, effect.Duration })
            .IsUnique();

        builder.Entity<MonsterStatSet>(entity => {
            entity.HasMany(ae => ae.AttackEffects).WithOne(me => me.MonsterStatSet);
            entity.HasMany(ae => ae.DefenseEffects).WithOne(me => me.MonsterStatSet);
        });

        builder.Entity<MonsterAttackEffect>(entity => {
            entity.HasKey(me => new {me.EffectId, me.MonsterStatSetId});
        });

        builder.Entity<MonsterDefenseEffect>(entity => {
            entity.HasKey(me => new {me.EffectId, me.MonsterStatSetId});
        });
    }

}
using GloomhavenTracker.Database.Models.Content;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace GloomhavenTracker.Database;

public class ContentContext : DbContext
{
    public ContentContext(DbContextOptions options): base(options) {}
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

        builder.DefineGameEntities();
        builder.DefineEffectEntities();
        builder.DefineMonsterEntities();
        builder.DefineAttackModifierEntities();
    }

}
using GloomhavenTracker.Database.Models.Content;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database;

public interface ContentContext
{
    public DbSet<Monster> Monster { get; }
    public DbSet<MonsterStatSet> MonsterStatSet { get; }
    public DbSet<Effect> Effect { get; }
}

public class ContentContextImplementation : DbContext, ContentContext
{
    public ContentContextImplementation(DbContextOptions options): base(options) {}
    public DbSet<Monster> Monster => Set<Monster>();
    public DbSet<MonsterStatSet> MonsterStatSet => Set<MonsterStatSet>();
    public DbSet<Effect> Effect => Set<Effect>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Effect>()
            .HasIndex(effect => new { effect.Type, effect.Value, effect.Duration })
            .IsUnique();

        builder.Entity<MonsterStatSet>()
            .HasMany(stat => stat.AttackEffects)
            .WithOne()
            .HasForeignKey(fk => fk.Id);

        builder.Entity<MonsterStatSet>()
            .HasMany(stat => stat.DefenseEffects)
            .WithOne()
            .HasForeignKey(fk => fk.Id);
    }

}
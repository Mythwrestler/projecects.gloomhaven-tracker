using GloomhavenTracker.Database.Models.Content;
using Microsoft.EntityFrameworkCore;

namespace GloomhavenTracker.Database;

public interface ContentContext
{
    public DbSet<Monster> Monster { get; }
    public DbSet<MonsterStatSet> MonsterStatSet { get; }
}

public class ContentContextImplementation : DbContext, ContentContext
{
    public ContentContextImplementation(DbContextOptions options): base(options) {}
    public DbSet<Monster> Monster => Set<Monster>();
    public DbSet<MonsterStatSet> MonsterStatSet => Set<MonsterStatSet>();


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<MonsterStatSet>()
            .HasKey(stat => new {stat.MonsterId, stat.Level, stat.isElite});
    }

}
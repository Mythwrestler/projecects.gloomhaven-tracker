using GloomhavenTracker.Database.Models.Content;

namespace GloomhavenTracker.Database.DataSeed;

public static partial class ContentSeedData
{
    public static void Seed(ContentContext context)
    {
        using (context)
        {
            context.Database.EnsureCreated();
            SeedGame(context);
            SeedMonsters(context);
            SeedAttackModifiers(context);
            context.SaveChanges();
        }
    }
}

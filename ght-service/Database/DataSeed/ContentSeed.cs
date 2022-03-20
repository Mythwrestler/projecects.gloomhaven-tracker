using GloomhavenTracker.Database.Models.Content;

namespace GloomhavenTracker.Database.DataSeed;

public static partial class ContentSeedData
{
    public static void Seed(ContentContextImplementation context)
    {
        using (context)
        {
            context.Database.EnsureCreated();
            SeedGame(context);
            SeedMonsters(context);
            context.SaveChanges();
        }
    }
}

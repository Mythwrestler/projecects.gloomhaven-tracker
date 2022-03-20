namespace GloomhavenTracker.Database.Models.Content;

public class Game
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ContentCode { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<Monster>? Monsters { get; set; }
};
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Text.Json;

namespace GloomhavenTracker.Database.Models;

interface IEntityDate
{
    DateTime? CreatedOnUTC { get; set; }
    DateTime? UpdatedOnUTC { get; set; }
}

public abstract class AuditableEntityBase: IEntityDate
{
    public DateTime? CreatedOnUTC { get; set; }
    public DateTime? UpdatedOnUTC { get; set; }
}

public enum AUDIT_ACTION
{
    None,
    Create,
    Update,
    Delete
}

public class Audit
{
    public Guid Id { get; set; }
    public string? UserId { get; set; } = string.Empty;
    public AUDIT_ACTION Action { get; set; }
    public string TableName { get; set; } = string.Empty;
    public string? PrimaryKey { get; set; }
    public string? AffectedColumns { get; set; }
    public string? OldValues { get; set; }
    public string? NewValues { get; set; }
    public DateTime DateTimeUTC { get; set; }
}

public class AuditEntry
{
    public AuditEntry(EntityEntry entry)
    {
        Entry = entry;
    }
    public EntityEntry Entry { get; }
    public string? UserId { get; set; }
    public string TableName { get; set; }
    public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
    public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
    public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
    public AUDIT_ACTION Action { get; set; }
    public List<string> ChangedColumns { get; } = new List<string>();
    public DateTime DateTimeUTC { get; set; }
    public Audit ToAudit()
    {
        var audit = new Audit();
        audit.UserId = UserId;
        audit.Action = Action;
        audit.TableName = TableName;
        audit.DateTimeUTC = DateTimeUTC;
        audit.PrimaryKey = KeyValues.Count == 0 ? null : JsonSerializer.Serialize(KeyValues);
        audit.OldValues = OldValues.Count == 0 ? null : JsonSerializer.Serialize(OldValues);
        audit.NewValues = NewValues.Count == 0 ? null : JsonSerializer.Serialize(NewValues);
        audit.AffectedColumns = ChangedColumns.Count == 0 ? null : JsonSerializer.Serialize(ChangedColumns);
        return audit;
    }
}
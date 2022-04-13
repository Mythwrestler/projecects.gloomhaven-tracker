namespace GloomhavenTracker.Database.Models;

interface IEntityDate
{
    DateTime? CreatedOn { get; set; }
    DateTime? UpdatedOn { get; set; }
}

public abstract class AuditEntityBase<T>: IEntityDate
{
    public T? Id { get; set; }
    public DateTime? CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
}
namespace SaAn.Domain;

public abstract class AAuditable
{
    public DateOnly CreationDate { get; set; }
    public TimeOnly CreationTime { get; set; }
    public DateOnly? UpdatedDate { get; set; }
    public TimeOnly? UpdatedTime { get; set; }
}
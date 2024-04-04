namespace SaAn.Domain.Entities;

public class Category : AAuditable
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<SparePart> SpareParts { get; set; }

    public Category()
    {
        SpareParts = new HashSet<SparePart>();
    }
}
namespace SaAn.Domain.Entities;

public class Manufacturer : AAuditable
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string ContactInfo { get; set; }
    public virtual ICollection<SparePart> SpareParts { get; set; }

    public Manufacturer()
    {
        SpareParts = new HashSet<SparePart>();
    }
}
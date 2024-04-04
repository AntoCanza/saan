namespace SaAn.Domain.Entities;

public class SparePart : AAuditable
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PartNumber { get; set; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }

    public Guid ManufacturerId { get; set; }
    public virtual Manufacturer Manufacturer { get; set; }
    public virtual ICollection<VehicleSparePart> VehicleSpareParts { get; set; }

    public SparePart()
    {
        VehicleSpareParts = new HashSet<VehicleSparePart>();
    }
}
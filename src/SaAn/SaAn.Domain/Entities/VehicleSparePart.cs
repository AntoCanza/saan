namespace SaAn.Domain.Entities;

public class VehicleSparePart
{
    public Guid VehicleId { get; set; }
    public virtual Vehicle Vehicle { get; set; }
    public Guid SparePartId { get; set; }
    public virtual SparePart SparePart { get; set; }
}
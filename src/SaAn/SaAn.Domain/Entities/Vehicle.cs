using SaAn.Domain.Enums;

namespace SaAn.Domain.Entities;

public class Vehicle : AAuditable
{
    public Guid Id { get; set; }
    public string Model { get; set; }
    public string Brand { get; set; }
    public VehicleType VehicleType { get; set; }
    public virtual ICollection<VehicleSparePart> VehicleSpareParts { get; set; }

    public Vehicle()
    {
        VehicleSpareParts = new HashSet<VehicleSparePart>();
    }
}
using SaAn.Domain.Enums;
using SaAn.Domain.Interfaces;

namespace SaAn.Domain.Entities;

public class Vehicle : AAuditable, ISearchableLowerCase
{
    public Guid Id { get; set; }
    public string Model { get; set; }
    public string Brand { get; set; }
    public string ModelLowerCase { get; private set; }
    public string BrandLowerCase { get; private set; }
    public VehicleType VehicleType { get; set; }
    public virtual ICollection<VehicleSparePart> VehicleSpareParts { get; set; }

    public Vehicle()
    {
        VehicleSpareParts = new HashSet<VehicleSparePart>();
    }

    public void SetLowerCaseField()
    {
        ModelLowerCase = Model.ToLowerInvariant();
        BrandLowerCase = Brand.ToLowerInvariant();
    }
}
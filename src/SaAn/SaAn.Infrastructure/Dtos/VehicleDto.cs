using SaAn.Domain;
using SaAn.Domain.Enums;

namespace SaAn.Infrastructure.Dtos;

public class VehicleDto : AAuditable
{
    public Guid Id { get; set; }
    public string Model { get; set; }
    public string Brand { get; set; }
    public VehicleType VehicleType { get; set; }
}
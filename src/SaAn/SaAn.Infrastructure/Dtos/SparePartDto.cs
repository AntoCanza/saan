using SaAn.Domain;

namespace SaAn.Infrastructure.Dtos;

public class SparePartDto : AAuditable
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string PartNumber { get; set; }
    public string Description { get; set; }
}
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SaAn.Application.Interfaces;
using SaAn.Domain.Entities;
using SaAn.Infrastructure.Dtos;

namespace SaAn.Infrastructure.Services;

public class VehicleService(IDbContext dbContext, IMapper mapper)
{
    public async Task<List<VehicleDto>> GetVehicles(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Vehicle>()
            .OrderBy(e => e.Model)
            .Select(e => new VehicleDto
            {
                Id = e.Id,
                Model = e.Model,
                Brand = e.Brand,
                VehicleType = e.VehicleType
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<VehicleDto> GetVehicleById(Guid id, CancellationToken cancellationToken = default)
    {
        var vehicle = await dbContext.Set<Vehicle>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (vehicle == null)
        {
            return new VehicleDto
            {
                Model = "NOT FOUND"
            };
        }

        return mapper.Map<VehicleDto>(vehicle);
    }

    public async Task<List<VehicleDto>> SearchVehicles(string searchTerm, CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Vehicle>()
            .Where(e => e.ModelLowerCase.StartsWith(searchTerm.ToLowerInvariant()) ||
                        e.BrandLowerCase.StartsWith(searchTerm.ToLowerInvariant()))
            .OrderBy(e => e.Model)
            .Select(e => new VehicleDto
            {
                Id = e.Id,
                Model = e.Model,
                Brand = e.Brand,
                VehicleType = e.VehicleType
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<List<SparePartDto>> SearchSparePartsByVehicle(VehicleDto vehicle,
        CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<SparePart>()
            .Where(e => e.VehicleSpareParts.Any(vsp => vsp.VehicleId == vehicle.Id))
            .OrderBy(e => e.Name)
            .Select(e => new SparePartDto
            {
                Id = e.Id,
                Name = e.Name,
                PartNumber = e.PartNumber
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<SparePartDto> GetSparePartById(Guid id, CancellationToken cancellationToken = default)
    {
        var sparePart = await dbContext.Set<SparePart>().FirstOrDefaultAsync(e => e.Id == id, cancellationToken);

        if (sparePart == null)
        {
            return new SparePartDto
            {
                Name = "NOT FOUND"
            };
        }

        return mapper.Map<SparePartDto>(sparePart);
    }
}
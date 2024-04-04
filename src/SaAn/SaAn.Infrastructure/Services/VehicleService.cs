using Microsoft.EntityFrameworkCore;
using SaAn.Application.Interfaces;
using SaAn.Domain.Entities;

namespace SaAn.Infrastructure.Services;

public class VehicleService(IDbContext dbContext)
{
    public async Task<List<Vehicle>> GetVehicles(CancellationToken cancellationToken = default)
    {
        return await dbContext.Set<Vehicle>()
            .Include(e => e.VehicleSpareParts)
            .ThenInclude(e => e.SparePart)
            .OrderBy(e=>e.Model)
            .ToListAsync(cancellationToken);
    }
}
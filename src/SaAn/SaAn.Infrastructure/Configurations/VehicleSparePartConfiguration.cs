using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaAn.Domain.Entities;

namespace SaAn.Infrastructure.Configurations;

public class VehicleSparePartConfiguration : IEntityTypeConfiguration<VehicleSparePart>
{
    public void Configure(EntityTypeBuilder<VehicleSparePart> builder)
    {
        builder.ToTable("VehicleSpareParts");
        builder.HasKey(vsp => new { vsp.VehicleId, vsp.SparePartId });

        builder.HasOne(vsp => vsp.Vehicle)
            .WithMany(v => v.VehicleSpareParts)
            .HasForeignKey(vsp => vsp.VehicleId);

        builder.HasOne(vsp => vsp.SparePart)
            .WithMany(sp => sp.VehicleSpareParts)
            .HasForeignKey(vsp => vsp.SparePartId);
    }
}
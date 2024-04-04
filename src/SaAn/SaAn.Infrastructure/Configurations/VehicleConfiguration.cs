using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaAn.Domain.Entities;

namespace SaAn.Infrastructure.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");
        builder.HasKey(v => v.Id);
        builder.Property(v => v.Id).ValueGeneratedOnAdd();

        builder.Property(v => v.Model).IsRequired().HasMaxLength(150);
        builder.Property(v => v.Brand).IsRequired().HasMaxLength(150);
        builder.Property(v => v.VehicleType).IsRequired();
    }
}
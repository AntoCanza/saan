using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SaAn.Domain.Entities;

namespace SaAn.Infrastructure.Configurations;

public class SparePartConfiguration : IEntityTypeConfiguration<SparePart>
{
    public void Configure(EntityTypeBuilder<SparePart> builder)
    {
        builder.ToTable("SpareParts");
        builder.HasKey(sp => sp.Id);
        builder.Property(sp => sp.Id).ValueGeneratedOnAdd();
        
        builder.Property(sp => sp.Name).IsRequired().HasMaxLength(150);
        builder.Property(sp => sp.PartNumber).IsRequired().HasMaxLength(100);
        builder.Property(sp => sp.Description).HasMaxLength(1000);
        
        builder.HasOne(sp => sp.Category)
            .WithMany(c => c.SpareParts)
            .HasForeignKey(sp => sp.CategoryId);
        
        builder.HasOne(sp => sp.Manufacturer)
            .WithMany(m => m.SpareParts)
            .HasForeignKey(sp => sp.ManufacturerId);
    }
}
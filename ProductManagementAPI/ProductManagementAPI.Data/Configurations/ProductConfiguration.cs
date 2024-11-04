using ProductManagementAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ProductManagementAPI.Data.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.Name).IsRequired().HasMaxLength(100);
            builder.Property(e => e.Price).HasPrecision(18, 2);
            builder.Property(e => e.Available);
            builder.Property(e => e.Description).HasMaxLength(500);
            builder.Property(e => e.RowVersion).IsRowVersion();
        }
    }
}

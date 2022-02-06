using DeliveryApp.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DeliveryApp.Data.EntityFramework.Mappings
{
    public class ProductMap : IEntityTypeConfiguration<Core.Entities.Concrete.Product>
    {
        public void Configure(EntityTypeBuilder<Core.Entities.Concrete.Product> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Name).HasMaxLength(100);
            builder.Property(p => p.Name).IsRequired();
            builder.Property(p => p.Price).HasColumnType("DECIMAL");
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.Description).HasMaxLength(500);
            builder.Property(p => p.Description).IsRequired();
            builder.Property(p => p.PictureUrl).HasMaxLength(250);
            builder.Property(p => p.PictureUrl).IsRequired();
            builder.HasOne<ProductType>(p => p.ProductType).WithMany(p => p.Products).HasForeignKey(P => P.ProductTypeId);
            builder.HasOne<ProductBrand>(p => p.ProductBrand).WithMany(p => p.Products).HasForeignKey(P => P.ProductBrandId);

        }
    }
}

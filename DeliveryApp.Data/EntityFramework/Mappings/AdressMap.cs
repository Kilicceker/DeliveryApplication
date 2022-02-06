using DeliveryApp.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace DeliveryApp.Data.EntityFramework.Mappings
{
    public class AdressMap : IEntityTypeConfiguration<Adress>
    {
        public void Configure(EntityTypeBuilder<Adress> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Street).HasMaxLength(100);
            builder.Property(p => p.Street).IsRequired();
            builder.Property(p => p.City).HasMaxLength(30);
            builder.Property(p => p.City).IsRequired();
            builder.Property(p => p.Neighbourhood).HasMaxLength(100);
            builder.Property(p => p.Neighbourhood).IsRequired();
            builder.Property(p => p.DoorNumber).HasMaxLength(20);
            builder.Property(p => p.DoorNumber).IsRequired();
            builder.Property(p => p.Defination).HasMaxLength(150);
            builder.Property(p => p.Defination).IsRequired();
            builder.HasOne<User>(p => p.User).WithOne(p => p.Adresses).HasForeignKey<Adress>(p => p.UserId);
        }
    }
}

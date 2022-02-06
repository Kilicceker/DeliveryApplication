using DeliveryApp.Core.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeliveryApp.Data.EntityFramework.Mappings
{
    public class CommentMap : IEntityTypeConfiguration<Comment>
    {

        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).ValueGeneratedOnAdd();
            builder.Property(p => p.Text).HasMaxLength(250);
            builder.Property(p => p.Text).IsRequired();
            builder.Property(p => p.Date).IsRequired();
            builder.HasOne<Product>(p => p.Product).WithMany(p => p.Comments).HasForeignKey(P => P.ProductId);
        }
    }
}

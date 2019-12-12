using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class DetailOrderConfiguration : IEntityTypeConfiguration<DetailOrder>
    {
        public void Configure(EntityTypeBuilder<DetailOrder> builder)
        {
            builder.HasKey(e => new { e.ID, e.ProductID, e.OrderID });

            builder.Property(m => m.ID)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(m => m.OrderID)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(m => m.ProductID)
               .IsRequired()
               .HasColumnType("int");

            builder.Property(m => m.Qty)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(m => m.Price)
                .IsRequired()
                .HasColumnType("decimal(6,2)");

            builder.Property(m => m.Total)
                .IsRequired()
                .HasColumnType("decimal(8,2)");

            builder.Property(m => m.Created_at)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Property(m => m.Updated_at)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnUpdate();

            //set relationship 
            builder.HasOne(c => c.Product).WithMany(p => p.DetailOrder);

            builder.HasOne(c => c.Order).WithMany(p => p.DetailOrder);
        }
    }
}

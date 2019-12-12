using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(e => e.ID);

            builder.Property(m => m.ID)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(m => m.Price)
               .IsRequired()
               .HasColumnType("decimal(6,2)");

            builder.Property(m => m.Name)
                .IsRequired()
                .HasColumnType("nvarchar(45)");

            builder.Property(m => m.Sale)
                .IsRequired();

            builder.Property(m => m.ImagePath)
                .HasColumnType("nvarchar(255)")
                .HasDefaultValue("default.png");

            builder.Property(m => m.Content)
                .HasColumnType("nvarchar(1000)")
                .HasDefaultValue("This Product has no content");

            builder.Property(m => m.View)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(m => m.Pay)
                .IsRequired()
                .HasDefaultValue(0);

            builder.Property(m => m.Hot)
                .IsRequired()
                .HasColumnType("smallint");

            builder.Property(m => m.Amount)
                .IsRequired();

            builder.Property(m => m.Status)
                .IsRequired()
                .HasColumnType("smallint");

            builder.Property(m => m.Created_at)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Property(m => m.Updated_at)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnUpdate();

            builder.Property(m => m.BrandID)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(m => m.CategoryID)
                .IsRequired()
                .HasColumnType("int");

            //set relationship 
            builder.HasOne(c => c.Category).WithMany(p => p.Products);

            builder.HasOne(c => c.Brand).WithMany(p => p.Products);
        }
    }
}

using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(e => e.ID);

            builder.Property(m => m.ID)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(m => m.Amount)
               .IsRequired()
               .HasColumnType("decimal(8,2)");

            builder.Property(m => m.Phone)
                .HasColumnType("nvarchar(45)");
                

            builder.Property(m => m.Address)
                .IsRequired()
                .HasColumnType("nvarchar(255)");

            builder.Property(m => m.MethodPaying)
                .HasColumnType("smallint")
                .HasDefaultValue(0);

            builder.Property(m => m.BankBrand)
                .HasColumnType("nvarchar(45)");

            builder.Property(m => m.CardNumber)
                .HasColumnType("nvarchar(45)");

            builder.Property(m => m.Note)
                .HasColumnType("nvarchar(1000)")
                .HasDefaultValue("This order dont have note");

            builder.Property(m => m.Status)
                .IsRequired()
                .HasColumnType("smallint")
                .HasDefaultValue(0);

            builder.Property(m => m.Created_at)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Property(m => m.Updated_at)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnUpdate();

            builder.Property(m => m.UserID)
                .IsRequired()
                .HasColumnType("int");

            builder.Property(m => m.AdminID)
                .HasColumnType("int");

        }
    }
}

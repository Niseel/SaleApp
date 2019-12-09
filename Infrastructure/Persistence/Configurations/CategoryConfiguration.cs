using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(e => e.ID);

            builder.Property(m => m.ID)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(m => m.Name)
                .IsRequired()
                .HasColumnType("nvarchar(45)");

            builder.Property(m => m.Description)
                .IsRequired()
                .HasColumnType("nvarchar(1000)")
                .HasDefaultValue("This category has no description");

            builder.Property(m => m.Status)
                .IsRequired()
                .HasColumnType("smallint")
                .HasDefaultValue(1);

            builder.Property(m => m.Created_at)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();

            builder.Property(m => m.Updated_at)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnUpdate();

            builder.Property(m => m.PhotoPath)
                .HasColumnType("nvarchar(255)")
                .HasDefaultValue("default.png");
        }
    }
}

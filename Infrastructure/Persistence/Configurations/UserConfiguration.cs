using ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.ID);

            builder.Property(m => m.ID)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder.Property(m => m.FirstName)
                .IsRequired()
                .HasColumnType("nvarchar(45)");

            builder.Property(m => m.LastName)
                .IsRequired()
                .HasColumnType("nvarchar(45)");

            builder.Property(m => m.Mail)
                .IsRequired()
                .HasColumnType("nvarchar(45)");

            builder.Property(m => m.Password)
                .IsRequired()
                .HasColumnType("nvarchar(45)")
                .HasDefaultValue("e10adc3949ba59abbe56e057f20f883e");


            builder.Property(m => m.Birth)
                .HasColumnType("DATE")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(m => m.Phone)
                .HasColumnType("nvarchar(45)");

            builder.Property(m => m.Address)
                .HasColumnType("nvarchar(255)");

            builder.Property(m => m.AvtPath)
                .HasColumnType("nvarchar(255)")
                .HasDefaultValue("default.png");

            builder.Property(m => m.Note)
                .HasColumnType("nvarchar(1000)");

            builder.Property(m => m.Status)
                .IsRequired()
                .HasColumnType("smallint")
                .HasDefaultValue(1);

            builder.Property(m => m.Level)
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
        }
    }
}

﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Infrastructure.Configurations.Write
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(v => v.Id);

            builder.Property(u => u.PasswordHash).IsRequired();

            builder.ComplexProperty(u => u.Email, b =>
            {
                b.Property(a => a.Value).HasColumnName("email")
                    .IsRequired();
            });

            builder.ComplexProperty(u => u.Role, builder =>
            {
                builder.Property(r => r.RoleName).HasColumnName("role");
                builder.Property(r => r.Permissions).HasColumnName("permissions");
            });
        }
    }
}

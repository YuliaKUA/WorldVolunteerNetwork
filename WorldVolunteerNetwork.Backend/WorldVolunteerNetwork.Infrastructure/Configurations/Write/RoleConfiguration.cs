using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Infrastructure.Configurations.Write
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");

            builder.HasKey(r => r.Id);
            builder.HasIndex(r => r.RoleName);

            builder.Property(r => r.Permissions);
            builder.HasData(Role.Admin, Role.Organizer);
        }
    }
}

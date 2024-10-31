using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Infrastructure.Configurations.Write
{
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {
        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.ToTable("admin");

            builder.HasKey(x => x.Id);

            builder.HasOne<User>()
                .WithOne()
                .HasForeignKey<Admin>(x => x.Id);
        }
    }
}

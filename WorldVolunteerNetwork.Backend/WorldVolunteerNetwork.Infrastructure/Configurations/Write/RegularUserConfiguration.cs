using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Infrastructure.Configurations.Write
{
    internal class RegularUserConfiguration : IEntityTypeConfiguration<RegularUser>
    {
        public void Configure(EntityTypeBuilder<RegularUser> builder)
        {
            builder.ToTable("regular_users");

            builder.HasKey(x => x.Id);

            builder.HasOne<User>()
                .WithOne()
                .HasForeignKey<RegularUser>(x => x.Id);
        }
    }
}

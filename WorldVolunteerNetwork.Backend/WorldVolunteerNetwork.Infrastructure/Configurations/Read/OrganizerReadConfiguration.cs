using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldVolunteerNetwork.Infrastructure.ReadModels;

namespace WorldVolunteerNetwork.Infrastructure.Configurations.Read
{
    public class OrganizerReadConfiguration : IEntityTypeConfiguration<OrganizerReadModel>
    {
        public void Configure(EntityTypeBuilder<OrganizerReadModel> builder)
        {
            builder.ToTable("organizers");
            builder.HasKey(v => v.Id);

            builder.OwnsMany(v => v.SocialMedias, navigationBuilder =>
            {
                navigationBuilder.ToJson();

            });

            builder
                .HasMany(o => o.Photos)
                .WithOne()
                .HasForeignKey(ph => ph.OrganizerId)
                .IsRequired();

        }
    }
}

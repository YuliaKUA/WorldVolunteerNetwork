using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Infrastructure.Configurations.Write
{
    public class OrganizerConfiguration : IEntityTypeConfiguration<Organizer>
    {
        public void Configure(EntityTypeBuilder<Organizer> builder)
        {
            builder.ToTable("organizers");

            builder.HasKey(v => v.Id);
            builder.Property(v => v.Name).IsRequired();
            builder.Property(v => v.Description).IsRequired();
            builder.Property(v => v.YearsVolunteeringExperience).IsRequired();

            builder.Property(v => v.ActsBehalfCharitableOrganization).IsRequired();

            builder.HasMany(v => v.Photos).WithOne();
            builder.HasMany(v => v.SocialMedias).WithOne();
            builder.HasMany(v => v.Posts).WithOne();
        }
    }
}
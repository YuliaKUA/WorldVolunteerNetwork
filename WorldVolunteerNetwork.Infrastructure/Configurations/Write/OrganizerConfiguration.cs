using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldVolunteerNetwork.Domain.Entities;
using WorldVolunteerNetwork.Domain.ValueObjects;

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

            builder.OwnsMany(v => v.SocialMedias, navigationBuilder =>
            {
                navigationBuilder.ToJson();
                navigationBuilder.Property(s => s.Social)
                    .HasConversion(
                        s => s.Value, 
                        s => Social.Create(s).Value);
            });

            builder.HasMany(v => v.Photos).WithOne();
            builder.HasMany(v => v.Posts).WithOne();
        }
    }
}
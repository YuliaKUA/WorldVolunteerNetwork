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

            builder.HasOne<User>()
                .WithOne()
                .HasForeignKey<Organizer>(o => o.Id);
            
            builder.ComplexProperty(v => v.FullName, b =>
            {
                b.Property(f => f.FirstName).HasColumnName("first_name");
                b.Property(f => f.LastName).HasColumnName("last_name");
                b.Property(f => f.Patronymic).HasColumnName("patronymic").IsRequired(false);
            });

            builder.Property(v => v.Description).IsRequired();
            builder.Property(v => v.YearsVolunteeringExperience).IsRequired();

            builder.Property(v => v.ActsBehalfCharitableOrganization).IsRequired();

            builder.OwnsMany(v => v.SocialMedias, navigationBuilder =>
            {
                navigationBuilder.ToJson();
                navigationBuilder.Property(s => s.Social).IsRequired(false)
                    .HasConversion(
                        s => s.Value, 
                        s => Social.Create(s).Value);
            });

            builder.HasMany(v => v.Photos).WithOne().IsRequired();
            builder.HasMany(v => v.Posts).WithOne().IsRequired();
        }
    }
}
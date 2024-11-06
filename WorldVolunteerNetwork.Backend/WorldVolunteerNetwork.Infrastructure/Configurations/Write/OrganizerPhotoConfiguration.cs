using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Infrastructure.Configurations.Write
{
    public class OrganizerPhotoConfiguration : IEntityTypeConfiguration<OrganizerPhoto>
    {
        public void Configure(EntityTypeBuilder<OrganizerPhoto> builder)
        {
            builder.ToTable("organizer_photos");

            builder.HasKey(p => p.Id);
        }
    }
}

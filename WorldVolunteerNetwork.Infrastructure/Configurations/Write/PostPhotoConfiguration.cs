using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Infrastructure.Configurations.Write
{
    public class PostPhotoConfiguration : IEntityTypeConfiguration<PostPhoto>
    {
        public void Configure(EntityTypeBuilder<PostPhoto> builder)
        {
            builder.ToTable("post_photos");

            builder.HasKey(p => p.Id);
        }
    }
}

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldVolunteerNetwork.Infrastructure.ReadModels;

namespace WorldVolunteerNetwork.Infrastructure.Configurations.Read
{
    public class PostPhotoReadConfiguration : IEntityTypeConfiguration<PostPhotoReadModel>
    {
        public void Configure(EntityTypeBuilder<PostPhotoReadModel> builder)
        {
            builder.ToTable("post_photos");

            builder.HasKey(p => p.Id);
        }
    }
}

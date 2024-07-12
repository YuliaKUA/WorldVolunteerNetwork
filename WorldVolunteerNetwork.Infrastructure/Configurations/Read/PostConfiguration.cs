using Contracts.Posts.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WorldVolunteerNetwork.Infrastructure.Configurations.Read
{
    public class PostConfiguration : IEntityTypeConfiguration<PostDto>
    {
        public void Configure(EntityTypeBuilder<PostDto> builder)
        {
            builder.ToTable("posts");

            builder.HasKey(p => p.Id);

            builder
                .HasMany(p => p.Photos)
                .WithOne()
                .HasForeignKey(ph => ph.PostId);

        }
    }
}

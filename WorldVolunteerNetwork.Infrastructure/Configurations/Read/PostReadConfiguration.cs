using WorldVolunteerNetwork.Application.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldVolunteerNetwork.Infrastructure.ReadModels;

namespace WorldVolunteerNetwork.Infrastructure.Configurations.Read
{
    public class PostReadConfiguration : IEntityTypeConfiguration<PostReadModel>
    {
        public void Configure(EntityTypeBuilder<PostReadModel> builder)
        {
            builder.ToTable("posts");

            builder
                .HasOne<OrganizerReadModel>()
                .WithMany(o => o.Posts)
                .HasForeignKey(p => p.OrganizerId);

        }
    }
}

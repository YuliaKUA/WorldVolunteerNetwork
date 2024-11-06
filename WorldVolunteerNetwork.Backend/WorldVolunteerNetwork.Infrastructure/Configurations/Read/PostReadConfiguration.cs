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
            //builder.Property(x => x.Id).HasColumnName("id");
            builder.HasKey(p => p.Id);

            builder
                .HasOne<OrganizerReadModel>()
                .WithMany(p => p.Posts)
                .HasForeignKey(p => p.OrganizerId)
                .IsRequired();

        }
    }
}

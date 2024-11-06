using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Infrastructure.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Name).IsRequired();
            builder.ComplexProperty(p => p.Location, b =>
            {
                b.Property(a => a.PostalCode).HasColumnName("postalcode");
                b.Property(a => a.Country).HasColumnName("country");
                b.Property(a => a.City).HasColumnName("city");
                b.Property(a => a.Street).HasColumnName("street");
                b.Property(a => a.Building).HasColumnName("building");
            });
            builder.Property(p => p.Payment).IsRequired();
            builder.Property(p => p.Reward).IsRequired();
            builder.Property(p => p.Duration).IsRequired();
            builder.Property(p => p.Employment).IsRequired();
            builder.Property(p => p.Restriction).IsRequired();
            builder.Property(p => p.SubmissionDeadline).IsRequired();
            builder.Property(p => p.Description).IsRequired();
            builder.ComplexProperty(p => p.ContactNumber, b =>
            {
                b.Property(a => a.Number).HasColumnName("contact_number");
            });
            builder.ComplexProperty(p => p.Status, b =>
            {
                b.Property(a => a.Value).HasColumnName("status");
            });

            builder.Property(p => p.DateCreate).IsRequired();
            builder.ComplexProperty(p => p.Requirement, b =>
            {
                b.Property(a => a.Age).HasColumnName("age");
                b.Property(a => a.Gender).HasColumnName("gender");
            });


<<<<<<<< HEAD:WorldVolunteerNetwork.Backend/WorldVolunteerNetwork.Infrastructure/Configurations/Write/PostConfiguration.cs
            builder.HasMany(p => p.Photos).WithOne().IsRequired();
            builder.HasMany(p => p.Vaccinations).WithOne();
========
            builder.HasMany(p => p.Photos).WithOne();
>>>>>>>> origin/main:WorldVolunteerNetwork.Backend/WorldVolunteerNetwork.Infrastructure/Configurations/PostConfiguration.cs
        }
    }
}
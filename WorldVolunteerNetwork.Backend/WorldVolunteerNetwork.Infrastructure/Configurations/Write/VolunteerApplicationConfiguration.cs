using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NuGet.Protocol;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Infrastructure.Configurations.Write
{
    public class VolunteerApplicationConfiguration : IEntityTypeConfiguration<VolunteerApplication>
    {
        public void Configure(EntityTypeBuilder<VolunteerApplication> builder)
        {
            builder.ToTable("volunteer_application");

            builder.HasKey(v => v.Id);
            
            builder.ComplexProperty(v => v.FullName, b =>
            {
                b.Property(f => f.FirstName).HasColumnName("first_name");
                b.Property(f => f.LastName).HasColumnName("last_name");
                b.Property(f => f.Patronymic).HasColumnName("patronymic").IsRequired(false);
            });
            builder.ComplexProperty(v => v.Email, b =>
            {
                b.Property(f => f.Value).HasColumnName("email").IsRequired();
            });
            builder.Property(v => v.YearsVolunteeringExperience).IsRequired();
            builder.Property(v => v.ExperienceDescription).IsRequired();

            builder.Property(v => v.IsMemberOfOrganization).IsRequired();
            builder.Property(v => v.NameOfOrganization).IsRequired();

            builder.ComplexProperty(v => v.StatusApplication, b =>
            {
                b.Property(f => f.Value).HasColumnName("status");
            });
        }
    }
}

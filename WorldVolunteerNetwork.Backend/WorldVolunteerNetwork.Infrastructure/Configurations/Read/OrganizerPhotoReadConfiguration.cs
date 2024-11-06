using WorldVolunteerNetwork.Application.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldVolunteerNetwork.Domain.Entities;
using WorldVolunteerNetwork.Infrastructure.ReadModels;

namespace WorldVolunteerNetwork.Infrastructure.Configurations.Read
{
    public class OrganizerPhotoReadConfiguration : IEntityTypeConfiguration<PhotoReadModel>
    {
        public void Configure(EntityTypeBuilder<PhotoReadModel> builder)
        {
            builder.ToTable("organizer_photos");

            builder.HasKey(p => p.Id);
        }
    }
}

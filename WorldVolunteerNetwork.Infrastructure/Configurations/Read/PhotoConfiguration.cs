using WorldVolunteerNetwork.Application.Dtos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Infrastructure.Configurations.Read
{
    public class PhotoConfiguration : IEntityTypeConfiguration<PhotoDto>
    {
        public void Configure(EntityTypeBuilder<PhotoDto> builder)
        {
            builder.ToTable("photos");

            builder.HasKey(p => p.Id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldVolunteerNetwork.Infrastructure.ReadModels
{
    public class OrganizerPhotoReadModel
    {
        public Guid Id { get; init; }
        public string Path { get; init; } = string.Empty;
        public bool IsMain { get; init; }

        public Guid OrganizerId { get; init; }
    }
}

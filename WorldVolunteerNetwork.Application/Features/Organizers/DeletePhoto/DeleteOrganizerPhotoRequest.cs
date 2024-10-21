using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldVolunteerNetwork.Application.Features.Organizers.DeletePhoto
{
    public record DeleteOrganizerPhotoRequest(Guid OrganizerId, string Path);
}

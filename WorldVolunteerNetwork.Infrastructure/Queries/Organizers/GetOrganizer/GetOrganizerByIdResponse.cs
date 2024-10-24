using Microsoft.CodeAnalysis.Elfie.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WorldVolunteerNetwork.Application.Dtos;

namespace WorldVolunteerNetwork.Infrastructure.Queries.Organizers.GetOrganizer
{
    public record GetOrganizerByIdResponse(OrganizerDto dto);
}

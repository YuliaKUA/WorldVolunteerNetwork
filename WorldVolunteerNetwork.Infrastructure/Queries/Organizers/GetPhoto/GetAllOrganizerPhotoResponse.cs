using Microsoft.CodeAnalysis.Elfie.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldVolunteerNetwork.Infrastructure.Queries.Organizers.GetPhoto
{
    public record GetAllOrganizerPhotoResponse(IReadOnlyList<string> urls);
}

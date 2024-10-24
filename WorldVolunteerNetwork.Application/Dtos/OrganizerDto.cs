using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldVolunteerNetwork.Application.Dtos
{
    public class OrganizerDto
    {
        public Guid Id { get; init; }
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Patronymic { get; init; } = string.Empty;
        public IReadOnlyList<OrganizerPhotoDto> Photos { get; init; } = [];
    }
}

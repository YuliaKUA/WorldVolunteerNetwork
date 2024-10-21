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
        public List<PhotoDto> Photos { get; init; } = [];
    }
}

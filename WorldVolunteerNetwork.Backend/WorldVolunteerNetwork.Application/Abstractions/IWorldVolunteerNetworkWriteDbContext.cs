using Microsoft.EntityFrameworkCore;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Application.Abstractions
{
    public interface IWorldVolunteerNetworkWriteDbContext
    {
        DbSet<Organizer> Organizers { get; }
        DbSet<Post> Posts { get; }
        DbSet<VolunteerApplication> volunteerApplications { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
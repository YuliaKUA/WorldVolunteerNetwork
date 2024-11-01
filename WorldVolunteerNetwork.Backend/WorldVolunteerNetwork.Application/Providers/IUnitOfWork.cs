namespace WorldVolunteerNetwork.Application.Abstractions
{
    public interface IUnitOfWork // UnitOfWork pattern
    {
        //DbSet<Organizer> Organizers { get; }
        //DbSet<Post> Posts { get; }
        //DbSet<VolunteerApplication> volunteerApplications { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using WorldVolunteerNetwork.Application.Abstractions;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.Entities;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.Application.Features.Organizers.CreateOrganizer
{
    public class CreateOrganizersHandler
    {
        private readonly IOrganizersRepository _organizersRepository;
        private readonly IUnitOfWork _unitOfWorkRepository;
        private readonly ILogger<CreateOrganizersHandler> _logger;

        public CreateOrganizersHandler(
            IOrganizersRepository organizersRepository,
            IUnitOfWork unitOfWorkRepository,
            ILogger<CreateOrganizersHandler> logger)
        {
            _organizersRepository = organizersRepository;
            _unitOfWorkRepository = unitOfWorkRepository;
            _logger = logger;
        }
        public async Task<Result<Guid, Error>> Handle(CreateOrganizerRequest request, CancellationToken ct)
        {
            var socialMedias = request.SocialMedias
                .Select(s =>
                {
                    var social = Social.Create(s.Social).Value;
                    return SocialMedia.Create(s.Link, social).Value;
                });

            var fullName = FullName.Create(request.FirstName, request.LastName, request.Patronymic).Value;

            var organizer = Organizer.Create(
                Guid.NewGuid(),
                fullName,
                request.Description,
                request.VolunteeringExperience,
                request.ActsBehalfCharitableOrganization,
                socialMedias
                ).Value;

            await _organizersRepository.Add(organizer, ct);
            var idResult = await _unitOfWorkRepository.SaveChangesAsync(ct);

            _logger.LogInformation("Organizer with {id} has been created", organizer.Id);
            return organizer.Id;
        }
    }
}

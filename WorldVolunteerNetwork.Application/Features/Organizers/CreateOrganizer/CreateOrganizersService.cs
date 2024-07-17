using CSharpFunctionalExtensions;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.Entities;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.Application.Features.Organizers.CreateOrganizer
{
    public class CreateOrganizersService
    {
        private readonly IOrganizersRepository _organizersRepository;
        public CreateOrganizersService(IOrganizersRepository organizersRepository)
        {
            _organizersRepository = organizersRepository;
        }
        public async Task<Result<Guid, Error>> Handle(CreateOrganizerRequest request, CancellationToken ct)
        {
            var socialMedias = request.SocialMedias
                .Select(s =>
                {
                    var social = Social.Create(s.Social).Value;
                    return SocialMedia.Create(s.Link, social).Value;
                });

            var organizer = Organizer.Create(
                request.Name,
                request.Description,
                request.VolunteeringExperience,
                request.ActsBehalfCharitableOrganization,
                socialMedias
                );

            await _organizersRepository.Add(organizer.Value, ct);

            var idResult = await _organizersRepository.Save(organizer.Value, ct);
            if (idResult.IsFailure)
                return idResult.Error;

            return idResult;
        }
    }
}

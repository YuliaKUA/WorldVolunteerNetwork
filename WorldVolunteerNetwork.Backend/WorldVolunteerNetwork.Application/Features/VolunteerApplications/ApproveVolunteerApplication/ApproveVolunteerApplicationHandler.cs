using CSharpFunctionalExtensions;
using Microsoft.Extensions.Logging;
using WorldVolunteerNetwork.Application.Abstractions;
using WorldVolunteerNetwork.Application.Features.Accounts;
using WorldVolunteerNetwork.Application.Features.Organizers;
using WorldVolunteerNetwork.Application.Features.VolunteerApplication;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.Entities;

namespace WorldVolunteerNetwork.Application.Features.VolunteerApplications.ApproveOrganizerApplication
{
    public class ApproveVolunteerApplicationHandler
    {
        private readonly IVolunteerApplicationRepository _applicationRrepository;
        private readonly IUserRepository _userRepository;
        private readonly IOrganizersRepository _organizersRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<ApproveVolunteerApplicationHandler> _logger;

        public ApproveVolunteerApplicationHandler(
            IVolunteerApplicationRepository repository,
            IUserRepository userRepository,
            IOrganizersRepository organizersRepository,
            IUnitOfWork unitOfWork,
            ILogger<ApproveVolunteerApplicationHandler> logger)
        {
            _applicationRrepository = repository;
            _userRepository = userRepository;
            _organizersRepository = organizersRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<Result<bool,Error>> Handle(ApproveVolunteerApplicationRequest request, CancellationToken ct)
        {
            // create volunteer from application
            // return application from DB
            var organizerApplicationResult = await _applicationRrepository.GetById(request.Id, ct);
            if (organizerApplicationResult.IsFailure)
                return organizerApplicationResult.Error;
            var organizerApplication = organizerApplicationResult.Value;

            var approvedResult = organizerApplication.Approve();
            if(approvedResult.IsFailure)
                return approvedResult.Error;

            // create user entity
            string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword("randomPassword");
            var user = User.CreateOrganizer(organizerApplication.Email, passwordHash);

            if(user.IsFailure)
                return user.Error;

            // save user to DB
            await _userRepository.Add(user.Value, ct);

            // create organizer entity
            var organizer = Organizer.Create
                (
                    user.Value.Id,
                    organizerApplication.FullName,
                    organizerApplication.ExperienceDescription,
                    organizerApplication.YearsVolunteeringExperience,
                    organizerApplication.IsMemberOfOrganization,
                    []
                );

            if (organizer.IsFailure)
                return organizer.Error;

            // save organizer to DB
            await _organizersRepository.Add(organizer.Value, ct);
            

            await _unitOfWork.SaveChangesAsync(ct);

            _logger.LogInformation("volunteer application has been successfully approved and organizer has been created with id: {}", organizer.Value.Id);
            
            //TODO: send message on email

            return true;
        }
    }
}

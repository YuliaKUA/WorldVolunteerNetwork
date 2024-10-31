using CSharpFunctionalExtensions;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Moq;
using System.Diagnostics.SymbolStore;
using WorldVolunteerNetwork.Application.Abstractions;
using WorldVolunteerNetwork.Application.Features.Organizers;
using WorldVolunteerNetwork.Application.Features.Organizers.UploadPhoto;
using WorldVolunteerNetwork.Domain.Common;
using WorldVolunteerNetwork.Domain.Entities;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.UnitTests.Features
{
    public class UploadOrganizerPhotoTests
    {
        private readonly Mock<IMinioProvider> _minioProviderMock = new Mock<IMinioProvider>();
        private readonly Mock<IOrganizersRepository> _organizersRepositoryMock = new Mock<IOrganizersRepository>();
        private readonly Mock<IFormFile> _formFileMock = new();
        private readonly Mock<IUnitOfWork> _unitOfWorkMock = new Mock<IUnitOfWork>();

        public UploadOrganizerPhotoTests()
        {
            var fileName = "file.png";

            _formFileMock.Setup(x => x.FileName).Returns(fileName);
        }

        [Fact]
        public async Task UploadOrganizerPhoto_with_valid_photo()
        {
            //Arrange
            var organizerId = Guid.NewGuid();
            var isMain = true;

            var request = new UploadOrganizerPhotoRequest(
                organizerId,
                _formFileMock.Object,
                isMain);

            var ct = new CancellationToken();

            var organizer = Organizer.Create(
                FullName.Create("Yul", "Kuz", "Alex").Value,
                "Ghjkls brkwld,o op49gj4mc  ooemvm",
                0,
                false,
                new List<SocialMedia>()
                {
                    SocialMedia.Create("http://t.me", Social.Telegram).Value
                });

            _organizersRepositoryMock.Setup(x => x.GetById(organizerId, ct))
                .ReturnsAsync(organizer);
            _unitOfWorkMock.Setup(x => x.SaveChangesAsync(ct)).ReturnsAsync(1);

            var path = "";
            _minioProviderMock.Setup(x => x.UploadPhoto(_formFileMock.Object, It.IsAny<string>()))
               .ReturnsAsync(Result.Success<string, Error>("path"));

            var sut = new UploadOrganizerPhotoHandler(
                _minioProviderMock.Object,
                _organizersRepositoryMock.Object,
                _unitOfWorkMock.Object);


            //Act
            var result = await sut.Handle(request, ct);

            //Asssert
            _organizersRepositoryMock.Verify(x => x.GetById(organizerId, ct), Times.Once);
            _unitOfWorkMock.Verify(x => x.SaveChangesAsync(ct), Times.Once);

            _minioProviderMock.Verify(x => x.UploadPhoto(_formFileMock.Object, It.IsAny<string>()), Times.Once);

            result.IsSuccess.Should().Be(true);
            result.Value.Should().BeOfType<string>();
            result.Value.Should().NotBeEmpty();
        }
    }
}

using FluentAssertions;
using WorldVolunteerNetwork.Domain.Entities;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.UnitTests.Entities
{
    public class OrganizersTests()
    {
        [Theory]
        [MemberData(nameof(CreateValidOrganizerData))]
        public void Create_with_valid_parameters_return_organizer(
            Guid id,
            FullName fullName,
            string description,
            int volunteeringExperience,
            bool actsBehalfCharitableOrganization,
            List<SocialMedia> socialMedias
            )
        {
            //Arrange -> memberdata

            //Act
            var result = Organizer.Create(
                id,
                fullName,
                description,
                volunteeringExperience,
                actsBehalfCharitableOrganization,
                socialMedias);

            //Assert
            result.IsSuccess.Should().Be(true);
            result.IsFailure.Should().Be(false);
            result.Value.Should().NotBeNull();
            result.Invoking(x => x.Error).Should()
                .Throw<CSharpFunctionalExtensions.ResultSuccessException>();
        }

        public static IEnumerable<object[]> CreateValidOrganizerData =>
            new List<object[]>
            {
                new object[]
                {
                    Guid.NewGuid(),
                    FullName.Create("Yul", "Kuz", "Alex").Value,
                    "Ghjkls brkwld,o op49gj4mc  ooemvm",
                    0,
                    false,
                    new List<SocialMedia>()
                    {
                        SocialMedia.Create("http://t.me", Social.Telegram).Value
                    }
                },
                new object[]
                {
                    Guid.NewGuid(),
                    FullName.Create("Yul", "Kuz", null).Value,
                    "G",
                    0,
                    false,
                    new List<SocialMedia>()
                    {
                        SocialMedia.Create("http://t.me", Social.Telegram).Value
                    }
                },
                new object[]
                {
                    Guid.NewGuid(),
                    FullName.Create("Yul", "Kuz", "Alex").Value,
                    "Ghjkls brkwld,o op49gj4mc  ooemvm",
                    10000,
                    false,
                    new List<SocialMedia>()
                    {
                        SocialMedia.Create("http://t.me", Social.Telegram).Value
                    }
                },
                new object[]
                {
                    Guid.NewGuid(),
                    FullName.Create("Yul", "Kuz", "Alex").Value,
                    "Ghjkls brkwld,o op49gj4mc  ooemvm",
                    0,
                    false,
                    new List<SocialMedia>(),
                }
            };

        [Theory]
        [MemberData(nameof(CreateInvalidOrganizerData))]
        public void Create_with_invalid_parameters_return_error(
            Guid id,
            FullName fullName,
            string description,
            int volunteeringExperience,
            bool actsBehalfCharitableOrganization,
            List<SocialMedia> socialMedias
            )
        {
            //Arrange -> memberdata

            //Act
            var result = Organizer.Create(
                id,
                fullName,
                description,
                volunteeringExperience,
                actsBehalfCharitableOrganization,
                socialMedias);

            //Assert
            result.IsSuccess.Should().Be(false);
            result.IsFailure.Should().Be(true);
            result.Error.Should().NotBeNull();
            result.Invoking(x => x.Value).Should()
                .Throw<CSharpFunctionalExtensions.ResultFailureException>()
                .WithMessage("You attempted to access the Value property for a failed result. A failed result has no Value. The error was: You attempted to access the Value property for a failed result. A failed result has no Value. The error was: WorldVolunteerNetwork.Domain.Common.Error");
        }

        public static IEnumerable<object[]> CreateInvalidOrganizerData =>
           new List<object[]>
           {
                new object[]
                {
                    Guid.NewGuid(),
                    FullName.Create("Yul", "Kuz", "Alex").Value,
                    "",
                    -1,
                    false,
                    new List<SocialMedia>()
                    {
                        SocialMedia.Create("http://t.me", Social.Telegram).Value
                    }
                },
                new object[]
                {
                    Guid.NewGuid(),
                    FullName.Create("Yul", "Kuz", "Alex").Value,
                    "",
                    10000,
                    false,
                    new List<SocialMedia>(),
                }
           };

        [Fact]
        public void AddPhoto_when_organizer_has_zero_photos()
        {
            //Arrange
            var organizer = Organizer.Create(
                Guid.NewGuid(),
                FullName.Create("Yul", "Kuz", "Alex").Value,
                "Ghjkls brkwld,o op49gj4mc  ooemvm",
                0,
                false,
                new List<SocialMedia>()
                {
                    SocialMedia.Create("http://t.me", Social.Telegram).Value
                }).Value;

            var organizerPhoto = OrganizerPhoto.Create("file.png", false).Value;

            //Act
            var result = organizer.AddPhoto(organizerPhoto);

            //Assert
            result.IsSuccess.Should().Be(true);
            organizer.Photos.Should().Contain(organizerPhoto);
            organizer.Photos.Should().HaveCount(1);
        }

        [Fact]
        public void AddPhoto_when_organizer_has_five_photos()
        {
            //Arrange
            var organizer = Organizer.Create(
                Guid.NewGuid(),
                FullName.Create("Yul", "Kuz", "Alex").Value,
                "Ghjkls brkwld,o op49gj4mc  ooemvm",
                0,
                false,
                new List<SocialMedia>()
                {
                    SocialMedia.Create("http://t.me", Social.Telegram).Value
                }).Value;

            var organizerPhoto = OrganizerPhoto.Create("file.png", false).Value;
            var organizerPhoto2 = OrganizerPhoto.Create("file2.png", true).Value;

            //Act
            for (int i = 0; i < 5; i++)
            {
                organizer.AddPhoto(organizerPhoto);
            }
            var result = organizer.AddPhoto(organizerPhoto2);

            //Assert
            result.IsSuccess.Should().Be(false);
            result.IsFailure.Should().Be(true);
            organizer.Photos.Should().NotContain(organizerPhoto2);
            organizer.Photos.Should().HaveCount(5);
        }
    }
}
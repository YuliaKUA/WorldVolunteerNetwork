using FluentAssertions;
using WorldVolunteerNetwork.Domain.ValueObjects;

namespace WorldVolunteerNetwork.UnitTests.ValueObjects
{
    public class EmailTests
    {
        [Theory]
        [InlineData("yul@gmail.com")]
        [InlineData("    yul@gmail.com ")]
        [InlineData("r_8@mail.ru")]
        [InlineData("1234567890-yul@yandex.ru")]
        public void Create_with_valid_parameters_return_email(string email)
        {
            //Act
            var result = Email.Create(email);

            //Assert
            result.IsSuccess.Should().BeTrue();
            result.IsFailure.Should().BeFalse();
            result.Value.Value.Should().Be(email.Trim());
            result.Invoking(x => x.Error).Should()
               .Throw<CSharpFunctionalExtensions.ResultSuccessException>();
        }

        [Theory]
        [InlineData("yulgmail.com")]
        [InlineData("    yulgmail.com ")]
        [InlineData("r_8mail.ru")]
        [InlineData("1234567890-yulyandex.ru")]
        public void Create_without_valid_parameters_return_error(string email)
        {
            //Act
            var result = Email.Create(email);

            //Assert
            result.IsSuccess.Should().BeFalse();
            result.IsFailure.Should().BeTrue();
            result.Error.Should().NotBeNull();
            result.Error.Message.Should().Be(Errors.General.ValueIsInvalid("email").Message);
            result.Error.Code.Should().Be(Errors.General.ValueIsInvalid("email").Code);
            result.Invoking(x => x.Value).Should()
                .Throw<CSharpFunctionalExtensions.ResultFailureException>()
                .WithMessage("You attempted to access the Value property for a failed result. A failed result has no Value. The error was: You attempted to access the Value property for a failed result. A failed result has no Value. The error was: WorldVolunteerNetwork.Domain.Common.Error");
        }
    }
}

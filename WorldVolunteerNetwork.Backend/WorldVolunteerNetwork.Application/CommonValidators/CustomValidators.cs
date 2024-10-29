using CSharpFunctionalExtensions;
using FluentValidation;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Application.Validators
{
    //IRuleBuilder class extension (see 'RuleFor' in CPRValidator for example)
    //in order not to write again the verifications implemented in Create()
    public static class CustomValidators
    {
        public static IRuleBuilderOptions<T, TElement> MustBeValueObject<T, TElement, TValueObject>(
            this IRuleBuilder<T, TElement> ruleBuilder,
            Func<TElement, Result<TValueObject, Error>> factoryMethod)
        {
            return (IRuleBuilderOptions<T, TElement>)ruleBuilder.Custom((value, context) =>
            {
                Result<TValueObject, Error> result = factoryMethod(value);

                if (result.IsSuccess)
                {
                    return;
                }
                context.AddFailure(result.Error.Serialize());
            });
        }

        public static IRuleBuilderOptions<T, TProperty> NotEmptyWithError<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder)
        {
            return ruleBuilder
                .NotEmpty()
                .WithError(Errors.General.ValueIsRequired());
        }

        public static IRuleBuilderOptions<T, string> MaximumLengthWithError<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        int maxLength)
        {
            return ruleBuilder
                .MaximumLength(maxLength)
                .WithError(Errors.General.InvalidLength());
        }

        public static IRuleBuilderOptions<T, TProperty> GreaterThanWithError<T, TProperty>(
         this IRuleBuilder<T, TProperty> ruleBuilder, TProperty valueToCompare)
         where TProperty : IComparable<TProperty>, IComparable
        {
            return ruleBuilder
                .GreaterThan(valueToCompare)
                .WithError(Errors.General.InvalidLength());
        }

        public static IRuleBuilderOptions<T, TProperty?> GreaterThanWithError<T, TProperty>(
            this IRuleBuilder<T, TProperty?> ruleBuilder, TProperty valueToCompare)
            where TProperty : struct, IComparable<TProperty>, IComparable
        {
            return ruleBuilder
                .GreaterThan(valueToCompare)
                .WithError(Errors.General.InvalidLength());
        }

        public static IRuleBuilderOptions<T, TProperty> LessThanWithError<T, TProperty>(
            this IRuleBuilder<T, TProperty> ruleBuilder, TProperty valueToCompare)
            where TProperty : IComparable<TProperty>, IComparable
        {
            return ruleBuilder
                .LessThan(valueToCompare)
                .WithError(Errors.General.InvalidLength());
        }

        public static IRuleBuilderOptions<T, TProperty> WithError<T, TProperty>(
        this IRuleBuilderOptions<T, TProperty> rule, Error error)
        {
            return rule
                .WithMessage(error.Serialize());
        }
    }
}


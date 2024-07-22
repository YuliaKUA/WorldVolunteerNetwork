using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;

namespace WorldVolunteerNetwork.API.Validation;
//Fluent validation: override for auto validation
public class CustomResultFactory : IFluentValidationAutoValidationResultFactory
{
    public IActionResult CreateActionResult(
        ActionExecutingContext context,
        ValidationProblemDetails? validationProblemDetails)
    {
        if (validationProblemDetails is null)
        {
            throw new("ValidationProblemDetails is null");
        }
        List<Envelope.ErrorInfo> errorInfos = [];
        foreach (var (invalidField, validationErrors) in validationProblemDetails.Errors)
        {
            var errors = validationErrors
                .Select(Domain.Common.Error.Deserialize)
                .Select(e => new Envelope.ErrorInfo(e, invalidField));

            errorInfos.AddRange(errors);
        }

        var envelope = Envelope.Error(errorInfos);

        return new BadRequestObjectResult(envelope);
    }
}

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Results;
using WorldVolunteerNetwork.Domain.Common;

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
            return new BadRequestObjectResult("Invalid error");
        }
        var validationError = validationProblemDetails.Errors.First();
        var errorString = validationError.Value.First();
        var error = Error.Deserialize(errorString);


        var envelope = Envelope.Error(error);

        return new BadRequestObjectResult(envelope);
    }
}

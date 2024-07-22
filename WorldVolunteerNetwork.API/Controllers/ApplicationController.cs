using Microsoft.AspNetCore.Mvc;
using WorldVolunteerNetwork.API.Validation;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.API.Controllers
{
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        protected new IActionResult Ok(object? result = null)
        {
            var envelope = Envelope.Ok(result);
            return base.Ok(envelope);
        }

        protected IActionResult BadRequest(Error? error)
        {
            var envelope = Envelope.Ok(error);
            return base.BadRequest(envelope);
        }

        protected IActionResult NotFound(Error? error)
        {
            var envelope = Envelope.Ok(error);
            return base.NotFound(envelope);
        }
    }
}

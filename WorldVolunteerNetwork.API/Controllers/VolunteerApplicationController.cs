using Microsoft.AspNetCore.Mvc;
using WorldVolunteerNetwork.Application.Features.Organizers.CreateOrganizer;
using WorldVolunteerNetwork.Application.Features.VolunteerApplication.ApplyVolunteerApplication;

namespace WorldVolunteerNetwork.API.Controllers
{
    public class VolunteerApplicationController : ApplicationController
    {
        [HttpPost]
        public async Task<IActionResult> Create(
               [FromServices] ApplyVolunteerApplicationHandler createHandler,
               [FromBody] ApplyVolunteerApplicationRequest request,
               CancellationToken ct)
        {
            var idResult = await createHandler.Handle(request, ct);
            if (idResult.IsFailure)
            {
                return BadRequest(idResult.Error);
            }

            return Ok(idResult.Value);
        }
    }
}

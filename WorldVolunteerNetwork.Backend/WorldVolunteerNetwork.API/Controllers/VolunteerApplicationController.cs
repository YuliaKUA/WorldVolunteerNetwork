using Microsoft.AspNetCore.Mvc;
using WorldVolunteerNetwork.Application.Features.Organizers.CreateOrganizer;
using WorldVolunteerNetwork.Application.Features.VolunteerApplication.ApplyVolunteerApplication;
using WorldVolunteerNetwork.Application.Features.VolunteerApplications.ApproveOrganizerApplication;

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

        [HttpPost("approve")]
        public async Task<IActionResult> Approve(
            [FromServices] ApproveVolunteerApplicationHandler handler,
            [FromBody] ApproveVolunteerApplicationRequest request,
            CancellationToken ct)
        {
            var result = await handler.Handle(request, ct);
            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
        //[HttpPost]
        //public async Task<IActionResult> Reject(
        //    [FromServices] ApproveOrganizerApplicationHandler handler,
        //    [FromBody] ApproveOrganizerApplicationRequest request)
        //{

        //}
    }
}

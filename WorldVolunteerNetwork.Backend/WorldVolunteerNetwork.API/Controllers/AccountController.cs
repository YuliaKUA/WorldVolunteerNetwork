using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using WorldVolunteerNetwork.API.Attributes;
using WorldVolunteerNetwork.Application.Features.Accounts.Login;
using WorldVolunteerNetwork.Domain.Common;
using LoginRequest = WorldVolunteerNetwork.Application.Features.Accounts.Login.LoginRequest;

namespace WorldVolunteerNetwork.API.Controllers
{
    public class AccountController : ApplicationController
    {
        [HttpPost]
        public async Task<IActionResult> Login(
            [FromServices] LoginHandler handler, 
            [FromBody] LoginRequest request,
            CancellationToken ct)
        {
            var token = await handler.Handle(request, ct);
            if (token.IsFailure)
            {
                return BadRequest(token.Error);
            }

            return Ok(token.Value);
        }

        [HasPermissions(Permissions.Organizers.Create)]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(HttpContext.User.Claims.Select(x => x.Value));
        }
    }
}

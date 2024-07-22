using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;
using WorldVolunteerNetwork.Application.Features.Organizers.CreateOrganizer;
using WorldVolunteerNetwork.Application.Features.Organizers.CreatePost;
using WorldVolunteerNetwork.Application.Features.Organizers.UploadPhoto;
using CSharpFunctionalExtensions;

namespace WorldVolunteerNetwork.API.Controllers
{
    [Route("[controller]")]
    public class OrganizerController : ApplicationController
    {
        [HttpPost]
        public async Task<IActionResult> Create(
            [FromServices] CreateOrganizersHandler createHandler,
            [FromBody] CreateOrganizerRequest request,
            CancellationToken ct)
        {
            var idResult = await createHandler.Handle(request, ct);
            if (idResult.IsFailure)
            {
                return BadRequest(idResult.Error);
            }

            return Ok(idResult.Value);
        }

        [HttpPost("post")]
        public async Task<IActionResult> Create(
            [FromServices] CreatePostsHandler postsHandler,
            [FromBody] CreatePostRequest request,
            CancellationToken ct)
        {
            /// ! App uses auto-validation (see SharpFluentValidation library)

            //var result = await _validator.ValidateAsync(request, ct);
            //if (result.IsValid == false)
            //{
            //    return BadRequest(result.Errors);
            //}

            var idResult = await postsHandler.Handle(request, ct);

            if (idResult.IsFailure)
            {
                return BadRequest(idResult.Error);
            }

            return Ok(idResult.Value);
        }

        [HttpPost("photo")]
        public async Task<IActionResult> UploadPhoto(
            [FromServices] UploadOrganizerPhotoHandler uploadHandler,
            [FromForm] UploadOrganizerPhotoRequest uploadOrganizerPhotoRequest,
            CancellationToken ct)
        {
            var result = await uploadHandler.Handle(uploadOrganizerPhotoRequest, ct);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
            //return Ok();
        }

        [HttpGet("photo")]
        public async Task<IActionResult> GetPhoto(
           string photo,
           [FromServices] IMinioClient minioClient)
        {
            var presignedGetObjectArgs = new PresignedGetObjectArgs()
                .WithBucket("images")
                .WithObject(photo)
                .WithExpiry(604800); // link is valid for a week

            var url = await minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);
            
            return Ok(url);
        }
    }
}

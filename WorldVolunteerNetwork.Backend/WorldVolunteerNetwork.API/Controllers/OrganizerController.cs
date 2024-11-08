﻿using Microsoft.AspNetCore.Mvc;
using Minio;
using Minio.DataModel.Args;
using WorldVolunteerNetwork.Application.Features.Organizers.CreateOrganizer;
using WorldVolunteerNetwork.Application.Features.Organizers.CreatePost;
using WorldVolunteerNetwork.Application.Features.Organizers.UploadPhoto;
using WorldVolunteerNetwork.Application.Features.Organizers.GetPhoto;
using WorldVolunteerNetwork.Application.Features.Organizers.DeletePhoto;
using WorldVolunteerNetwork.Infrastructure.Queries.Organizers.GetOrganizer;
using WorldVolunteerNetwork.Infrastructure.Queries.Organizers.GetAllOrganizers;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using WorldVolunteerNetwork.Application.Providers;

namespace WorldVolunteerNetwork.API.Controllers
{
    public class OrganizerController : ApplicationController
    {
        private readonly IDistributedCache _distributedCache;
        private readonly IMemoryCache _memoryCache;
        private readonly ICacheProvider _cache;

        public OrganizerController(ICacheProvider cache)
        {
            _cache = cache;
        }

        [HttpPost]
        //[HasPermissions(Permissions.Organizers.Create)] //requirment
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
        //[HasPermissions(Permissions.Posts.Create)]
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

        [HttpGet("organiser-with-photo")]
        public async Task<IActionResult> GetOrganizerWithPhotosById(
            [FromServices] GetAllOrganizerPhotosQuery handler,
            [FromQuery] GetAllOrganizerPhotosRequest request,
            CancellationToken ct)
        {
            var result = await handler.Handle(request, ct);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpDelete("photo")]
        public async Task<IActionResult> DeletePhoto(
            [FromServices] DeleteOrganizerPhotoHandler handler,
            [FromQuery] DeleteOrganizerPhotoRequest request,
            CancellationToken ct)
        {
            var result = await handler.Handle(request, ct);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("organizer-with-photo-by-id")]
        public async Task<IActionResult> GetOrganizerWithPhotoPhotosById(
            [FromServices] GetOrganizerByIdQuery handler,
            [FromQuery] GetAllOrganizerRequest request,
            CancellationToken ct)
        {
            var result = await handler.Handle(request, ct);
            if (result.IsFailure)
            {
                return BadRequest(result.Error);
            }

            return Ok(result.Value);
        }

        [HttpGet("all-organizer")]
        public async Task<ActionResult<GetOrganizersResponse>> GetAllOrganizers(
            [FromServices] GetAllOrganizersQuery query,
            CancellationToken ct)
        {
            var response = await query.Handle(ct);

            return Ok(response);


            /////////////////////////////////
            ///IDISTRIBUTED CASH
            //var organizersKey = "organizers";

            //var cashedString = await _distributedCache.GetStringAsync(organizersKey, ct);
            //if (!string.IsNullOrWhiteSpace(cashedString))
            //{
            //    var value = JsonSerializer.Deserialize<GetOrganizersResponse>(cashedString);
            //    return Ok(value!);
            //}

            //var response = await query.Handle();

            //var responseString = JsonSerializer.Serialize(response);

            //await _distributedCache.SetStringAsync(
            //    organizersKey,
            //    responseString,
            //    new DistributedCacheEntryOptions()
            //    {
            //        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5),
            //    },
            //    ct);

            //return Ok(response);


            ////////////////////////////////
            ///IMEMORY CASH
            ///// try get data from cash
            //var organizersKey = "organizers";
            //var data = _memoryCache.Get<GetOrganizersResponse>(organizersKey);

            //var response = _memoryCache.GetOrCreateAsync(organizersKey, async _ => await query.Handle());

            ///// if success, return data

            //if (data is not null) {
            //    return Ok(data);
            //}

            ///// else, get data from DB
            //var response = await query.Handle();

            ///// set data in cash
            //_memoryCache.Set(organizersKey, response, new MemoryCacheEntryOptions()
            //{
            //    SlidingExpiration = TimeSpan.FromSeconds(5)
            //});

            //return Ok(response);


            ///////////////////////////////////////////////
            //var response = await query.Handle();
            //return Ok(response);
        }

        [HttpGet("organizer")]
        public async Task<IActionResult> GetOrganizerById(
            CancellationToken ct)
        {
            return Ok();
        }
    }
}

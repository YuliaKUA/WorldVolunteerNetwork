﻿using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.Application.Abstractions
{
    public interface IMinioProvider
    {
        Task<Result<string, Error>> UploadPhoto(IFormFile photo, Guid photoId);
     }
}
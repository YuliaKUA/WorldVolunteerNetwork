﻿using System.Net;
using System.Text.Json;
using WorldVolunteerNetwork.Domain.Common;

namespace WorldVolunteerNetwork.API.Middlewares
{
    //Error handler(catches and puts errors in context.Response)
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

                var error = new Error("server.iternal", ex.Message);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsJsonAsync(error);
            }
        }

    }
}

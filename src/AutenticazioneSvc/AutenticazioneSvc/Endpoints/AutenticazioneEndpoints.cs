﻿using AutenticazioneSvc.BusinessLayer.Services.Interfaces;
using AutenticazioneSvc.Shared.DTO;
using GSWCloudApp.Common.Routing;
using GSWCloudApp.Common.Validation.Extensions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.OpenApi.Models;

namespace AutenticazioneSvc.Endpoints;

public class AutenticazioneEndpoints : IEndpointRouteHandlerBuilder
{
    public static void MapEndpoints(IEndpointRouteBuilder endpoints)
    {
        var apiGroup = endpoints
        .MapGroup("/autenticazione")
        .MapToApiVersion(1)
        .WithOpenApi(opt =>
        {
            opt.Tags = [new OpenApiTag { Name = "Autenticazione" }];

            return opt;
        })
        .AllowAnonymous();

        apiGroup.MapPost("/login", async Task<Results<Ok<AuthResponse>, BadRequest>> (LoginRequest request,
            IIdentityService identityService, ILogger<AutenticazioneEndpoints> logger) =>
        {
            var result = await identityService.LoginAsync(request);

            if (result != null)
            {
                logger.LogInformation("LoginAsync: {result}", result);
                return TypedResults.Ok(result);
            }
            else
            {
                logger.LogError("LoginAsync: {result}", result);
                return TypedResults.BadRequest();
            }
        })
        .Produces<AuthResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
        .WithValidation<LoginRequest>()
        .WithOpenApi(opt =>
        {
            opt.Summary = "User authentication process";
            opt.Description = "User authentication process";

            return opt;
        });

        apiGroup.MapPost("/refresh-token", async Task<Results<Ok<AuthResponse>, BadRequest>> (RefreshTokenRequest request,
            IIdentityService identityService, ILogger<AutenticazioneEndpoints> logger) =>
        {
            var result = await identityService.RefreshTokenAsync(request);

            if (result != null)
            {
                logger.LogInformation("RefreshTokenAsync: {result}", result);
                return TypedResults.Ok(result);
            }
            else
            {
                logger.LogError("RefreshTokenAsync: {result}", result);
                return TypedResults.BadRequest();
            }
        })
        .Produces<AuthResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
        .WithValidation<RefreshTokenRequest>()
        .WithOpenApi(opt =>
        {
            opt.Summary = "Token refresh process";
            opt.Description = "Token refresh process";

            return opt;
        });

        //apiGroup.MapPost("/register", async Task<Results<Ok<RegisterResponse>, BadRequest>> (RegisterRequest request,
        //    IIdentityService identityService) =>
        //{
        //    return await identityService.RegisterAsync(request) switch
        //    {
        //        { } response => TypedResults.Ok(response),
        //        _ => TypedResults.BadRequest()
        //    };
        //})
        //.Produces<AuthResponse>(StatusCodes.Status200OK)
        //.ProducesProblem(StatusCodes.Status400BadRequest)
        //.WithOpenApi(opt =>
        //{
        //    opt.Summary = "User registration process";
        //    opt.Description = "User registration process";

        //    return opt;
        //});
    }
}
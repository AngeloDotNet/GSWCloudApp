using AutenticazioneSvc.BusinessLayer.Services;
using AutenticazioneSvc.Shared.DTO;
using GSWCloudApp.Common.Routing;
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
            IIdentityService identityService) =>
        {
            var response = await identityService.LoginAsync(request);

            //TODO: da eliminare al prossimo refactoring
            //if (response != null)
            //{
            //    return TypedResults.Ok(response);
            //}

            //return TypedResults.BadRequest();

            return response != null ? TypedResults.Ok(response) : TypedResults.BadRequest();
        })
        .Produces<AuthResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithOpenApi(opt =>
        {
            opt.Summary = "User authentication process";
            opt.Description = "User authentication process";

            return opt;
        });

        apiGroup.MapPost("/refresh-token", async Task<Results<Ok<AuthResponse>, BadRequest>> (RefreshTokenRequest request,
            IIdentityService identityService) =>
        {
            var response = await identityService.RefreshTokenAsync(request);

            //TODO: da eliminare al prossimo refactoring
            //if (response != null)
            //{
            //    return TypedResults.Ok(response);
            //}

            //return TypedResults.BadRequest();

            return response != null ? TypedResults.Ok(response) : TypedResults.BadRequest();
        })
        .Produces<AuthResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithOpenApi(opt =>
        {
            opt.Summary = "Token refresh process";
            opt.Description = "Token refresh process";

            return opt;
        });

        apiGroup.MapPost("/register", async Task<Results<Ok<RegisterResponse>, BadRequest>> (RegisterRequest request,
            IIdentityService identityService) =>
        {
            var response = await identityService.RegisterAsync(request);

            //TODO: da eliminare al prossimo refactoring
            //if (response != null)
            //{
            //    return TypedResults.Ok(response);
            //}

            //return TypedResults.BadRequest();

            return response != null ? TypedResults.Ok(response) : TypedResults.BadRequest();
        })
        .Produces<AuthResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithOpenApi(opt =>
        {
            opt.Summary = "User registration process";
            opt.Description = "User registration process";

            return opt;
        });
    }
}

using System.Diagnostics.CodeAnalysis;
using System.Net.Mime;
using HashidsNet;
using MediatR;
using Microsoft.AspNetCore.Http.Extensions;
using Service.Showcase.Application.Common;
using Service.Showcase.Application.Showcase.Commands.CreateShowcase;
using Service.Showcase.Application.Showcase.Queries.GetShowcaseById;
using Service.Showcase.Application.Showcase.Queries.GetShowcases;
using Service.Showcase.Presentation.Endpoints.Requests;
using Service.Showcase.Presentation.Errors;
using Swashbuckle.AspNetCore.Annotations;

namespace Service.Showcase.Presentation.Endpoints;

[ExcludeFromCodeCoverage]
public static class ServiceEndpoints
{
    public static WebApplication MapEndpoints(this WebApplication app)
    {
        _ = app.MapGet("/api/healthcheck", () => "👍")
            .WithTags("Showcase")
            .WithMetadata(new SwaggerOperationAttribute("Simple 200 response to indicate api is running"))
            .Produces(200);

        _ = app.MapGet("/api/showcase",
                async (int? pageSize, int? currentPage, IMediator mediator) 
                    => Results.Ok(await mediator.Send(new GetShowcasesQuery() { CurrentPage = currentPage, PageSize = pageSize})))
            .WithTags("Showcase")
            .WithMetadata(new SwaggerOperationAttribute("Lookup all showcases"))
            .Produces<PaginatedList<Application.Showcase.Entities.Showcase>>(StatusCodes.Status200OK, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

        _ = app.MapPost("/api/showcase",
                async (CreateShowcaseRequest showcaseRequest, HttpRequest httpRequest, IMediator mediator) =>
                {
                    var command = new CreateShowcaseCommand()
                    {
                        Title = showcaseRequest.Title,
                        Summary = showcaseRequest.Summary,
                        AuthorId = showcaseRequest.AuthorId,
                        Description = showcaseRequest.Description,
                        ImageSource = showcaseRequest.ImageSource,
                        Features = showcaseRequest.Features,
                        Hostings = showcaseRequest.Hostings,
                        Sectors = showcaseRequest.Sectors,
                        MajorVersion = showcaseRequest.MajorVersion,
                        MinorVersion = showcaseRequest.MinorVersion,
                        PatchVersion = showcaseRequest.PatchVersion,
                    };
                    return Results.Created(httpRequest.GetEncodedUrl(), await mediator.Send(command));
                })
            .WithTags("Showcase")
            .WithMetadata(new SwaggerOperationAttribute("Create a new showcase"))
            .Produces<int>()
            .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);


        _ = app.MapGet(
                "/api/showcases/{id}",
                async (IMediator mediator, string id) =>
                    Results.Ok(await mediator.Send(new GetShowcaseByIdQuery { Id =  id })))
            .WithTags("Showcase")
            .WithMetadata(new SwaggerOperationAttribute("Lookup a showcase by their Id"))
            .Produces<Application.Showcase.Entities.Showcase>(StatusCodes.Status200OK,
                contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status400BadRequest, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status404NotFound, contentType: MediaTypeNames.Application.Json)
            .Produces<ApiError>(StatusCodes.Status500InternalServerError, contentType: MediaTypeNames.Application.Json);

        return app;
    }
}
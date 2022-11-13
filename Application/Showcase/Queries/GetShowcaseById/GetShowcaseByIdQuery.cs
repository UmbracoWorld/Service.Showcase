namespace Service.Showcase.Application.Showcase.Queries.GetShowcaseById;

using System.ComponentModel.DataAnnotations;
using MediatR;

public class GetShowcaseByIdQuery : IRequest<Entities.Showcase>
{
    [Required] public string Id { get; init; }
}

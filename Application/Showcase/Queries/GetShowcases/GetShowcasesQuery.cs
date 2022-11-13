using MediatR;
using Service.Showcase.Application.Common;

namespace Service.Showcase.Application.Showcase.Queries.GetShowcases;

public class GetShowcasesQuery : IRequest<PaginatedList<Entities.Showcase>>
{
    public int? CurrentPage { get; set; } = 1;
    public int? PageSize { get; set; } = 10;
    
    public string? SortBy { get; set; }
    public string? Query { get; set; }
}

using MediatR;
using Service.Showcase.Application.Common;

namespace Service.Showcase.Application.Showcase.Queries.GetShowcases;

public class GetShowcasesHandler : IRequestHandler<GetShowcasesQuery, PaginatedList<Entities.Showcase>>
{
    private readonly IShowcaseRepository _repository;

    public GetShowcasesHandler(IShowcaseRepository repository)
    {
        _repository = repository;
    }

    public async Task<PaginatedList<Entities.Showcase>> Handle(GetShowcasesQuery request,
        CancellationToken cancellationToken)
    {
        var showcases = await _repository.GetShowcases(cancellationToken);
        
        var paginated = await PaginatedList<Entities.Showcase>.CreateAsync(showcases, request.CurrentPage ?? 1, request.PageSize ?? 10);
        return paginated;
    }
}
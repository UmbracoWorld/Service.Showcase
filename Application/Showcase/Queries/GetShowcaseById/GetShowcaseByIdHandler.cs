namespace Service.Showcase.Application.Showcase.Queries.GetShowcaseById;

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Common.Exceptions;
using Entities;

public class GetShowcaseByIdHandler : IRequestHandler<GetShowcaseByIdQuery, Showcase>
{
    private readonly IShowcaseRepository _repository;

    public GetShowcaseByIdHandler(IShowcaseRepository repository)
    {
        _repository = repository;
    }

    public async Task<Showcase> Handle(GetShowcaseByIdQuery request, CancellationToken cancellationToken)
    {
        var result = await _repository.GetShowcaseById(request.Id, cancellationToken);

        NotFoundException.ThrowIfNull(result);

        return result;
    }
}

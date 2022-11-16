using HashidsNet;

namespace Service.Showcase.Application.Showcase.Queries.GetShowcaseById;

using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Common.Exceptions;
using Entities;

public class GetShowcaseByIdHandler : IRequestHandler<GetShowcaseByIdQuery, Showcase>
{
    private readonly IShowcaseRepository _repository;
    private readonly IHashids _hashids;

    public GetShowcaseByIdHandler(IShowcaseRepository repository, IHashids hashids)
    {
        _repository = repository;
        _hashids = hashids;
    }

    public async Task<Showcase> Handle(GetShowcaseByIdQuery request, CancellationToken cancellationToken)
    {
        var tryDecodeSingle = _hashids.TryDecodeSingle(request.Id, out var id);
        if (!tryDecodeSingle)
        {
            throw new NotFoundException("Showcase not found");
        }

        var result = await _repository.GetShowcaseById(id, cancellationToken);

        NotFoundException.ThrowIfNull(result);

        return result;
    }
}
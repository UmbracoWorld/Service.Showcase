
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
        var decodedId = _hashids.Decode(request.Id);


        NotFoundException.ThrowIfNull(decodedId);

        var result = await _repository.GetShowcaseById(decodedId.First(), cancellationToken);

        NotFoundException.ThrowIfNull(result);

        return result;
    }
}
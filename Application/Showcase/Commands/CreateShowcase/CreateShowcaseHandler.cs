using Algolia.Search.Clients;
using Algolia.Search.Utils;
using MediatR;
using Newtonsoft.Json;

namespace Service.Showcase.Application.Showcase.Commands.CreateShowcase;

public class CreateShowcaseHandler : IRequestHandler<CreateShowcaseCommand, Entities.Showcase>
{
    private readonly IShowcaseRepository _repository;
    private readonly ISearchClient _searchClient;

    public CreateShowcaseHandler(IShowcaseRepository repository)
    {
        _repository = repository;

        _searchClient = new SearchClient("C7Q4UJ2CDE", "61fbdf624c7c41c9c26f755f2a951c3d");
    }

    public async Task<Entities.Showcase> Handle(CreateShowcaseCommand request, CancellationToken cancellationToken)
    {
        var showcase = await _repository.CreateShowcase(request, cancellationToken);

        var index = _searchClient.InitIndex("dev_showcases");
        
        await index.SaveObjectAsync(showcase, null, cancellationToken);
        
        return showcase;
    }
}
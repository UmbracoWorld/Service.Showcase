using AutoMapper;
using MediatR;

namespace Service.Showcase.Application.Showcase.Commands.CreateShowcase;

public class CreateShowcaseHandler : IRequestHandler<CreateShowcaseCommand, Entities.Showcase>
{
    private readonly IShowcaseRepository _repository;
    private readonly IMapper _mapper;

    public CreateShowcaseHandler(IShowcaseRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Entities.Showcase> Handle(CreateShowcaseCommand request, CancellationToken cancellationToken)
    {
        var guid = await _repository.CreateShowcase(request, cancellationToken);

        return guid;
    }
}
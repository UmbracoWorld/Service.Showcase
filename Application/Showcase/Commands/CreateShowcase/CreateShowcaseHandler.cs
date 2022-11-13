using AutoMapper;
using MediatR;

namespace Service.Showcase.Application.Showcase.Commands.CreateShowcase;

public class CreateShowcaseHandler : IRequestHandler<CreateShowcaseCommand, Entities.Showcase>
{
    private readonly IShowcaseRepository _repository;

    public CreateShowcaseHandler(IShowcaseRepository repository)
    {
        _repository = repository;
    }

    public async Task<Entities.Showcase> Handle(CreateShowcaseCommand request, CancellationToken cancellationToken)
    {
        var guid = await _repository.CreateShowcase(request, cancellationToken);

        return guid;
    }
}
namespace Service.Showcase.Application.Showcase.Queries.GetShowcaseById;

using FluentValidation;

public class GetShowcaseByIdValidator : AbstractValidator<GetShowcaseByIdQuery>
{
    public GetShowcaseByIdValidator()
    {
        _ = RuleFor(r => r.Id)
            .NotNull()
            .WithMessage("A showcase Id was not supplied.");
    }
}

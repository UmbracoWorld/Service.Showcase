using FluentValidation;
using Service.Showcase.Application.Showcase.Commands.CreateShowcase;

namespace Service.Showcase.Application.Showcase.Queries.CreateShowcase;

public class CreateShowcaseValidator : AbstractValidator<CreateShowcaseCommand>
{
    public CreateShowcaseValidator()
    {
        _ = RuleFor(x => x.Title)
            .NotNull()
            .WithMessage("Showcase must have title");
    }
}
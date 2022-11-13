using FluentValidation;

namespace Service.Showcase.Application.Showcase.Commands.CreateShowcase;

public class CreateShowcaseValidator : AbstractValidator<CreateShowcaseCommand>
{
    public CreateShowcaseValidator()
    {
        _ = RuleFor(x => x.Title)
            .NotNull()
            .WithMessage("Showcase must have title");
    }
}
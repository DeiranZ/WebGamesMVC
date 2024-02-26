using FluentValidation;

namespace WebGames.Application.Game.Commands.EditGame
{
    public class EditGameCommandValidator : AbstractValidator<EditGameCommand>
    {
        public EditGameCommandValidator()
        {
            RuleFor(p => p.Description)
                .MaximumLength(32767);

            RuleFor(p => p.Source)
                .NotEmpty()
                .MaximumLength(32767);
        }
    }
}

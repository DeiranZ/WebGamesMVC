using FluentValidation;

namespace WebGames.Application.Genre.Commands.EditGenre
{
    public class EditGenreCommandValidator : AbstractValidator<EditGenreCommand>
    {
        public EditGenreCommandValidator()
        {
            RuleFor(p => p.Description)
                .MaximumLength(32767);
        }
    }
}

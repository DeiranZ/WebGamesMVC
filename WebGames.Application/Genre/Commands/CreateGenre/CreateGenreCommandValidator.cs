using FluentValidation;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.Genre.Commands.CreateGenre
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator(IGenreRepository repository)
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50)
                .Custom((value, context) =>
                {
                    var existingGenre = repository.GetByName(value).Result;
                    if (existingGenre != null)
                    {
                        context.AddFailure("A genre with this name already exists.");
                    }
                });

            RuleFor(p => p.Description)
                .MaximumLength(32767);
        }
    }
}

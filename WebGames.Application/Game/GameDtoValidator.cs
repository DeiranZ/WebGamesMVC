using FluentValidation;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.Game
{
	public class GameDtoValidator : AbstractValidator<GameDto>
	{
        public GameDtoValidator(IGameRepository repository)
        {
            RuleFor(p => p.Name)
                .NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50)
                .Custom((value, context) =>
                {
                    var existingGame = repository.GetByName(value).Result;
                    if (existingGame != null) 
                    {
                        context.AddFailure("A game with this name already exists.");
                    }
                });

            RuleFor(p => p.Description)
                .MaximumLength(32767);

            RuleFor(p => p.Source)
                .NotEmpty()
                .MaximumLength(32767);
        }
    }
}

using FluentValidation;
using Microsoft.Extensions.Configuration;
using WebGames.Domain.Interfaces;

namespace WebGames.Application.Game.Commands.CreateGame
{
    public class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
    {
        public CreateGameCommandValidator(IGameRepository repository, IConfiguration configuration)
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

            RuleFor(p => p.Image!.ContentType)
                .Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
                .WithMessage("File must be a .jpeg, .jpg or .png")
                .When(p => p.Image != null);

            var maxFileSize = configuration.GetValue<long>("ImageSizeLimit");
            
            RuleFor(p => p.Image!.Length)
                .ExclusiveBetween(0, maxFileSize)
                .WithMessage("File size must be less than " + maxFileSize / 1024 / 1024 + " MB")
                .When(p => p.Image != null);
        }
    }
}

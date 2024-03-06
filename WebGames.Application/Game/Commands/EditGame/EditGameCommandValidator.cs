using FluentValidation;
using Microsoft.Extensions.Configuration;

namespace WebGames.Application.Game.Commands.EditGame
{
    public class EditGameCommandValidator : AbstractValidator<EditGameCommand>
    {
        public EditGameCommandValidator(IConfiguration configuration)
        {
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

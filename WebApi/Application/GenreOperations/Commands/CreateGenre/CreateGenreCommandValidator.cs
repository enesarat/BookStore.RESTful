using FluentValidation;
using WebApi.Application.GenreOperations.Queries.GetGenresDetail;

namespace WebApi.Application.GenreOperations.Commands.CreateGenre
{
    public class CreateGenreCommandValidator : AbstractValidator<CreateGenreCommand>
    {
        public CreateGenreCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(4).NotEmpty();
        }
    }
}

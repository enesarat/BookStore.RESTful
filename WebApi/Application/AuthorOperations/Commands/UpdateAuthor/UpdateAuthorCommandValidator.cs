using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.UpdateAuthor
{
    public class UpdateAuthorCommandValidator : AbstractValidator<UpdateAuthorCommand>
    {
        public UpdateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(4).NotEmpty();
            RuleFor(command => command.Model.Surname).MinimumLength(2).NotEmpty();
        }
    }
}

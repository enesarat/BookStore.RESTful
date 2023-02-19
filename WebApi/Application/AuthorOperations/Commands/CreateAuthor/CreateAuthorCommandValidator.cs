using FluentValidation;

namespace WebApi.Application.AuthorOperations.Commands.CreateAuthor
{
    public class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator()
        {
            RuleFor(command => command.Model.Name).MinimumLength(4).NotEmpty();
            RuleFor(command => command.Model.Surname).MinimumLength(2).NotEmpty();
            RuleFor(command => command.Model.BirthDate).LessThan(System.DateTime.Now.Date);
        }
    }
}

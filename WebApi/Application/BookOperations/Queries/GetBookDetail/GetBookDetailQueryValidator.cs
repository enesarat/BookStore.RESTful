using FluentValidation;

namespace WebApi.Application.BookOperations.Queries.GetBookDetail
{
    public class GetBookDetailQueryValidator : AbstractValidator<GetBookByIdQuery>
    {
        public GetBookDetailQueryValidator()
        {
            RuleFor(command => command.BookId).GreaterThan(0);
        }
    }
}
